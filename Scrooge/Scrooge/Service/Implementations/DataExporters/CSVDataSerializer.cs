using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public class CSVDataSerializer : IDataSerializer
    {
        public void SerializeFinancialReport(FinancialReport report, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.FinancialReportToCellData(report);
            CSVDataSerializer.WriteToCSV(cellData, filename);
        }

        public void SerializeTaxReport(TaxReport report, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.TaxReportToCellData(report);
            CSVDataSerializer.WriteToCSV(cellData, filename);
        }

        public void SerializeInventoryReport(IEnumerable<InventoryViewModel> report, int year, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.InventoryReportToCellData(report, year);
            CSVDataSerializer.WriteToCSV(cellData, filename);
        }

        private static void WriteToCSV(DataCell[][] cells, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var row in cells)
                {
                    writer.WriteLine(row.Select(x => x.Content).Join(";"));
                }
            }
        }
    }
}