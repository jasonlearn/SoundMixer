using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// class to display form controls for manipulating wave data of the project
    /// </summary>
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();
            this.Enabled = false;
            tutorialTextBox.SelectionFont = new Font("Times New Roman", 12, FontStyle.Bold);
            tutorialTextBox.SelectedText = ("Sound Wave Project Controls:\n");
            tutorialTextBox.SelectedText = ("Control                    Key:\n");
            tutorialTextBox.SelectionFont = new Font("Times New Roman", 9, FontStyle.Underline);
            tutorialTextBox.SelectedText = ("Time Domain:\n");
            tutorialTextBox.SelectedText = ("Scroll                      Mouse wheel\n");
            tutorialTextBox.SelectedText = ("Play/Pause            Space bar\n");
            tutorialTextBox.SelectedText = ("Cut                          Ctrl + x\n");
            tutorialTextBox.SelectedText = ("Copy                       Ctrl + c\n");
            tutorialTextBox.SelectedText = ("Paste                      Ctrl + v\n");
            tutorialTextBox.SelectedText = ("Filter                        Delete\n");

            tutorialTextBox.SelectionFont = new Font("Times New Roman", 9, FontStyle.Underline);
            tutorialTextBox.SelectedText = ("\nHow to Use this program\n");

            tutorialTextBox.SelectedText = ("Step 1: Open a Wave File, under \"File\" menu strip. OR you may choose to record a sample.\n");
            tutorialTextBox.SelectedText = ("Step 2: The program then visually plots the sound as a wave.\n");
            tutorialTextBox.SelectedText = ("Step 3: You may then copy/cut/paste portion of the wave to different sections of the same wave or a different wave file.\n");
            tutorialTextBox.SelectedText = ("Step 4: There are multiples of wave mix functionalities in \"Tool\" and \"Edit\".\n");
            tutorialTextBox.SelectedText = ("Step 5: All mixed and recorded waves could be saved to your local device.\n");
        }
    }
}
