using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Randomizer_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd;
            if (e.Args.Length == 1)
            {
                wnd = new MainWindow(e.Args[0]);    // Load the given file.
            }
            else
            {
                wnd = new MainWindow();             // Normal startup.
            }
            wnd.Show();
        }
    }
}
