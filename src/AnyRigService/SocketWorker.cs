using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigService.Services;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AnyRigService
{
    public class SocketWorker : BackgroundService
    {
        private readonly ILogger<SocketWorker> _logger;
        private readonly IRigsMachine rigsMachine;
        private RigCoreCommands rigCmd;
        private List<TcpClient> clientList;


        public SocketWorker(ILogger<SocketWorker> logger, IRigsMachine rigsMachine)
        {
            this._logger = logger;
            this.rigsMachine = rigsMachine;            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            /*
            // Create config file
            RigCoreX rig = new RigCoreX();
            rig.FRig = new TRig();
            rig.FRig.FRigCommands = new TRigCommands();
            rig.FRig.RigType = "TS-590";

            rig.FRig.ComPort.ConfigurePort("COM3,115200,N,8,1");
            rig.FRig.ComPort.RtsMode = true;
            rig.FRig.ComPort.DtrMode = true;

            RigCoreX[] x = new RigCoreX[] { rig };

            ConfigManager.Save(x);
            return;
            */

            clientList = new List<TcpClient>();

            AnyRigConfig config = rigsMachine.GetConfig();
            if (!config.SocketEnabled || (config.SocketPort == 0))
                return;

            rigsMachine.RegisterCheckConnections(() => clientList.Any(c => c.Connected));

            RigCore[] rigs = rigsMachine.GetRigs();
            rigsMachine.AddNotifyChanges((rx, changed) => OnChanges(rx, changed));

            rigCmd = new RigCoreCommands(rigs);

            /*
            rig = new RigCoreX();
            rig.FRig = new TRig();
            rig.FRig.FRigCommands = new TRigCommands();
            rig.FRig.FRigCommands.FromIni(@"C:\Users\Claudio\Dropbox\Git-repos\clone-AnyRigLibrary\src\Rigs\TS-590.ini");
           
            rig.FRig.NotifyParams = (rx, changed) => OnChanges(rx, changed);

            rig.FRig.ComPort.ConfigurePort("COM3,115200,N,8,1");
            rig.FRig.ComPort.RtsMode = true;
            rig.FRig.ComPort.DtrMode = true;

            rig.Log += (text) => Console.WriteLine(text);
            rig.FRig.Log += (text) => Console.WriteLine(text);

            rig.FRig.ComPort.OpenPort();
            rig.FRig.Start();
            */

            _logger.LogInformation($"Starting SocketWorker on port {config.SocketPort}");

            TcpListener listener = new TcpListener(IPAddress.Any, config.SocketPort);
            listener.Start();

            while (!stoppingToken.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync(stoppingToken);
                _ = Task.Run(async () => await RunConnectionAsync(client, stoppingToken));                
            }

            Array.ForEach(rigs, r => r.Stop());

            _logger.LogInformation("SocketWorker stopped");

        }

        private void OnChanges(int rx, RigParam[] changed)
        {
            if (!clientList.Any(c => c.Connected))
                return;

            string changes = rigCmd.GetChanges(rx, changed);
            _logger.LogInformation($"SocketWorker: OnChanges {changes}");
            byte[] data = Encoding.UTF8.GetBytes($"*{rx} {changes}\r\n");

            foreach (TcpClient c in clientList)
            {
                if (c.Connected)
                {
                    try
                    {
                        NetworkStream stream = c.GetStream();
                        stream.Write(data, 0, data.Length);
                        stream.Flush();
                    }
                    catch { }
                }
            }
        }

        private async Task RunConnectionAsync(TcpClient client, CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("SocketWorker: A new client connected");

                rigsMachine.Start();
                clientList.Add(client);

                NetworkStream stream = client.GetStream();
                using StreamReader reader = new StreamReader(stream);

                while (!stoppingToken.IsCancellationRequested)
                {

                    string cmd = await reader.ReadLineAsync();
                    if (cmd == null)
                        break;
                    _logger.LogInformation($"SocketWorker: Received : {cmd}");
                    if (cmd.ToUpper() == "QUIT")
                        break;

                    string result = $"{cmd}={rigCmd.Request(cmd)}\r\n";
                    _logger.LogInformation($"SocketWorker: Result {result}");
                    byte[] data = Encoding.UTF8.GetBytes(result);

                    await stream.WriteAsync(data, 0, data.Length, stoppingToken);
                    stream.Flush();
                }

                _logger.LogInformation("SocketWorker: Disconnectiong client");

                client.Close();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker");
            }

        }
    }
}