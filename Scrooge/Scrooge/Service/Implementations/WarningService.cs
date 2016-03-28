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
        private readonly ILoggingService loggingService;
        private readonly IStorageService storageService;
        private IList<WarningViewModel> warnings;

        public WarningService()
        {
            Singleton<ServiceController>.Instance
                .Get<IApplicationEventService>()
                .RegisterApplicationEventListener(this);

            this.loggingService = Singleton<ServiceController>.Instance
                .Get<ILoggingService>();

            this.storageService = Singleton<ServiceController>.Instance
                .Get<IStorageService>();

            this.loggingService.WriteLine("WarningService setup complete.");
        }

        public void ApplicationInitialized()
        {
            this.loggingService.WriteLine("Beginning warning service initialization...");

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

            this.loggingService.WriteLine("Checking if quarterly warning is necessary...");
            if (!this.warnings.Where(w => w.Title.Contains("Quarter")).Any(w => w.Issued >= quarterStart))
            {
                this.warnings.Add(new WarningViewModel("New Quarter",
                    $"A new quarter has arrived, let's welcome Q{(quarterStart.Month == 1 ? 1 : (quarterStart.Month == 4 ? 2 : (quarterStart.Month == 7 ? 3 : 4)))}! Don't forget to pay your taxes."));
                this.loggingService.WriteLine("Quarterly warning issued.");
            }

            // Yearly warnings
            this.loggingService.WriteLine("Checking if yearly warning is necessary...");
            if (!this.warnings.Where(w => w.Title.Contains("Year")).Any(w => w.Issued.Year >= now.Year))
            {
                this.warnings.Add(new WarningViewModel("New Year",
                    $"A new year has arrived, let's welcome {now.Year}! You may want to create a report of the passed year using the tab above."));
                this.loggingService.WriteLine("Yearly warning issued.");
            }

            this.storageService.UpdateWarnings(this.warnings);

            this.loggingService.WriteLine(
                $"Warning service initialized. There are currently {this.warnings.Count} warnings.");
        }

        public void ApplicationClosing()
        {
        }

        public void SetAllWarningsRead()
        {
            this.loggingService.WriteLine("Settings all warnings to read.");

            foreach (var warning in this.warnings)
            {
                warning.Read = true;
            }

            this.storageService.UpdateWarnings(this.warnings);
        }

        public IEnumerable<WarningViewModel> GetWarnings()
        {
            this.loggingService.WriteLine("Retrieving warnings...");
            return this.warnings;
        }

        public IWarningService Push(WarningViewModel warning)
        {
            this.loggingService.WriteLine("Warning to be pushed: " + warning.Title);
            this.warnings.Add(warning);
            this.storageService.UpdateWarnings(this.warnings);
            return this;
        }

        public bool ShouldShowNewWarningsDialog => this.warnings.Any(x => !x.Read);
    }
}