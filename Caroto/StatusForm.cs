using Caroto.EventHandlers;
using Caroto.RecurringTasks;
using Caroto.Services;
using System;
using System.Windows.Forms;

namespace Caroto
{
    public partial class StatusForm : Form
    {
        private AuthorizationService _authService;
        private MediaPlayerWindow _mediaPlayerWindow;
        private delegate void HideCallback();

        public StatusForm()
        {
            InitializeComponent();
            _authService = AuthorizationService.Instance;
            MessageHub.Instance.TriggerSequenceEvent += new TriggerSequenceEventHandler(HideStatusWindowOnTriggerPlayList);
            CreateMediaPlayerWindow();
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            _authService.Disconnect();
            Close();
        }

        private void Play_Click(object sender, EventArgs e)
        {
           _mediaPlayerWindow.ReproduccionManual();
            Hide();
        }

        private void MediaPlayerWindowClose(object sender, EventArgs e)
        {
            Show();
            CreateMediaPlayerWindow();
        }

        private void CreateMediaPlayerWindow()
        {
            _mediaPlayerWindow = new MediaPlayerWindow();
            _mediaPlayerWindow.WindowState = FormWindowState.Maximized;
            _mediaPlayerWindow.FormClosed += MediaPlayerWindowClose;
        }

        private void HideStatusWindowOnTriggerPlayList(object sender, TriggerSequenceEventArgs args)
        {
            OnHideWindow();
        }

        private void OnHideWindow()
        {
            if (InvokeRequired)
            {
                var callback = new HideCallback(OnHideWindow);
                Invoke(callback);
            }
            else
            {
                Hide();
            }
        }
    }
}
