using System;
using Scrooge.Model;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations
{
    public class MockupStorageService : StorageService
    {
        public override StorageService AddBooking(Booking booking)
        {
            ServiceController.Instance.Get<LoggingService>().WriteLine("Booking added");
            return this;
        }
    }
}
