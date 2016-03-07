namespace Scrooge.Service.Definitions
{
    public interface ILoggingService
    {
        void Write(object o);

        void WriteLine(object o);
    }
}
