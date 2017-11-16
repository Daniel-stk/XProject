using DTO;
using Gateway;
using Gateway.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Caroto.Services
{
    
    public class AuthorizationService
    {
        private AuthorizationGateway _gateway;

        public AuthorizationService()
        {
            _gateway = new AuthorizationGateway();
        }

        public async void ExecuteAuthorization(string apiKey,string identidad)
        {
            var authDto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad};
            try
            {
                var accessTokenResponse = await _gateway.GetAccessToken(authDto);
            }
            catch (Exception exception)
            {
                if(exception is ErrorResponseException)
                {
                }
                if(exception is NoResponseException)
                {
                }
            }
        }
    }
}
