using DTO;
using Gateway.Exceptions;
using Responses;
using Responses.Tools;
using System.Threading.Tasks;

namespace Gateway
{
    public class AuthorizationGateway
    {
        private HttpRequestSender _request;
        public AuthorizationGateway()
        {
            _request = new HttpRequestSender();
        }

        public async Task<AccessTokenResponse> GetAccessToken(AuthorizationDto dto)
        {
            var response = await _request.GetAsync<AccessTokenResponse>("/Caroto/API/IdentificarPantalla", dto);
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
    }
}
