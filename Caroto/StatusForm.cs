using Caroto.EventHandlers;
using Caroto.RecurringTasks;
using Caroto.Services;
using Caroto.Tools;
using Gateway;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Caroto
{
    public partial class StatusForm : Form
    {
        private AuthorizationService _authService;
        private VideoManagerService _videoService;
        private ProgrammingManagerService _programmingService;
        private MediaPlayerWindow _mediaPlayerWindow;
        private delegate void HideCallback();
        private delegate void UpdateVideoCountCallback(int videoCount);
        private delegate void ProgrammingUpdatedCallback(DateTime lastUpdate);
        private delegate void NextSequenceCreatedCallback(string text);
        private delegate void UpdateTotalTimeCallback();

        public StatusForm()
        {
            InitializeComponent();

            _authService = AuthorizationService.Instance;
            _videoService = VideoManagerService.Instance;
            _programmingService = ProgrammingManagerService.Instance;

            InitializeEvents();
            InitializeData();
            CreateMediaPlayerWindow();
        }

        private void InitializeEvents()
        {
            MessageHub.Instance.TriggerSequenceEvent += new TriggerSequenceEventHandler(HideStatusWindowOnTriggerPlayList);
            MessageHub.Instance.StopSequenceEvent += new StopSequenceEventHandler(UpdateTotalTime);
            MessageHub.Instance.VideoDownloadEvent += new VideoDownloadEventHandler(UpdateVideoCount);
            MessageHub.Instance.ProgrammingUpdatedEvent += new ProgrammingUpdatedEventHandler(UpdatedProgramming);
            MessageHub.Instance.NextSequenceCreatedEvent += new NextSequenceCreatedEventHandler(NextSequenceCreated);
            FormClosing += new FormClosingEventHandler(AppToTray);
        }

        private void InitializeData()
        {
            notifyIcon.Visible = false;
            videosAlmacenados.Text = _videoService.GetCountVideosOnFolder().ToString();
            ultimaActualizacion.Text = Properties.Settings.Default.LastUpdate.ToString();
            proximaReproduccion.Text = Properties.Settings.Default.NextSequence;
            if (CarotoSettings.Default.TotalTime == null)
            {
                CarotoSettings.Default.TotalTime = new TimeSpan(0, 0, 0);
                CarotoSettings.Default.Save();
            }
            tiempoTotal.Text = CarotoSettings.Default.TotalTime.ToString();
        }

        private void Desconectar_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("¿Desea desconectar el servicio de reproducción automática del servidor? (Se borrara toda la información)", "Confrimación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _authService.Disconnect();
                Close();
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
           if(_mediaPlayerWindow.ReproduccionManual())
                Hide();
        }

        private void MediaPlayerWindowClose(object sender, EventArgs e)
        {
            //Show();
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

        private void UpdateVideoCount(object sender,VideoDownloadEventArgs args)
        {
            OnUpdateVideoCount(args.VideoCount);
        }

        private void UpdatedProgramming(object sender,ProgrammingUpdatedEventArgs args)
        {
            OnProgrammingUpdated(args.LastUpdate);
        }

        private void UpdateTotalTime(object sender,StopSequenceEventArgs args)
        {
            OnUpdateTotalTime();
        }

        private void NextSequenceCreated(object sender, NextSequenceCreatedEventArgs args)
        {
            OnNextSequenceCreated(args.NextSequenceStart);
        }

        private void OnUpdateTotalTime()
        {
            if (InvokeRequired)
            {
                var callback = new UpdateTotalTimeCallback(OnUpdateTotalTime);
                Invoke(callback);
            }
            else
            {
                tiempoTotal.Text = CarotoSettings.Default.TotalTime.ToString();
            }
        }

        private void OnNextSequenceCreated(string nextSequenceStart)
        {
            if (InvokeRequired)
            {
                var callback = new NextSequenceCreatedCallback(OnNextSequenceCreated);
                Invoke(callback, new object[] { nextSequenceStart });
            }
            else
            {
                proximaReproduccion.Text = nextSequenceStart;
            }
        }

        private void OnProgrammingUpdated(DateTime lastUpdate)
        {
            if (InvokeRequired)
            {
                var callback = new ProgrammingUpdatedCallback(OnProgrammingUpdated);
                Invoke(callback, new object[] { lastUpdate });
            }
            else
            {
                ultimaActualizacion.Text = lastUpdate.ToString();
            }
        }

        private void OnUpdateVideoCount(int videoCount)
        {
            if (InvokeRequired)
            {
                var callback = new UpdateVideoCountCallback(OnUpdateVideoCount);
                Invoke(callback, new object[] { videoCount });
            }
            else
            {
                videosAlmacenados.Text = videoCount.ToString();
            }
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

        private void AppToTray(object sender, FormClosingEventArgs args)
        {
            if (args.CloseReason == CloseReason.UserClosing && Properties.Settings.Default.IsActivated)
            {
                notifyIcon.Visible = true;
                Hide();
                notifyIcon.ShowBalloonTip(500);
                args.Cancel = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Visible)
            {
                Show();
                notifyIcon.Visible = false;
            }
        }

        private void Ver_Click(object sender, EventArgs e)
        {
            try
            {
                if(Directory.Exists(CarotoSettings.Default.VideoFolder + @"\videos\"))
                {
                    Process.Start(CarotoSettings.Default.VideoFolder + @"\videos\");
                }
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                MessageBox.Show("Error al abrir carpeta de videos "+ex.ToString());
            }
        }

        private async void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                await _programmingService.CreateProgrammingManual(Properties.Settings.Default.ApiKey, Properties.Settings.Default.Identidad);
            }
            catch (Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                MessageBox.Show("Error al actualizar programación de manera manual" + ex.Message);
            }
        }
    }
}
