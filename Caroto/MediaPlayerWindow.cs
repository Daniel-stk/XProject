using Caroto.EventHandlers;
using Caroto.RecurringTasks;
using Caroto.Services;
using System;
using System.Windows.Forms;

namespace Caroto
{
    public partial class MediaPlayerWindow : Form
    {
        private delegate void ShowCallback();
        private WindowsMediaPlayerService _service;

        public MediaPlayerWindow()
        {
            InitializeComponent();
            _service = WindowsMediaPlayerService.Instace;
            _service.SetMediaPlayerInstance(WindowsMediaPlayer);
            WindowsMediaPlayer.uiMode = "none";
            MessageHub.Instance.TriggerSequenceEvent += new TriggerSequenceEventHandler(TriggerPlayList);
        }

        public void ReproduccionManual()
        {
            Show();
        }

        private void TriggerPlayList(object sender, TriggerSequenceEventArgs args)
        {
            var playlist = _service.ComposePlaylist(args.PlayList);
            OnWindowShow();
        }

        private void OnWindowShow()
        {
            if (InvokeRequired)
            {
                var callback = new ShowCallback(OnWindowShow);
                Invoke(callback);
            }
            else
            {
                Show();
            }
        }
    }

}
