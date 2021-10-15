using RandoHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandoHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public ModuleVertex TestVertex { get; set; } = new();

        public static string WindowTitle
        {
            get
            {
                Version v = System.Reflection.Assembly.GetAssembly(typeof(MainWindow)).GetName().Version;
                return $"Rando Helper (v{v.Major}.{v.Minor}.{v.Build})";
            }
        }
    }
}
