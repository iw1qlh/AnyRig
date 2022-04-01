using AnyRigLibrary;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyRigNetWrapper
{
    public class NetpipeCommandWrapper : BaseAnyRigCommandWrapper
    {
        CancellationTokenSource cancelToken = new CancellationTokenSource();

        public override void Start()
        {

            _ = Task.Run(async () =>
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    try
                    {
                        using (NamedPipeClientStream client = new NamedPipeClientStream(".", RigCommon.PIPE_CHANGES_NAME, PipeDirection.In))
                        {
                            await client.ConnectAsync(300);

                            using (StreamReader reader = new StreamReader(client))
                            {

                                while (client.IsConnected && !cancelToken.IsCancellationRequested)
                                {
                                    string change = await reader.ReadLineAsync();
                                    OnChange?.Invoke(change);
                                }
                            }

                            client.Close();

                        }
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(ex);
                    }
                }

            });
        }

        public override void Stop()
        {
            cancelToken.Cancel();
        }

        public override string SendCommand(string command)
        {
            string result = null;

            try
            {
                using (NamedPipeClientStream client = new NamedPipeClientStream(".", RigCommon.PIPE_COMMAND_NAME, PipeDirection.InOut))
                {

                    client.Connect(300);
                    client.ReadMode = PipeTransmissionMode.Message;

                    byte[] buff = Encoding.UTF8.GetBytes(command + "\n");
                    client.Write(buff, 0, buff.Length);
                    client.Flush();

                    buff = new byte[1024];
                    string response = "";
                    do
                    {
                        int read = client.Read(buff, 0, buff.Length);
                        response += Encoding.UTF8.GetString(buff, 0, read);
                    } while (!client.IsMessageComplete);

                    result = response;

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }

            return result;

        }

    }
}
