using AnyRigNetWrapper;
using System;



//BaseAnyRigCommandWrapper wrapper = new NetpipeCommandWrapper();
BaseAnyRigCommandWrapper wrapper = new SocketCommandWrapper();

wrapper.OnChange = (text) => Console.WriteLine($"ON_CHANGE: <{text}>");
wrapper.OnError = (ex) => Console.WriteLine($"ERROR: {ex.Message}");
wrapper.Start();

while (true)
{

    Console.WriteLine("Enter command (or Enter to exit):");
    string cmd = Console.ReadLine();
    if (string.IsNullOrEmpty(cmd))
        break;

    string response = wrapper.SendCommand(cmd);
    Console.WriteLine($"[{response}]");

}

wrapper.Stop();


