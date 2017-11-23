
using Caroto.Exceptions;
using Caroto.RecurringTasks;
using Caroto.RecurringTasks.Tasks;
using DTO;
using Gateway;
using System;
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

        public void Disconnect()
        {
            CarotoSettings.Default.AccessToken = "";
            CarotoSettings.Default.BaseFolder = "";
            Properties.Settings.Default.ApiKey = "";
            Properties.Settings.Default.Identidad = "";
            Properties.Settings.Default.IsActivated = false;

            MessageHub.Instance.StopRecurringTask();
            TestSender.Instance.StopRecurringTask();
            TriggerSequenceTask.Instance.StopRecurringTask();

            CarotoSettings.Default.Save();
            Properties.Settings.Default.Save();
        }
    }
}
