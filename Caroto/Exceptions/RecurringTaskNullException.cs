using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Exceptions
{
    public class RecurringTaskNullException : Exception
    {
        public RecurringTaskNullException() : base() { }
        public RecurringTaskNullException(string message) : base(message) { }
        public RecurringTaskNullException(string message, Exception inner) : base(message, inner) { }
    }
}
