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
            using (var stream = new FileStream(filename, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var row in cellData)
                {
                    writer.WriteLine(row.Select(x => x.Content).Join(";"));
                }
            }
        }
    }
}
