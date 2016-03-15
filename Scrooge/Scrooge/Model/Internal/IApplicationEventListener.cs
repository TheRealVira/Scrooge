using System.Threading.Tasks;

namespace Scrooge.Model.Internal
{
    public interface IApplicationEventListener
    {
        Task ApplicationInitialized();

        Task ApplicationClosing();
    }
}
