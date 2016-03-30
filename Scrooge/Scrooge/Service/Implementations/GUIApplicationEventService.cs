using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scrooge.Model.Internal;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations
{
    public class GUIApplicationEventService : IApplicationEventService
    {
        private readonly IList<Func<Task<bool>>> closeRequestHandlers = new List<Func<Task<bool>>>();
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

        public async Task<bool> ApplicationCloseRequest()
        {
            foreach (var closeRequestHandler in closeRequestHandlers)
            {
                if (!await closeRequestHandler())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Registers a close request handler. Important: The handler should return true when closing should proceed, false
        ///     when it should be aborted. And remember: With great power comes great responsibility!
        /// </summary>
        public IApplicationEventService RegisterApplicationCloseRequestHandler(Func<Task<bool>> handler)
        {
            this.closeRequestHandlers.Add(handler);
            return this;
        }
    }
}