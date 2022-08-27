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

            cbNameGen.Checked = Properties.Settings.Default.RandomizeNameGen;
            cbPolymorph.Checked = Properties.Settings.Default.PolymorphMode;
            cbPazaak.Checked = Properties.Settings.Default.RandomizePazaakDecks;
            cbPartyRando.Checked = Properties.Settings.Default.RandomizePartyMembers;
            cbSwoopBoosters.Checked = Properties.Settings.Default.RandomizeSwoopBoosters;
            cbSwoopObstacles.Checked = Properties.Settings.Default.RandomizeSwoopObstacles;
        }

        #region private properties

        //Keep the program from triggering text change events
        private bool ignoretextchange = false;

        #endregion

        #region private methods

        //Loading and saving string data to a setting using StringCollections
        private void load_name_List(StringCollection sc)
        {
            ignoretextchange = true;
            tbNameData.Text = "";

            foreach (string s in sc)
            {
                tbNameData.Text += s + "\r\n";
            }
            ignoretextchange = false;
        }

        private void save_name_List(StringCollection sc)
        {
            sc.Clear();

            foreach (string s in tbNameData.Text.Split('\n'))
            {
                sc.Add(s.TrimEnd('\r').ToLower());
            }
        }

        //Name Gen functionality
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

        private void cbNameGen_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RandomizeNameGen = cbNameGen.Checked;
            tbNameData.Enabled = cbNameGen.Checked;
            cbNameList.Enabled = cbNameGen.Checked;
        }

        private void OtherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void tbNameData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Globals.NAMEGEN_CHARS.Contains(char.ToLower(e.KeyChar)) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbPazaak_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RandomizePazaakDecks = cbPazaak.Checked;
        }

        private void cbPartyRando_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RandomizePartyMembers = cbPartyRando.Checked;
        }

        private void cbSwoopBoosters_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RandomizeSwoopBoosters = cbSwoopBoosters.Checked;
        }

        private void cbSwoopObstacles_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RandomizeSwoopObstacles = cbSwoopObstacles.Checked;
        }

        #endregion
    }
}
