using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasonChan_SoundWaveProject
{

    public delegate void TimeSelChangedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// class Time domain, used to diplay sound using composite sine waves.
    /// x - axis is time, y - axis is amplitude.
    /// </summary>
    class TimeDomain : System.Windows.Forms.Panel
    {
        private float WavePanelHeight;
        private float WavePanelWidth;
        private double[][] samples;
        private int drawHeight;
        private int tViewStart;
        private int tViewRange;
        private int timeSelStart;
        private int timeSelEnd;
        private int tMouseDown;
        private int tMouseDrag;
        private Label lblTime;
        private HScrollBar scrollBar;
        public event TimeSelChangedEventHandler SelChanged;

        /// <summary>
        /// Initialize TimeDomain panel
        /// </summary>
        public TimeDomain() : base()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            samples = new double[1][];
            samples[0] = new double[128];
            updateView(0, 1000);
            updateSelection(0, 0);
            WavePanelHeight = Height;
            WavePanelWidth = Width;
            updateScrollRange();
        }

        /// <summary>
        /// Method for updating user selection
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelChanged(EventArgs e)
        {
            if (SelChanged != null)
                SelChanged(this, e);
        }

        /// <summary>
        /// Method to redraw (invalidate) the window when resized by user.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Graphics g = CreateGraphics();
            if (Height != WavePanelHeight)
                Invalidate();

            int newRange = (int)(tViewRange * (float)Width / (float)WavePanelWidth);
            updateView(tViewStart, newRange);
            WavePanelHeight = Height;
            WavePanelWidth = Width;
            updateScrollRange();
        }

        /// <summary>
        /// Method to store keyboard shorcut for user friendliness
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool HotKeys(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Home | Keys.Shift:
                    updateSelection(0, timeSelEnd);
                    return true;
                case Keys.Home:
                    updateSelection(0, 0);
                    return true;
                case Keys.End | Keys.Shift:
                    updateSelection(timeSelStart, samples[0].Length - 1);
                    return true;
                case Keys.End:
                    updateSelection(samples[0].Length - 1, samples[0].Length - 1);
                    return true;
                case Keys.F:
                    updateView(0, samples[0].Length);
                    Invalidate();
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method used to initialize the TimeDomain panel
        /// Draws .wav data as composite sinewaves
        /// </summary>
        private void InitializeComponent()
        {
            this.scrollBar = new System.Windows.Forms.HScrollBar();
            this.lblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDFTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(3, 3);
            this.lblTime.Name = "lblDFTTime";
            this.lblTime.Size = new System.Drawing.Size(100, 23);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time: 0sec";
            // 
            // scrollBar
            // 
            this.scrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollBar.Location = new System.Drawing.Point(0, 83);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(200, 17);
            this.scrollBar.TabIndex = 0;
            this.scrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollerBar_Scroll);
            // 
            // TimeDomain
            // 
            this.Controls.Add(this.scrollBar);
            this.Controls.Add(this.lblTime);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Method to set samples of wave data
        /// </summary>
        /// <param name="data"></param>
        public void setSamples(double[][] data)
        {
            samples = data;
            updateScrollRange();
        }

        /// <summary>
        /// Setter and getter for start of user selection
        /// </summary>
        public int SelectionStart
        {
            get { return timeSelStart; }
            set { updateSelection(value, timeSelEnd); }
        }
        /// <summary>
        /// Setter and getter for end of user selection
        /// </summary>
        public int SelectionEnd
        {
            get { return timeSelEnd; }
            set { updateSelection(timeSelStart, value); }
        }

        /// <summary>
        /// Method to paint the wave
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            scrollBar.Invalidate();
            scrollBar.Refresh();
            drawHeight = Height - scrollBar.ClientRectangle.Height;
            Graphics g = e.Graphics;
            drawSelection(g);
            drawGrid(g);
            drawWave(g);
        }

        /// <summary>
        /// Method to draw grid in Time Domain panel
        /// </summary>
        /// <param name="g"></param>
        private void drawGrid(Graphics g)
        {
            RectangleF r = g.VisibleClipBounds;
            //x-axis line
            int channels = samples.Length;
            for (int c = 0; c < channels; c++)
            {
                float row = drawHeight * (float)(c * 2 + 1) / (channels * 2);
                g.FillRectangle(Brushes.Red, 0, row - 1, r.Width, 2);
            }
        }

        /// <summary>
        /// Method to fill rectangle to indicate user selection on Time Domain panel
        /// </summary>
        /// <param name="g"></param>
        private void drawSelection(Graphics g)
        {

            RectangleF client = g.VisibleClipBounds;
            RectangleF r = new RectangleF();
            r.Y = 0;
            r.Height = client.Height;
            r.X = Math.Max(0, (float)(timeSelStart - tViewStart) / tViewRange * client.Width);
            r.Width = Math.Max(2, (float)(timeSelEnd - tViewStart) / tViewRange * client.Width - r.X);
            g.FillRectangle(Brushes.LightBlue, r);
        }

        /// <summary>
        /// Method to draw wave data as composite sine waves
        /// </summary>
        /// <param name="g"></param>
        private void drawWave(Graphics g)
        {
            RectangleF r = g.VisibleClipBounds;
            r.Height = drawHeight;
            int numPoints = Math.Min(tViewRange, (int)Math.Ceiling(r.Width));
            if (numPoints <= 0) return;
            float timeStep = (float)(tViewRange) / numPoints;
            float xStep = r.Width / numPoints;
            Pen p = new Pen(Color.LimeGreen, 2);
            int index;
            int channels = samples.Length;

            PointF[] pathPoints = new PointF[numPoints + 2];

            for (int c = 0; c < channels; c++)
            {
                float center = drawHeight * (float)(c * 2 + 1) / (channels * 2);
                float range = drawHeight / (float)channels;
                pathPoints[0] = new PointF(-1, center);
                pathPoints[numPoints + 1] = new PointF(r.Width, center);
                for (int i = 1; i <= numPoints; i++)
                {
                    index = (int)(Math.Round(i * timeStep + tViewStart));
                    if (index >= 0 && index < samples[c].Length)
                    {
                        pathPoints[i] = new PointF((float)(index - tViewStart) / tViewRange * r.Width, (-(float)(samples[c][index] * 0.5)) * range + center);
                    }
                    else
                    {
                        pathPoints[i] = new PointF(i * xStep, center);
                    }
                }
                g.DrawLines(p, pathPoints);
            }

            p.Dispose();
        }

        /// <summary>
        /// Method to update scrollbar range on time domain panel
        /// </summary>
        private void updateScrollRange()
        {
            int scrollRange = samples[0].Length - tViewRange;
            if (scrollRange < 0)
            {
                scrollBar.Maximum = 0;
                scrollBar.Value = 0;
                scrollBar.Enabled = false;
            }
            else
            {
                scrollBar.Maximum = samples[0].Length - tViewRange;
                scrollBar.Value = Math.Min(tViewStart, scrollBar.Maximum);
                scrollBar.Enabled = true;
            }
        }

        /// <summary>
        /// Method update view range of the time domain panel
        /// Redraws window upon update.
        /// </summary>
        /// <param name="scrollValue"></param>
        private void scrollWave(int scrollValue)
        {
            if (scrollBar.Value + scrollValue > scrollBar.Maximum)
            {
                scrollBar.Value = scrollBar.Maximum;
            }
            else if (scrollBar.Value + scrollValue < scrollBar.Minimum)
            {
                scrollBar.Value = scrollBar.Minimum;
            }
            else
            {
                scrollBar.Value += scrollValue;
            }
            updateView(scrollBar.Value, tViewRange);
            Invalidate();
        }

        /// <summary>
        /// Initiate time domain scroll bar as 0 (far left)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollerBar_Scroll(object sender, ScrollEventArgs e)
        {
            scrollWave(0);
        }

        /// <summary>
        /// Method to update time domain panel view range
        /// </summary>
        /// <param name="low"></param>
        /// <param name="range"></param>
        private void updateView(int low, int range)
        {
            if (range > samples[0].Length)
            {
                range = samples[0].Length - 1;
            }

            if (low < 0)
            {
                tViewStart = 0;
            }
            else if (low > samples[0].Length)
            {
                tViewStart = samples[0].Length - range;
            }
            else
            {
                tViewStart = low;
            }
            tViewRange = range;
            updateScrollRange();
            Invalidate();
        }

        /// <summary>
        /// Method to magnify or de-magnify the view on Time Domain panel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            float zoomAmount = e.Delta / 120.0f;
            if (zoomAmount < 0 && tViewRange >= samples[0].Length)
            {
                return;
            }
            else if (zoomAmount > 0 && tViewRange <= 40)
            {
                return;
            }
            int tMousePoint = (int)((double)e.X / this.Width * tViewRange + tViewStart);
            int less = tMousePoint - tViewStart;
            less = (int)(less * Math.Pow(0.83f, zoomAmount));
            int newRange = (int)(tViewRange * Math.Pow(0.83f, zoomAmount));

            updateView(tMousePoint - less, newRange);
            updateScrollRange();
        }

        /// <summary>
        /// method to initialize user selection vairables
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int time = (int)((double)e.X / Width * tViewRange + tViewStart);

            timeSelStart = time;
            timeSelEnd = time;
            tMouseDown = time;
            tMouseDrag = time;
        }

        /// <summary>
        /// Method to track user selection on Time Domain panel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            float viewLength = e.X * tViewRange / Width + tViewStart;
            float position = viewLength / samples[0].Length;

            // updates label as user moves mouse at different position in the time domain panel.
            lblTime.Text = String.Format("Time: {0:0.000}sec", Math.Round(position, 3));

            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Point clientOrigin = new Point(0, 0);
            clientOrigin = this.PointToScreen(clientOrigin);

            tMouseDrag = (int)((double)e.X * tViewRange / Width + tViewStart);
            timeSelStart = Math.Min(tMouseDown, tMouseDrag);
            timeSelEnd = Math.Max(tMouseDown, tMouseDrag);
            
            //update user drag selection
            if (tMouseDrag > (tViewStart + tViewRange))
            {
                scrollWave(tMouseDrag - (tViewStart + tViewRange));
            }
            else if (tMouseDrag < tViewStart)
            {
                scrollWave(tMouseDrag - tViewStart);
            }
            else
            {
                Invalidate();
            }
            Update();
        }

        /// <summary>
        /// Method to indicate user selection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            updateSelection(Math.Min(tMouseDown, tMouseDrag), Math.Max(tMouseDown, tMouseDrag));
        }

        private void updateSelection(int low, int high)
        {
            if (low < 0)
                timeSelStart = 0;
            else if (low > samples[0].Length)
                timeSelStart = samples[0].Length - 1;
            else
                timeSelStart = low;
            if (high < 0)
                timeSelEnd = 0;
            else if (high > samples[0].Length)
                timeSelEnd = samples[0].Length - 1;
            else
                timeSelEnd = high;

            OnSelChanged(EventArgs.Empty);
            Invalidate();
        }
    }
}
