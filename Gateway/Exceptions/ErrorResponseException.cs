using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Exceptions
{
    public class ErrorResponseException : Exception
    {
        public ErrorResponseException() : base() { }
        public ErrorResponseException(string message) : base(message) { }
        public ErrorResponseException(string message, Exception inner) : base(message, inner) { }
    }
}
