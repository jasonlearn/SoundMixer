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
    /// enum for Playback messages
    /// </summary>
    public enum PlayerMsg
    {
        WOM_OPEN,
        WOM_DONE,
        WOM_CLOSE,
        PLAYBACK_START,
        PLAYBACK_STOP,
        PLAYBACK_PAUSE,
        TERMINATING,
        EXIT
    }

    /// <summary>
    /// enum of playback status
    /// </summary>
    public enum PlaybackStatus
    {
        Recording,
        Playing,
        Paused,
        Stopped,
        Disabled
    }

    class Player : IDisposable
    {
        private BlockingCollection<PlayerMsg> MsgQueue;
        private Thread player;

        private WaveFile wave;
        private WaveFormat waveform;
        private WaveHdr pWaveHdr1;
        private byte[] pbuffer;
        private GCHandle h_pbuffer;
        private IntPtr hWaveOut;
        private bool bPlaying, bPaused, bTerminating;

        private WinmmHook.WaveDelegate WaveOutProc;
        private WaveWindow parentWindow;
        private IntPtr parentHandle;

        public Player(WaveWindow parent)
        {
            parentWindow = parent;
            parentHandle = parent.Handle;
            MsgQueue = new BlockingCollection<PlayerMsg>();
            hWaveOut = new IntPtr();
            h_pbuffer = new GCHandle();
            WaveOutProc = new WinmmHook.WaveDelegate(WOM_proc);
        }

        /// <summary>
        /// Destructor for player
        /// </summary>
        ~Player()
        {
            Dispose();
        }

        /// <summary>
        /// Play interface
        /// </summary>
        public bool Playing
        {
            get { return bPlaying; }
        }

        /// <summary>
        /// Mehod for data implementation.
        /// </summary>
        /// <param name="source"></param>
        public void setWave(WaveFile source)
        {
            wave = source;
            waveform = new WaveFormat(wave.sampleRate, wave.bitDepth, wave.channels);
            pbuffer = wave.getData();
        }

        /// <summary>
        /// Start playback interface
        /// </summary>
        public void PlaybackStart()
        {
            if (player != null && player.IsAlive)
            {
                return;
            }

            player = new Thread(new ThreadStart(threadProc));
            player.Start();

            MsgQueue.Add(PlayerMsg.PLAYBACK_START);
        }

        /// <summary>
        /// Stop playback interface
        /// </summary>
        public void PlaybackStop()
        {
            MsgQueue.Add(PlayerMsg.PLAYBACK_STOP);
            player.Join(); //should I block here until the player quits?
        }

        /// <summary>
        /// Pause playback interface
        /// </summary>
        public void PlaybackPause()
        {
            MsgQueue.Add(PlayerMsg.PLAYBACK_PAUSE);
        }


        private void WOM_proc(IntPtr hdrvr, int uMsg, int dwUser, ref WaveHdr wavhdr, int dwParam2)
        {
            switch (uMsg)
            {
                case WinmmHook.MM_WOM_OPEN:
                    MsgQueue.Add(PlayerMsg.WOM_OPEN);
                    break;
                case WinmmHook.MM_WOM_DONE:
                    MsgQueue.Add(PlayerMsg.WOM_DONE);
                    break;
                case WinmmHook.MM_WOM_CLOSE:
                    MsgQueue.Add(PlayerMsg.WOM_CLOSE);
                    break;
            }
        }

        /// <summary>
        /// Method to switch case to handle Player message
        /// </summary>
        private void threadProc()
        {
            PlayerMsg msg;
            msg = MsgQueue.Take();
            while (msg != PlayerMsg.EXIT)
            {
                switch (msg)
                {
                    case PlayerMsg.WOM_OPEN:
                        OnWomOpen();
                        break;
                    case PlayerMsg.WOM_DONE:
                        OnWomDone();
                        break;
                    case PlayerMsg.WOM_CLOSE:
                        OnWomClose();
                        break;
                    case PlayerMsg.PLAYBACK_START:
                        OnPlaybackStart();
                        break;
                    case PlayerMsg.PLAYBACK_PAUSE:
                        OnPlaybackPause();
                        break;
                    case PlayerMsg.PLAYBACK_STOP:
                        OnPlaybackStop();
                        break;
                    case PlayerMsg.TERMINATING:
                        OnTerminating();
                        break;
                }
                msg = MsgQueue.Take();
            }
        }

        /// <summary>
        /// Handles Open event of file
        /// </summary>
        private void OnWomOpen()
        {
            if (h_pbuffer.IsAllocated)
                h_pbuffer.Free();
            h_pbuffer = GCHandle.Alloc(pbuffer, GCHandleType.Pinned);  //handle (pointer) to the buffer

            pWaveHdr1.dwUser = (IntPtr)GCHandle.Alloc(this);    //pointer to this
            pWaveHdr1.dwBufferLength = pbuffer.Length;          //size of the buffer in bytes
            pWaveHdr1.lpData = h_pbuffer.AddrOfPinnedObject();  //IntPtr to buffer
            pWaveHdr1.dwFlags = 0;

            WinmmHook.waveOutPrepareHeader(hWaveOut, ref pWaveHdr1, Marshal.SizeOf(pWaveHdr1));
            WinmmHook.waveOutWrite(hWaveOut, ref pWaveHdr1, Marshal.SizeOf(pWaveHdr1));

            bPlaying = true;
            WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Playing, 0);
        }

        /// <summary>
        /// Handles event for terminating
        /// </summary>
        private void OnWomDone()
        {
            bTerminating = true;
            WinmmHook.waveOutUnprepareHeader(hWaveOut, ref pWaveHdr1, Marshal.SizeOf(pWaveHdr1));
            WinmmHook.waveOutClose(hWaveOut);
        }

        /// <summary>
        /// Handle event for closing file
        /// </summary>
        private void OnWomClose()
        {
            bPaused = false;
            bPlaying = false;
            WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Stopped, 0);

            if (bTerminating)
            {
                MsgQueue.Add(PlayerMsg.TERMINATING);
            }
        }

        private void OnPlaybackStart()
        {
            int val = WinmmHook.waveOutOpen(out hWaveOut, WinmmHook.WAVE_MAPPER, waveform, WaveOutProc, 0, WinmmHook.CALLBACK_FUNCTION);

            if (val == 11)
            {
                //invalid parameter.
                return;
            }
        }

        private void OnPlaybackPause()
        {
            if (!bPaused)
            {
                WinmmHook.waveOutPause(hWaveOut);
                bPaused = true;
                WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Paused, 0);
            }
            else
            {
                WinmmHook.waveOutRestart(hWaveOut);
                bPaused = false;
                WinmmHook.PostMessage(parentHandle, WinmmHook.WM_USER + 1, (int)PlaybackStatus.Playing, 0);
            }
        }

        private void OnPlaybackStop()
        {
            bTerminating = true;
            WinmmHook.waveOutReset(hWaveOut);
            WinmmHook.waveOutUnprepareHeader(hWaveOut, ref pWaveHdr1, Marshal.SizeOf(pWaveHdr1));
            WinmmHook.waveOutClose(hWaveOut);
        }

        private void OnTerminating()
        {
            if (bPlaying)
            {
                bTerminating = true;
                WinmmHook.waveOutReset(hWaveOut);
                return;
            }

            if (h_pbuffer.IsAllocated)
                h_pbuffer.Free();
            MsgQueue.Add(PlayerMsg.EXIT);
        }

        /// <summary>
        /// Method to dispose
        /// </summary>
        public void Dispose()
        {
            if (player != null && player.IsAlive)
            {
                MsgQueue.Add(PlayerMsg.TERMINATING);
                player.Join();
            }
        }
    }
}
