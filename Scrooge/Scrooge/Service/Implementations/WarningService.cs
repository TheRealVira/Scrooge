using System;
using System.Collections.Generic;
using System.Linq;
using Scrooge.Model;
using Scrooge.Model.Internal;
using Scrooge.Service.Definitions;

namespace Scrooge.Service.Implementations
{
    public class WarningService : IWarningService, IApplicationEventListener
    {
        private readonly IStorageService storageService;
        private IList<WarningViewModel> warnings;

        public WarningService()
        {
            Singleton<ServiceController>.Instance
                .Get<IApplicationEventService>()
                .RegisterApplicationEventListener(this);

            this.storageService = Singleton<ServiceController>.Instance
                .Get<IStorageService>();
        }

        public void ApplicationInitialized()
        {
            this.warnings = this.storageService.RetrieveWarnings();

            // Issue warnings
            var now = DateTime.Now;

            // Quarterly warnings
            var quarterStart = now.Month <= 3
                ? new DateTime(now.Year, 1, 1)
                : (now.Month <= 6
                    ? new DateTime(now.Year, 4, 1)
                    : (now.Month <= 9
                        ? new DateTime(now.Year, 7, 1)
                        : new DateTime(now.Year, 10, 1)));

            if (!this.warnings.Where(w => w.Title.Contains("Quarter")).Any(w => w.Issued >= quarterStart))
            {
                this.warnings.Add(new WarningViewModel("New Quarter", $"A new quarter has arrived, let's welcome Q{(quarterStart.Month == 1 ? 1 : (quarterStart.Month == 4 ? 2 : (quarterStart.Month == 7 ? 3 : 4)))}! Don't forget to pay your taxes."));
            }

            // Yearly warnings
            if (!this.warnings.Where(w => w.Title.Contains("Year")).Any(w => w.Issued.Year >= now.Year))
            {
                this.warnings.Add(new WarningViewModel("New Year", $"A new year has arrived, let's welcome {now.Year}! You may want to create a report of the passed year using the tab above."));
            }

            this.storageService.UpdateWarnings(this.warnings);
        }

        public void ApplicationClosing()
        {
        }

        public void SetAllWarningsRead()
        {
            foreach (var warning in this.warnings)
            {
                warning.Read = true;
            }

            this.storageService.UpdateWarnings(this.warnings);
        }

        public IEnumerable<WarningViewModel> GetWarnings()
        {
            return this.warnings;
        }

        public IWarningService Push(WarningViewModel warning)
        {
            this.warnings.Add(warning);
            this.storageService.UpdateWarnings(this.warnings);
            return this;
        }

        public bool ShouldShowNewWarningsDialog => this.warnings.Any(x => !x.Read);
    }
}