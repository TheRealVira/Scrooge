using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public interface IDataExportService
    {
        IDataExportService ExportFinancialReport(FinancialReport report);
    }
}
