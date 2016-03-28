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

        public DataExportService()
        {
            this.serializers = new Dictionary<string, IDataSerializer>
            {
                {"csv", new CSVDataSerializer()}
            };
        }

        public IDataExportService ExportFinancialReport(FinancialReport report)
        {
            var dialog = new OpenFileDialog
            {
                Filter = this.GenerateFilter()
            };

            var result = dialog.ShowDialog();

            if (result.GetValueOrDefault(false))
            {
                var ext = Path.GetExtension(dialog.FileName)?.Substring(1);
                var serializer = ext == null
                    ? null
                    : (this.serializers.ContainsKey(ext) ? this.serializers[ext] : null);

                if (serializer == null)
                {
                    MessageBox.Show("You have to specify a file type!", "Error"); // No styling, shouldn't happen anyway
                    return this;
                }

                serializer.SerializeFinancialReport(report, dialog.FileName);
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