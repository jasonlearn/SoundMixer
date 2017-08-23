using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// This class is used to represent fraction.
    /// </summary>
    public class Fraction
    {
        public int num;
        public int denom;
        private double epsilon;

        /// <summary>
        /// Method to construct a fraction using a double value.
        /// </summary>
        /// <param name="value"></param>
        public Fraction(double value)
        {
            epsilon = 0.0001;
            setValsFromDouble(value);
        }

        /// <summary>
        /// Method to construct a fraction using a rational value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="e"></param>
        public Fraction(double value, double e)
        {
            epsilon = e;
            setValsFromDouble(value);
        }

        /// <summary>
        /// Mutator method to calculate a fraction within epsilon distance.
        /// </summary>
        /// <param name="value"></param>
        private void setValsFromDouble(double value)
        {
            int whole = (int)Math.Floor(value);
            value -= whole;
            double a = 0;
            double b = 1;

            double temp = a / b;

            while (Math.Abs(temp - value) > epsilon)
            {
                if (temp < value)
                {
                    a += 1;
                }
                else
                {
                    b += 1;
                }
                temp = a / b;
            }
            num = (int)a + (whole * (int)b);
            denom = (int)b;
        }
    }
}

