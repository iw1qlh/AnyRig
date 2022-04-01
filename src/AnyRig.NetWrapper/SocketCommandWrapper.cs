using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyRigNetWrapper
{
    public class SocketCommandWrapper : BaseAnyRigCommandWrapper
    {
        public int Port { get; set; } = 4532;

        TcpClient client;
        NetworkStream stream;
        bool waitResult = false;

        CancellationTokenSource cancelToken = new CancellationTokenSource();

        private string ReadStream()
        {
            string response;
            string result = "";
            string newline = "";

            do
            {
                byte[] buff = new byte[1024];
                int read = stream.Read(buff, 0, buff.Length);
                string lines = Encoding.UTF8.GetString(buff, 0, read);
                using (StringReader sr = new StringReader(lines))
                {                    
                    while ((response = sr.ReadLine()) != null)
                    {
                        if (response.StartsWith("*"))
                            OnChange?.Invoke(response);
                        else
                        {
                            result += newline + response;
                            newline = "\n";
                        }
                    }
                }
            } while (stream.DataAvailable);

            return result;

        }

        public override string SendCommand(string command)
        {
            string result = null;
            try
            {
                waitResult = true;
                byte[] buff = Encoding.UTF8.GetBytes(command + "\n");
                stream.Write(buff, 0, buff.Length);

                result = ReadStream();
                waitResult = false;

            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }

            return result;
        }

        public override void Start()
        {

            client = new TcpClient("localhost", Port);
            stream = client.GetStream();

            //Start(4532);

            _ = Task.Run(async () =>
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    try
                    {
                        if (!waitResult && (client.Available > 0))
                        {
                            ReadStream();
                        }
                        else
                        {
                            await Task.Delay(10);
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
            stream.Close();
            client.Close();
        }
    }
}
