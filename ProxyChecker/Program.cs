using Eto.Forms;

namespace ProxyChecker;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        new Application().Run(new MainForm());
    }
}
