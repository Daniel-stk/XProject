﻿using AxWMPLib;
using Caroto.DomainObjects;
using Caroto.EventHandlers;
using Caroto.RecurringTasks;
using Caroto.Services;
using Caroto.Tools;
using Gateway;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using WMPLib;

namespace Caroto
{
    public partial class MediaPlayerWindow : Form
    {
        private delegate void ShowAndPlayCallback(IWMPPlaylist playlist,Sequence sequence);
        private delegate void StopMediaPlayerCallback();

        private WindowsMediaPlayerService _service;
        private VideoManagerService _videoService;

        private string _currentPlayList;
        private Queue<PlayListData> playListQueueData;
        private Timer waitToPlayNext;


        public MediaPlayerWindow()
        {
            InitializeComponent();
            _service = WindowsMediaPlayerService.Instace;
            _service.SetMediaPlayerInstance(WindowsMediaPlayer);

            _videoService = VideoManagerService.Instance;

            waitToPlayNext = new Timer();
            waitToPlayNext.Interval = 1000;
            waitToPlayNext.Tick += (sender, args) => { waitToPlayNext.Stop(); PlayNextSequenceOnQueue(); };

            notifyWMP.Visible = false;

            MessageHub.Instance.TriggerSequenceEvent += new TriggerSequenceEventHandler(TriggerPlayList);
            MessageHub.Instance.StopSequenceEvent += new StopSequenceEventHandler(StopPlayList);

            playListQueueData = new Queue<PlayListData>();
            WindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(PlayStateChanged);
        }

        public bool ReproduccionManual()
        {
            try { 
                var sequence = _service.GetNextSequence();
                if(sequence != null && _videoService.GetCountVideosOnFolder() > 0)
                {
                    var playList = _service.ComposePlaylist(sequence.PlayList);
                    if (WindowsMediaPlayer.playState == WMPPlayState.wmppsPlaying)
                    {
                        EnqueuePlaylist(playList, sequence);
                        return true;
                    }
                    else
                    {
                        Play(playList, sequence.OnLoop);
                        return true;
                    }
                }
                else
                {
                    ShowNotification("CommunicTv Player", "No hay más programaciónes o videos descargados");
                    Close();
                    return false;
                }
            }
            catch(Exception ex)
            {
                ShowNotification("CommunicTv Player Error",ex.Message);
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                return false;
            }
        }

        private void TriggerPlayList(object sender, TriggerSequenceEventArgs args)
        {
            try
            {
                var playlist = _service.ComposePlaylist(args.Sequence.PlayList);
                OnWindowShowAndPlay(playlist, args.Sequence);
            }
            catch (Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
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
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }
        }

        private void OnWindowShowAndPlay(IWMPPlaylist playlist,Sequence sequence)
        {
            if (InvokeRequired)
            {
                var callback = new ShowAndPlayCallback(OnWindowShowAndPlay);
                Invoke(callback, new object[] { playlist, sequence });
            }
            else
            {
                if (string.IsNullOrEmpty(_currentPlayList))
                {
                    _currentPlayList = sequence.SequenceName;
                    Play(playlist,sequence.OnLoop);
                }
                else
                {
                    if(WindowsMediaPlayer.playState == WMPPlayState.wmppsPlaying)
                    {
                        if (string.Equals(_currentPlayList, "default_sequence"))
                        {
                            _currentPlayList = sequence.SequenceName;                           
                            StopMediaPlayer();
                            Play(playlist,sequence.OnLoop);
                        }
                        else
                        {
                            EnqueuePlaylist(playlist, sequence);
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
                if (!ActionsBeforeClose())
                {
                    Close();//Posible change to play default sequence
                }
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
                  
                    break;
                case 9:
                    Console.WriteLine("Transitioning");
                    break;
                case 10:
                    Console.WriteLine("Ready");
                    if (!WindowsMediaPlayer.settings.getMode("loop"))
                    {
                        MessageHub.Instance.PublishMessage("Stop sequence");
                    }
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

        private void EnqueuePlaylist(IWMPPlaylist playlist, Sequence sequence)
        {
            playListQueueData.Enqueue(new PlayListData { PlayList = playlist, OnLoop = sequence.OnLoop, SequenceName = sequence.SequenceName, TotalSequenceDuration = sequence.TotalSequenceDuration, TimeToPlay = sequence.TimeToPlay, EndTime = sequence.EndTime });
            ShowNotification("CommunicTv Player Reproductor activo", "Se agrego lista de reproducción " + sequence.SequenceName + " a la fila de reporducción");

        }

        private bool ActionsBeforeClose()
        {
            if (playListQueueData.Count > 0)
            {
                waitToPlayNext.Start();
                return true;
            }
            return false;
        }

        private void PlayNextSequenceOnQueue()
        {
            var playListData = playListQueueData.Dequeue();

            if (_service.CreateCurrentSequenceJson(playListData))
            {
                _currentPlayList = playListData.SequenceName;
                CarotoSettings.Default.TotalTime += TimeSpan.ParseExact(playListData.TotalSequenceDuration, @"hh\:mm\:ss", CultureInfo.InvariantCulture);
                Play(playListData.PlayList, playListData.OnLoop);
            }
            else
            {
                ShowNotification("Error", "No se puede recuperar playlist en fila "+playListData.SequenceName);
            }
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

        private void ShowNotification(string title, string body)
        {
            notifyWMP.Visible = true;

            if (title != null)
            {
                notifyWMP.BalloonTipTitle = title;
            }

            if (body != null)
            {
                notifyWMP.BalloonTipText = body;
            }

            notifyWMP.ShowBalloonTip(5000);
        }
    }
}
