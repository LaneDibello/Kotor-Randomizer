using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class OtherForm : Form
    {
        public OtherForm()
        {
            InitializeComponent();

            cbNameList.SelectedIndex = 0;
        }

        #region private properties

        //Keep the program from triggering text change events
        private bool ignoretextchange = false;

        #endregion

        #region private methods

        private void load_name_List(StringCollection sc)
        {
            ignoretextchange = true;
            tbNameData.Text = "";

            foreach (string s in sc)
            {
                tbNameData.Text += s + "\n";
            }
            ignoretextchange = false;
        }

        private void save_name_List(StringCollection sc)
        {
            sc.Clear();

            foreach (string s in tbNameData.Text.Split('\n'))
            {
                sc.Add(s);
            }
        }

        private void tbNameData_TextChanged(object sender, EventArgs e)
        {
            if (!ignoretextchange)
            {
                StringCollection sc;
                switch (cbNameList.SelectedIndex)
                {
                    case 1:
                        sc = Properties.Settings.Default.FirstnamesF;
                        break;
                    case 2:
                        sc = Properties.Settings.Default.Lastnames;
                        break;
                    case 0:
                    default:
                        sc = Properties.Settings.Default.FirstnamesM;
                        break;
                }

                save_name_List(sc);
            }
        }

        private void cbNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringCollection sc;
            switch (cbNameList.SelectedIndex)
            {
                case 1:
                    sc = Properties.Settings.Default.FirstnamesF;
                    break;
                case 2:
                    sc = Properties.Settings.Default.Lastnames;
                    break;
                case 0:
                default:
                    sc = Properties.Settings.Default.FirstnamesM;
                    break;
            }

            load_name_List(sc);
        }

        private void cbPolymorph_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PolymorphMode = cbPolymorph.Checked;
        }

        #endregion
    }
}
