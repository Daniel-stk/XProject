using Caroto.Exceptions;
using DTO;
using Gateway;
using Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Services
{
    public class ProgrammingManagerService
    {
        private static readonly Lazy<ProgrammingManagerService> _instance = new Lazy<ProgrammingManagerService>(() => new ProgrammingManagerService());
        private ProgrammingManagerGateway _gateway;

        private ProgrammingManagerService()
        {
            _gateway = new ProgrammingManagerGateway();
        }

        public static ProgrammingManagerService Instance { get { return _instance.Value; } }

        public async Task<List<ProgrammingResponse>> CreateProgrammingInformation(string apiKey,string identidad)
        {
            var authDto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad };
            var programming = await _gateway.GetProgramming(authDto);
            if(programming == null)
            {
                throw new NullResponseException("No hubo respuesta exitosa de parte del servidor");
            }
            if (programming.Any())
            {
                return programming;
            }
            else
            {
                throw new NullResponseException("Creación de programación fallida");

            }
        }
    }
}
