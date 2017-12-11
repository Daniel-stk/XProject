using DTO;
using Gateway.Exceptions;
using Responses;
using Responses.Tools;
using System.Threading.Tasks;
using static Responses.Tools.ResponseExtensions;

namespace Gateway
{
    public class AuthorizationGateway
    {
        private HttpRequestSender _request;
        public AuthorizationGateway()
        {
            _request = HttpRequestSender.Instance;
        }

        public async Task<AccessTokenResponse> GetAccessToken(AuthorizationDto dto)
        {
            var response = await _request.GetAsync<AccessTokenResponse>("Caroto/API/IdentificarPantalla", dto,ResponseType.simple);
            if(response != null)
            {
                var simpleResponse = response.ConvertToSimpleResponse<AccessTokenResponse>();
                if (simpleResponse.NoResponse == 1)
                {
                    throw new NoResponseException(simpleResponse.Message);
                }
                else if(simpleResponse.Error == 1)
                {
                    throw new ErrorResponseException(simpleResponse.Message);
                }
                else if(simpleResponse.Success == 1)
                {
                    return simpleResponse.Data;
                }
                
            }
            return null;
        }

        public async Task<string> GetServerFolder(AuthorizationDto dto) 
        {
            var response = await _request.GetAsync<string>("Caroto/API/Folder", dto, ResponseType.simple);
            if(response != null)
            {
                var simpleResponse = response.ConvertToSimpleResponse<string>();
                if (simpleResponse.NoResponse == 1)
                {
                    throw new NoResponseException(simpleResponse.Message);
                }
                else if (simpleResponse.Error == 1)
                {
                    throw new ErrorResponseException(simpleResponse.Message);
                }
                else if (simpleResponse.Success == 1)
                {
                    return simpleResponse.Data;
                }
            }
            return null;
        }
    }
}
