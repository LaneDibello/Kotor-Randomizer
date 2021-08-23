using Randomizer_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Randomizer_WPF.Views
{
    /// <summary>
    /// Interaction logic for OtherView.xaml
    /// </summary>
    public partial class OtherView : UserControl
    {
        public OtherView()
        {
            InitializeComponent();

            DataObject.AddPastingHandler(rtbFName, OnPaste);
            DataObject.AddPastingHandler(rtbMName, OnPaste);
            DataObject.AddPastingHandler(rtbLName, OnPaste);
        }

        /// <summary>
        /// Sanitizes input pasted into the restricted text boxes.
        /// </summary>
        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            string clipboard = e.DataObject.GetData(typeof(string)) as string;
            var rtb = sender as RestrictedTextBox;

            Regex regex =  new Regex("[a-zA-Z\r\n'-]");
            string toRemove = regex.Replace(clipboard, string.Empty);
            var removeList = toRemove.Distinct();

            foreach (var item in removeList)
                clipboard = clipboard.Replace(item.ToString(), string.Empty);

            int start = rtb.SelectionStart;
            int length = rtb.SelectionLength;
            int caret = rtb.CaretIndex;

            string text = rtb.Text.Substring(0, start);
            text += rtb.Text.Substring(start + length);

            string newText = text.Substring(0, rtb.CaretIndex) + clipboard;
            newText += text.Substring(caret);
            rtb.Text = newText;
            rtb.CaretIndex = caret + clipboard.Length;

            e.CancelCommand();
        }
    }
}
