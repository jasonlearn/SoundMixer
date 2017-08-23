using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasonChan_SoundWaveProject
{

    public delegate void FreqSelChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// class to represent samples in the the frequency domain.
    /// Frequency domain represents wave data by using frequency bins.
    /// </summary>
    public partial class FrequencyDomain : System.Windows.Forms.Panel
    {
        private Complex[][] DFT;
        private int drawHeight;
        private int drawBottomMargin;
        private int fourierN;
        private int sampleRate;

        //mouse controls for frequency domain
        private int fourierMouseDown = 0;
        private int fourierMouseDrag = 0;
        private int fourierSelectStart = 0;
        private int fourierSelectEnd = 0;

        public event FreqSelChangedEventHandler SelChanged;
        public event ReportEventHandler Report;

        /// <summary>
        /// Initiates Frequency Domain Window
        /// </summary>
        public FrequencyDomain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            fourierN = 8;
            DFT = new Complex[1][];
            DFT[0] = new Complex[fourierN];
            for (int i = 0; i < fourierN; i++)
            {
                DFT[0][i] = new Complex(0, 0);
            }
        }

        /// <summary>
        /// Initiates a container for fourier domain panel
        /// </summary>
        /// <param name="container"></param>
        public FrequencyDomain(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            DoubleBuffered = true;
            fourierN = 8;
            DFT = new Complex[1][];
            DFT[0] = new Complex[fourierN];
            for (int i = 0; i < fourierN; i++)
            {
                DFT[0][i] = new Complex(0, 0);
            }
            sampleRate = 44100;
        }

        /// <summary>
        /// Method to handle changes of selection
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelChanged(EventArgs e)
        {
            if (SelChanged != null)
                SelChanged(this, e);
        }

        /// <summary>
        /// Method for Report
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReport(ReportEventArgs e)
        {
            if (Report != null)
                Report(this, e);
        }

        /// <summary>
        /// Method to invalidate the the window when resized
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            drawHeight = Height - drawBottomMargin;
            Invalidate();
        }

        /// <summary>
        /// Method to set fourier variables
        /// </summary>
        public Complex[][] Fourier
        {
            set
            {
                DFT = value;
                fourierN = value[0].Length;
            }
        }

        /// <summary>
        /// Method to set sampleRate
        /// </summary>
        public int SampleRate
        {
            set { sampleRate = value; }
        }

        /// <summary>
        /// Method to indicate start of user selection
        /// </summary>
        public int SelectionStart
        {
            get { return fourierSelectStart; }
            set { updateSelection(value, fourierSelectEnd); }
        }

        /// <summary>
        /// Method to indicate end of user slection
        /// </summary>
        public int SelectionEnd
        {
            get { return fourierSelectEnd; }
            set { updateSelection(fourierSelectStart, value); }
        }

        /// <summary>
        /// Paint method for frequency domain
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            selectedFrquencies(g);
            drawAliasAxis(g);
            drawFourier(g);
        }

        /// <summary>
        /// Method to draw frequency data in the frequency domain
        /// </summary>
        /// <param name="g"></param>
        private void drawFourier(Graphics g)
        {
            int start, end;
            float value;
            if (fourierN < Width)
            {
                for (int i = 0; i < fourierN; i++)
                {
                    start = (int)(i / (double)fourierN * Width);
                    end = (int)((i + 1) / (double)fourierN * Width);
                    value = (float)(DFT[0][i].magnitude() / (fourierN / 2) * 2) * drawHeight;
                    g.FillRectangle(Brushes.Green, start, drawHeight - value, end - start, value);
                }
            }
            else
            {
                for (int i = 0; i < Width; i++)
                {
                    start = (int)((float)i / (float)Width * (fourierN - 1));
                    end = (int)((float)(i + 1) / (float)Width * (fourierN - 1));
                    value = 0;
                    for (int j = start; j < end; j++)
                    {
                        value = Math.Max(value, (float)(DFT[0][j].magnitude() / (fourierN / 2) * 2));
                    }
                    value *= drawHeight;
                    g.DrawLine(Pens.Green, i, drawHeight, i, drawHeight - Math.Abs(value));
                }
            }
        }

        /// <summary>
        /// Method to highlight selected frequencies
        /// </summary>
        /// <param name="g"></param>
        private void selectedFrquencies(Graphics g)
        {
            if (fourierSelectStart == fourierSelectEnd)
            {
                return;
            }
            RectangleF r = new RectangleF();
            r.Y = 0;
            r.Height = drawHeight;
            r.X = Math.Max(1.0f / fourierN * Width, (float)(fourierSelectStart) / fourierN * Width);
            r.Width = Math.Max(2, (float)(fourierSelectEnd + 1) / fourierN * Width - r.X);
            g.FillRectangle(Brushes.LightBlue, r);
            r.X = Width - (Math.Max(0, (float)(fourierSelectStart - 1) / fourierN * Width)) - r.Width;
            g.FillRectangle(Brushes.LightBlue, r);
        }

        /// <summary>
        /// Method to draw a line divider to identify alias frequencies
        /// </summary>
        /// <param name="g"></param>
        private void drawAliasAxis(Graphics g)
        {
            Pen divider = new Pen(Color.Red, Math.Max(2, g.VisibleClipBounds.Width / fourierN / 6));
            float mid = g.VisibleClipBounds.Width / 2.0f + (g.VisibleClipBounds.Width / fourierN) / 2.0f;
            g.DrawLine(divider, mid, 0.0f, mid, drawHeight);
        }

        /// <summary>
        /// Method to indicate user selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            float position = (float)e.X / Width;
            int index = (int)(position * DFT[0].Length);
            updateSelection(index, index);
            fourierMouseDown = index;
            fourierMouseDrag = index;
        }

        /// <summary>
        /// Method to update user selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            float position = (float)e.X / Width;
            int index = (int)(position * DFT[0].Length);
            fourierMouseDrag = index;
            if (fourierMouseDrag == fourierMouseDown)
            {
                updateSelection(0, 0);
            }
            else
            {
                updateSelection(Math.Min(fourierMouseDown, fourierMouseDrag), Math.Max(fourierMouseDown, fourierMouseDrag));
            }
        }

        /// <summary>
        /// Method for displaying the frequency bin the mouse is hovered on
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            float position = (float)e.X / Width;
            int index = (int)(position * DFT[0].Length);
            int loval = (int)((float)index / DFT[0].Length * sampleRate);
            String info;

            if (index >= 0 && index <= fourierN - 1)
            {
                if (loval >= sampleRate / 2)
                {
                    lblDFTFreq.Text = String.Format("Frequency: {0}Hz Alias", loval);
                }
                else
                {
                    lblDFTFreq.Text = String.Format("Frequency: {0}Hz", loval);
                }
                double amplitude = DFT[0][index].magnitude() / (fourierN / 2);
                double phase = 0;
                if (amplitude > 0.00001)
                    phase = DFT[0][index].angle() / Math.PI;
                if (e.Button != MouseButtons.Left)
                {
                    info = String.Format("Frequency: {0}Hz; Amplitude: {1:0.00}; Phase: {2:0.000}pi*rad", loval, amplitude, phase);
                    OnReport(new ReportEventArgs(info));
                    return;
                }
            }

            fourierMouseDrag = index;
            updateSelection(Math.Min(fourierMouseDown, fourierMouseDrag), Math.Max(fourierMouseDown, fourierMouseDrag));

            loval = (int)((float)fourierSelectStart / DFT[0].Length * sampleRate);
            int hival = (int)((float)(fourierSelectEnd + 1) / DFT[0].Length * sampleRate);
            info = String.Format("Selected: {0}Hz to {1}Hz", loval, hival);
            OnReport(new ReportEventArgs(info));
        }

        /// <summary>
        /// Method to update user selection.
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        private void updateSelection(int low, int high)
        {
            fourierSelectStart = Math.Max(0, low);
            fourierSelectEnd = Math.Min(fourierN - 1, high);
            Invalidate();
            OnSelChanged(EventArgs.Empty);
        }

    }
}
