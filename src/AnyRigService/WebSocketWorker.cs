using Fleck;
using AnyRigLibrary;
using AnyRigLibrary.Models;
using AnyRigService.Services;

namespace AnyRigService
{
    public class WebSocketWorker : BackgroundService
    {
        private readonly ILogger<WebSocketWorker> logger;
        private readonly IRigsMachine rigsMachine;
        private RigCoreCommands rigCmd;
        private List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        public WebSocketWorker(ILogger<WebSocketWorker> logger, IRigsMachine rigsMachine)
        {
            this.logger = logger;
            this.rigsMachine = rigsMachine;            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            AnyRigConfig config = rigsMachine.GetConfig();
            if (!config.WebSocketEnabled || (config.WebSocketPort == 0))
                return;

            rigsMachine.RegisterCheckConnections(() => allSockets.Count > 0);

            RigCore[] rigs = rigsMachine.GetRigs();
            rigsMachine.AddNotifyChanges((rx, changed) => OnChanges(rx, changed));

            rigCmd = new RigCoreCommands(rigs);

            logger.LogInformation($"Starting WebSocketWorker on port {config.WebSocketPort}");

            var socket = new WebSocketServer($"ws://127.0.0.1:{config.WebSocketPort}/");
            socket.Start(conn =>
            {
                conn.OnOpen = () =>
                {
                    logger.LogInformation($"WebSocketWorker: Connection from {conn.ConnectionInfo.ClientIpAddress}");
                    rigsMachine.Start();
                    allSockets.Add(conn);
                };
                conn.OnMessage = cmd =>
                {
                    logger.LogInformation($"WebSocketWorker: Received {cmd}");
                    string result = $"{cmd}={rigCmd.Request(cmd)}";
                    logger.LogInformation($"SocketWorker: Result {result}");
                    conn.Send(result);
                    logger.LogInformation($"WebSocketWorker: Reply: {result}");
                    Console.WriteLine();
                };
                conn.OnClose = () =>
                {
                    logger.LogInformation($"WebSocketWorker: Disconnected from {conn.ConnectionInfo.ClientIpAddress}");
                    allSockets.Remove(conn);
                };
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            allSockets.ForEach(s => s.Close());
            socket.Dispose();

            logger.LogInformation("WebSocketWorker stopped");

        }

        private void OnChanges(int rx, RigParam[] changed)
        {
            if (allSockets.Count == 0)
                return;

            string changes = rigCmd.GetChanges(rx, changed);
            logger.LogInformation($"WebSocketWorker: OnChanges {changes}");
            foreach (var socket in allSockets.ToList())
            {                
                socket.Send($"*{rx} {changes}");
            }
        }
    }
}