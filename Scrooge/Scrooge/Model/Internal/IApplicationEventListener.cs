using System.Threading.Tasks;

namespace Scrooge.Model.Internal
{
    public interface IApplicationEventListener
    {
        void ApplicationInitialized();

        void ApplicationClosing();
    }
}
