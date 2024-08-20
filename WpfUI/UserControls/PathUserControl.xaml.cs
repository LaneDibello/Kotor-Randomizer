using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Randomizer_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for PathUserControl.xaml
    /// </summary>
    public partial class PathUserControl : System.Windows.Controls.UserControl
    {
        public PathUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty PathTextProperty = DependencyProperty.Register("PathText", typeof(string), typeof(PathUserControl));

        public string ButtonLabel { get; set; }
        public string TextLabel { get; set; }
        public int TextBoxMinWidth { get; set; }

        public string PathText
        {
            get { return (string)GetValue(PathTextProperty); }
            set { SetValue(PathTextProperty, value); }
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (PathText != "")
            {
                // This solution only works if it's a directory the dialog is allowed to access
                // Otherwise it will just spit you out in a recent directory
                dialog.InitialDirectory = PathText + @"\";
            }
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PathText = dialog.SelectedPath;
            }
        }
    }
}
