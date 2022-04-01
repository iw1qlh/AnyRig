using AnyRigLibrary;
using AnyRigLibrary.Models;

namespace AnyRigService.Services
{
    public class RigsMachine : IRigsMachine
    {

        private readonly ILogger<RigsMachine> logger;
        private readonly AnyRigConfig config;
        private readonly RigCore[] rigs = new RigCore[0];
        private List<Func<bool>> connFuncList = new List<Func<bool>>();

        private bool started;
        private bool appRunning = true;

        public bool Started { get => started; }

        public RigsMachine(ILogger<RigsMachine> logger, IHostApplicationLifetime appLifeTime)
        {
            this.logger = logger;
            appLifeTime.ApplicationStopping.Register(() => appRunning = false);

            config = ConfigManager.Load();
            if ((!config.SocketEnabled || (config.SocketPort == 0)) && (!config.NetpipeEnabled) && (!config.WebSocketEnabled || (config.WebSocketPort == 0)))
                return;

            rigs = ConfigManager.LoadRigs(config);

            foreach (RigCore r in rigs)
            {
                //r.FRig.NotifyParams = (rx, changed) => OnChanges(rx, changed);
                r.Log += (text) => logger.LogInformation($"Rig: {text}");
                r.InternalLog += (text) => logger.LogDebug($"Rig: {text}");
                //r.Start();
            }

            _ = Task.Run(async () => await CheckConnections());

        }

        private async Task CheckConnections()
        {
            while (appRunning)
            {
                if (started)
                {
                    bool connected = false;

                    foreach (var f in connFuncList)
                        connected |= f();

                    if (!connected)
                    {
                        foreach (RigCore r in rigs)
                        {
                            r.Stop();
                        }
                        started = false;
                        logger.LogInformation("Rigs stopped");
                    }
                }

                await Task.Delay(1000);

            }
        }

        public void Start()
        {
            foreach (RigCore r in rigs)
            {
                r.Start();
            }
            started = true;
            logger.LogInformation("Rigs started");
        }

        public AnyRigConfig GetConfig() => config;

        public RigCore[] GetRigs() => rigs;

        public void AddNotifyChanges(Action<int, RigParam[]> act)
        {
            foreach (RigCore r in rigs)
            {
                r.NotifyChanges += act;
            }
        }

        public void RegisterCheckConnections(Func<bool> f)
        {
            connFuncList.Add(f);
        }
    }
}
