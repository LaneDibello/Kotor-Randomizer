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
    public partial class TextF : Form
    {
        private bool checks_set = false;

        private Properties.Settings settings = Properties.Settings.Default;

        public TextF()
        {
            InitializeComponent();
            
            //Set up controls
            cbDialogRando.Checked = settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogEntries) || settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogReplies);
            cbEntries.Checked = settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogEntries);
            cbReplies.Checked = settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogReplies);
            cbMatchEntrySound.Checked = settings.TextSettingsValue.HasFlag(TextSettings.MatchEntrySoundsWText);
            cbTLKRando.Checked = settings.TextSettingsValue.HasFlag(TextSettings.RandoFullTLK);
            cbMatchStringLen.Checked = settings.TextSettingsValue.HasFlag(TextSettings.MatchSimLengthStrings);

            checks_set = true;
        }

        #region Events

        //Checkboxes
        private void cbDialogRando_CheckedChanged(object sender, EventArgs e)
        {
            pDialogRando.Enabled = cbDialogRando.Checked;
            if (!checks_set) { return; }
            if (!pDialogRando.Enabled)
            {
                cbEntries.Checked = false;
                cbReplies.Checked = false;
                cbMatchEntrySound.Checked = false;
            }
        }

        private void cbTLKRando_CheckedChanged(object sender, EventArgs e)
        {
            pTLK.Enabled = cbTLKRando.Checked;
            if (!checks_set) { return; }
            settings.TextSettingsValue ^= TextSettings.RandoFullTLK;
        }

        private void cbEntries_CheckedChanged(object sender, EventArgs e)
        {
            cbMatchEntrySound.Enabled = cbEntries.Checked;
            if (!checks_set) { return; }
            settings.TextSettingsValue ^= TextSettings.RandoDialogEntries;
        }

        private void cbReplies_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            settings.TextSettingsValue ^= TextSettings.RandoDialogReplies;
        }

        private void cbMatchEntrySound_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            settings.TextSettingsValue ^= TextSettings.MatchEntrySoundsWText;
        }

        private void cbMatchStringLen_CheckedChanged(object sender, EventArgs e)
        {
            if (!checks_set) { return; }
            settings.TextSettingsValue ^= TextSettings.MatchSimLengthStrings;
        }

        //Form Closing
        private void TextF_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.Save();
        }
        #endregion
    }
}
