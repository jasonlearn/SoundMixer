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
    /// class MainWindow, main container for the entire program.
    /// Used to initialize Frequency and Time doamin panels.
    /// Contains a menustrip of controls to manipulate data.
    /// </summary>
    public partial class MainWindow : Form
    {
        Recorder record;
        List<WaveWindow> children = new List<WaveWindow>();
        WaveWindow activeWaveWindow = null;
        ToolStripMenuItem[] activeWindowSelection;

        /// <summary>
        /// Method to initialize MainWindow of the program.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        /// <summary>
        /// method new button, used to create a child Wave Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, EventArgs e)
        {
            createChildWindow();
        }

        /// <summary>
        /// Button save button, used to save the data as a wav file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (activeWaveWindow != null)
                {
                    activeWaveWindow.setPath(saveFileDialog1.FileName);
                    activeWaveWindow.save();
                }
                else
                {
                    MessageBox.Show("Unable to save.", "Error");
                }
            }
        }

        /// <summary>
        /// Button to open an existing .wav file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                createChildWindow(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Method to close current focused window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeWaveWindow.Close();
        }

        /// <summary>
        /// Button to record a wave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordButton_Click(object sender, EventArgs e)
        {
            if (record == null)
            {
                record = new Recorder(this);
                record.RecordingStart();
            }
            else
            {
                record.RecordingStop();
                WaveFile result = record.getSamples();
                if (result != null)
                {
                    createChildWindow(result);
                }
                record.Dispose();
                record = null;
            }
        }

        /// <summary>
        /// Menu Item to stop the wave from playing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            foreach (WaveWindow wave in children)
            {
                wave.waveStop();
            }
        }

        /// <summary>
        /// Menu Item to play the current focused wave.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            if (activeWaveWindow == null)
            {
                return;
            }

            activeWaveWindow.wavePlayPause();
            playButton.Invalidate();
        }

        /// <summary>
        /// Menu item to exit program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// MenuItem 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeWaveWindow == null)
            {
                return;
            }
            activeWaveWindow.applyDSPFX(EDIT_FX.reverse, null);
        }

        /// <summary>
        /// Menu Strip item for data manipulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Tool_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem[] sampleRates = { menu_Tool_SampleRate_11025, menu_Tool_SampleRate_22050, menu_Tool_SampleRate_44100, menu_Tool_SampleRate_88200 };
            ToolStripMenuItem[] bitRates = { menu_Tool_8_Bits, menu_Tool_16_Bits, menu_Tool_24_Bits, menu_Tool_32_Bits };
            ToolStripMenuItem[] channels = { menu_Tool_channels_mono, menu_Tool_channels_stereo };
            ToolStripMenuItem source = (ToolStripMenuItem)sender;

            if (sampleRates.Contains(source))
            {
                int targetRate = (int)(source.Tag);
                if (targetRate > activeWaveWindow.SampleRate)
                {
                    DialogResult result = MessageBox.Show(String.Format("Please confirm upsampling of {0} from {1}Hz to {2}Hz", activeWaveWindow.Text, activeWaveWindow.SampleRate, targetRate), "Up sampling confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        activeWaveWindow.changeSampleRate(targetRate);
                    }
                }
                else if (targetRate < activeWaveWindow.SampleRate)
                {
                    DialogResult result = MessageBox.Show(String.Format("Please confirm downsampling of {0} from {1}Hz to {2}Hz", activeWaveWindow.Text, activeWaveWindow.SampleRate, targetRate), "Downsampling confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        activeWaveWindow.changeSampleRate(targetRate);
                    }
                }
                else
                {
                    MessageBox.Show("Sample rate is already at" + targetRate + "Hz!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }

            if (bitRates.Contains(source))
            {
                short targetRate = (short)(int)(source.Tag);
                activeWaveWindow.changeBitRate(targetRate);
            }

            if (channels.Contains(source))
            {
                short nChannels = (short)(int)(source.Tag);
                if (nChannels == activeWaveWindow.Channels)
                {
                    return;
                }
                if (activeWaveWindow.Channels != 1)
                {
                    DialogResult result = MessageBox.Show(String.Format("Please confirm you want to switch {0} to {1} channel", activeWaveWindow.Text, nChannels), "Channel change confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        activeWaveWindow.changeChannels(nChannels);
                    }
                }
                else
                {
                    activeWaveWindow.changeChannels(nChannels);
                }
            }
            updateToolMenu();
        }

        /// <summary>
        /// Menu Item to maximize amplitude in time domain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maximizeAmplitudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeWaveWindow == null)
            {
                return;
            }
            activeWaveWindow.applyDSPFX(EDIT_FX.normalize, null);
        }

        /// <summary>
        /// Menu item to shift the pitch of the wave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchShift(object sender, EventArgs e)
        {
            ToolStripMenuItem source = (ToolStripMenuItem)sender;
            int shift = (int)source.Tag;
            object[] args = { shift };
            if (activeWaveWindow != null)
            {
                activeWaveWindow.applyDSPFX(EDIT_FX.pitchshift, args);
            }
        }

        /// <summary>
        /// Method to set a focused child (waveWindow) as the focused form for form control
        /// </summary>
        /// <param name="child"></param>
        public void setActiveWindow(WaveWindow child)
        {
            activeWaveWindow = child;
            updateWindowMenu();
            updateToolMenu();
            if (activeWaveWindow == null)
            {
                playbackUpdate(PlaybackStatus.Disabled);
            }
            else
            {
                playbackUpdate(activeWaveWindow.State);
            }
        }

        /// <summary>
        /// Method to select a child window to focus on uing a menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void focusWindow(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            children[(int)menuItem.Tag].Focus();
        }

        /// <summary>
        /// Method to handle close child wave window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            //to ensure child windows are disposed properly
            while (children.Count > 0)
            {
                children[0].Close();
            }
        }

        /// <summary>
        /// WndProc to wav playback
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinmmHook.WM_USER + 1)
            {
                PlaybackStatus status = (PlaybackStatus)(int)m.WParam;
                playbackUpdate(status);
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Method to set waveWindow inactive
        /// </summary>
        /// <param name="waveWindow"></param>
        public void waveWindowInactive(WaveWindow waveWindow)
        {
            children.Remove(waveWindow);
            updateWindowMenu();
            if (children.Count == 1)
            {
                setActiveWindow(children[0]);
            }
            else
            {
                setActiveWindow(null);
            }
        }

        /// <summary>
        /// Case switch for updating Sound control
        /// Endables and Disables buttons according to different message procs
        /// </summary>
        /// <param name="update"></param>
        public void playbackUpdate(PlaybackStatus update)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            switch (update)
            {
                case PlaybackStatus.Playing:
                    playButton.Enabled = true;
                    playButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnPause;
                    stopButton.Enabled = true;
                    stopButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnStop;
                    recordButton.Enabled = false;
                    recordButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnRecordDisabled;
                    newButton.Enabled = false;
                    newButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnNewDisabled;
                    openButton.Enabled = false;
                    openButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnOpenDisabled;
                    saveButton.Enabled = false;
                    saveButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnSaveDisabled;
                    break;
                case PlaybackStatus.Paused:
                    playButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnPlay;
                    break;
                case PlaybackStatus.Recording:
                    playButton.Enabled = false;
                    playButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnPlayDisabled;
                    stopButton.Enabled = false;
                    stopButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnStopDisabled;
                    recordButton.Enabled = true;
                    recordButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnRecording;
                    newButton.Enabled = false;
                    newButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnNewDisabled;
                    openButton.Enabled = false;
                    openButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnOpenDisabled;
                    saveButton.Enabled = false;
                    saveButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnSaveDisabled;
                    break;
                case PlaybackStatus.Stopped:
                    playButton.Enabled = true;
                    playButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnPlay;
                    stopButton.Enabled = true;
                    stopButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnStop;
                    recordButton.Enabled = true;
                    recordButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnRecord;
                    newButton.Enabled = true;
                    newButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnNew;
                    openButton.Enabled = true;
                    openButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnOpen;
                    saveButton.Enabled = true;
                    saveButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnSave;
                    break;
                case PlaybackStatus.Disabled:
                    playButton.Enabled = false;
                    playButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnPlayDisabled;
                    stopButton.Enabled = true;
                    stopButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnStop;
                    recordButton.Enabled = true;
                    recordButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnRecord;
                    newButton.Enabled = true;
                    newButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnNew;
                    openButton.Enabled = true;
                    openButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnOpen;
                    saveButton.Enabled = true;
                    saveButton.Image = JasonChan_SoundWaveProject.Properties.Resources.btnSave;
                    break;
            }
        }

        /// <summary>
        /// Method to update the SelectWindow menu.
        /// </summary>
        private void updateWindowMenu()
        {
            windowToolStripMenuItem.DropDownItems.Clear();
            activeWindowSelection = new ToolStripMenuItem[children.Count];
            for (int i = 0; i < children.Count; i++)
            {
                activeWindowSelection[i] = new ToolStripMenuItem(children[i].Text, null, new System.EventHandler(this.focusWindow));
                activeWindowSelection[i].Tag = i;
                if (children[i] == activeWaveWindow)
                {
                    activeWindowSelection[i].Checked = true;
                }
                windowToolStripMenuItem.DropDownItems.Add(activeWindowSelection[i]);
            }
        }

        /// <summary>
        /// Method to update tool menuitem
        /// </summary>
        private void updateToolMenu()
        {
            ToolStripMenuItem[] bitRates = { menu_Tool_8_Bits, menu_Tool_16_Bits, menu_Tool_24_Bits, menu_Tool_32_Bits };
            ToolStripMenuItem[] sampleRates = { menu_Tool_SampleRate_11025, menu_Tool_SampleRate_22050, menu_Tool_SampleRate_44100, menu_Tool_SampleRate_88200 };
            ToolStripMenuItem[] channels = { menu_Tool_channels_mono, menu_Tool_channels_stereo };
            foreach (ToolStripMenuItem item in sampleRates)
            {
                if (activeWaveWindow != null && activeWaveWindow.SampleRate == (int)item.Tag)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
            foreach (ToolStripMenuItem item in bitRates)
            {
                if (activeWaveWindow != null && activeWaveWindow.BitDepth == (int)item.Tag)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
            foreach (ToolStripMenuItem item in channels)
            {
                if (activeWaveWindow != null && activeWaveWindow.Channels == (int)item.Tag)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        /// <summary>
        /// Method to create a child waveWindow
        /// </summary>
        /// <param name="path"></param>
        private void createChildWindow(string path = null)
        {
            WaveFile wave;
            WaveWindow child;
            if (path == null)
            {
                wave = new WaveFile();
                child = new WaveWindow(this, wave);
                child.updateReport("New waves generated.");
            }
            else
            {
                try
                {
                    wave = new WaveFile(path);
                    child = new WaveWindow(this, wave);
                    child.updateReport("Current wave file: " + wave.getName());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to open: " + e.Message);
                    return;
                }
            }

            child.MdiParent = this;
            children.Add(child);
            activeWaveWindow = child;
            child.Show();
            updateWindowMenu();
        }

        /// <summary>
        /// Method to create a child waveWindow
        /// </summary>
        /// <param name="wave"></param>
        private void createChildWindow(WaveFile wave)
        {
            WaveWindow child;
            child = new WaveWindow(this, wave);
            child.MdiParent = this;
            children.Add(child);
            activeWaveWindow = child;
            child.Show();
            updateWindowMenu();
        }

        /// <summary>
        /// Menu item for details of this project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jason Chan\nA00698160\nComp 3931 Project", "About");
        }

        /// <summary>
        /// Menu item for tutorial on contorls for this project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tutorial tutorial;
            tutorial = new Tutorial();

            tutorial.Show();
        }
    }
}