using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Exceptions
{
    public class NullResponseException : Exception
    {
        public NullResponseException() : base() { }
        public NullResponseException(string message) : base(message) { }
        public NullResponseException(string message, Exception inner) : base(message, inner) { }
    }
}
