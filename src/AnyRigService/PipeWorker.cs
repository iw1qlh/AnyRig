using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigService.Services;
using System.IO.Pipes;
using System.Text;

namespace AnyRigService
{
    public class PipeWorker : BackgroundService
    {
        const int MAX_INSTANCES = 4;
        List<NamedPipeServerStream> clientList = new List<NamedPipeServerStream>();


        private readonly ILogger<PipeWorker> logger;
        private readonly IRigsMachine rigsMachine;
        private RigCoreCommands rigCmd;

        public PipeWorker(ILogger<PipeWorker> logger, IRigsMachine rigsMachine)
        {
            this.logger = logger;
            this.rigsMachine = rigsMachine;            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            AnyRigConfig config = rigsMachine.GetConfig();

            if (!config.NetpipeEnabled)
                return;

            rigsMachine.RegisterCheckConnections(() => clientList.Any(c => c.IsConnected));

            RigCore[] rigs = rigsMachine.GetRigs();
            rigsMachine.AddNotifyChanges((rx, changed) => OnChanges(rx, changed));

            rigCmd = new RigCoreCommands(rigs);

            logger.LogInformation("Starting PipeWorker");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    /*
                    PipeSecurity ps = new PipeSecurity();
                    System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
                    PipeAccessRule par = new PipeAccessRule(sid, PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
                    ps.AddAccessRule(par);
                    */


                    _ = Task.Run(async () =>
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            NamedPipeServerStream server = new NamedPipeServerStream(RigCommon.PIPE_CHANGES_NAME, PipeDirection.Out, MAX_INSTANCES, PipeTransmissionMode.Message, PipeOptions.None, 1024, 1024);
                            logger.LogInformation("PipeWorker: WaitForConnection");
                            await server.WaitForConnectionAsync(stoppingToken);
                            rigsMachine.Start();
                            clientList.Add(server);
                        }
                    });

                    logger.LogInformation("PipeWorker: Starting command Pipe");

                    using (NamedPipeServerStream server = new NamedPipeServerStream(RigCommon.PIPE_COMMAND_NAME, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.None, 1024, 1024))
                    {
                        await server.WaitForConnectionAsync(stoppingToken);
                        using (StreamReader reader = new StreamReader(server))
                        {

                            string cmd = reader.ReadLine();
                            if (cmd != null)
                            {

                                logger.LogInformation($"PipeWorker: Received {cmd}");
                                string result = $"{cmd}={rigCmd.Request(cmd)}";
                                logger.LogInformation($"PipeWorker: Result {result}");

                                byte[] buff = Encoding.UTF8.GetBytes(result + "\n");
                                server.Write(buff, 0, buff.Length);
                                server.Flush();

                                logger.LogInformation($"PipeWorker: Reply: {result}");

                                server.WaitForPipeDrain();

                            }

                            //server.Disconnect();

                        }

                        //await Task.Delay(100);
                        

                    }
                    
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "PipeWorker");
                }
            }

            Array.ForEach(rigs, r => r.Stop());

            logger.LogInformation("PipeWorker stopped");

        }

        /*
        private async Task RunPipeAsync(NamedPipeServerStream server, CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("A new client connected");

                while (server.IsConnected && !stoppingToken.IsCancellationRequested)
                {

                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred: {0}", ex.Message);
            }

            server.Close();

        }
        */

        private void OnChanges(int rx, RigParam[] changed)
        {
            if (!clientList.Any(c => c.IsConnected))
                return;

            string changes = rigCmd.GetChanges(rx, changed);
            logger.LogInformation($"PipeWorker: OnChanges {changes}");
            byte[] data = Encoding.UTF8.GetBytes($"*{rx} {changes}\r\n");

            foreach (var c in clientList)
            {
                if (c.IsConnected)
                {
                    try
                    {
                        c.Write(data, 0, data.Length);
                        c.Flush();
                    }
                    catch { }
                }
            }

        }
    }
}