
using Caroto.Exceptions;
using Caroto.RecurringTasks;
using Caroto.RecurringTasks.Tasks;
using Caroto.Tools;
using DTO;
using Gateway;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Caroto.Services
{
    
    public class AuthorizationService
    {
        private static readonly Lazy<AuthorizationService> _instance = new Lazy<AuthorizationService>(() => new AuthorizationService()); 
        private AuthorizationGateway _gateway;

        public static AuthorizationService Instance { get { return _instance.Value; } }

        private AuthorizationService()
        {
            _gateway = new AuthorizationGateway();
        }

        public async Task<int> ExecuteAuthorization(string apiKey,string identidad)
        {
            var authDto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad};
            var accessTokenResponse = await _gateway.GetAccessToken(authDto);
            if(accessTokenResponse == null)
            {
                throw new NullResponseException("No hubo respuesta exitosa de parte del servidor");
            }
            if (!string.IsNullOrEmpty(accessTokenResponse.AccessToken))
            {
                CarotoSettings.Default.AccessToken = accessTokenResponse.AccessToken;
                Properties.Settings.Default.ApiKey = apiKey;
                Properties.Settings.Default.Identidad = identidad;
                Properties.Settings.Default.IsActivated = true;

                CarotoSettings.Default.Save();
                Properties.Settings.Default.Save();
            }
            else
            {
                throw new NullResponseException("Autentificación no exitosa");
            }

            return 1;
        }

        public async Task<string> ServerFolder(string apiKey,string identidad)
        {
            var authDto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad };
            var folder = await _gateway.GetServerFolder(authDto);
            if (!string.IsNullOrEmpty(folder))
            {
                return folder;
            }
            else
            {
                throw new NullResponseException("Folder no encontrado");
            }
        }

        public void Disconnect()
        {
            MessageHub.Instance.StopRecurringTask();
            TriggerSequenceTask.Instance.StopRecurringTask();
            StopSequenceTask.Instance.StopRecurringTask();
            VideoDownloadListComposerTask.Instance.StopRecurringTask();
            VideoDownloadTask.Instance.StopRecurringTask();
            GenerateProgrammingTask.Instance.StopRecurringTask();
            ComposeNextSequenceTask.Instance.StopRecurringTask();

            CarotoSettings.Default.VideoFolder = "";
            CarotoSettings.Default.ProgrammingFolder = "";
            CarotoSettings.Default.VideoFolder = "";
            CarotoSettings.Default.NextSequenceFolder = "";
            CarotoSettings.Default.TotalTime = new TimeSpan(0, 0, 0);

            Properties.Settings.Default.NextSequence = "";
            Properties.Settings.Default.LastUpdate = new DateTime();
            Properties.Settings.Default.ApiKey = "";
            Properties.Settings.Default.Identidad = "";
            Properties.Settings.Default.IsActivated = false;
            CarotoSettings.Default.AccessToken = "";

          

            try
            { 
                Directory.Delete(CarotoSettings.Default.BaseFolder, true);
            }
            catch(Exception ex)
            {
#if DEBUG
                FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + "On Disconnect Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
            }

#if DEBUG
            CarotoSettings.Default.LogFolder = "";
#endif
            CarotoSettings.Default.BaseFolder = "";
            CarotoSettings.Default.Save();
            Properties.Settings.Default.Save();

        }
    }
}
