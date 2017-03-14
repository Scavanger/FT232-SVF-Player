using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nelix.JTAG
{
    public class XsfvException : Exception
    {
        public XsfvException() { }

        public XsfvException(string message) : base(message)  {  }

        public XsfvException(string message, Exception innerException) : base(message, innerException) { }
    }
}
