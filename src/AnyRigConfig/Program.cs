using AnyRigConfig;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Terminal.Gui;

static class Program
{

    [STAThread]
    static void Main(string[] Args)
    {

        bool useSystemConsole = false;

        foreach (string arg in Args)
        {
            Match m = Regex.Match(arg, "(?<name>.+)=(?<value>.+)");
            if (m.Success)
            {
                switch (m.Groups["name"].Value)
                {
                    case "lang":
                        Thread.CurrentThread.CurrentUICulture =
                            new System.Globalization.CultureInfo(m.Groups["value"].Value);
                        break;
                    case "usc":
                        useSystemConsole = true;
                        break;
                }
            }
        }

        // https://github.com/migueldeicaza/gui.cs/issues/1617
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            try
            {
                string release = File.ReadAllText("/etc/os-release");
                if (release.Contains("debian"))
                    useSystemConsole = true;
                if (release.Contains("ubuntu"))
                    useSystemConsole = true;
            }
            catch { }
        }

        Application.UseSystemConsole = useSystemConsole;
        Application.Init();
        Toplevel top = Application.Top;

        // Creates the top-level window to show
        Window win = new WinMain(top);

        try
        {
            Application.Run();
        }
        catch (Exception ex)
        {
            StackFrame frame = new StackTrace(ex, true).GetFrame(0);
            Console.WriteLine($"{frame.GetMethod()} line {frame.GetFileLineNumber()}: {ex.Message}");
        }
        Application.Shutdown();

    }
}
    
