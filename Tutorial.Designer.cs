namespace JasonChan_SoundWaveProject
{
    partial class Tutorial
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
            this.tutorialTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tutorialTextBox
            // 
            this.tutorialTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tutorialTextBox.Enabled = false;
            this.tutorialTextBox.Location = new System.Drawing.Point(0, 0);
            this.tutorialTextBox.Name = "tutorialTextBox";
            this.tutorialTextBox.Size = new System.Drawing.Size(336, 359);
            this.tutorialTextBox.TabIndex = 2;
            this.tutorialTextBox.Text = "";
            // 
            // Tutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 359);
            this.Controls.Add(this.tutorialTextBox);
            this.Name = "Tutorial";
            this.Text = "Tutorial";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tutorialTextBox;
    }
}