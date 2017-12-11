using DTO;
using Gateway.Exceptions;
using Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Responses.Tools.ResponseExtensions;

namespace Gateway
{
    public class ProgrammingManagerGateway
    {
        private HttpRequestSender _request;

        public ProgrammingManagerGateway()
        {
            _request = HttpRequestSender.Instance;
        }

        public async Task<List<ProgrammingResponse>> GetProgramming(AuthorizationDto dto)
        {
            var response = await _request.GetAsync<ProgrammingResponse>("Caroto/API/GenerarProgramacion", dto,ResponseType.complex);
            if (response != null)
            {
                var complexResponse = response.ConvertToComplexResponse<ProgrammingResponse>();
                if (complexResponse.NoResponse == 1)
                {
                    throw new NoResponseException(complexResponse.Message);
                }
                else if (complexResponse.Error == 1)
                {
                    throw new ErrorResponseException(complexResponse.Message);
                }
                else if (complexResponse.Success == 1)
                {
                    return complexResponse.Data;
                }
            }
            return null;
        }
    }
}
