using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Tools
{   
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true, Inherited = false)]
    public class UrlPropertyNameAttribute : Attribute
    {
        public UrlPropertyNameAttribute(string property)
        {
            _property = property;
        }

        protected string _property;
        public string Property
        {
            get { return _property; }
        }
    }
}
