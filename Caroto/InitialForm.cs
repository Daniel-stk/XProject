﻿using Caroto.Exceptions;
using Caroto.RecurringTasks;
using Caroto.Services;
using Caroto.Tools;
using Gateway;
using Gateway.Exceptions;
using System;
using System.Windows.Forms;

namespace Caroto
{
    public partial class InitialForm : Form
    {
        private AuthorizationService _authService;
        private ComposeRecurringTasks _composer;
        public InitialForm(ComposeRecurringTasks composer)
        {
            InitializeComponent();
            _authService = AuthorizationService.Instance;
            _composer = composer;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void Iniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(apiKey.Text) && !string.IsNullOrEmpty(identidad.Text))
                {
                    Iniciar.Enabled = false;
                    await _authService.ExecuteAuthorization(apiKey.Text, identidad.Text);
                }
                else
                {
                    MessageBox.Show("Debe de ingresar el numero de cliente y el identificador de pantalla");
                }
            } 
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On Iniciar_Click Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                if (ex is ErrorResponseException)
                {
                    MessageBox.Show("Se encontrar dificultades en la autentificación - " + ex.Message);
                }
                else if(ex is NoResponseException)
                {
                    MessageBox.Show("Se encontro un error en el servidor - " + ex.Message);
                }
                else if(ex is NullResponseException)
                {
                    MessageBox.Show("Se encontro un problema en la comunicación - " + ex.Message);
                }
                else
                {
                    throw ex;
                }
                Iniciar.Enabled = true;
            }
            if (Properties.Settings.Default.IsActivated)
            {
                ActivateStatusForm();
            }  
        }

        private void ActivateStatusForm()
        {
            Hide();
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
            System.IO.Directory.CreateDirectory(folder + @"\Communic");
            System.IO.Directory.CreateDirectory(folder + @"\Communic\Videos");
            System.IO.Directory.CreateDirectory(folder + @"\Communic\SiguientePlaylist");
            System.IO.Directory.CreateDirectory(folder + @"\Communic\Programacion");

            CarotoSettings.Default.BaseFolder = folder + @"\Communic";
            CarotoSettings.Default.VideoFolder = folder + @"\Communic\Videos";
            CarotoSettings.Default.NextSequenceFolder = folder + @"\Communic\SiguientePlaylist";
            CarotoSettings.Default.ProgrammingFolder = folder + @"\Communic\Programacion";
#if DEBUG
            var logFolder = @"C:\CommunicLog\";
            System.IO.Directory.CreateDirectory(logFolder);
            CarotoSettings.Default.LogFolder = logFolder;
#endif
            CarotoSettings.Default.Save();

            _composer.ComposeTasks();
            var statusForm = new StatusForm();
            statusForm.FormClosed += (s, args) => Close();
            statusForm.Show();
        }
    }
}
