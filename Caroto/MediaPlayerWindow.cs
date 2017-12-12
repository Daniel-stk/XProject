using AxWMPLib;
using Caroto.DomainObjects;
using Caroto.EventHandlers;
using Caroto.RecurringTasks;
using Caroto.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WMPLib;

namespace Caroto
{
    public partial class MediaPlayerWindow : Form
    {
        private delegate void ShowAndPlayCallback(IWMPPlaylist playlist,bool loop,string sequenceName);
        private delegate void StopMediaPlayerCallback();

        private WindowsMediaPlayerService _service;
        private string _currentPlayList;
        private Queue<PlayListData> playListQueueData;


        public MediaPlayerWindow()
        {
            InitializeComponent();
            _service = WindowsMediaPlayerService.Instace;
            _service.SetMediaPlayerInstance(WindowsMediaPlayer);

            MessageHub.Instance.TriggerSequenceEvent += new TriggerSequenceEventHandler(TriggerPlayList);
            MessageHub.Instance.StopSequenceEvent += new StopSequenceEventHandler(StopPlayList);

            playListQueueData = new Queue<PlayListData>();
            WindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(PlayStateChanged);
        }

        public void ReproduccionManual()
        {
            Show();
        }

        private void TriggerPlayList(object sender, TriggerSequenceEventArgs args)
        {
            try
            {
                var playlist = _service.ComposePlaylist(args.PlayList);
                OnWindowShowAndPlay(playlist, args.OnLoop, args.SequenceName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void StopPlayList(object sender, StopSequenceEventArgs args)
        {
            try
            {
                StopMediaPlayer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnWindowShowAndPlay(IWMPPlaylist playlist,bool loop,string sequenceName)
        {
            if (InvokeRequired)
            {
                var callback = new ShowAndPlayCallback(OnWindowShowAndPlay);
                Invoke(callback, new object[] { playlist, loop, sequenceName });
            }
            else
            {
                if (string.IsNullOrEmpty(_currentPlayList))
                {
                    _currentPlayList = sequenceName;
                    Play(playlist,loop);
                }
                else
                {
                    if(WindowsMediaPlayer.playState == WMPPlayState.wmppsPlaying)
                    {
                        if (string.Equals(_currentPlayList, "default_sequence"))
                        {
                            _currentPlayList = sequenceName;                           
                            StopMediaPlayer();
                            Play(playlist,loop);
                        }
                        else
                        {
                            playListQueueData.Enqueue(new PlayListData { PlayList = playlist, OnLoop = loop, SequenceName = sequenceName});
                        }
                    }
                }
            }
        }

        private void StopMediaPlayer()
        {
            if (InvokeRequired)
            {
                var callback = new StopMediaPlayerCallback(StopMediaPlayer);
                Invoke(callback);
            }
            else
            {
                WindowsMediaPlayer.Ctlcontrols.stop();
            }
        }

        private void PlayStateChanged(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 0:
                    Console.WriteLine("Undefinded");
                    break;
                case 1:
                    Console.WriteLine("Stopped");
                    if (!ActionsBeforeClose())
                    {
                        Close();//Posible change to play default sequence
                    }
                    break;
                case 2:
                    Console.WriteLine("Paused");
                    break;
                case 3:
                    Console.WriteLine("Playing");
                    WindowsMediaPlayer.fullScreen = true;
                    break;
                case 4:
                    Console.WriteLine("ScanForward");
                    break;
                case 5:
                    Console.WriteLine("ScanReverse");
                    break;
                case 6:
                    Console.WriteLine("Buffering");
                    break;
                case 7:
                    Console.WriteLine("Waiting");
                    break;
                case 8:
                    Console.WriteLine("MediaEnded");
                    //if (!ActionsBeforeClose())
                    //{
                    //    Close();//Posible change to play default sequence
                    //}
                    break;
                case 9:
                    Console.WriteLine("Transitioning");
                    break;
                case 10:
                    Console.WriteLine("Ready");
                    break;
                case 11:
                    Console.WriteLine("Reconnecting");
                    break;
                case 12:
                    Console.WriteLine("Last");
                    break;
                default:
                    Console.WriteLine("Estado de reproductor desconocido");
                    break;
            }
        }

        private bool ActionsBeforeClose()
        {
            if (playListQueueData.Count > 0)
            {
                var playListData = playListQueueData.Dequeue();
                _currentPlayList = playListData.SequenceName;
                Play(playListData.PlayList, playListData.OnLoop);
                return true;
            }
            return false;
        }

        private void Play(IWMPPlaylist playlist,bool loop)
        {
            if (!Visible)
            { 
                Show();
            }
            WindowsMediaPlayer.settings.setMode("loop", loop);
            WindowsMediaPlayer.uiMode = "none";
            WindowsMediaPlayer.currentPlaylist = playlist;
            WindowsMediaPlayer.Ctlcontrols.play();
        }
    }
}
