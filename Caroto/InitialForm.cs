using Caroto.Exceptions;
using Caroto.Services;
using Gateway.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroto
{
    public partial class InitialForm : Form
    {
        private AuthorizationService _authService;
        public InitialForm()
        {
            InitializeComponent();
            _authService = AuthorizationService.Instance;
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
                Hide();
                var statusForm = new StatusForm();
                statusForm.FormClosed += (s, args) => Close();
                statusForm.Show();
            }  
        }
    }
}
