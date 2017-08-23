namespace JasonChan_SoundWaveProject
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximizeAmplitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pitchShiftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_p3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_p2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_p1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_m1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_m2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Edit_ps_m3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBitRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_8_Bits = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_16_Bits = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_24_Bits = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_32_Bits = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_channels_mono = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_channels_stereo = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSampleRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_SampleRate_11025 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_SampleRate_22050 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_SampleRate_44100 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_SampleRate_88200 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarMain = new System.Windows.Forms.ToolStrip();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.recordButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolBarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(986, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openButton_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.quitToolStripMenuItem.Text = "&Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reverseToolStripMenuItem,
            this.maximizeAmplitudeToolStripMenuItem,
            this.pitchShiftToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            // 
            // reverseToolStripMenuItem
            // 
            this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
            this.reverseToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.reverseToolStripMenuItem.Text = "&Reverse Wave";
            this.reverseToolStripMenuItem.Click += new System.EventHandler(this.reverseToolStripMenuItem_Click);
            // 
            // maximizeAmplitudeToolStripMenuItem
            // 
            this.maximizeAmplitudeToolStripMenuItem.Name = "maximizeAmplitudeToolStripMenuItem";
            this.maximizeAmplitudeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.maximizeAmplitudeToolStripMenuItem.Text = "Maximize Amplitude";
            this.maximizeAmplitudeToolStripMenuItem.Click += new System.EventHandler(this.maximizeAmplitudeToolStripMenuItem_Click);
            // 
            // pitchShiftToolStripMenuItem
            // 
            this.pitchShiftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Edit_ps_p3,
            this.menu_Edit_ps_p2,
            this.menu_Edit_ps_p1,
            this.menu_Edit_ps_m1,
            this.menu_Edit_ps_m2,
            this.menu_Edit_ps_m3});
            this.pitchShiftToolStripMenuItem.Name = "pitchShiftToolStripMenuItem";
            this.pitchShiftToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.pitchShiftToolStripMenuItem.Text = "Pitch-Shift";
            // 
            // menu_Edit_ps_p3
            // 
            this.menu_Edit_ps_p3.Name = "menu_Edit_ps_p3";
            this.menu_Edit_ps_p3.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_p3.Tag = 3;
            this.menu_Edit_ps_p3.Text = "+3";
            this.menu_Edit_ps_p3.Click += new System.EventHandler(this.PitchShift);
            // 
            // menu_Edit_ps_p2
            // 
            this.menu_Edit_ps_p2.Name = "menu_Edit_ps_p2";
            this.menu_Edit_ps_p2.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_p2.Tag = 2;
            this.menu_Edit_ps_p2.Text = "+2";
            this.menu_Edit_ps_p2.Click += new System.EventHandler(this.PitchShift);
            // 
            // menu_Edit_ps_p1
            // 
            this.menu_Edit_ps_p1.Name = "menu_Edit_ps_p1";
            this.menu_Edit_ps_p1.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_p1.Tag = 1;
            this.menu_Edit_ps_p1.Text = "+1";
            this.menu_Edit_ps_p1.Click += new System.EventHandler(this.PitchShift);
            // 
            // menu_Edit_ps_m1
            // 
            this.menu_Edit_ps_m1.Name = "menu_Edit_ps_m1";
            this.menu_Edit_ps_m1.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_m1.Tag = -1;
            this.menu_Edit_ps_m1.Text = "-1";
            this.menu_Edit_ps_m1.Click += new System.EventHandler(this.PitchShift);
            // 
            // menu_Edit_ps_m2
            // 
            this.menu_Edit_ps_m2.Name = "menu_Edit_ps_m2";
            this.menu_Edit_ps_m2.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_m2.Tag = -2;
            this.menu_Edit_ps_m2.Text = "-2";
            this.menu_Edit_ps_m2.Click += new System.EventHandler(this.PitchShift);
            // 
            // menu_Edit_ps_m3
            // 
            this.menu_Edit_ps_m3.Name = "menu_Edit_ps_m3";
            this.menu_Edit_ps_m3.Size = new System.Drawing.Size(88, 22);
            this.menu_Edit_ps_m3.Tag = -3;
            this.menu_Edit_ps_m3.Text = "-3";
            this.menu_Edit_ps_m3.Click += new System.EventHandler(this.PitchShift);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBitRateToolStripMenuItem,
            this.channelsToolStripMenuItem,
            this.changeSampleRateToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolToolStripMenuItem.Text = "&Tools";
            // 
            // changeBitRateToolStripMenuItem
            // 
            this.changeBitRateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tool_8_Bits,
            this.menu_Tool_16_Bits,
            this.menu_Tool_24_Bits,
            this.menu_Tool_32_Bits});
            this.changeBitRateToolStripMenuItem.Name = "changeBitRateToolStripMenuItem";
            this.changeBitRateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.changeBitRateToolStripMenuItem.Text = "Bit Rate";
            // 
            // menu_Tool_8_Bits
            // 
            this.menu_Tool_8_Bits.Name = "menu_Tool_8_Bits";
            this.menu_Tool_8_Bits.Size = new System.Drawing.Size(86, 22);
            this.menu_Tool_8_Bits.Tag = 8;
            this.menu_Tool_8_Bits.Text = "8";
            this.menu_Tool_8_Bits.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_16_Bits
            // 
            this.menu_Tool_16_Bits.Name = "menu_Tool_16_Bits";
            this.menu_Tool_16_Bits.Size = new System.Drawing.Size(86, 22);
            this.menu_Tool_16_Bits.Tag = 16;
            this.menu_Tool_16_Bits.Text = "16";
            this.menu_Tool_16_Bits.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_24_Bits
            // 
            this.menu_Tool_24_Bits.Name = "menu_Tool_24_Bits";
            this.menu_Tool_24_Bits.Size = new System.Drawing.Size(86, 22);
            this.menu_Tool_24_Bits.Tag = 24;
            this.menu_Tool_24_Bits.Text = "24";
            this.menu_Tool_24_Bits.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_32_Bits
            // 
            this.menu_Tool_32_Bits.Name = "menu_Tool_32_Bits";
            this.menu_Tool_32_Bits.Size = new System.Drawing.Size(86, 22);
            this.menu_Tool_32_Bits.Tag = 32;
            this.menu_Tool_32_Bits.Text = "32";
            this.menu_Tool_32_Bits.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tool_channels_mono,
            this.menu_Tool_channels_stereo});
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.channelsToolStripMenuItem.Text = "Channels";
            // 
            // menu_Tool_channels_mono
            // 
            this.menu_Tool_channels_mono.Name = "menu_Tool_channels_mono";
            this.menu_Tool_channels_mono.Size = new System.Drawing.Size(107, 22);
            this.menu_Tool_channels_mono.Tag = 1;
            this.menu_Tool_channels_mono.Text = "Mono";
            this.menu_Tool_channels_mono.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_channels_stereo
            // 
            this.menu_Tool_channels_stereo.Name = "menu_Tool_channels_stereo";
            this.menu_Tool_channels_stereo.Size = new System.Drawing.Size(107, 22);
            this.menu_Tool_channels_stereo.Tag = 2;
            this.menu_Tool_channels_stereo.Text = "Stereo";
            this.menu_Tool_channels_stereo.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // changeSampleRateToolStripMenuItem
            // 
            this.changeSampleRateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tool_SampleRate_11025,
            this.menu_Tool_SampleRate_22050,
            this.menu_Tool_SampleRate_44100,
            this.menu_Tool_SampleRate_88200});
            this.changeSampleRateToolStripMenuItem.Name = "changeSampleRateToolStripMenuItem";
            this.changeSampleRateToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.changeSampleRateToolStripMenuItem.Text = "Sample Rate";
            // 
            // menu_Tool_SampleRate_11025
            // 
            this.menu_Tool_SampleRate_11025.Name = "menu_Tool_SampleRate_11025";
            this.menu_Tool_SampleRate_11025.Size = new System.Drawing.Size(104, 22);
            this.menu_Tool_SampleRate_11025.Tag = 11025;
            this.menu_Tool_SampleRate_11025.Text = "11025";
            this.menu_Tool_SampleRate_11025.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_SampleRate_22050
            // 
            this.menu_Tool_SampleRate_22050.Name = "menu_Tool_SampleRate_22050";
            this.menu_Tool_SampleRate_22050.Size = new System.Drawing.Size(104, 22);
            this.menu_Tool_SampleRate_22050.Tag = 22050;
            this.menu_Tool_SampleRate_22050.Text = "22050";
            this.menu_Tool_SampleRate_22050.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_SampleRate_44100
            // 
            this.menu_Tool_SampleRate_44100.Name = "menu_Tool_SampleRate_44100";
            this.menu_Tool_SampleRate_44100.Size = new System.Drawing.Size(104, 22);
            this.menu_Tool_SampleRate_44100.Tag = 44100;
            this.menu_Tool_SampleRate_44100.Text = "44100";
            this.menu_Tool_SampleRate_44100.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // menu_Tool_SampleRate_88200
            // 
            this.menu_Tool_SampleRate_88200.Name = "menu_Tool_SampleRate_88200";
            this.menu_Tool_SampleRate_88200.Size = new System.Drawing.Size(104, 22);
            this.menu_Tool_SampleRate_88200.Tag = 88200;
            this.menu_Tool_SampleRate_88200.Text = "88200";
            this.menu_Tool_SampleRate_88200.Click += new System.EventHandler(this.menu_Tool_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.windowToolStripMenuItem.Text = "&Select Window";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.tutorialToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // tutorialToolStripMenuItem
            // 
            this.tutorialToolStripMenuItem.Name = "tutorialToolStripMenuItem";
            this.tutorialToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.tutorialToolStripMenuItem.Text = "&Tutorial";
            this.tutorialToolStripMenuItem.Click += new System.EventHandler(this.tutorialToolStripMenuItem_Click);
            // 
            // toolBarMain
            // 
            this.toolBarMain.AutoSize = false;
            this.toolBarMain.BackColor = System.Drawing.SystemColors.Control;
            this.toolBarMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolBarMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolBarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.saveButton,
            this.toolStripSeparator2,
            this.playButton,
            this.recordButton,
            this.stopButton});
            this.toolBarMain.Location = new System.Drawing.Point(0, 24);
            this.toolBarMain.Name = "toolBarMain";
            this.toolBarMain.Size = new System.Drawing.Size(53, 407);
            this.toolBarMain.TabIndex = 3;
            this.toolBarMain.Text = "toolStrip1";
            // 
            // newButton
            // 
            this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnNew;
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.RightToLeftAutoMirrorImage = true;
            this.newButton.Size = new System.Drawing.Size(51, 52);
            this.newButton.Text = "New Wave";
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnOpen;
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(51, 52);
            this.openButton.Text = "Open Wave";
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnSave;
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(51, 52);
            this.saveButton.Text = "Save Wave";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 55);
            // 
            // playButton
            // 
            this.playButton.AutoSize = false;
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Enabled = false;
            this.playButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnPlayDisabled;
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(52, 52);
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // recordButton
            // 
            this.recordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recordButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnRecord;
            this.recordButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(51, 52);
            this.recordButton.Text = "Record";
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopButton.Image = global::JasonChan_SoundWaveProject.Properties.Resources.btnStop;
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(51, 52);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Wave Files|*.wav|All files|*.*";
            this.openFileDialog1.InitialDirectory = "Documents";
            this.openFileDialog1.Title = "Open a wave file";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "wav";
            this.saveFileDialog1.Filter = "Wave Files|*.wav|All files|*.*";
            this.saveFileDialog1.InitialDirectory = "Documents";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(986, 431);
            this.Controls.Add(this.toolBarMain);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.RightToLeftLayout = true;
            this.Text = "SoundWaveProject";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolBarMain.ResumeLayout(false);
            this.toolBarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSampleRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_SampleRate_11025;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_SampleRate_22050;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_SampleRate_44100;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_SampleRate_88200;
        private System.Windows.Forms.ToolStripMenuItem changeBitRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_8_Bits;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_16_Bits;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_24_Bits;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_32_Bits;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolBarMain;
        private System.Windows.Forms.ToolStripButton newButton;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton recordButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem maximizeAmplitudeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_channels_mono;
        private System.Windows.Forms.ToolStripMenuItem menu_Tool_channels_stereo;
        private System.Windows.Forms.ToolStripMenuItem pitchShiftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_p3;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_p2;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_p1;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_m1;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_m2;
        private System.Windows.Forms.ToolStripMenuItem menu_Edit_ps_m3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tutorialToolStripMenuItem;
    }
}