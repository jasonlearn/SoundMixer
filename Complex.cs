using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// This class is used to initiate Complex Numbers
    /// Also contains method to calculate magnitude of complex numbers
    /// </summary>
    public class Complex
    {
        public double r, i;

        /// <summary>
        /// class to construct complex numbers using real and imaginary portion of fouier
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public Complex(double real, double imaginary)
        {
            r = real;
            i = imaginary;
        }

        /// <summary>
        /// Method to compute the magnitude (Ampitude) of a complex number
        /// </summary>
        /// <returns>calculated magnitude</returns>
        public double magnitude()
        {
            return Math.Sqrt(r * r + i * i);
        }

        /// <summary>
        /// Method to contruct complex number with magnitude and angle
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="angle"></param>
        /// <returns>complex number</returns>
        public static Complex fromMagAngle(double magnitude, double angle)
        {
            return new Complex(magnitude * Math.Cos(angle), magnitude * Math.Sin(angle));
        }

        /// <summary>
        /// Method to compute  the angle 
        /// </summary>
        /// <returns>angle</returns>
        public double angle()
        {
            return Math.Atan2(i, r);
        }

        /// <summary>
        /// Method to normalize the length of the complex number
        /// </summary>
        public void normalize()
        {
            double length = magnitude();
            r /= length;
            i /= length;
        }
    }
}
