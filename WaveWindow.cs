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
    public delegate void ReportEventHandler(object sender, ReportEventArgs e);

    /// <summary>
    /// class WaveWindow is a container of both Time and Frequency Domain panels
    /// </summary>
    public partial class WaveWindow : Form
    {
        private WaveFile wave;
        private Player player2;
        private WINDOWING sampleWindowing = WINDOWING.pass;
        private PlaybackStatus statusPlayback = PlaybackStatus.Stopped;
        private int fourierN = 882;
        private bool invalidPlayer = true;
        private int timeSelStart;
        private int timeSelEnd;
        private int freqSelStart = 0;
        private int freqSelEnd = 0;

        private MainWindow parent = null;

        /// <summary>
        /// Constructor for class WaveWindow
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="wave"></param>
        public WaveWindow(MainWindow parent, WaveFile wave)
        {
            this.wave = wave;
            this.parent = parent;
            InitializeComponent();
            frequencyDomain.setBottomMargin(freqStatusBar.Height);
            this.Text = wave.getName();

            updateFreqStatusBar();
            calculateDFT();
            timeDomain.setSamples(wave.samples);
            frequencyDomain.SampleRate = wave.sampleRate;
        }

        /// <summary>
        /// Method for right-click menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Play/Pause"));
                m.MenuItems.Add(new MenuItem("Filter"));
            }
        }

        /// <summary>
        /// method to handle shortcut keys in WaveWindow Form
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.C | Keys.Control:
                    copySelection();
                    return true;
                case Keys.V | Keys.Control:
                    pasteAtSelection();
                    return true;
                case Keys.X | Keys.Control:
                    cutSelection();
                    return true;
                case Keys.Space:
                    wavePlayPause();
                    return true;
                case Keys.Delete:
                    filterSelectedFrequencies();
                    return true;
            }
            if (timeDomain.HotKeys(ref message, keys))
            {
                return true;
            }

            return base.ProcessCmdKey(ref message, keys);
        }

        /// <summary>
        /// Method to handle messages from playback.
        /// Updates play status
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinmmHook.WM_USER + 1)
            {
                PlaybackStatus status = (PlaybackStatus)(int)m.WParam;
                statusPlayback = status;
                parent.playbackUpdate(status);
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Method to get sampleRate
        /// </summary>
        public int SampleRate
        {
            get { return wave.sampleRate; }
        }

        /// <summary>
        /// Method to get bitDepth
        /// </summary>
        public int BitDepth
        {
            get { return wave.bitDepth; }
        }

        /// <summary>
        /// Method to get the number of channels
        /// </summary>
        public int Channels
        {
            get { return wave.channels; }
        }

        /// <summary>
        /// Method to get playback status
        /// </summary>
        public PlaybackStatus State
        {
            get { return statusPlayback; }
        }

        /// <summary>
        /// Mutator method to set path
        /// </summary>
        /// <param name="path"></param>
        public void setPath(String path)
        {
            wave.setPath(path);
            this.Text = wave.getName();
        }

        /// <summary>
        /// Method to change the sample rate of active waveWindow
        /// </summary>
        /// <param name="newRate"></param>
        public void changeSampleRate(int newRate)
        {
            if (newRate == wave.sampleRate)
            {
                return;
            }

            for (int channel = 0; channel < wave.channels; channel++)
            {
                wave.samples[channel] = Formulas.resample(ref wave.samples[channel], wave.sampleRate, newRate);
            }
            wave.sampleRate = newRate;
            updateFreqStatusBar();
            calculateDFT();
            timeDomain.setSamples(wave.samples);
            timeDomain.Invalidate();
        }

        /// <summary>
        /// Method to change bitdepth of wave
        /// Updates statusbar
        /// </summary>
        /// <param name="newBitDepth"></param>
        public void changeBitRate(short newBitDepth)
        {
            wave.bitDepth = newBitDepth;
            updateFreqStatusBar();
        }

        /// <summary>
        /// method to change number of channels
        /// Updates statusbar.
        /// redraws window.
        /// </summary>
        /// <param name="nChannels"></param>
        public void changeChannels(short nChannels)
        {
            wave.samples = Formulas.matchChannels(wave.samples, nChannels);
            wave.channels = nChannels;
            updateFreqStatusBar();
            timeDomain.setSamples(wave.samples);
            timeDomain.Invalidate();
        }

        /// <summary>
        /// Method for handling play and pause function
        /// </summary>
        public void wavePlayPause()
        {
            if (player2 == null)
            {
                player2 = new Player(this);
            }

            if (invalidPlayer)
            {
                if (timeSelStart == timeSelEnd)
                {
                    player2.setWave(wave.copySelection(timeSelStart, wave.getNumSamples()));
                }
                else
                {
                    player2.setWave(wave.copySelection(timeSelStart, timeSelEnd));
                }
                invalidPlayer = false;
            }

            if (player2.Playing)
            {
                player2.PlaybackPause();
            }
            else
            {
                player2.PlaybackStart();
            }
        }

        /// <summary>
        /// method to stop player
        /// </summary>
        public void waveStop()
        {
            if (player2 != null)
            {
                player2.PlaybackStop();
            }
        }

        /// <summary>
        /// Method to save wave
        /// </summary>
        public void save()
        {
            wave.save();
        }

        /// <summary>
        /// Method to copy user's selection
        /// </summary>
        public void copySelection()
        {
            WaveFile copy = wave.copySelection(timeSelStart, timeSelEnd);
            if (copy != null)
            {
                Clipboard.SetData("WaveFile", copy);
                updateReport((timeSelEnd - timeSelStart) + " samples copied to clipboard!");
            }
            else
            {
                MessageBox.Show("No samples selected to copy!");
            }
        }

        /// <summary>
        /// Method to cut user's selection
        /// </summary>
        public void cutSelection()
        {
            int before = wave.getNumSamples();
            WaveFile cut = wave.cutSelection(timeSelStart, timeSelEnd);
            if (cut != null)
            {
                Clipboard.SetData("WaveFile", cut);
                updateReport((timeSelEnd - timeSelStart) + " samples cut to clipboard!");
            }
            else
            {
                MessageBox.Show("No samples selected to cut!");
            }
            int after = wave.getNumSamples();
            timeDomain.SelectionEnd = timeSelStart;
            timeDomain.setSamples(wave.samples);
            waveStop();
            invalidPlayer = true;
        }

        /// <summary>
        /// Method to paste user's selection to desired position
        /// </summary>
        public void pasteAtSelection()
        {
            if (!Clipboard.ContainsData("WaveFile"))
            {
                MessageBox.Show("No samples in clipboard.", "Error");
                return;
            }
            WaveFile data = (WaveFile)Clipboard.GetData("WaveFile");
            if (data == null)
            {
                MessageBox.Show("Error reading clipboard.", "Error");
                return;
            }
            //match number of channels
            data.samples = Formulas.matchChannels(data.samples, wave.channels);
            data.channels = wave.channels;

            //match sampling rate
            for (int channel = 0; channel < data.channels; channel++)
            {
                data.samples[channel] = Formulas.resample(ref data.samples[channel], data.sampleRate, wave.sampleRate);
            }
            data.sampleRate = wave.sampleRate;

            wave.cutSelection(timeSelStart, timeSelEnd);
            wave.pasteSelection(timeSelStart, data);
            updateReport(data.getNumSamples() + " samples pasted");
            timeDomain.SelectionEnd = timeSelStart + data.getNumSamples();
            timeDomain.setSamples(wave.samples);
            waveStop();
            invalidPlayer = true;
        }

        /// <summary>
        /// Method to setFocus on current selected window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveWindowSetFocus(object sender, EventArgs e)
        {
            this.parent.setActiveWindow(this);
        }

        /// <summary>
        /// Method to close current wave window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveWindowClose(object sender, FormClosedEventArgs e)
        {
            if (player2 != null)
            {
                player2.Dispose();
            }
            parent.waveWindowInactive(this);
        }

        /// <summary>
        /// Menu Item tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem source = (ToolStripMenuItem)sender;
            sampleWindowing = (WINDOWING)source.Tag;
            calculateDFT();
        }

        /// <summary>
        /// menu item filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem source = (ToolStripMenuItem)sender;
            filterSelectedFrequencies((FILTERING)source.Tag);
        }

        /// <summary>
        /// Method to calculate DFT on wave window
        /// </summary>
        private void calculateDFT()
        {
            Complex[][] DFT = new Complex[wave.channels][];
            double[] samples = new double[fourierN];

            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = 0;
            }
            int startIndex = Math.Max(timeSelStart, 0);

            int N = Math.Min(wave.getNumSamples() - startIndex, fourierN);
            Array.Copy(wave.samples[0], startIndex, samples, 0, N);

            if (sampleWindowing == WINDOWING.triangle)
            {
                Formulas.WindowTriangle(ref samples);
            }
            else if (sampleWindowing == WINDOWING.cosine)
            {
                Formulas.WindowCosine(ref samples);
            }
            else if (sampleWindowing == WINDOWING.blackman)
            {
                Formulas.WindowBlackman(ref samples);
            }
            DFT[0] = Formulas.DFT(ref samples);
            frequencyDomain.SampleRate = wave.sampleRate;
            frequencyDomain.Fourier = DFT;
            frequencyDomain.Invalidate();
        }

        /// <summary>
        /// Frequency Domain status bar information
        /// </summary>
        private void updateFreqStatusBar()
        {
            statusSampling.Text = String.Format("Sample Rate: {0} Hz", wave.sampleRate);
            statusBits.Text = String.Format("Bit Depth: {0}-bit", wave.bitDepth);
            statusLength.Text = String.Format("Wave Length: {0:0.000}s ({1} samples)", (double)wave.getNumSamples() / wave.sampleRate, wave.getNumSamples());
            statusSelection.Text = String.Format("Selected: {0:0.000} seconds ({1}..{2})", (timeSelEnd - timeSelStart + 1) / (double)wave.sampleRate, timeSelStart, timeSelEnd);
        }

        /// <summary>
        /// Method to update status
        /// </summary>
        /// <param name="msg"></param>
        public void updateReport(String msg)
        {
            statusReport.Text = msg;
        }

        /// <summary>
        /// Method to handle user's new selection on Time Domain panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateSelection(Object sender, EventArgs e)
        {
            timeSelStart = timeDomain.SelectionStart;
            timeSelEnd = timeDomain.SelectionEnd;
            calculateDFT();
            frequencyDomain.Invalidate();
            updateFreqStatusBar();
            waveStop();
            invalidPlayer = true;
        }

        /// <summary>
        /// Method to handle user's new selection on Frequency Domain panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateFreqSelection(Object sender, EventArgs e)
        {
            freqSelStart = frequencyDomain.SelectionStart;
            freqSelEnd = frequencyDomain.SelectionEnd;
        }

        /// <summary>
        /// Method to handle user's selection in frequency domain
        /// passes selected samples to concolution filter.
        /// </summary>
        /// <param name="method"></param>
        public void filterSelectedFrequencies(FILTERING method = FILTERING.convolution)
        {
            if (freqSelStart == freqSelEnd)
                return;

            double[] filter = new double[fourierN];
            for (int fbin = 0; fbin < filter.Length; fbin++)
            {
                if ((fbin >= freqSelStart && fbin <= freqSelEnd) || (fbin >= fourierN - freqSelEnd && fbin <= fourierN - freqSelStart))
                {
                    filter[fbin] = 0;
                }
                else
                {
                    filter[fbin] = 1;
                }
            }

            double criticalPoint = 0;
            if (method == FILTERING.IIRLowpass)
                criticalPoint = freqSelStart * SampleRate / fourierN;
            else
                criticalPoint = freqSelEnd * SampleRate / fourierN;

            for (int channel = 0; channel < wave.channels; channel++)
            {
                switch (method)
                {
                    case FILTERING.convolution:
                        wave.samples[channel] = Formulas.convolveFilter(ref wave.samples[channel], filter);
                        break;
                    case FILTERING.DFT:
                        wave.samples[channel] = Formulas.dftFilter(wave.samples[channel], filter);
                        break;
                    case FILTERING.IIRLowpass: //low pass
                        wave.samples[channel] = Formulas.IIRFilter(wave.samples[channel], Math.Min(criticalPoint + 5000, SampleRate / 2), Math.Max(0, criticalPoint - 5000), 0, SampleRate);
                        break;
                    case FILTERING.IIRHighpass: //high pass
                        wave.samples[channel] = Formulas.IIRFilter(wave.samples[channel], Math.Max(0, criticalPoint - 7000), Math.Min(criticalPoint + 3000, SampleRate / 2), 1, SampleRate);
                        break;
                    default:
                        MessageBox.Show("Error occured");
                        break;
                }
            }
            timeDomain.setSamples(wave.samples);
            timeDomain.Invalidate();
            calculateDFT();
            invalidPlayer = true;
        }

        /// <summary>
        /// Method to apply FX effects
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="args"></param>
        public void applyDSPFX(EDIT_FX effect, object[] args)
        {
            int startIndex = Math.Max(timeSelStart, 0);
            int endIndex;
            if (timeSelEnd != timeSelStart)
            {
                endIndex = timeSelEnd;
            }
            else
            {
                endIndex = wave.getNumSamples();
            }

            WaveFile data = wave.cutSelection(startIndex, endIndex);
            Formulas.ApplyFX(effect, args, ref data);
            wave.pasteSelection(startIndex, data.samples);
            timeDomain.setSamples(wave.samples);
            timeDomain.Invalidate();
            invalidPlayer = true;
        }

        /// <summary>
        /// MEthod to handle event reports
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WaveForm_Report(Object sender, ReportEventArgs e)
        {
            updateReport(e.report);
        }
    }

    /// <summary>
    /// Method to pass report messaage
    /// </summary>
    public class ReportEventArgs : EventArgs
    {
        public String report;
        public ReportEventArgs(String message) : base()
        {
            report = message;
        }
    }
}
