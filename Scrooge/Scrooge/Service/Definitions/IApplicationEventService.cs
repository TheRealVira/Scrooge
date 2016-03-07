using Scrooge.Model.Internal;

namespace Scrooge.Service.Definitions
{
    public interface IApplicationEventService
    {
        IApplicationEventService RegisterApplicationEventListener(IApplicationEventListener listener);

        IApplicationEventService TriggerApplicationInitialized();

        IApplicationEventService TriggerApplicationClosing();
    }
}
