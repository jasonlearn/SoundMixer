using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * This class contains multiple formulas: 
 * Discrete Fourier Transform, Inverse Discrete Fourier Transform, Convolution
 * Filters:
 * Convolution Filter, DFT Filter, highpass IIR Filter, lowpass IIR Filter
 * Windowing techniques:
 * pass, triangle, cosine, blackman
 * Methods to:
 * pitchShift, channel change, and reverse the wave
 */

namespace JasonChan_SoundWaveProject
{
    // enum of windowing methods
    public enum WINDOWING { pass, triangle, cosine, blackman }
    // enum of FX methods
    public enum EDIT_FX { reverse, normalize, pitchshift }
    // enum of filtering methods
    public enum FILTERING { convolution, DFT, IIRLowpass, IIRHighpass }

    /// <summary>
    /// This class contains multiple formulas: 
    /// Discrete Fourier Transform, Inverse Discrete Fourier Transform, Convolution
    /// Filters:
    /// Convolution Filter, DFT Filter, highpass IIR Filter, lowpass IIR Filter
    /// Windowing techniques:
    /// pass, triangle, cosine, blackman
    /// Methods to:
    /// pitchShift, channel change, and reverse the wave
    /// </summary>
    class Formulas
    {
        /// <summary>
        /// Discrete Fourier Transform.  
        /// Method to convert data from Time domain to Frequency domain
        /// </summary>
        /// <param name="samples">Time-domain sample data</param>
        /// <returns>array of complex</returns>
        public static Complex[] DFT(ref double[] samples)
        {
            int N = samples.Length; // N, samples size
            Complex[] DFT = new Complex[N];

            double r, i;
            for (int f = 0; f < N; f++)
            {
                r = 0;
                i = 0;
                for (int t = 0; t < N; t++)
                {
                    r += (samples[t]) * Math.Cos(2 * Math.PI * f * t / N);
                    i -= (samples[t]) * Math.Sin(2 * Math.PI * f * t / N);
                }
                DFT[f] = new Complex(r, i);
            }

            return DFT;
        }

        /// <summary>
        /// Inverse Discrete Fourier Transform.  
        /// Method to convert data from Frequency domain to Time domain
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static double[] InverseDFT(ref Complex[] A)
        {
            int N = A.Length; // N, number of frequency bins
            double[] S = new double[N];
            double st;
            for (int t = 0; t < N; t++)
            {
                st = 0;
                for (int f = 0; f < N; f++)
                {
                    st += A[f].r * Math.Cos(2 * Math.PI * f * t / N);
                    st -= A[f].i * Math.Sin(2 * Math.PI * f * t / N);
                }
                S[t] = st / N;
            }
            return S;
        }

        /// <summary>
        /// Method to apply convolution on a set of sample.
        /// Uses a range.
        /// </summary>
        /// <param name="S"></param>
        /// <param name="kernel"></param>
        /// <returns>result - copy of S array</returns>
        public static double[] convolve(ref double[] S, double[] range)
        {
            int t;
            double[] result = new double[S.Length];
            for (t = 0; t < S.Length - range.Length; t++)
            {
                result[t] = 0;
                for (int i = 0; i < range.Length; i++)
                {
                    result[t] += S[t + i] * range[i];
                }
                result[t] /= range.Length;
            }
            for (; t < S.Length; t++)
            {
                result[t] = 0;
                for (int i = 0; i < S.Length - t; i++)
                {
                    result[t] += S[t + i] * range[i];
                }
                result[t] /= (S.Length - t);
            }
            return result;
        }

        /// <summary>
        /// Method to apply convolve filter on a given sample
        /// </summary>
        /// <param name="S"></param>
        /// <param name="filter">A set of multipliers in the frequency domain.</param>
        /// <returns>result</returns>
        public static double[] convolveFilter(ref double[] S, double[] filter)
        {
            int t;
            //create complex filter from simple filter
            Complex[] qFilter = new Complex[filter.Length];
            for (int af = 0; af < filter.Length; af++)
            {
                qFilter[af] = new Complex(filter[af], filter[af]);
            }

            //run complex filter through IDFT
            double[] w = InverseDFT(ref qFilter);

            //convolve signal with filter
            double[] result = new double[S.Length];
            for (t = 1; t < S.Length - w.Length; t++)
            {
                result[t] = 0;
                for (int i = 0; i < w.Length; i++)
                {
                    result[t] += S[t + i] * w[i];
                }
            }
            // Padding to make runoffs as 0
            for (; t < S.Length; t++)
            {
                result[t] = 0;
                for (int i = 0; i < S.Length - t; i++)
                {
                    result[t] += S[t + i] * w[i];
                }
            }

            result[0] = S[0];
            return result;
        }

        /// <summary>
        /// Method to construct a biquad infinite impulse response filter (IIR filter)
        /// </summary>
        /// <param name="S"></param>
        /// <param name="zFreq"></param>
        /// <param name="hFreq"></param>
        /// <param name="filterOpt">Highpass (1) or Lowpass (0) or reference frequency.</param>
        /// <param name="samplingRate"></param>
        /// <returns></returns>
        public static double[] IIRFilter(double[] S, double zFreq, double hFreq, double filterOpt, double sampleRate)
        {
            double[] a = new double[2];
            double[] b = new double[3];

            Complex z1 = Complex.fromMagAngle(1, zFreq / sampleRate * Math.PI * 2);
            Complex z2 = Complex.fromMagAngle(1, -zFreq / sampleRate * Math.PI * 2);
            Complex p1 = Complex.fromMagAngle(0.66, hFreq / sampleRate * Math.PI * 2);
            Complex p2 = Complex.fromMagAngle(0.66, -hFreq / sampleRate * Math.PI * 2);

            b[0] = 1;
            b[1] = -z1.r - z2.r;
            b[2] = z1.r * z2.r - z1.i * z2.i;

            a[0] = -p1.r - p2.r;
            a[1] = p1.r * p2.r - p1.i * p2.i;

            //reference frequency
            double Zr = 0; // reference frequency
            double Wr = 0; // reference angle

            //lowpass filter
            if (filterOpt == 0)
            {
                Zr = 0;
                Wr = 0;
            }

            // high pass filter
            else if (filterOpt == 1)
            {
                Zr = sampleRate / 2;
                Wr = Math.PI;
            }
            else
            {
                Zr = filterOpt;
                Wr = Zr / sampleRate * Math.PI * 2;
            }

            double c = 1;
            if (filterOpt == 0)
            {
                c = (1 + a[0] + a[1]) / (1 + b[1] + b[2]);
            }
            else if (filterOpt == 1)
            {
                c = (1 - a[0] + a[1]) / (1 - b[1] + b[2]);
            }
            else
            {
                c = Math.Sqrt(
                    (Math.Pow(1 + a[0] * Math.Cos(Wr) + a[1] * Math.Cos(2 * Wr), 2) + Math.Pow(a[0] * Math.Sin(Wr) + a[1] * Math.Sin(2 * Wr), 2))/
                    (Math.Pow(1 + b[1] * Math.Cos(Wr) + b[2] * Math.Cos(2 * Wr), 2) + Math.Pow(b[1] * Math.Sin(Wr) + b[2] * Math.Sin(2 * Wr), 2))
                );
            }

            b[0] = c;
            b[1] = c * (-z1.r - z2.r);
            b[2] = c * (z1.r * z2.r - z1.i * z2.i);

            a[0] = -p1.r - p2.r;
            a[1] = p1.r * p2.r - p1.i * p2.i;

            return processIIRFilter(ref S, a, b);
        }

        /// <summary>
        /// Method to apply IIR filter to a set of sample.
        /// </summary>
        /// <param name="S"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double[] processIIRFilter(ref double[] S, double[] a, double[] b)
        {
            double[] result = new double[S.Length]; //temporary array

            int shortest = Math.Min(a.Length, b.Length);
            int n = 0;
            for (; n < shortest; n++)
            {
                result[n] = S[n];
            }

            for (; n < S.Length; n++)
            {
                result[n] = 0;
                for (int i = 0; i < b.Length; i++)
                {
                    result[n] += S[n - i] * b[i];
                }
                for (int i = 0; i < a.Length; i++)
                {
                    result[n] -= result[n - 1 - i] * a[i];
                }
            }

            return result;
        }

        /// <summary>
        /// Method to filter a sample set from time to frequency domain.
        /// Multiply samples by factors, and reverts back to time domain.
        /// </summary>
        /// <param name="S"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static double[] dftFilter(double[] S, double[] filter)
        {
            int N = filter.Length;
            double[] result = new double[S.Length];

            double[] sChunk = new double[N];
            Complex[] fChunk;
            int first;
            first = 0;

            while (first + N < S.Length)
            {
                Array.Copy(S, first, sChunk, 0, N);
                fChunk = DFT(ref sChunk);
                for (int i = 0; i < N; i++)
                {
                    if (filter[i] == 0)
                    {
                        fChunk[i].i = 0;
                        fChunk[i].r = 0;
                    }
                }
                sChunk = InverseDFT(ref fChunk);
                Array.Copy(sChunk, 0, result, first, N);
                first += N;
            }


            return result;
        }

        /// <summary>
        /// Method to match the number of channels
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="newChannels">The channel to be changed to</param>
        /// <returns>samples</returns>
        public static double[][] matchChannels(double[][] samples, short newChannels)
        {
            if (samples.Length == newChannels)
            {
                return samples;
            }
            double[] data = samples[0];

            //downmix to 1 channel
            for (int i = 0; i < samples.Length; i++)
            {
                data = StereoToMono(ref samples[i], ref data);
            }

            //duplicate channel to every other channel.
            samples = new double[newChannels][];
            for (int channel = 0; channel < newChannels; channel++)
            {
                samples[channel] = data;
            }

            return samples;
        }

        /// <summary>
        /// Method to combine 2 sample arrays.
        /// </summary>
        /// <param name="sampleOne"></param>
        /// <param name="sampleTwo"></param>
        /// <returns>result</returns>
        public static double[] StereoToMono(ref double[] sampleOne, ref double[] samplesTwo)
        {
            double[] result = new double[Math.Max(sampleOne.Length, samplesTwo.Length)];
            long limit = Math.Min(sampleOne.Length, samplesTwo.Length);
            long t;
            for (t = 0; t < limit; t++)
            {
                result[t] = (sampleOne[t] + samplesTwo[t]) / 2;
            }

            if (t < sampleOne.Length)
            {
                Array.Copy(sampleOne, t, result, t, sampleOne.Length - t);
            }
            else if (t < samplesTwo.Length)
            {
                Array.Copy(samplesTwo, t, result, t, samplesTwo.Length - t);
            }
            return result;
        }

        /// <summary>
        /// Method to resample the sample set.
        /// Then goes through a lowpass filter
        /// </summary>
        /// <param name="samples">sample to be changed</param>
        /// <param name="originalRate">original samplerate</param>
        /// <param name="targetRate">target samplerate</param>
        /// <returns></returns>
        public static double[] resample(ref double[] samples, int originalRate, int targetRate)
        {
            double[] extendedSamples, result;
            int L, M;
            if (targetRate == originalRate)
            {
                return samples;
            }
            else if (targetRate == 2 * originalRate)
            {
                L = 2; M = 1;
            }
            else if (targetRate == 4 * originalRate)
            {
                L = 4; M = 1;
            }
            else if (targetRate == 8 * originalRate)
            {
                L = 8; M = 1;
            }
            else if (targetRate * 2 == originalRate)
            {
                L = 1; M = 2;
            }
            else if (targetRate * 4 == originalRate)
            {
                L = 1; M = 4;
            }
            else if (targetRate * 8 == originalRate)
            {
                L = 1; M = 8;
            }
            else
            {
                return null;
            }

            //used to pad 0's between samples
            extendedSamples = new double[samples.Length * L];
            int i = 0;
            for (int s = 0; s < samples.Length; s++)
            {
                extendedSamples[i] = samples[s];
                i++;
                for (int extra = 0; extra < L - 1; extra++)
                {
                    extendedSamples[i] = 0;
                    i++;
                }
            }

            //lowpass filter 
            int S = Math.Min(originalRate, targetRate);
            extendedSamples = Formulas.IIRFilter(extendedSamples, S / 2 + 5000, S / 2 - 5000, 0, S * 2);


            //skips and select every Mth sample.
            result = new double[extendedSamples.Length / M];
            i = 0;
            for (int s = 0; s < extendedSamples.Length; s += M)
            {
                try
                {
                    result[i++] = extendedSamples[s];
                } catch{

                }
            }

            return result;
        }

        /// <summary>
        /// Method to pitchShift
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="sampleRate"></param>
        /// <param name="semitones"></param>
        /// <returns>result</returns>
        public static double[] pitchShift(ref double[] samples, int sampleRate, int semitones)
        {
            double[] extendedSamples, result;
            double freqRatio = Math.Pow(2, (double)semitones / 12.0);
            Fraction frac = new Fraction(freqRatio);
            int M = frac.num;
            int L = frac.denom;

            //used to pad 0's between samples
            extendedSamples = new double[samples.Length * L];
            int i = 0;
            for (int s = 0; s < samples.Length; s++)
            {
                extendedSamples[i] = samples[s];
                for (int extra = 1; extra <= L - 1; extra++)
                {
                    extendedSamples[i + extra] = extendedSamples[i + extra - 1];
                }
                i += L;
            }

            //skips and select every Mth sample.
            result = new double[(int)Math.Ceiling(extendedSamples.Length / (double)M)];
            i = 0;
            for (int s = 0; s < extendedSamples.Length; s += M)
            {
                result[i++] = extendedSamples[s];
            }

            return result;
        }


        /// <summary>
        /// Method to maximize the amplitude of a given sample set.
        /// </summary>
        /// <param name="samples"></param>
        /// <returns>max - absolute value</returns>
        private static double maxAmplitude(double[] samples)
        {
            double maxAmp = 0;
            for (int t = 0; t < samples.Length; t++)
            {
                if (Math.Abs(samples[t]) > maxAmp)
                {
                    maxAmp = Math.Abs(samples[t]);
                }
            }
            return maxAmp;
        }

        /// <summary>
        /// Method to pass given sample set
        /// </summary>
        /// <param name="samples"></param>
        public static void WindowPassthrough(ref double[] samples)
        {
            return;
        }

        /// <summary>
        /// Method to perform Triangle Windowing technique on a give sample set
        /// </summary>
        /// <param name="samples">samples</param>
        public static void WindowTriangle(ref double[] samples)
        {
            double N = samples.Length;
            double a = (N - 1.0) / 2.0;
            double b = N / 2.0;
            double weight;

            for (int n = 0; n < N; n++)
            {
                weight = 1.0 - Math.Abs((n - a) / b);
                samples[n] *= weight;
            }
        }

        /// <summary>
        /// Method to perform Cosine Windowing technique on a given sample set
        /// </summary>
        /// <param name="samples">samples</param>
        public static void WindowCosine(ref double[] samples)
        {
            double N = samples.Length;
            double a = 0.54;
            double b = 1.0 - a;
            double weight;

            for (int n = 0; n < N; n++)
            {
                weight = a - b * Math.Cos((2 * Math.PI * n) / (N - 1));
                samples[n] *= weight;
            }
        }

        /// <summary>
        /// Method to perform Blackman Windowing technique to a given sample set
        /// </summary>
        /// <param name="samples">samples</param>
        public static void WindowBlackman(ref double[] samples)
        {
            double N = samples.Length;
            const double a = 0.16;
            const double a0 = (1.0 - a) / 2.0;
            const double a1 = 1.0 / 2.0;
            const double a2 = a / 2.0;
            double weight;

            for (int n = 0; n < N; n++)
            {
                weight
                    = a0
                    - a1 * Math.Cos((2 * Math.PI * n) / (N - 1))
                    + a2 * Math.Cos((4 * Math.PI * n) / (N - 1));
                samples[n] *= weight;
            }
        }

        /// <summary>
        /// Method to perform FX effects on the wave data.
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="args"></param>
        /// <param name="data"></param>
        public static void ApplyFX(EDIT_FX edit, object[] args, ref WaveFile data)
        {
            double factor;
            switch (edit)
            {
                case EDIT_FX.reverse:
                    for (int channel = 0; channel < data.channels; channel++)
                    {
                        Array.Reverse(data.samples[channel]);
                    }
                    break;
                case EDIT_FX.normalize:
                    for (int channel = 0; channel < data.channels; channel++)
                    {
                        factor = maxAmplitude(data.samples[channel]) * 1.1;
                        for (int sample = 0; sample < data.samples[channel].Length; sample++)
                        {
                            data.samples[channel][sample] = data.samples[channel][sample] / factor;
                        }
                    }
                    break;
                case EDIT_FX.pitchshift:
                    for (int channel = 0; channel < data.channels; channel++)
                    {
                        data.samples[channel] = pitchShift(ref data.samples[channel], data.sampleRate, (int)args[0]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
