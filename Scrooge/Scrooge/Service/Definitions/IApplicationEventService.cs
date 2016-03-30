using System;
using System.Threading.Tasks;
using Scrooge.Model.Internal;

namespace Scrooge.Service.Definitions
{
    public interface IApplicationEventService
    {
        IApplicationEventService RegisterApplicationEventListener(IApplicationEventListener listener);

        IApplicationEventService TriggerApplicationInitialized();

        IApplicationEventService TriggerApplicationClosing();

        Task<bool> ApplicationCloseRequest();

        IApplicationEventService RegisterApplicationCloseRequestHandler(Func<Task<bool>> handler);
    }
}