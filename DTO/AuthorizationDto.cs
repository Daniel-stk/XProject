using DTO.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AuthorizationDto : Dto
    {
        [UrlPropertyName("api_key")]
        public string ApiKey { get; set; }
        [UrlPropertyName("identidad")]
        public string Identidad { get; set; }
    }
}
