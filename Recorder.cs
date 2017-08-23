using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// enum of handle messages for class Recorder
    /// </summary>
    public enum RecorderMsg
    {
        WIM_OPEN,
        WIM_DATA,
        WIM_CLOSE,
        RECORDING_START,
        RECORDING_STOP,
        TERMINATING,
        EXIT
    }

    class Recorder : IDisposable
    {
        private BlockingCollection<RecorderMsg> MsgQueue;
        private Thread recorder;

        private WaveFormat wavefmt;
        private byte[] pSaveBuffer;
        private int lastHeader;

        private WaveHdr waveHdr1;
        private byte[] pBuffer1;
        private GCHandle h_pBuffer1;

        private WaveHdr waveHdr2;
        private byte[] pBuffer2;
        private GCHandle h_pBuffer2;

        private IntPtr hWaveIn;
        private bool bRecording, bEnding;
        private static int INP_BUFFER_SIZE = 4096;

        private WinmmHook.WaveDelegate WaveInProc;
        private IntPtr parentHandle;

        public Recorder(System.Windows.Forms.Form parent)
        {
            parentHandle = parent.Handle;
            MsgQueue = new BlockingCollection<RecorderMsg>();

            hWaveIn = new IntPtr();
            WaveInProc = new WinmmHook.WaveDelegate(WIM_proc);

            pBuffer1 = new byte[INP_BUFFER_SIZE];
            pBuffer2 = new byte[INP_BUFFER_SIZE];
            wavefmt = new WaveFormat(22050, 16, 2);
        }

        ~Recorder()
        {
            Dispose();
        }

        /// <summary>
        /// method to check if recording is true or false.
        /// </summary>
        public bool Recording
        {
            get { return bRecording; }
        }

        /// <summary>
        /// interface to start recording a wave
        /// </summary>
        public void RecordingStart()
        {
            MsgQueue.Add(RecorderMsg.RECORDING_START);
            if (recorder != null && recorder.IsAlive)
            {
                throw new Exception("Recording session still active");
            }
            recorder = new Thread(new ThreadStart(threadProc));
            recorder.Start();
        }

        /// <summary>
        /// interface to stop recording
        /// </summary>
        public void RecordingStop()
        {
            MsgQueue.Add(RecorderMsg.RECORDING_STOP);
        }

        /// <summary>
        /// Method to get samples of the recorded wave
        /// </summary>
        /// <returns></returns>
        public WaveFile getSamples()
        {
            if (recorder.IsAlive)
            {
                MsgQueue.Add(RecorderMsg.TERMINATING);
                recorder.Join();
            }

            //create a new wave file with the byte array.
            WaveFile result = null;
            if (pSaveBuffer != null && pSaveBuffer.Length > 0)
            {
                result = new WaveFile(16, 2, 22050, pSaveBuffer);
            }

            return result;
        }

        /// <summary>
        /// WIM_proc method for handling different case
        /// </summary>
        /// <param name="hdrvr"></param>
        /// <param name="uMsg"></param>
        /// <param name="dwUser"></param>
        /// <param name="wavhdr"></param>
        /// <param name="dwParam2"></param>
        private void WIM_proc(IntPtr hdrvr, int uMsg, int dwUser, ref WaveHdr wavhdr, int dwParam2)
        {
            switch (uMsg)
            {
                case WinmmHook.MM_WIM_OPEN:
                    MsgQueue.Add(RecorderMsg.WIM_OPEN);
                    break;
                case WinmmHook.MM_WIM_DATA:
                    if (wavhdr.Equals(waveHdr1))
                    {
                        lastHeader = 1;
                    }
                    else if (wavhdr.Equals(waveHdr2))
                    {
                        lastHeader = 2;
                    }
                    else
                    {
                        lastHeader = 0;
                    }
                    MsgQueue.Add(RecorderMsg.WIM_DATA);
                    break;
                case WinmmHook.MM_WIM_CLOSE:
                    MsgQueue.Add(RecorderMsg.WIM_CLOSE);
                    break;
            }
        }

        /// <summary>
        /// ThreadProc to handle Record threading
        /// </summary>
        private void threadProc()
        {
            RecorderMsg msg;
            msg = MsgQueue.Take();
            while (msg != RecorderMsg.EXIT)
            {
                switch (msg)
                {
                    case RecorderMsg.WIM_OPEN:
                        OnWimOpen();
                        break;
                    case RecorderMsg.WIM_DATA:
                        OnWimData();
                        break;
                    case RecorderMsg.WIM_CLOSE:
                        OnWimClose();
                        break;
                    case RecorderMsg.RECORDING_START:
                        OnRecordingStart();
                        break;
                    case RecorderMsg.RECORDING_STOP:
                        OnRecordingStop();
                        break;
                    case RecorderMsg.TERMINATING:
                        OnTerminating();
                        break;
                }
                msg = MsgQueue.Take();
            }
        }

        /// <summary>
        /// Method to initiate Winmm
        /// </summary>
        private void OnWimOpen()
        {
            WinmmHook.waveInAddBuffer(hWaveIn, ref waveHdr1, Marshal.SizeOf(waveHdr1));
            WinmmHook.waveInAddBuffer(hWaveIn, ref waveHdr2, Marshal.SizeOf(waveHdr2));

            bRecording = true;
            bEnding = false;
            WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Recording, 0);
            WinmmHook.waveInStart(hWaveIn);
        }

        /// <summary>
        /// Method to handle recorded data
        /// </summary>
        private void OnWimData()
        {
            WaveHdr header;
            byte[] samples;
            if (lastHeader == 1)
            {
                header = waveHdr1;
                samples = pBuffer1;
            }
            else if (lastHeader == 2)
            {
                header = waveHdr2;
                samples = pBuffer2;
            }
            else
            {
                if (bEnding)
                {
                    WinmmHook.waveInClose(hWaveIn);
                }
                return;
            }

            byte[] result;
            int copyPos;
            if (pSaveBuffer == null)
            {
                result = new byte[header.dwBytesRecorded];
                copyPos = 0;
            }
            else
            {
                result = new byte[pSaveBuffer.Length + header.dwBytesRecorded];
                pSaveBuffer.CopyTo(result, 0);
                copyPos = pSaveBuffer.Length;
            }
            Array.Copy(samples, 0, result, copyPos, header.dwBytesRecorded);
            pSaveBuffer = result;

            if (bEnding)
            {
                WinmmHook.waveInReset(hWaveIn);
                WinmmHook.waveInClose(hWaveIn);
                return;
            }


            if (lastHeader == 1)
            {
                WinmmHook.waveInAddBuffer(hWaveIn, ref waveHdr1, Marshal.SizeOf(waveHdr1));
            }
            else if (lastHeader == 2)
            {
                WinmmHook.waveInAddBuffer(hWaveIn, ref waveHdr2, Marshal.SizeOf(waveHdr2));
            }
        }

        /// <summary>
        /// Method to terminate winmm
        /// </summary>
        private void OnWimClose()
        {
            WinmmHook.waveInUnprepareHeader(hWaveIn, ref waveHdr1, Marshal.SizeOf(waveHdr1));
            WinmmHook.waveInUnprepareHeader(hWaveIn, ref waveHdr2, Marshal.SizeOf(waveHdr2));

            if (h_pBuffer1.IsAllocated)
            {
                h_pBuffer1.Free();
            }
            if (h_pBuffer2.IsAllocated)
            {
                h_pBuffer2.Free();
            }

            bRecording = false;
            WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Stopped, 0);
            MsgQueue.Add(RecorderMsg.TERMINATING);
        }

        /// <summary>
        /// Method to prepare recording a wav, sets wave header.
        /// </summary>
        private void OnRecordingStart()
        {
            int bFailed = WinmmHook.waveInOpen(out hWaveIn, WinmmHook.WAVE_MAPPER, wavefmt, WaveInProc, 0, WinmmHook.CALLBACK_FUNCTION);
            if (bFailed != 0)
            {
                return;
            }

            h_pBuffer1 = GCHandle.Alloc(pBuffer1, GCHandleType.Pinned);
            waveHdr1.lpData = h_pBuffer1.AddrOfPinnedObject();
            waveHdr1.dwBufferLength = INP_BUFFER_SIZE;
            waveHdr1.dwBytesRecorded = 0;
            waveHdr1.dwUser = new IntPtr(0); //(IntPtr)GCHandle.Alloc(this);
            waveHdr1.dwFlags = 0;
            waveHdr1.dwLoops = 1;
            waveHdr1.lpNext = new IntPtr(0);
            waveHdr1.reserved = 0;

            WinmmHook.waveInPrepareHeader(hWaveIn, ref waveHdr1, Marshal.SizeOf(waveHdr1));

            h_pBuffer2 = GCHandle.Alloc(pBuffer2, GCHandleType.Pinned);
            waveHdr2.lpData = h_pBuffer2.AddrOfPinnedObject();
            waveHdr2.dwBufferLength = INP_BUFFER_SIZE;
            waveHdr2.dwBytesRecorded = 0;
            waveHdr2.dwUser = new IntPtr(0); //(IntPtr)GCHandle.Alloc(this);
            waveHdr2.dwFlags = 0;
            waveHdr2.dwLoops = 1;
            waveHdr2.lpNext = new IntPtr(0);
            waveHdr2.reserved = 0;

            WinmmHook.waveInPrepareHeader(hWaveIn, ref waveHdr2, Marshal.SizeOf(waveHdr2));

        }

        /// <summary>
        /// Method to set record ended to true
        /// </summary>
        private void OnRecordingStop()
        {
            bEnding = true;
        }

        /// <summary>
        /// Method to set record ended to true
        /// </summary>
        private void OnTerminating()
        {
            if (bRecording)
            {
                bEnding = true;
                return;
            }

            if (h_pBuffer1.IsAllocated)
            {
                h_pBuffer1.Free();
            }
            if (h_pBuffer2.IsAllocated)
            {
                h_pBuffer2.Free();
            }
            MsgQueue.Add(RecorderMsg.EXIT);
        }

        /// <summary>
        /// Method to terminate recording
        /// </summary>
        public void Dispose()
        {
            if (recorder.IsAlive)
            {
                MsgQueue.Add(RecorderMsg.TERMINATING);
                recorder.Join();
            }
        }

    }
}
