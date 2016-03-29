using System.Collections.Generic;
using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public interface IDataSerializer
    {
        void SerializeFinancialReport(FinancialReport report, string filename);

        void SerializeTaxReport(TaxReport report, string filename);

        void SerializeInventoryReport(IEnumerable<InventoryViewModel> report, int year, string filename);
    }
}
