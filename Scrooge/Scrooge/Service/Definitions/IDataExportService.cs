using System.Collections.Generic;
using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public interface IDataExportService
    {
        IDataExportService ExportFinancialReport(FinancialReport report);

        IDataExportService ExportTaxReport(TaxReport report);

        IDataExportService ExportInventoryReport(IEnumerable<InventoryViewModel> report, int year);
    }
}
