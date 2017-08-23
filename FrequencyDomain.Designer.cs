using System;

namespace JasonChan_SoundWaveProject
{
    partial class FrequencyDomain
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDFTFreq = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDFTFreq
            // 
            this.lblDFTFreq.AutoSize = true;
            this.lblDFTFreq.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDFTFreq.Location = new System.Drawing.Point(3, 3);
            this.lblDFTFreq.Name = "lblDFTFreq";
            this.lblDFTFreq.Size = new System.Drawing.Size(100, 23);
            this.lblDFTFreq.TabIndex = 0;
            this.lblDFTFreq.Text = "Frequency: 0Hz";
            // 
            // FrequencyDomainPanel
            // 
            this.Controls.Add(this.lblDFTFreq);
            this.ResumeLayout(false);

        }

        internal void setBottomMargin(int margin)
        {
            drawBottomMargin = margin;
            drawHeight = Height - drawBottomMargin;
        }

        #endregion

        private System.Windows.Forms.Label lblDFTFreq;
    }
}
