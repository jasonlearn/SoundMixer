using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// class WaveFile used to store wave information as objects:
    /// bitDepth, channels, sampleRate, path, name, and data as sample array.
    /// </summary>
    [Serializable]
    public class WaveFile
    {
        public short bitDepth;
        public short channels;
        public int sampleRate;
        public double[][] samples = null;
        private string sourcePath;
        private string name; 

        /// <summary>
        /// constructor to initialize WaveFile object
        /// </summary>
        public WaveFile()
        {
            generateSamples();
            sourcePath = null;
            name = "NewWave";
        }

        /// <summary>
        /// Constructor
        /// initialize a wave object containing variables needed to perform data manipulation of .wav file
        /// </summary>
        /// <param name="bitDepth"></param>
        /// <param name="channels"></param>
        /// <param name="sampleRate"></param>
        /// <param name="data"></param>
        public WaveFile(short bitDepth, short channels, int sampleRate, byte[] data)
        {
            this.bitDepth = bitDepth;
            this.channels = channels;
            this.sampleRate = sampleRate;
            sourcePath = null;
            name = "NewWave";

            System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            int numSamples = data.Length / (channels * (bitDepth / 8));
            samples = new double[channels][];
            for (int c = 0; c < channels; c++)
            {
                samples[c] = new double[numSamples];
            }

            if (bitDepth == 16)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming signed
                        //normalized to -1.0..+1.0
                        samples[c][i] = (double)br.ReadInt16() / 32768.0;
                    }
                }
            }
            else if (bitDepth == 8)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming unsigned
                        //normalized to -1.0..+1.0
                        samples[c][i] = (double)br.ReadByte() / 128.0 - 1.0;
                    }
                }
            }
            else
            {
                throw new FormatException("Bit depth must be one of 8 or 16 bits.");
            }

        }

        /// <summary>
        /// Constructor for WaveFile, set wavefile variables
        /// </summary>
        /// <param name="bitDepth"></param>
        /// <param name="channels"></param>
        /// <param name="sampleRate"></param>
        private WaveFile(short bitDepth, short channels, int sampleRate)
        {
            sourcePath = null;
            name = "NewWave";
            this.bitDepth = bitDepth;
            this.channels = channels;
            this.sampleRate = sampleRate;
            samples = new double[channels][];
        }

        /// <summary>
        /// Constructor of class WaveFile using only thre source of the .wav file 
        /// </summary>
        /// <param name="src">source destination</param>
        public WaveFile(string src)
        {
            sourcePath = src;
            name = sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);
            readFile();
        }

        /// <summary>
        /// Method to save sample as a .wav file
        /// </summary>
        public void save()
        {
            writeFile(sourcePath);
        }

        /// <summary>
        /// Method to read a .wav file
        /// Uses try and catch block to handle IOExceptions
        /// </summary>
        public void readFile()
        {
            System.IO.BinaryReader sr;

            try
            {
                sr = new System.IO.BinaryReader(System.IO.File.Open(sourcePath, System.IO.FileMode.Open));
            }
            catch (System.IO.IOException)
            {
                throw;
            }

            char[] ckID = sr.ReadChars(4);
            String a = new string(ckID);
            if (a.CompareTo("RIFF") != 0)
            {
                throw new FormatException("RIFF chunkID missing. Found " + ckID[0] + ckID[1] + ckID[2] + ckID[3] + ".");
            }

            UInt32 RIFFSize = sr.ReadUInt32();

            ckID = sr.ReadChars(4);
            a = new string(ckID);
            if (a.CompareTo("WAVE") != 0)
            {
                throw new FormatException("WAVE chunkID missing. Found " + ckID[0] + ckID[1] + ckID[2] + ckID[3] + ".");
            }

            ckID = sr.ReadChars(4);
            a = new string(ckID);
            UInt32 chunkSize = sr.ReadUInt32();
            while (a.CompareTo("fmt ") != 0)
            {
                sr.ReadBytes((int)chunkSize);
                ckID = sr.ReadChars(4);
                a = new string(ckID);
                chunkSize = sr.ReadUInt32();
            }
            Int16 wFormatTag = sr.ReadInt16();
            Int16 nChannels = sr.ReadInt16();
            Int32 nSamplesPerSec = sr.ReadInt32();
            Int32 nAvgBytesPerSec = sr.ReadInt32();
            Int16 nBlockAlign = sr.ReadInt16();
            Int16 wBitsPerSample = sr.ReadInt16();
            chunkSize -= 16;
            sr.ReadBytes((int)chunkSize);

            if (wFormatTag != 0x0001)
            {
                throw new FormatException("Invalid wave format.");
            }

            ckID = sr.ReadChars(4);
            a = new string(ckID);
            chunkSize = sr.ReadUInt32();
            while (a.CompareTo("data") != 0)
            {
                sr.ReadBytes((int)chunkSize);
                ckID = sr.ReadChars(4);
                a = new string(ckID);
                chunkSize = sr.ReadUInt32();
            }

            channels = (short)nChannels;
            bitDepth = (short)wBitsPerSample;
            sampleRate = nSamplesPerSec;
            long numSamples = chunkSize / (bitDepth / 8) / channels;
            samples = new double[channels][];
            for (int c = 0; c < channels; c++)
            {
                samples[c] = new double[numSamples];
            }
            if (bitDepth == 16)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming signed
                        //normalized to -1.0..+1.0
                        samples[c][i] = (double)sr.ReadInt16() / 32768.0;
                    }
                }
            }
            else if (bitDepth == 8)
            {
                for (int i = 0; i < numSamples; i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        samples[c][i] = (double)sr.ReadByte() / 128.0 - 1.0;
                    }
                }
            }
            else
            {
                throw new FormatException("Bit depth must be one of 8 or 16 bits.");
            }
            sr.Close();
        }

        public void writeFile(string dest)
        {
            System.IO.BinaryWriter wr;

            try
            { //TODO: test, rather than try/fail?
                wr = new System.IO.BinaryWriter(System.IO.File.Open(dest, System.IO.FileMode.Create));
            }
            catch (System.IO.IOException)
            {
                throw;
            }

            wr.Write("RIFF".ToCharArray());
            wr.Write((Int32)(4 + (8 + 16) + (8 + (getNumSamples() * channels * bitDepth / 8)))); //TODO: 4 + size of fmt + size of data

            wr.Write("WAVE".ToCharArray());

            wr.Write("fmt ".ToCharArray());
            wr.Write((Int32)16);
            wr.Write((Int16)0x0001);
            wr.Write((Int16)channels);
            wr.Write((Int32)sampleRate);
            wr.Write((Int32)(sampleRate * channels * bitDepth / 8));
            wr.Write((Int16)(channels * bitDepth / 8));
            wr.Write((Int16)bitDepth);

            wr.Write("data".ToCharArray());
            wr.Write((Int32)(getNumSamples() * channels * bitDepth / 8));

            if (bitDepth == 16)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming signed
                        wr.Write((short)(samples[c][i] * 32768));
                    }
                }
            }
            else if (bitDepth == 8)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        wr.Write((byte)(samples[c][i] * 128 + 128));
                    }
                }
            }

            if ((getNumSamples() * channels * bitDepth / 8) % 2 == 1)
                wr.Write((byte)0);

            wr.Close();
        }

        /// <summary>
        /// Method to write a WaveFile.
        /// Uses try and catch block to handle IOExceptions
        /// </summary>
        /// <param name="outStream"></param>
        public void writeStream(System.IO.Stream outStream)
        {
            System.IO.BinaryWriter wr;

            try
            {
                wr = new System.IO.BinaryWriter(outStream);
            }
            catch (System.IO.IOException)
            {
                throw;
            }

            wr.Write("RIFF".ToCharArray());
            wr.Write((Int32)(4 + (8 + 16) + (8 + (getNumSamples() * channels * bitDepth / 8)))); //TODO: 4 + size of fmt + size of data

            wr.Write("WAVE".ToCharArray());

            wr.Write("fmt ".ToCharArray());
            wr.Write((Int32)16);
            wr.Write((Int16)0x0001);
            wr.Write((Int16)channels);
            wr.Write((Int32)sampleRate);
            wr.Write((Int32)(sampleRate * channels * bitDepth / 8));
            wr.Write((Int16)(channels * bitDepth / 8));
            wr.Write((Int16)bitDepth);

            wr.Write("data".ToCharArray());
            wr.Write((Int32)(getNumSamples() * channels * bitDepth / 8));

            if (bitDepth == 16)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        wr.Write((short)(samples[c][i] * 32768));
                    }
                }
            }
            else if (bitDepth == 8)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        wr.Write((byte)(samples[c][i] * 128 + 128));
                    }
                }
            }

            if ((getNumSamples() * channels * bitDepth / 8) % 2 == 1)
                wr.Write((byte)0);
        }

        public byte[] getData()
        {
            byte[] result = new byte[getNumSamples() * channels * (bitDepth / 8)];
            System.IO.MemoryStream ms = new System.IO.MemoryStream(result);
            System.IO.BinaryWriter wr = new System.IO.BinaryWriter(ms);

            if (bitDepth == 16)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming signed
                        wr.Write((short)(samples[c][i] * 32768));
                    }
                }
            }
            else if (bitDepth == 8)
            {
                for (int i = 0; i < getNumSamples(); i++)
                {
                    for (int c = 0; c < channels; c++)
                    {
                        //assuming unsigned
                        wr.Write((byte)(samples[c][i] * 128 + 128));
                    }
                }
            }
            wr.Dispose();
            ms.Dispose();
            return result;
        }

        private void generateSamples()
        {
            double amp = 1.0, f, phase = 0.0;

            bitDepth = 8;
            channels = 1;
            sampleRate = 22050;

            samples = new double[channels][];
            for (int c = 0; c < channels; c++)
            {
                samples[c] = new double[sampleRate];
                for (int t = 0; t < sampleRate; t++)
                {
                    samples[c][t] = 0;

                    f = 1000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 2000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 3000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 4000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 5000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 6000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 7000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 8000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 9000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 10000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 11000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 12000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 13000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 14000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 15000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 16000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 17000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 18000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 19000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 20000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);
                    f = 21000;
                    samples[c][t] += amp * Math.Sin(Math.PI * 2 * f * t / sampleRate + phase);

                    samples[c][t] = samples[c][t] / 18;
                }
            }
        }

        /// <summary>
        /// Method to get number of samples in selected .wav file
        /// </summary>
        /// <returns></returns>
        public int getNumSamples()
        {
            if (samples != null && samples[0] != null)
            {
                return samples[0].Length;
            }
            return 0;
        }

        /// <summary>
        /// Method to get the duration of the .wav file
        /// </summary>
        /// <returns>duration</returns>
        public double getDuration()
        {
            double duration =  (double)getNumSamples() / sampleRate;
            return duration;
        }

        /// <summary>
        /// method to get .wav file source destination
        /// </summary>
        /// <returns>path</returns>
        public string getPath()
        {
            return sourcePath;
        }

        /// <summary>
        /// Method to get .wav file name
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return name;
        }

        /// <summary>
        /// Check if a given sample index is in range
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public bool inRange(int sample)
        {
            return (sample >= 0 && sample < getNumSamples());
        }

        /// <summary>
        /// Mutator method to set .wav file path when user decides to save data as .wav
        /// </summary>
        /// <param name="src"></param>
        public void setPath(string src)
        {
            sourcePath = src;
            name = sourcePath.Substring(sourcePath.LastIndexOf("\\") + 1);
        }

        /// <summary>
        /// Method to drop samples
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void dropSamples(int start, int end)
        {
            if (!inRange(start) || end <= start)
            {
                return;
            }

            double[][] result = new double[channels][];
            int i;

            if (end > getNumSamples())
            {
                end = getNumSamples();
            }

            for (i = 0; i < channels; i++)
            {
                result[i] = new double[start + getNumSamples() - end];
            }

            for (int channel = 0; channel < channels; channel++)
            {
                //fill in pre
                for (i = 0; i < start; i++)
                {
                    result[channel][i] = samples[channel][i];
                }
                //fill in post
                for (i = end; i < getNumSamples(); i++)
                {
                    result[channel][i - (end - start)] = samples[channel][i];
                }
            }
            samples = result;
        }

        /// <summary>
        /// Method to paste user copy/cut selection onto user's desired destination
        /// </summary>
        /// <param name="destIndex"></param>
        /// <param name="newSamples"></param>
        internal void pasteSelection(int destIndex, double[][] newSamples)
        {
            double[][] result = new double[channels][];
            int i;
            if (destIndex > getNumSamples())
            {
                destIndex = getNumSamples();
            }
            int pre = destIndex;
            int mid = newSamples[0].Length;
            int post = getNumSamples() - destIndex;

            for (i = 0; i < channels; i++)
            {
                result[i] = new double[pre + mid + post];
            }

            for (int channel = 0; channel < channels; channel++)
            {
                //fill in pre
                for (i = 0; i < pre; i++)
                {
                    result[channel][i] = samples[channel][i];
                }
                //fill in mid
                for (i = pre; i < pre + mid; i++)
                {
                    result[channel][i] = newSamples[channel][i - pre];
                }
                //fill in post
                for (i = pre; i < pre + post; i++)
                {
                    result[channel][i + mid] = samples[channel][i];
                }
            }
            samples = result;
        }

        /// <summary>
        /// Method to paste user copy/cut selection onto user's desired destination
        /// </summary>
        /// <param name="destIndex"></param>
        /// <param name="newSamples"></param>
        internal void pasteSelection(int destIndex, WaveFile newWave)
        {
            double[][] result = new double[channels][];
            int i;
            if (destIndex > getNumSamples())
            {
                destIndex = getNumSamples();
            }
            int pre = destIndex;
            int mid = newWave.getNumSamples();
            int post = getNumSamples() - destIndex;

            for (i = 0; i < channels; i++)
            {
                result[i] = new double[pre + mid + post];
            }

            for (int channel = 0; channel < channels; channel++)
            {
                //fill in pre
                for (i = 0; i < pre; i++)
                {
                    result[channel][i] = samples[channel][i];
                }
                //fill in mid
                for (i = pre; i < pre + mid; i++)
                {
                    result[channel][i] = newWave.samples[channel][i - pre];
                }
                //fill in post
                for (i = pre; i < pre + post; i++)
                {
                    result[channel][i + mid] = samples[channel][i];
                }
            }
            samples = result;
        }

        /// <summary>
        /// Method to copy user's selection
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal WaveFile copySelection(int copyStart, int copyEnd)
        {
            if (!inRange(copyStart) || copyEnd <= copyStart)
            {
                return null;
            }
            if (copyEnd > getNumSamples())
            {
                copyEnd = getNumSamples();
            }
            double[][] duplicate = new double[channels][];
            for (int i = 0; i < channels; i++)
            {
                duplicate[i] = new double[copyEnd - copyStart];
            }

            for (int channel = 0; channel < channels; channel++)
            {
                Array.Copy(samples[channel], copyStart, duplicate[channel], 0, copyEnd - copyStart);
            }

            WaveFile copy = new WaveFile(bitDepth, channels, sampleRate);
            copy.pasteSelection(0, duplicate);
            return copy;
        }

        /// <summary>
        /// Method to cut user's selection
        /// </summary>
        /// <param name="cutStart"></param>
        /// <param name="cutEnd"></param>
        /// <returns></returns>
        internal WaveFile cutSelection(int cutStart, int cutEnd)
        {
            WaveFile copy = copySelection(cutStart, cutEnd);
            dropSamples(cutStart, cutEnd);
            return copy;
        }
    }
}

