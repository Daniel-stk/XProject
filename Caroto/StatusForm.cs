using Caroto.Services;
using System;
using System.Windows.Forms;

namespace Caroto
{
    public partial class StatusForm : Form
    {
        private AuthorizationService _authService;
        private MediaPlayerWindow _mediaPlayerWindow;

        public StatusForm()
        {
            InitializeComponent();
            _authService = AuthorizationService.Instance;
            CreateMediaPlayerWindow();
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            _authService.Disconnect();
            Close();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            _mediaPlayerWindow.WindowState = FormWindowState.Maximized;
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
            _mediaPlayerWindow.FormClosed += MediaPlayerWindowClose;
        }
    }
}
