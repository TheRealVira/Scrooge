using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public abstract class StorageService
    {
        public abstract StorageService AddBooking(Booking booking);
    }
}
