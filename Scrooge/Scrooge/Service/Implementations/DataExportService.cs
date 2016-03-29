using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using Scrooge.Model;
using Scrooge.Service.Definitions;
using Scrooge.Service.Implementations.DataExporters;

namespace Scrooge.Service.Implementations
{
    public class DataExportService : IDataExportService
    {
        private readonly Dictionary<string, IDataSerializer> serializers;
        private readonly ILoggingService loggingService;

        public DataExportService()
        {
            this.loggingService = Singleton<ServiceController>.Instance
                .Get<ILoggingService>();

            this.serializers = new Dictionary<string, IDataSerializer>
            {
                {"xlsx", new XLSXDataSerializer()},
                {"csv", new CSVDataSerializer()}
            };
        }

        public IDataExportService ExportFinancialReport(FinancialReport report)
        {
            this.loggingService.WriteLine("Beginning export of financial report...");

            var dialog = new SaveFileDialog
            {
                Filter = this.GenerateFilter(),
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName = report.Year + "-scrooge-financial-report",
                DefaultExt = ".xlsx",
                CheckPathExists = true
            };

            this.loggingService.WriteLine("Activating SaveFileDialog...");
            var result = dialog.ShowDialog();

            if (result.GetValueOrDefault(false))
            {
                this.loggingService.WriteLine("User confirmed");
                var ext = Path.GetExtension(dialog.FileName)?.Substring(1);
                var serializer = ext == null
                    ? null
                    : (this.serializers.ContainsKey(ext) ? this.serializers[ext] : null);

                if (serializer == null)
                {
                    this.loggingService.WriteLine("No file type specified - uh oh...");
                    MessageBox.Show("You have to specify a file type!", "Error"); // No styling, shouldn't happen anyway
                    return this;
                }

                this.loggingService.WriteLine("Using serializer: " + ext);
                serializer.SerializeFinancialReport(report, dialog.FileName);
                this.loggingService.WriteLine("Exporting done.");
            }
            else
            {
                this.loggingService.WriteLine("Exporting aborted.");
            }

            return this;
        }

        public IDataExportService ExportTaxReport(TaxReport report)
        {
            this.loggingService.WriteLine("Beginning export of tax report...");

            var dialog = new SaveFileDialog
            {
                Filter = this.GenerateFilter(),
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName = report.Year + "-scrooge-tax-report",
                DefaultExt = ".xlsx",
                CheckPathExists = true
            };

            this.loggingService.WriteLine("Activating SaveFileDialog...");
            var result = dialog.ShowDialog();

            if (result.GetValueOrDefault(false))
            {
                this.loggingService.WriteLine("User confirmed");
                var ext = Path.GetExtension(dialog.FileName)?.Substring(1);
                var serializer = ext == null
                    ? null
                    : (this.serializers.ContainsKey(ext) ? this.serializers[ext] : null);

                if (serializer == null)
                {
                    this.loggingService.WriteLine("No file type specified - uh oh...");
                    MessageBox.Show("You have to specify a file type!", "Error"); // No styling, shouldn't happen anyway
                    return this;
                }

                this.loggingService.WriteLine("Using serializer: " + ext);
                serializer.SerializeTaxReport(report, dialog.FileName);
                this.loggingService.WriteLine("Exporting done.");
            }
            else
            {
                this.loggingService.WriteLine("Exporting aborted.");
            }

            return this;
        }

        private string GenerateFilter()
        {
            var retval = new StringBuilder();
            foreach (var dataSerializer in this.serializers)
            {
                retval.Append($"|{dataSerializer.Key.ToUpper()}-Files|*.{dataSerializer.Key}");
            }
            return retval.ToString(1, retval.Length - 1);
        }
    }
}