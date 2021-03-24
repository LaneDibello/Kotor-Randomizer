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
            this.lblGoals = new System.Windows.Forms.Label();
            this.lblGlitches = new System.Windows.Forms.Label();
            this.cbGoalMalak = new System.Windows.Forms.CheckBox();
            this.cbGoalStarMaps = new System.Windows.Forms.CheckBox();
            this.cbGoalPazaak = new System.Windows.Forms.CheckBox();
            this.cbGlitchDlz = new System.Windows.Forms.CheckBox();
            this.cbGlitchFlu = new System.Windows.Forms.CheckBox();
            this.cbGlitchGpw = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ModulesToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbGlitchClip = new System.Windows.Forms.CheckBox();
            this.cbIgnoreOnceEdges = new System.Windows.Forms.CheckBox();
            this.lblRule1 = new System.Windows.Forms.Label();
            this.lblRule2 = new System.Windows.Forms.Label();
            this.lblRule3 = new System.Windows.Forms.Label();
            this.cbUseRandoRules = new System.Windows.Forms.CheckBox();
            this.lblWIP = new System.Windows.Forms.Label();
            this.lblSaveData = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblShuffleSettings = new System.Windows.Forms.Label();
            this.lblGeneralSettings = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlGoals = new System.Windows.Forms.Panel();
            this.pnlGlitches = new System.Windows.Forms.Panel();
            this.pnlOther = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flpSaveData = new System.Windows.Forms.FlowLayoutPanel();
            this.flpTimeSavers = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlGoals.SuspendLayout();
            this.pnlGlitches.SuspendLayout();
            this.pnlOther.SuspendLayout();
            this.flpSaveData.SuspendLayout();
            this.flpTimeSavers.SuspendLayout();
            this.SuspendLayout();
            // 
            // OmittedListBox
            // 
            this.OmittedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.OmittedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OmittedListBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OmittedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.OmittedListBox.FormattingEnabled = true;
            this.OmittedListBox.Location = new System.Drawing.Point(287, 255);
            this.OmittedListBox.Name = "OmittedListBox";
            this.OmittedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.OmittedListBox.Size = new System.Drawing.Size(248, 299);
            this.OmittedListBox.TabIndex = 13;
            this.OmittedListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OmittedListBox_KeyPress);
            this.OmittedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OmittedListBox_MouseDoubleClick);
            // 
            // RandomizedListBox
            // 
            this.RandomizedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.RandomizedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RandomizedListBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RandomizedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.RandomizedListBox.FormattingEnabled = true;
            this.RandomizedListBox.Location = new System.Drawing.Point(20, 255);
            this.RandomizedListBox.Name = "RandomizedListBox";
            this.RandomizedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RandomizedListBox.Size = new System.Drawing.Size(248, 299);
            this.RandomizedListBox.TabIndex = 12;
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
            this.PresetComboBox.Location = new System.Drawing.Point(71, 206);
            this.PresetComboBox.Name = "PresetComboBox";
            this.PresetComboBox.Size = new System.Drawing.Size(197, 21);
            this.PresetComboBox.TabIndex = 11;
            this.ModulesToolTip.SetToolTip(this.PresetComboBox, resources.GetString("PresetComboBox.ToolTip"));
            this.PresetComboBox.SelectedIndexChanged += new System.EventHandler(this.PresetComboBox_SelectedIndexChanged);
            // 
            // cbDeleteMilestones
            // 
            this.cbDeleteMilestones.AutoSize = true;
            this.cbDeleteMilestones.Checked = true;
            this.cbDeleteMilestones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteMilestones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbDeleteMilestones.Location = new System.Drawing.Point(7, 5);
            this.cbDeleteMilestones.Name = "cbDeleteMilestones";
            this.cbDeleteMilestones.Size = new System.Drawing.Size(159, 17);
            this.cbDeleteMilestones.TabIndex = 1;
            this.cbDeleteMilestones.Text = "Delete Milestone Save Data";
            this.ModulesToolTip.SetToolTip(this.cbDeleteMilestones, resources.GetString("cbDeleteMilestones.ToolTip"));
            this.cbDeleteMilestones.UseVisualStyleBackColor = true;
            this.cbDeleteMilestones.CheckedChanged += new System.EventHandler(this.cbDeleteMilestones_CheckedChanged);
            // 
            // lblPresets
            // 
            this.lblPresets.AutoSize = true;
            this.lblPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblPresets.Location = new System.Drawing.Point(20, 209);
            this.lblPresets.Name = "lblPresets";
            this.lblPresets.Size = new System.Drawing.Size(45, 13);
            this.lblPresets.TabIndex = 20;
            this.lblPresets.Text = "Presets:";
            // 
            // cbSaveAllMods
            // 
            this.cbSaveAllMods.AutoSize = true;
            this.cbSaveAllMods.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbSaveAllMods.Location = new System.Drawing.Point(7, 51);
            this.cbSaveAllMods.Name = "cbSaveAllMods";
            this.cbSaveAllMods.Size = new System.Drawing.Size(157, 17);
            this.cbSaveAllMods.TabIndex = 3;
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
            this.cbSaveMiniGame.Location = new System.Drawing.Point(7, 28);
            this.cbSaveMiniGame.Name = "cbSaveMiniGame";
            this.cbSaveMiniGame.Size = new System.Drawing.Size(153, 17);
            this.cbSaveMiniGame.TabIndex = 2;
            this.cbSaveMiniGame.Text = "Include Minigames in Save";
            this.ModulesToolTip.SetToolTip(this.cbSaveMiniGame, "This feature makes the game commit the minigame module\r\ndata to the save when ent" +
        "ering them, preventing possible\r\ngame crashes with randomized swoops or turrets." +
        "");
            this.cbSaveMiniGame.UseVisualStyleBackColor = true;
            this.cbSaveMiniGame.CheckedChanged += new System.EventHandler(this.cbSaveMiniGame_CheckedChanged);
            // 
            // lblRandomized
            // 
            this.lblRandomized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandomized.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRandomized.Location = new System.Drawing.Point(20, 234);
            this.lblRandomized.Name = "lblRandomized";
            this.lblRandomized.Size = new System.Drawing.Size(248, 14);
            this.lblRandomized.TabIndex = 23;
            this.lblRandomized.Text = "Randomized";
            this.lblRandomized.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblOmitted
            // 
            this.lblOmitted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOmitted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblOmitted.Location = new System.Drawing.Point(287, 234);
            this.lblOmitted.Name = "lblOmitted";
            this.lblOmitted.Size = new System.Drawing.Size(248, 14);
            this.lblOmitted.TabIndex = 24;
            this.lblOmitted.Text = "Omitted";
            this.lblOmitted.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbFixDream
            // 
            this.cbFixDream.AutoSize = true;
            this.cbFixDream.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixDream.Location = new System.Drawing.Point(7, 28);
            this.cbFixDream.Name = "cbFixDream";
            this.cbFixDream.Size = new System.Drawing.Size(125, 17);
            this.cbFixDream.TabIndex = 4;
            this.cbFixDream.Text = "Fix Dream Sequence";
            this.ModulesToolTip.SetToolTip(this.cbFixDream, "Fixes a rare instance in which a game crash could occur\r\nwith dream sequences.");
            this.cbFixDream.UseVisualStyleBackColor = true;
            this.cbFixDream.CheckedChanged += new System.EventHandler(this.cbFixDream_CheckedChanged);
            // 
            // cbUnlockGalaxyMap
            // 
            this.cbUnlockGalaxyMap.AutoSize = true;
            this.cbUnlockGalaxyMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbUnlockGalaxyMap.Location = new System.Drawing.Point(164, 5);
            this.cbUnlockGalaxyMap.Name = "cbUnlockGalaxyMap";
            this.cbUnlockGalaxyMap.Size = new System.Drawing.Size(119, 17);
            this.cbUnlockGalaxyMap.TabIndex = 9;
            this.cbUnlockGalaxyMap.Text = "Unlock Galaxy Map";
            this.ModulesToolTip.SetToolTip(this.cbUnlockGalaxyMap, "Unlock all destinations on the Ebon Hawk galaxy map from\r\nthe start of the game.");
            this.cbUnlockGalaxyMap.UseVisualStyleBackColor = true;
            this.cbUnlockGalaxyMap.CheckedChanged += new System.EventHandler(this.cbUnlockGalaxyMap_CheckedChanged);
            // 
            // cbFixCoordinates
            // 
            this.cbFixCoordinates.AutoSize = true;
            this.cbFixCoordinates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixCoordinates.Location = new System.Drawing.Point(7, 74);
            this.cbFixCoordinates.Name = "cbFixCoordinates";
            this.cbFixCoordinates.Size = new System.Drawing.Size(136, 17);
            this.cbFixCoordinates.TabIndex = 6;
            this.cbFixCoordinates.Text = "Fix Module Coordinates";
            this.ModulesToolTip.SetToolTip(this.cbFixCoordinates, resources.GetString("cbFixCoordinates.ToolTip"));
            this.cbFixCoordinates.UseVisualStyleBackColor = true;
            this.cbFixCoordinates.CheckedChanged += new System.EventHandler(this.cbFixCoordinates_CheckedChanged);
            // 
            // cbFixMindPrison
            // 
            this.cbFixMindPrison.AutoSize = true;
            this.cbFixMindPrison.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixMindPrison.Location = new System.Drawing.Point(7, 51);
            this.cbFixMindPrison.Name = "cbFixMindPrison";
            this.cbFixMindPrison.Size = new System.Drawing.Size(97, 17);
            this.cbFixMindPrison.TabIndex = 5;
            this.cbFixMindPrison.Text = "Fix Mind Prison";
            this.ModulesToolTip.SetToolTip(this.cbFixMindPrison, "Updates the Mystery Box so it can be used multiple times.");
            this.cbFixMindPrison.UseVisualStyleBackColor = true;
            this.cbFixMindPrison.CheckedChanged += new System.EventHandler(this.cbFixMindPrison_CheckedChanged);
            // 
            // cbDoorFix
            // 
            this.cbDoorFix.AutoSize = true;
            this.cbDoorFix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbDoorFix.Location = new System.Drawing.Point(164, 28);
            this.cbDoorFix.Name = "cbDoorFix";
            this.cbDoorFix.Size = new System.Drawing.Size(129, 17);
            this.cbDoorFix.TabIndex = 10;
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
            this.cbFixLevElevators.Location = new System.Drawing.Point(7, 97);
            this.cbFixLevElevators.Name = "cbFixLevElevators";
            this.cbFixLevElevators.Size = new System.Drawing.Size(136, 17);
            this.cbFixLevElevators.TabIndex = 7;
            this.cbFixLevElevators.Text = "Fix Leviathan Elevators";
            this.ModulesToolTip.SetToolTip(this.cbFixLevElevators, "The Leviathan elevator will not restrict you from going to\r\nthe Hangar early, and" +
        " the Hangar elevator is now usable.");
            this.cbFixLevElevators.UseVisualStyleBackColor = true;
            this.cbFixLevElevators.CheckedChanged += new System.EventHandler(this.cbFixLevElevators_CheckedChanged);
            // 
            // cbVulkSpiceLZ
            // 
            this.cbVulkSpiceLZ.AutoSize = true;
            this.cbVulkSpiceLZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbVulkSpiceLZ.Location = new System.Drawing.Point(7, 5);
            this.cbVulkSpiceLZ.Name = "cbVulkSpiceLZ";
            this.cbVulkSpiceLZ.Size = new System.Drawing.Size(151, 17);
            this.cbVulkSpiceLZ.TabIndex = 8;
            this.cbVulkSpiceLZ.Text = "Add Spice Lab Load Zone";
            this.ModulesToolTip.SetToolTip(this.cbVulkSpiceLZ, "A new loading zone to the unused Vulkar Spice Lab\r\nis added to the far west end o" +
        "f the Vulkar Base.");
            this.cbVulkSpiceLZ.UseVisualStyleBackColor = true;
            this.cbVulkSpiceLZ.CheckedChanged += new System.EventHandler(this.cbVulkSpiceLZ_CheckedChanged);
            // 
            // cbReachability
            // 
            this.cbReachability.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbReachability.Location = new System.Drawing.Point(20, 616);
            this.cbReachability.Name = "cbReachability";
            this.cbReachability.Size = new System.Drawing.Size(151, 28);
            this.cbReachability.TabIndex = 15;
            this.cbReachability.Text = "Verify Module Reachability";
            this.ModulesToolTip.SetToolTip(this.cbReachability, "Reachability means that all modules leading up to\r\nand including the ones contain" +
        "ing the goal(s) can\r\nbe found using either normal in-game logic or using\r\nthe gl" +
        "itches enabled below.");
            this.cbReachability.UseVisualStyleBackColor = true;
            this.cbReachability.CheckedChanged += new System.EventHandler(this.cbReachability_CheckedChanged);
            // 
            // lblGoals
            // 
            this.lblGoals.AutoSize = true;
            this.lblGoals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblGoals.Location = new System.Drawing.Point(37, 652);
            this.lblGoals.Name = "lblGoals";
            this.lblGoals.Size = new System.Drawing.Size(132, 13);
            this.lblGoals.TabIndex = 33;
            this.lblGoals.Text = "Goal(s) of this playthrough:";
            this.ModulesToolTip.SetToolTip(this.lblGoals, "Goals define the final win conditions. Reachability will ensure\r\nthat the modules" +
        " related to the active goals are accessible\r\nfrom the starting module.");
            // 
            // lblGlitches
            // 
            this.lblGlitches.AutoSize = true;
            this.lblGlitches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGlitches.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblGlitches.Location = new System.Drawing.Point(202, 652);
            this.lblGlitches.Name = "lblGlitches";
            this.lblGlitches.Size = new System.Drawing.Size(138, 13);
            this.lblGlitches.TabIndex = 34;
            this.lblGlitches.Text = "Potentially required glitches:";
            this.ModulesToolTip.SetToolTip(this.lblGlitches, "Glitches may be enabled to be in-logic, and MAY be required\r\nto complete the acti" +
        "ve goals.\r\n\r\nDisabled glitches are NEVER required.");
            // 
            // cbGoalMalak
            // 
            this.cbGoalMalak.AutoSize = true;
            this.cbGoalMalak.Checked = true;
            this.cbGoalMalak.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGoalMalak.Enabled = false;
            this.cbGoalMalak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGoalMalak.Location = new System.Drawing.Point(3, 3);
            this.cbGoalMalak.Name = "cbGoalMalak";
            this.cbGoalMalak.Size = new System.Drawing.Size(131, 17);
            this.cbGoalMalak.TabIndex = 16;
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
            this.cbGoalStarMaps.Location = new System.Drawing.Point(3, 23);
            this.cbGoalStarMaps.Name = "cbGoalStarMaps";
            this.cbGoalStarMaps.Size = new System.Drawing.Size(109, 17);
            this.cbGoalStarMaps.TabIndex = 17;
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
            this.cbGoalPazaak.Location = new System.Drawing.Point(3, 43);
            this.cbGoalPazaak.Name = "cbGoalPazaak";
            this.cbGoalPazaak.Size = new System.Drawing.Size(112, 17);
            this.cbGoalPazaak.TabIndex = 18;
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
            this.cbGlitchDlz.Location = new System.Drawing.Point(4, 23);
            this.cbGlitchDlz.Name = "cbGlitchDlz";
            this.cbGlitchDlz.Size = new System.Drawing.Size(47, 17);
            this.cbGlitchDlz.TabIndex = 20;
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
            this.cbGlitchFlu.Location = new System.Drawing.Point(73, 23);
            this.cbGlitchFlu.Name = "cbGlitchFlu";
            this.cbGlitchFlu.Size = new System.Drawing.Size(46, 17);
            this.cbGlitchFlu.TabIndex = 22;
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
            this.cbGlitchGpw.Location = new System.Drawing.Point(73, 3);
            this.cbGlitchGpw.Name = "cbGlitchGpw";
            this.cbGlitchGpw.Size = new System.Drawing.Size(52, 17);
            this.cbGlitchGpw.TabIndex = 21;
            this.cbGlitchGpw.Text = "GPW";
            this.ModulesToolTip.SetToolTip(this.cbGlitchGpw, "Gather Party Warp / Teleport");
            this.cbGlitchGpw.UseVisualStyleBackColor = true;
            this.cbGlitchGpw.CheckedChanged += new System.EventHandler(this.cbGlitchGpw_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(20, 562);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 2);
            this.panel1.TabIndex = 41;
            // 
            // ModulesToolTip
            // 
            this.ModulesToolTip.AutoPopDelay = 10000;
            this.ModulesToolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ModulesToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.ModulesToolTip.InitialDelay = 500;
            this.ModulesToolTip.IsBalloon = true;
            this.ModulesToolTip.ReshowDelay = 100;
            // 
            // cbGlitchClip
            // 
            this.cbGlitchClip.AutoSize = true;
            this.cbGlitchClip.Enabled = false;
            this.cbGlitchClip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbGlitchClip.Location = new System.Drawing.Point(4, 3);
            this.cbGlitchClip.Name = "cbGlitchClip";
            this.cbGlitchClip.Size = new System.Drawing.Size(63, 17);
            this.cbGlitchClip.TabIndex = 19;
            this.cbGlitchClip.Text = "Clipping";
            this.ModulesToolTip.SetToolTip(this.cbGlitchClip, "Clipping through doors, etc.");
            this.cbGlitchClip.UseVisualStyleBackColor = true;
            this.cbGlitchClip.CheckedChanged += new System.EventHandler(this.cbGlitchClip_CheckedChanged);
            // 
            // cbIgnoreOnceEdges
            // 
            this.cbIgnoreOnceEdges.AutoSize = true;
            this.cbIgnoreOnceEdges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbIgnoreOnceEdges.Location = new System.Drawing.Point(3, 3);
            this.cbIgnoreOnceEdges.Name = "cbIgnoreOnceEdges";
            this.cbIgnoreOnceEdges.Size = new System.Drawing.Size(164, 17);
            this.cbIgnoreOnceEdges.TabIndex = 23;
            this.cbIgnoreOnceEdges.Text = "Ignore Single-Use Transitions";
            this.ModulesToolTip.SetToolTip(this.cbIgnoreOnceEdges, "The reachability algorithm will ignore one-time use loading\r\nzones or transitions" +
        " to fulfill the reachability condition.\r\n*Enable this option for a more stable r" +
        "andomization.*");
            this.cbIgnoreOnceEdges.UseVisualStyleBackColor = true;
            this.cbIgnoreOnceEdges.CheckedChanged += new System.EventHandler(this.cbAllowOnceEdges_CheckedChanged);
            // 
            // lblRule1
            // 
            this.lblRule1.AutoSize = true;
            this.lblRule1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRule1.Location = new System.Drawing.Point(222, 593);
            this.lblRule1.Name = "lblRule1";
            this.lblRule1.Size = new System.Drawing.Size(44, 13);
            this.lblRule1.TabIndex = 50;
            this.lblRule1.Text = "(Rule 1,";
            this.ModulesToolTip.SetToolTip(this.lblRule1, "If a module has one exit, the module cannot replace its only\r\ndestination. (e.g.," +
        " Czerka Office cannot replace Anchorhead)\r\nThis prevents binary infinite loops t" +
        "hat you can\'t escape from.");
            // 
            // lblRule2
            // 
            this.lblRule2.AutoSize = true;
            this.lblRule2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRule2.Location = new System.Drawing.Point(272, 593);
            this.lblRule2.Name = "lblRule2";
            this.lblRule2.Size = new System.Drawing.Size(41, 13);
            this.lblRule2.TabIndex = 51;
            this.lblRule2.Text = "Rule 2,";
            this.ModulesToolTip.SetToolTip(this.lblRule2, "The parent of a module with only one entrance cannot\r\nreplace that module. (e.g.," +
        " Anchorhead cannot replace\r\nCzerka Office)\r\nThis prevents some modules from beco" +
        "ming completely\r\nunreachable.");
            // 
            // lblRule3
            // 
            this.lblRule3.AutoSize = true;
            this.lblRule3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRule3.Location = new System.Drawing.Point(319, 593);
            this.lblRule3.Name = "lblRule3";
            this.lblRule3.Size = new System.Drawing.Size(41, 13);
            this.lblRule3.TabIndex = 52;
            this.lblRule3.Text = "Rule 3)";
            this.ModulesToolTip.SetToolTip(this.lblRule3, resources.GetString("lblRule3.ToolTip"));
            // 
            // cbUseRandoRules
            // 
            this.cbUseRandoRules.AutoSize = true;
            this.cbUseRandoRules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbUseRandoRules.Location = new System.Drawing.Point(20, 592);
            this.cbUseRandoRules.Name = "cbUseRandoRules";
            this.cbUseRandoRules.Size = new System.Drawing.Size(196, 17);
            this.cbUseRandoRules.TabIndex = 14;
            this.cbUseRandoRules.Text = "Use Randomization Exclusion Rules";
            this.ModulesToolTip.SetToolTip(this.cbUseRandoRules, "These rules prevent certain modules from replacing others\r\nwhen that replacement " +
        "would cause problems - inescapable\r\nor inaccessible rooms.");
            this.cbUseRandoRules.UseVisualStyleBackColor = true;
            this.cbUseRandoRules.CheckedChanged += new System.EventHandler(this.cbUseRandoRules_CheckedChanged);
            // 
            // lblWIP
            // 
            this.lblWIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.lblWIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblWIP.Location = new System.Drawing.Point(197, 616);
            this.lblWIP.Name = "lblWIP";
            this.lblWIP.Size = new System.Drawing.Size(337, 28);
            this.lblWIP.TabIndex = 53;
            this.lblWIP.Text = "Reachability verification is a work in progress. It does not yet ensure the seed " +
    "is beatable, but it does improve those chances.";
            this.lblWIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSaveData
            // 
            this.lblSaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblSaveData.Location = new System.Drawing.Point(20, 30);
            this.lblSaveData.Name = "lblSaveData";
            this.lblSaveData.Size = new System.Drawing.Size(179, 14);
            this.lblSaveData.TabIndex = 55;
            this.lblSaveData.Text = "Save Data";
            this.lblSaveData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSaveData.Click += new System.EventHandler(this.lblRandomized_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(223, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 14);
            this.label3.TabIndex = 56;
            this.label3.Text = "Time Savers / Quality of Life";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.panel2.Location = new System.Drawing.Point(20, 174);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 2);
            this.panel2.TabIndex = 60;
            // 
            // lblShuffleSettings
            // 
            this.lblShuffleSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShuffleSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblShuffleSettings.Location = new System.Drawing.Point(20, 181);
            this.lblShuffleSettings.Name = "lblShuffleSettings";
            this.lblShuffleSettings.Size = new System.Drawing.Size(515, 18);
            this.lblShuffleSettings.TabIndex = 54;
            this.lblShuffleSettings.Text = "Module Shuffle Settings";
            this.lblShuffleSettings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblGeneralSettings
            // 
            this.lblGeneralSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneralSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblGeneralSettings.Location = new System.Drawing.Point(20, 9);
            this.lblGeneralSettings.Name = "lblGeneralSettings";
            this.lblGeneralSettings.Size = new System.Drawing.Size(515, 18);
            this.lblGeneralSettings.TabIndex = 61;
            this.lblGeneralSettings.Text = "Module General Settings";
            this.lblGeneralSettings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(20, 569);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(515, 18);
            this.label2.TabIndex = 62;
            this.label2.Text = "Module Shuffle Logic";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlGoals
            // 
            this.pnlGoals.AutoSize = true;
            this.pnlGoals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.pnlGoals.Controls.Add(this.cbGoalMalak);
            this.pnlGoals.Controls.Add(this.cbGoalStarMaps);
            this.pnlGoals.Controls.Add(this.cbGoalPazaak);
            this.pnlGoals.Location = new System.Drawing.Point(37, 672);
            this.pnlGoals.Name = "pnlGoals";
            this.pnlGoals.Size = new System.Drawing.Size(137, 63);
            this.pnlGoals.TabIndex = 63;
            // 
            // pnlGlitches
            // 
            this.pnlGlitches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.pnlGlitches.Controls.Add(this.cbGlitchClip);
            this.pnlGlitches.Controls.Add(this.cbGlitchGpw);
            this.pnlGlitches.Controls.Add(this.cbGlitchFlu);
            this.pnlGlitches.Controls.Add(this.cbGlitchDlz);
            this.pnlGlitches.Location = new System.Drawing.Point(202, 672);
            this.pnlGlitches.Name = "pnlGlitches";
            this.pnlGlitches.Size = new System.Drawing.Size(137, 63);
            this.pnlGlitches.TabIndex = 64;
            // 
            // pnlOther
            // 
            this.pnlOther.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.pnlOther.Controls.Add(this.cbIgnoreOnceEdges);
            this.pnlOther.Location = new System.Drawing.Point(366, 672);
            this.pnlOther.Name = "pnlOther";
            this.pnlOther.Size = new System.Drawing.Size(169, 63);
            this.pnlOther.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(366, 652);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Other settings:";
            // 
            // flpSaveData
            // 
            this.flpSaveData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.flpSaveData.Controls.Add(this.cbDeleteMilestones);
            this.flpSaveData.Controls.Add(this.cbSaveMiniGame);
            this.flpSaveData.Controls.Add(this.cbSaveAllMods);
            this.flpSaveData.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSaveData.Location = new System.Drawing.Point(20, 49);
            this.flpSaveData.Name = "flpSaveData";
            this.flpSaveData.Padding = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.flpSaveData.Size = new System.Drawing.Size(179, 73);
            this.flpSaveData.TabIndex = 67;
            // 
            // flpTimeSavers
            // 
            this.flpTimeSavers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.flpTimeSavers.Controls.Add(this.cbVulkSpiceLZ);
            this.flpTimeSavers.Controls.Add(this.cbFixDream);
            this.flpTimeSavers.Controls.Add(this.cbFixMindPrison);
            this.flpTimeSavers.Controls.Add(this.cbFixCoordinates);
            this.flpTimeSavers.Controls.Add(this.cbFixLevElevators);
            this.flpTimeSavers.Controls.Add(this.cbUnlockGalaxyMap);
            this.flpTimeSavers.Controls.Add(this.cbDoorFix);
            this.flpTimeSavers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTimeSavers.Location = new System.Drawing.Point(226, 49);
            this.flpTimeSavers.Name = "flpTimeSavers";
            this.flpTimeSavers.Padding = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.flpTimeSavers.Size = new System.Drawing.Size(309, 119);
            this.flpTimeSavers.TabIndex = 68;
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(556, 752);
            this.Controls.Add(this.flpTimeSavers);
            this.Controls.Add(this.flpSaveData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlOther);
            this.Controls.Add(this.pnlGlitches);
            this.Controls.Add(this.pnlGoals);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblGeneralSettings);
            this.Controls.Add(this.lblShuffleSettings);
            this.Controls.Add(this.lblPresets);
            this.Controls.Add(this.lblWIP);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.RandomizedListBox);
            this.Controls.Add(this.lblRule3);
            this.Controls.Add(this.OmittedListBox);
            this.Controls.Add(this.lblRule2);
            this.Controls.Add(this.PresetComboBox);
            this.Controls.Add(this.lblRule1);
            this.Controls.Add(this.lblRandomized);
            this.Controls.Add(this.lblSaveData);
            this.Controls.Add(this.cbUseRandoRules);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblOmitted);
            this.Controls.Add(this.cbReachability);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblGoals);
            this.Controls.Add(this.lblGlitches);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModuleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modules";
            this.Activated += new System.EventHandler(this.ModuleForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleForm_FormClosing);
            this.pnlGoals.ResumeLayout(false);
            this.pnlGoals.PerformLayout();
            this.pnlGlitches.ResumeLayout(false);
            this.pnlGlitches.PerformLayout();
            this.pnlOther.ResumeLayout(false);
            this.pnlOther.PerformLayout();
            this.flpSaveData.ResumeLayout(false);
            this.flpSaveData.PerformLayout();
            this.flpTimeSavers.ResumeLayout(false);
            this.flpTimeSavers.PerformLayout();
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
        private System.Windows.Forms.Label lblGoals;
        private System.Windows.Forms.Label lblGlitches;
        private System.Windows.Forms.CheckBox cbGoalMalak;
        private System.Windows.Forms.CheckBox cbGoalStarMaps;
        private System.Windows.Forms.CheckBox cbGoalPazaak;
        private System.Windows.Forms.CheckBox cbGlitchDlz;
        private System.Windows.Forms.CheckBox cbGlitchFlu;
        private System.Windows.Forms.CheckBox cbGlitchGpw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip ModulesToolTip;
        private System.Windows.Forms.CheckBox cbGlitchClip;
        private System.Windows.Forms.CheckBox cbIgnoreOnceEdges;
        private System.Windows.Forms.CheckBox cbUseRandoRules;
        private System.Windows.Forms.Label lblRule1;
        private System.Windows.Forms.Label lblRule2;
        private System.Windows.Forms.Label lblRule3;
        private System.Windows.Forms.Label lblWIP;
        private System.Windows.Forms.Label lblSaveData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblShuffleSettings;
        private System.Windows.Forms.Label lblGeneralSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlGoals;
        private System.Windows.Forms.Panel pnlGlitches;
        private System.Windows.Forms.Panel pnlOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpSaveData;
        private System.Windows.Forms.FlowLayoutPanel flpTimeSavers;
    }
}