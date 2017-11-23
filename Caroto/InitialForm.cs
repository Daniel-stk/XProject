using Caroto.Exceptions;
using Caroto.RecurringTasks;
using Caroto.Services;
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
                if(!string.IsNullOrEmpty(apiKey.Text) && !string.IsNullOrEmpty(identidad.Text))
                {
                   await _authService.ExecuteAuthorization(apiKey.Text, identidad.Text);
                }
                else
                {
                    MessageBox.Show("Debe de ingresar el numero de cliente y el identificador de pantalla");
                }
            } 
            catch(Exception ex)
            {
                if(ex is ErrorResponseException)
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
            CarotoSettings.Default.BaseFolder = folder + @"\Communic";
            CarotoSettings.Default.Save();

            _composer.ComposeTasks();
            var statusForm = new StatusForm();
            statusForm.FormClosed += (s, args) => Close();
            statusForm.Show();
        }
    }
}
