namespace kotor_Randomizer_2
{
    partial class ModuleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleForm));
            this.OmittedListBox = new System.Windows.Forms.ListBox();
            this.RandomizedListBox = new System.Windows.Forms.ListBox();
            this.PresetComboBox = new System.Windows.Forms.ComboBox();
            this.cbDeleteMilestones = new System.Windows.Forms.CheckBox();
            this.lblPresets = new System.Windows.Forms.Label();
            this.cbSaveAllMods = new System.Windows.Forms.CheckBox();
            this.cbSaveMiniGame = new System.Windows.Forms.CheckBox();
            this.lblRandomized = new System.Windows.Forms.Label();
            this.lblOmitted = new System.Windows.Forms.Label();
            this.cbFixDream = new System.Windows.Forms.CheckBox();
            this.cbUnlockGalaxyMap = new System.Windows.Forms.CheckBox();
            this.cbFixCoordinates = new System.Windows.Forms.CheckBox();
            this.cbFixMindPrison = new System.Windows.Forms.CheckBox();
            this.cbDoorFix = new System.Windows.Forms.CheckBox();
            this.cbFixLevElevators = new System.Windows.Forms.CheckBox();
            this.cbVulkSpiceLZ = new System.Windows.Forms.CheckBox();
            this.cbReachability = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGoalMalak = new System.Windows.Forms.CheckBox();
            this.cbGoalStarMaps = new System.Windows.Forms.CheckBox();
            this.cbGoalPazaak = new System.Windows.Forms.CheckBox();
            this.cbGlitchDlz = new System.Windows.Forms.CheckBox();
            this.cbGlitchFlu = new System.Windows.Forms.CheckBox();
            this.cbGlitchGpw = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ModulesToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbGlitchClip = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OmittedListBox
            // 
            this.OmittedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.OmittedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OmittedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.OmittedListBox.FormattingEnabled = true;
            this.OmittedListBox.Location = new System.Drawing.Point(280, 50);
            this.OmittedListBox.Name = "OmittedListBox";
            this.OmittedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.OmittedListBox.Size = new System.Drawing.Size(240, 379);
            this.OmittedListBox.TabIndex = 17;
            this.OmittedListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OmittedListBox_KeyPress);
            this.OmittedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OmittedListBox_MouseDoubleClick);
            // 
            // RandomizedListBox
            // 
            this.RandomizedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.RandomizedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RandomizedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.RandomizedListBox.FormattingEnabled = true;
            this.RandomizedListBox.Location = new System.Drawing.Point(20, 50);
            this.RandomizedListBox.Name = "RandomizedListBox";
            this.RandomizedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RandomizedListBox.Size = new System.Drawing.Size(240, 379);
            this.RandomizedListBox.TabIndex = 16;
            this.RandomizedListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RandomizedListBox_KeyPress);
            this.RandomizedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RandomizedListBox_MouseDoubleClick);
            // 
            // PresetComboBox
            // 
            this.PresetComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.PresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PresetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PresetComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.PresetComboBox.FormattingEnabled = true;
            this.PresetComboBox.Location = new System.Drawing.Point(20, 460);
            this.PresetComboBox.Name = "PresetComboBox";
            this.PresetComboBox.Size = new System.Drawing.Size(500, 21);
            this.PresetComboBox.TabIndex = 18;
            this.PresetComboBox.SelectedIndexChanged += new System.EventHandler(this.PresetComboBox_SelectedIndexChanged);
            // 
            // cbDeleteMilestones
            // 
            this.cbDeleteMilestones.AutoSize = true;
            this.cbDeleteMilestones.Checked = true;
            this.cbDeleteMilestones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteMilestones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbDeleteMilestones.Location = new System.Drawing.Point(20, 490);
            this.cbDeleteMilestones.Name = "cbDeleteMilestones";
            this.cbDeleteMilestones.Size = new System.Drawing.Size(159, 17);
            this.cbDeleteMilestones.TabIndex = 19;
            this.cbDeleteMilestones.Text = "Delete Milestone Save Data";
            this.ModulesToolTip.SetToolTip(this.cbDeleteMilestones, resources.GetString("cbDeleteMilestones.ToolTip"));
            this.cbDeleteMilestones.UseVisualStyleBackColor = true;
            this.cbDeleteMilestones.CheckedChanged += new System.EventHandler(this.cbDeleteMilestones_CheckedChanged);
            // 
            // lblPresets
            // 
            this.lblPresets.AutoSize = true;
            this.lblPresets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblPresets.Location = new System.Drawing.Point(20, 440);
            this.lblPresets.Name = "lblPresets";
            this.lblPresets.Size = new System.Drawing.Size(53, 13);
            this.lblPresets.TabIndex = 20;
            this.lblPresets.Text = "Presets:";
            // 
            // cbSaveAllMods
            // 
            this.cbSaveAllMods.AutoSize = true;
            this.cbSaveAllMods.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbSaveAllMods.Location = new System.Drawing.Point(340, 490);
            this.cbSaveAllMods.Name = "cbSaveAllMods";
            this.cbSaveAllMods.Size = new System.Drawing.Size(157, 17);
            this.cbSaveAllMods.TabIndex = 21;
            this.cbSaveAllMods.Text = "Include All Modules in Save";
            this.ModulesToolTip.SetToolTip(this.cbSaveAllMods, "This feature extends the minigame one to also include\r\nSTUNT cutscenes and variou" +
        "s other odd modules in the\r\nsave file.");
            this.cbSaveAllMods.UseVisualStyleBackColor = true;
            this.cbSaveAllMods.CheckedChanged += new System.EventHandler(this.cbSaveAllMods_CheckedChanged);
            // 
            // cbSaveMiniGame
            // 
            this.cbSaveMiniGame.AutoSize = true;
            this.cbSaveMiniGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbSaveMiniGame.Location = new System.Drawing.Point(180, 490);
            this.cbSaveMiniGame.Name = "cbSaveMiniGame";
            this.cbSaveMiniGame.Size = new System.Drawing.Size(153, 17);
            this.cbSaveMiniGame.TabIndex = 22;
            this.cbSaveMiniGame.Text = "Include Minigames in Save";
            this.ModulesToolTip.SetToolTip(this.cbSaveMiniGame, "This feature makes the game commit the minigame module\r\ndata to the save when ent" +
        "ering them, preventing possible\r\ngame crashes with randomized swoops or turrets." +
        "");
            this.cbSaveMiniGame.UseVisualStyleBackColor = true;
            this.cbSaveMiniGame.CheckedChanged += new System.EventHandler(this.cbSaveMiniGame_CheckedChanged);
            // 
            // lblRandomized
            // 
            this.lblRandomized.AutoSize = true;
            this.lblRandomized.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandomized.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRandomized.Location = new System.Drawing.Point(20, 20);
            this.lblRandomized.Name = "lblRandomized";
            this.lblRandomized.Size = new System.Drawing.Size(95, 16);
            this.lblRandomized.TabIndex = 23;
            this.lblRandomized.Text = "Randomized";
            this.lblRandomized.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRandomized.Click += new System.EventHandler(this.lblRandomized_Click);
            // 
            // lblOmitted
            // 
            this.lblOmitted.AutoSize = true;
            this.lblOmitted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOmitted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblOmitted.Location = new System.Drawing.Point(460, 20);
            this.lblOmitted.Name = "lblOmitted";
            this.lblOmitted.Size = new System.Drawing.Size(61, 16);
            this.lblOmitted.TabIndex = 24;
            this.lblOmitted.Text = "Omitted";
            this.lblOmitted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFixDream
            // 
            this.cbFixDream.AutoSize = true;
            this.cbFixDream.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixDream.Location = new System.Drawing.Point(20, 510);
            this.cbFixDream.Name = "cbFixDream";
            this.cbFixDream.Size = new System.Drawing.Size(125, 17);
            this.cbFixDream.TabIndex = 25;
            this.cbFixDream.Text = "Fix Dream Sequence";
            this.ModulesToolTip.SetToolTip(this.cbFixDream, "Fixes a rare instance in which a game crash could occur\r\nwith dream sequences.");
            this.cbFixDream.UseVisualStyleBackColor = true;
            this.cbFixDream.CheckedChanged += new System.EventHandler(this.cbFixDream_CheckedChanged);
            // 
            // cbUnlockGalaxyMap
            // 
            this.cbUnlockGalaxyMap.AutoSize = true;
            this.cbUnlockGalaxyMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbUnlockGalaxyMap.Location = new System.Drawing.Point(180, 510);
            this.cbUnlockGalaxyMap.Name = "cbUnlockGalaxyMap";
            this.cbUnlockGalaxyMap.Size = new System.Drawing.Size(119, 17);
            this.cbUnlockGalaxyMap.TabIndex = 26;
            this.cbUnlockGalaxyMap.Text = "Unlock Galaxy Map";
            this.ModulesToolTip.SetToolTip(this.cbUnlockGalaxyMap, "Unlock all destinations on the Ebon Hawk galaxy map from\r\nthe start of the game.");
            this.cbUnlockGalaxyMap.UseVisualStyleBackColor = true;
            this.cbUnlockGalaxyMap.CheckedChanged += new System.EventHandler(this.cbUnlockGalaxyMap_CheckedChanged);
            // 
            // cbFixCoordinates
            // 
            this.cbFixCoordinates.AutoSize = true;
            this.cbFixCoordinates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixCoordinates.Location = new System.Drawing.Point(340, 510);
            this.cbFixCoordinates.Name = "cbFixCoordinates";
            this.cbFixCoordinates.Size = new System.Drawing.Size(136, 17);
            this.cbFixCoordinates.TabIndex = 27;
            this.cbFixCoordinates.Text = "Fix Module Coordinates";
            this.ModulesToolTip.SetToolTip(this.cbFixCoordinates, resources.GetString("cbFixCoordinates.ToolTip"));
            this.cbFixCoordinates.UseVisualStyleBackColor = true;
            this.cbFixCoordinates.CheckedChanged += new System.EventHandler(this.cbFixCoordinates_CheckedChanged);
            // 
            // cbFixMindPrison
            // 
            this.cbFixMindPrison.AutoSize = true;
            this.cbFixMindPrison.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixMindPrison.Location = new System.Drawing.Point(20, 530);
            this.cbFixMindPrison.Name = "cbFixMindPrison";
            this.cbFixMindPrison.Size = new System.Drawing.Size(97, 17);
            this.cbFixMindPrison.TabIndex = 28;
            this.cbFixMindPrison.Text = "Fix Mind Prison";
            this.ModulesToolTip.SetToolTip(this.cbFixMindPrison, "Updates the Mystery Box so it can be used multiple times.");
            this.cbFixMindPrison.UseVisualStyleBackColor = true;
            this.cbFixMindPrison.CheckedChanged += new System.EventHandler(this.cbFixMindPrison_CheckedChanged);
            // 
            // cbDoorFix
            // 
            this.cbDoorFix.AutoSize = true;
            this.cbDoorFix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbDoorFix.Location = new System.Drawing.Point(180, 530);
            this.cbDoorFix.Name = "cbDoorFix";
            this.cbDoorFix.Size = new System.Drawing.Size(129, 17);
            this.cbDoorFix.TabIndex = 29;
            this.cbDoorFix.Text = "Unlock Various Doors";
            this.ModulesToolTip.SetToolTip(this.cbDoorFix, "Unlocks the door into the Dantooine Ruins and the door\r\nout of the Unknown World\'" +
        "s Temple Summit.");
            this.cbDoorFix.UseVisualStyleBackColor = true;
            this.cbDoorFix.CheckedChanged += new System.EventHandler(this.cbDoorFix_CheckedChanged);
            // 
            // cbFixLevElevators
            // 
            this.cbFixLevElevators.AutoSize = true;
            this.cbFixLevElevators.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixLevElevators.Location = new System.Drawing.Point(340, 530);
            this.cbFixLevElevators.Name = "cbFixLevElevators";
            this.cbFixLevElevators.Size = new System.Drawing.Size(136, 17);
            this.cbFixLevElevators.TabIndex = 30;
            this.cbFixLevElevators.Text = "Fix Leviathan Elevators";
            this.ModulesToolTip.SetToolTip(this.cbFixLevElevators, "The Leviathan elevator will not restrict you from going to\r\nthe Hanger early, and" +
        " the Hanger elevator is now usable.");
            this.cbFixLevElevators.UseVisualStyleBackColor = true;
            this.cbFixLevElevators.CheckedChanged += new System.EventHandler(this.cbFixLevElevators_CheckedChanged);
            // 
            // cbVulkSpiceLZ
            // 
            this.cbVulkSpiceLZ.AutoSize = true;
            this.cbVulkSpiceLZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbVulkSpiceLZ.Location = new System.Drawing.Point(20, 550);
            this.cbVulkSpiceLZ.Name = "cbVulkSpiceLZ";
            this.cbVulkSpiceLZ.Size = new System.Drawing.Size(151, 17);
            this.cbVulkSpiceLZ.TabIndex = 31;
            this.cbVulkSpiceLZ.Text = "Add Spice Lab Load Zone";
            this.ModulesToolTip.SetToolTip(this.cbVulkSpiceLZ, "A new loading zone to the unused Vulkar Spice Lab\r\nis added to the far west end o" +
        "f the Vulkar Base.");
            this.cbVulkSpiceLZ.UseVisualStyleBackColor = true;
            this.cbVulkSpiceLZ.CheckedChanged += new System.EventHandler(this.cbVulkSpiceLZ_CheckedChanged);
            // 
            // cbReachability
            // 
            this.cbReachability.AutoSize = true;
            this.cbReachability.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbReachability.Location = new System.Drawing.Point(20, 588);
            this.cbReachability.Name = "cbReachability";
            this.cbReachability.Size = new System.Drawing.Size(151, 17);
            this.cbReachability.TabIndex = 32;
            this.cbReachability.Text = "Verify Module Reachability";
            this.ModulesToolTip.SetToolTip(this.cbReachability, "Reachability means that all modules leading up to\r\nand including the ones contain" +
        "ing the goal(s) can\r\nbe found using either normal in-game logic or using\r\nthe gl" +
        "itches enabled below.");
            this.cbReachability.UseVisualStyleBackColor = true;
            this.cbReachability.CheckedChanged += new System.EventHandler(this.cbReachability_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(20, 612);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Goal(s) of this playthrough:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(20, 635);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Potentially required glitches:";
            // 
            // cbGoalMalak
            // 
            this.cbGoalMalak.AutoSize = true;
            this.cbGoalMalak.Checked = true;
            this.cbGoalMalak.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGoalMalak.Enabled = false;
            this.cbGoalMalak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGoalMalak.Location = new System.Drawing.Point(164, 611);
            this.cbGoalMalak.Name = "cbGoalMalak";
            this.cbGoalMalak.Size = new System.Drawing.Size(131, 17);
            this.cbGoalMalak.TabIndex = 35;
            this.cbGoalMalak.Text = "Defeat Malak (default)";
            this.ModulesToolTip.SetToolTip(this.cbGoalMalak, "Defeat Malak on the Viewing Platform\r\nof the Star Forge.");
            this.cbGoalMalak.UseVisualStyleBackColor = true;
            this.cbGoalMalak.CheckedChanged += new System.EventHandler(this.cbGoalMalak_CheckedChanged);
            // 
            // cbGoalStarMaps
            // 
            this.cbGoalStarMaps.AutoSize = true;
            this.cbGoalStarMaps.Enabled = false;
            this.cbGoalStarMaps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGoalStarMaps.Location = new System.Drawing.Point(301, 611);
            this.cbGoalStarMaps.Name = "cbGoalStarMaps";
            this.cbGoalStarMaps.Size = new System.Drawing.Size(109, 17);
            this.cbGoalStarMaps.TabIndex = 36;
            this.cbGoalStarMaps.Text = "Collect Star Maps";
            this.ModulesToolTip.SetToolTip(this.cbGoalStarMaps, "Collect the 5 star maps from Dantooine,\r\nKashyyyk, Korriban, Manaan, and Tatooine" +
        ".");
            this.cbGoalStarMaps.UseVisualStyleBackColor = true;
            this.cbGoalStarMaps.CheckedChanged += new System.EventHandler(this.cbGoalStarMaps_CheckedChanged);
            // 
            // cbGoalPazaak
            // 
            this.cbGoalPazaak.AutoSize = true;
            this.cbGoalPazaak.Enabled = false;
            this.cbGoalPazaak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGoalPazaak.Location = new System.Drawing.Point(416, 611);
            this.cbGoalPazaak.Name = "cbGoalPazaak";
            this.cbGoalPazaak.Size = new System.Drawing.Size(112, 17);
            this.cbGoalPazaak.TabIndex = 37;
            this.cbGoalPazaak.Text = "Pazaak Champion";
            this.ModulesToolTip.SetToolTip(this.cbGoalPazaak, "Defeat all pazaak players across the galaxy.");
            this.cbGoalPazaak.UseVisualStyleBackColor = true;
            this.cbGoalPazaak.CheckedChanged += new System.EventHandler(this.cbGoalPazaak_CheckedChanged);
            // 
            // cbGlitchDlz
            // 
            this.cbGlitchDlz.AutoSize = true;
            this.cbGlitchDlz.Enabled = false;
            this.cbGlitchDlz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGlitchDlz.Location = new System.Drawing.Point(233, 634);
            this.cbGlitchDlz.Name = "cbGlitchDlz";
            this.cbGlitchDlz.Size = new System.Drawing.Size(47, 17);
            this.cbGlitchDlz.TabIndex = 38;
            this.cbGlitchDlz.Text = "DLZ";
            this.ModulesToolTip.SetToolTip(this.cbGlitchDlz, "Displaced Loading Zone");
            this.cbGlitchDlz.UseVisualStyleBackColor = true;
            this.cbGlitchDlz.CheckedChanged += new System.EventHandler(this.cbGlitchDlz_CheckedChanged);
            // 
            // cbGlitchFlu
            // 
            this.cbGlitchFlu.AutoSize = true;
            this.cbGlitchFlu.Enabled = false;
            this.cbGlitchFlu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGlitchFlu.Location = new System.Drawing.Point(286, 634);
            this.cbGlitchFlu.Name = "cbGlitchFlu";
            this.cbGlitchFlu.Size = new System.Drawing.Size(46, 17);
            this.cbGlitchFlu.TabIndex = 39;
            this.cbGlitchFlu.Text = "FLU";
            this.ModulesToolTip.SetToolTip(this.cbGlitchFlu, "Fake Level Up");
            this.cbGlitchFlu.UseVisualStyleBackColor = true;
            this.cbGlitchFlu.CheckedChanged += new System.EventHandler(this.cbGlitchFlu_CheckedChanged);
            // 
            // cbGlitchGpw
            // 
            this.cbGlitchGpw.AutoSize = true;
            this.cbGlitchGpw.Enabled = false;
            this.cbGlitchGpw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGlitchGpw.Location = new System.Drawing.Point(338, 634);
            this.cbGlitchGpw.Name = "cbGlitchGpw";
            this.cbGlitchGpw.Size = new System.Drawing.Size(52, 17);
            this.cbGlitchGpw.TabIndex = 40;
            this.cbGlitchGpw.Text = "GPW";
            this.ModulesToolTip.SetToolTip(this.cbGlitchGpw, "Gather Party Warp / Teleport");
            this.cbGlitchGpw.UseVisualStyleBackColor = true;
            this.cbGlitchGpw.CheckedChanged += new System.EventHandler(this.cbGlitchGpw_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(20, 576);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 2);
            this.panel1.TabIndex = 41;
            // 
            // cbGlitchClip
            // 
            this.cbGlitchClip.AutoSize = true;
            this.cbGlitchClip.Enabled = false;
            this.cbGlitchClip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGlitchClip.Location = new System.Drawing.Point(164, 634);
            this.cbGlitchClip.Name = "cbGlitchClip";
            this.cbGlitchClip.Size = new System.Drawing.Size(63, 17);
            this.cbGlitchClip.TabIndex = 42;
            this.cbGlitchClip.Text = "Clipping";
            this.ModulesToolTip.SetToolTip(this.cbGlitchClip, "Clipping through doors, etc.");
            this.cbGlitchClip.UseVisualStyleBackColor = true;
            this.cbGlitchClip.CheckedChanged += new System.EventHandler(this.cbGlitchClip_CheckedChanged);
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(541, 667);
            this.Controls.Add(this.cbGlitchClip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbGlitchGpw);
            this.Controls.Add(this.cbGlitchFlu);
            this.Controls.Add(this.cbGlitchDlz);
            this.Controls.Add(this.cbGoalPazaak);
            this.Controls.Add(this.cbGoalStarMaps);
            this.Controls.Add(this.cbGoalMalak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbReachability);
            this.Controls.Add(this.cbVulkSpiceLZ);
            this.Controls.Add(this.cbFixLevElevators);
            this.Controls.Add(this.cbDoorFix);
            this.Controls.Add(this.cbFixMindPrison);
            this.Controls.Add(this.cbFixCoordinates);
            this.Controls.Add(this.cbUnlockGalaxyMap);
            this.Controls.Add(this.cbFixDream);
            this.Controls.Add(this.lblOmitted);
            this.Controls.Add(this.lblRandomized);
            this.Controls.Add(this.cbSaveMiniGame);
            this.Controls.Add(this.cbSaveAllMods);
            this.Controls.Add(this.lblPresets);
            this.Controls.Add(this.cbDeleteMilestones);
            this.Controls.Add(this.PresetComboBox);
            this.Controls.Add(this.OmittedListBox);
            this.Controls.Add(this.RandomizedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModuleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modules";
            this.Activated += new System.EventHandler(this.ModuleForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox OmittedListBox;
        private System.Windows.Forms.ListBox RandomizedListBox;
        private System.Windows.Forms.ComboBox PresetComboBox;
        private System.Windows.Forms.CheckBox cbDeleteMilestones;
        private System.Windows.Forms.Label lblPresets;
        private System.Windows.Forms.CheckBox cbSaveAllMods;
        private System.Windows.Forms.CheckBox cbSaveMiniGame;
        private System.Windows.Forms.Label lblRandomized;
        private System.Windows.Forms.Label lblOmitted;
        private System.Windows.Forms.CheckBox cbFixDream;
        private System.Windows.Forms.CheckBox cbUnlockGalaxyMap;
        private System.Windows.Forms.CheckBox cbFixCoordinates;
        private System.Windows.Forms.CheckBox cbFixMindPrison;
        private System.Windows.Forms.CheckBox cbDoorFix;
        private System.Windows.Forms.CheckBox cbFixLevElevators;
        private System.Windows.Forms.CheckBox cbVulkSpiceLZ;
        private System.Windows.Forms.CheckBox cbReachability;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbGoalMalak;
        private System.Windows.Forms.CheckBox cbGoalStarMaps;
        private System.Windows.Forms.CheckBox cbGoalPazaak;
        private System.Windows.Forms.CheckBox cbGlitchDlz;
        private System.Windows.Forms.CheckBox cbGlitchFlu;
        private System.Windows.Forms.CheckBox cbGlitchGpw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip ModulesToolTip;
        private System.Windows.Forms.CheckBox cbGlitchClip;
    }
}