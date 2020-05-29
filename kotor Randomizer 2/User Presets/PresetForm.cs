using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class PresetForm : Form
    {
        public PresetForm()
        {
            InitializeComponent();

            Properties.Settings.Default.twoda_rando_active = false; //2DA rando inactive until I work out the issues with the file format
        }
    }
}
