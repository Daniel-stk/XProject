using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Exceptions
{
    public class NoResponseException : Exception
    {
        public NoResponseException():base() { }
        public NoResponseException(string message) : base(message) { }
        public NoResponseException(string message, Exception inner) : base(message, inner) { }
    }
}
