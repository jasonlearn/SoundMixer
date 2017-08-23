using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JasonChan_SoundWaveProject
{
    /// <summary>
    /// struct WaveHeader built using Win32
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WaveHdr
    {
        public IntPtr lpData;
        public int dwBufferLength;
        public int dwBytesRecorded;
        public IntPtr dwUser;
        public int dwFlags;
        public int dwLoops;
        public IntPtr lpNext;
        public int reserved;
    }
    /// <summary>
    /// struct WaveFormat built using Win32
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class WaveFormat
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;

        public WaveFormat(int rate, int bits, int channels)
        {
            wFormatTag = (short)1;
            nChannels = (short)channels;
            nSamplesPerSec = rate;
            wBitsPerSample = (short)bits;
            cbSize = 0;

            nBlockAlign = (short)(channels * (bits / 8));
            nAvgBytesPerSec = nSamplesPerSec * nBlockAlign;
        }
    }

    /// <summary>
    /// Recording and Playing Sound with the Waveform Audio Interface
    /// </summary>
    class WinmmHook
    {
        public const int MMSYSERR_BASE = 0;
        public const int MMSYSERR_NOERROR = 0; // no error
        public const int MMSYSERR_ERROR = (MMSYSERR_BASE + 1);  /* unspecified error */
        public const int MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2);  /* device ID out of range */
        public const int MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3);  /* driver failed enable */
        public const int MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4);  /* device already allocated */
        public const int MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5);  /* device handle is invalid */
        public const int MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6);  /* no device driver present */
        public const int MMSYSERR_NOMEM = (MMSYSERR_BASE + 7);  /* memory allocation error */
        public const int MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8);  /* function isn't supported */
        public const int MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9);  /* error value out of range */
        public const int MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10); /* invalid flag passed */
        public const int MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11); /* invalid parameter passed */
        public const int MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12); /* handle being used */
                                                                     /* simultaneously on another */
                                                                     /* thread (eg callback) */

        public const int WAVERR_BASE = 32;
        public const int WAVERR_BADFORMAT = (WAVERR_BASE + 0);   /* unsupported wave format */
        public const int WAVERR_STILLPLAYING = (WAVERR_BASE + 1);   /* still something playing */
        public const int WAVERR_UNPREPARED = (WAVERR_BASE + 2);   /* header not prepared */
        public const int WAVERR_SYNC = (WAVERR_BASE + 3);   /* device is synchronous */
        public const int WAVERR_LASTERROR = (WAVERR_BASE + 3);   /* last error in range */

        public const int MM_WOM_OPEN = 0x3BB;
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;
        public const int MM_WIM_OPEN = 0x3BE;
        public const int MM_WIM_CLOSE = 0x3BF;
        public const int MM_WIM_DATA = 0x3C0;

        public const int CALLBACK_FUNCTION = 0x00030000;    // dwCallback is a FARPROC 

        public const int WAVE_MAPPER = -1;

        public const int TIME_MS = 0x0001;  // time in milliseconds 
        public const int TIME_SAMPLES = 0x0002;  // number of wave samples 
        public const int TIME_BYTES = 0x0004;  // current byte offset 

        public const int WM_USER = 0x0400;




        // callbacks
        public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WaveHdr wavhdr, int dwParam2);

        private const string mmdll = "winmm.dll";
        // native calls
        [DllImport(mmdll)]
        public static extern int waveOutGetNumDevs();
        [DllImport(mmdll)]
        public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveOutWrite(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport(mmdll)]
        public static extern int waveOutReset(IntPtr hWaveOut);
        [DllImport(mmdll)]
        public static extern int waveOutClose(IntPtr hWaveOut);
        [DllImport(mmdll)]
        public static extern int waveOutPause(IntPtr hWaveOut);
        [DllImport(mmdll)]
        public static extern int waveOutRestart(IntPtr hWaveOut);
        [DllImport(mmdll)]
        public static extern int waveOutGetPosition(IntPtr hWaveOut, out int lpInfo, int uSize);
        [DllImport(mmdll)]
        public static extern int waveOutSetVolume(IntPtr hWaveOut, int dwVolume);
        [DllImport(mmdll)]
        public static extern int waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

        [DllImport(mmdll)]
        public static extern int waveInOpen(out IntPtr hWaveIn, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport(mmdll)]
        public static extern int waveInReset(IntPtr hWaveIn);
        [DllImport(mmdll)]
        public static extern int waveInClose(IntPtr hWaveIn);
        [DllImport(mmdll)]
        public static extern int waveInStop(IntPtr hWaveIn);
        [DllImport(mmdll)]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);
        [DllImport(mmdll)]
        public static extern int waveInStart(IntPtr hWaveIn);

        [DllImport("User32.dll")]
        public static extern int PostMessage(IntPtr hwnd, uint msg, int wParam, int lParam);
    }
}
