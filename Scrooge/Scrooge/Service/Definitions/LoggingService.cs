using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Service.Definitions
{
    public abstract class LoggingService
    {
        public abstract void Write(object o);

        public abstract void WriteLine(object o);
    }
}
