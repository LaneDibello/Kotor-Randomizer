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
    public partial class TwodaForm : Form
    {
        public TwodaForm()
        {
            InitializeComponent();
            cbTwodaOptions.DataSource = Globals.TWODA_COLLUMNS.Keys.ToList();
        }

        private void UpdateParentListBox()
        {
            selected_twoda.DataSource = Globals.Selected2DAs.Keys.ToList();
            selected_twoda.Update();
        }

        private void UpdateChildListBox()
        {
            if (!Globals.Selected2DAs.Keys.Any())
            {
                selected_columns.Enabled = false;
                cbColumnOptions.Enabled = false;
                bAddColumn.Enabled = false;
                return;
            }
            selected_columns.DataSource = Globals.Selected2DAs[selected_twoda.SelectedItem as string].ToList();
            cbColumnOptions.DataSource = Globals.TWODA_COLLUMNS[selected_twoda.SelectedItem as string].ToList();
            selected_columns.Update();
        }

        private void selected_twoda_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_columns.Enabled = true;
            cbColumnOptions.Enabled = true;
            bAddColumn.Enabled = true;

            UpdateChildListBox();
        }

        private void bAdd2da_Click(object sender, EventArgs e)
        {
            if (!Globals.Selected2DAs.Keys.Contains(cbTwodaOptions.Text))
            {
                Globals.Selected2DAs.Add(cbTwodaOptions.Text, new List<string>());
                UpdateParentListBox();
                selected_twoda_SelectedIndexChanged(sender, e);
            }
            else
            {
                selected_twoda.SelectedIndex = Globals.Selected2DAs.Keys.ToList().IndexOf(cbTwodaOptions.Text);
            }
        }

        private void bAddColumn_Click(object sender, EventArgs e)
        {
            if (!Globals.Selected2DAs[selected_twoda.SelectedItem as string].Contains(cbColumnOptions.Text))
            {
                Globals.Selected2DAs[selected_twoda.SelectedItem as string].Add(cbColumnOptions.Text);
                UpdateChildListBox();
            }
            else
            {
                selected_columns.SelectedIndex = Globals.Selected2DAs[selected_twoda.SelectedItem as string].IndexOf(cbColumnOptions.Text);
            }
        }

        private void selected_twoda_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Globals.Selected2DAs.Remove(selected_twoda.SelectedItem as string);
            UpdateParentListBox();
            selected_twoda_SelectedIndexChanged(sender, e);
        }

        private void selected_columns_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Globals.Selected2DAs[selected_twoda.SelectedItem as string].Remove(selected_columns.SelectedItem as string);
            UpdateChildListBox();
        }

        private void bMax_Click(object sender, EventArgs e)
        {
            Globals.Selected2DAs = Globals.TWODA_COLLUMNS.ToDictionary(x => x.Key, x => new List<string>(x.Value));
            UpdateParentListBox();
        }

        private void bAllOff_Click(object sender, EventArgs e)
        {
            Globals.Selected2DAs.Clear();
            UpdateParentListBox();
            selected_twoda_SelectedIndexChanged(sender, e);
        }
    }
}
