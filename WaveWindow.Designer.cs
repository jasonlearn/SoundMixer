namespace JasonChan_SoundWaveProject
{
    partial class WaveWindow
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.windowingModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passthroughToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convolutionFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dFTFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iIRLowpassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iIRHighpassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyDomain = new JasonChan_SoundWaveProject.FrequencyDomain(this.components);
            this.freqStatusBar = new System.Windows.Forms.StatusStrip();
            this.statusSampling = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBits = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusReport = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeDomain = new JasonChan_SoundWaveProject.TimeDomain();
            this.contextMenuStrip1.SuspendLayout();
            this.frequencyDomain.SuspendLayout();
            this.freqStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 160);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(826, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowingModeToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 48);
            // 
            // windowingModeToolStripMenuItem
            // 
            this.windowingModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passthroughToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.cosineToolStripMenuItem,
            this.blackmanToolStripMenuItem});
            this.windowingModeToolStripMenuItem.Name = "windowingModeToolStripMenuItem";
            this.windowingModeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.windowingModeToolStripMenuItem.Text = "Windowing";
            // 
            // passthroughToolStripMenuItem
            // 
            this.passthroughToolStripMenuItem.Name = "passthroughToolStripMenuItem";
            this.passthroughToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.passthroughToolStripMenuItem.Tag = JasonChan_SoundWaveProject.WINDOWING.pass;
            this.passthroughToolStripMenuItem.Text = "Passthrough";
            this.passthroughToolStripMenuItem.Click += new System.EventHandler(this.windowingToolStripMenuItem_Click);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.triangleToolStripMenuItem.Tag = JasonChan_SoundWaveProject.WINDOWING.triangle;
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.windowingToolStripMenuItem_Click);
            // 
            // cosineToolStripMenuItem
            // 
            this.cosineToolStripMenuItem.Name = "cosineToolStripMenuItem";
            this.cosineToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.cosineToolStripMenuItem.Tag = JasonChan_SoundWaveProject.WINDOWING.cosine;
            this.cosineToolStripMenuItem.Text = "Cosine";
            this.cosineToolStripMenuItem.Click += new System.EventHandler(this.windowingToolStripMenuItem_Click);
            // 
            // blackmanToolStripMenuItem
            // 
            this.blackmanToolStripMenuItem.Name = "blackmanToolStripMenuItem";
            this.blackmanToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.blackmanToolStripMenuItem.Tag = JasonChan_SoundWaveProject.WINDOWING.blackman;
            this.blackmanToolStripMenuItem.Text = "Blackman";
            this.blackmanToolStripMenuItem.Click += new System.EventHandler(this.windowingToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convolutionFilterToolStripMenuItem,
            this.dFTFilteringToolStripMenuItem,
            this.iIRLowpassToolStripMenuItem,
            this.iIRHighpassToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // convolutionFilterToolStripMenuItem
            // 
            this.convolutionFilterToolStripMenuItem.Name = "convolutionFilterToolStripMenuItem";
            this.convolutionFilterToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.convolutionFilterToolStripMenuItem.Tag = JasonChan_SoundWaveProject.FILTERING.convolution;
            this.convolutionFilterToolStripMenuItem.Text = "Convolution Filter";
            this.convolutionFilterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // dFTFilteringToolStripMenuItem
            // 
            this.dFTFilteringToolStripMenuItem.Name = "dFTFilteringToolStripMenuItem";
            this.dFTFilteringToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.dFTFilteringToolStripMenuItem.Tag = JasonChan_SoundWaveProject.FILTERING.DFT;
            this.dFTFilteringToolStripMenuItem.Text = "DFT Filter";
            this.dFTFilteringToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // iIRLowpassToolStripMenuItem
            // 
            this.iIRLowpassToolStripMenuItem.Name = "iIRLowpassToolStripMenuItem";
            this.iIRLowpassToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.iIRLowpassToolStripMenuItem.Tag = JasonChan_SoundWaveProject.FILTERING.IIRLowpass;
            this.iIRLowpassToolStripMenuItem.Text = "IIR Lowpass";
            this.iIRLowpassToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // iIRHighpassToolStripMenuItem
            // 
            this.iIRHighpassToolStripMenuItem.Name = "iIRHighpassToolStripMenuItem";
            this.iIRHighpassToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.iIRHighpassToolStripMenuItem.Tag = JasonChan_SoundWaveProject.FILTERING.IIRHighpass;
            this.iIRHighpassToolStripMenuItem.Text = "IIR Highpass";
            this.iIRHighpassToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // frequencyDomain
            // 
            this.frequencyDomain.AutoScroll = true;
            this.frequencyDomain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.frequencyDomain.ContextMenuStrip = this.contextMenuStrip1;
            this.frequencyDomain.Controls.Add(this.freqStatusBar);
            this.frequencyDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frequencyDomain.ForeColor = System.Drawing.Color.Lime;
            this.frequencyDomain.Location = new System.Drawing.Point(0, 163);
            this.frequencyDomain.Name = "frequencyDomain";
            this.frequencyDomain.SelectionEnd = 0;
            this.frequencyDomain.SelectionStart = 0;
            this.frequencyDomain.Size = new System.Drawing.Size(826, 132);
            this.frequencyDomain.TabIndex = 3;
            this.frequencyDomain.SelChanged += new JasonChan_SoundWaveProject.FreqSelChangedEventHandler(this.updateFreqSelection);
            // 
            // freqStatusBar
            // 
            this.freqStatusBar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.freqStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusSampling,
            this.statusBits,
            this.statusLength,
            this.statusSelection,
            this.statusReport});
            this.freqStatusBar.Location = new System.Drawing.Point(0, 108);
            this.freqStatusBar.Name = "freqStatusBar";
            this.freqStatusBar.Size = new System.Drawing.Size(826, 24);
            this.freqStatusBar.TabIndex = 0;
            this.freqStatusBar.Text = "freqStatusBar";
            // 
            // statusSampling
            // 
            this.statusSampling.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusSampling.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusSampling.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusSampling.ForeColor = System.Drawing.Color.Lime;
            this.statusSampling.Name = "statusSampling";
            this.statusSampling.Size = new System.Drawing.Size(96, 19);
            this.statusSampling.Text = "status_sampling";
            // 
            // statusBits
            // 
            this.statusBits.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusBits.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusBits.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusBits.ForeColor = System.Drawing.Color.Lime;
            this.statusBits.Name = "statusBits";
            this.statusBits.Size = new System.Drawing.Size(61, 19);
            this.statusBits.Text = "bit_depth";
            // 
            // statusLength
            // 
            this.statusLength.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusLength.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLength.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusLength.ForeColor = System.Drawing.Color.Lime;
            this.statusLength.Name = "statusLength";
            this.statusLength.Size = new System.Drawing.Size(77, 19);
            this.statusLength.Text = "wave_length";
            // 
            // statusSelection
            // 
            this.statusSelection.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusSelection.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusSelection.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusSelection.ForeColor = System.Drawing.Color.Lime;
            this.statusSelection.Name = "statusSelection";
            this.statusSelection.Size = new System.Drawing.Size(54, 19);
            this.statusSelection.Text = "selected";
            // 
            // statusReport
            // 
            this.statusReport.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusReport.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusReport.ForeColor = System.Drawing.Color.Lime;
            this.statusReport.Name = "statusReport";
            this.statusReport.Size = new System.Drawing.Size(67, 19);
            this.statusReport.Text = "text_report";
            // 
            // timeDomain
            // 
            this.timeDomain.AutoScroll = true;
            this.timeDomain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.timeDomain.Dock = System.Windows.Forms.DockStyle.Top;
            this.timeDomain.ForeColor = System.Drawing.Color.Lime;
            this.timeDomain.Location = new System.Drawing.Point(0, 0);
            this.timeDomain.Name = "timeDomain";
            this.timeDomain.SelectionEnd = 0;
            this.timeDomain.SelectionStart = 0;
            this.timeDomain.Size = new System.Drawing.Size(826, 160);
            this.timeDomain.TabIndex = 1;
            this.timeDomain.SelChanged += new JasonChan_SoundWaveProject.TimeSelChangedEventHandler(this.updateSelection);
            // 
            // WaveWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 295);
            this.Controls.Add(this.frequencyDomain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.timeDomain);
            this.Name = "WaveWindow";
            this.Text = "NewWave";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.waveWindowClose);
            this.GotFocus += new System.EventHandler(this.WaveWindowSetFocus);
            this.contextMenuStrip1.ResumeLayout(false);
            this.frequencyDomain.ResumeLayout(false);
            this.frequencyDomain.PerformLayout();
            this.freqStatusBar.ResumeLayout(false);
            this.freqStatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip freqStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusSampling;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowingModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passthroughToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cosineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackmanToolStripMenuItem;
        private JasonChan_SoundWaveProject.TimeDomain timeDomain;
        private JasonChan_SoundWaveProject.FrequencyDomain frequencyDomain;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convolutionFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dFTFilteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iIRLowpassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iIRHighpassToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusBits;
        private System.Windows.Forms.ToolStripStatusLabel statusLength;
        private System.Windows.Forms.ToolStripStatusLabel statusSelection;
        private System.Windows.Forms.ToolStripStatusLabel statusReport;
    }
}

