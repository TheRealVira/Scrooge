using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrooge.Model.Internal;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations
{
    public class GUIApplicationEventService : IApplicationEventService
    {
        private readonly IList<IApplicationEventListener> listenerList = new List<IApplicationEventListener>(); 

        public IApplicationEventService RegisterApplicationEventListener(IApplicationEventListener listener)
        {
            this.listenerList.Add(listener);
            return this;
        }

        public IApplicationEventService TriggerApplicationInitialized()
        {
            foreach (var applicationEventListener in this.listenerList)
            {
                applicationEventListener.ApplicationInitialized();
            }

            return this;
        }

        public IApplicationEventService TriggerApplicationClosing()
        {
            foreach (var applicationEventListener in this.listenerList)
            {
                applicationEventListener.ApplicationClosing();
            }

            return this;
        }
    }
}
