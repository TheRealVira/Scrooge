using System.Collections.Generic;
using System.Linq;
using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public class DataSerializationHelper
    {
        public DataCell[][] FinancialReportToCellData(FinancialReport report)
        {
            var data = new List<DataCell[]>
            {
                new[]
                {
                    new DataCell("Annual report - " + report.Year, DataCellType.HeadingBig),
                    DataCell.Empty,
                    DataCell.Empty
                },
                new[] {DataCell.Empty, DataCell.Empty, DataCell.Empty},
                new[]
                {
                    new DataCell("Name", DataCellType.Heading),
                    new DataCell("Income", DataCellType.Heading),
                    new DataCell("Expenses", DataCellType.Heading)
                }
            };


            data.AddRange(
                report.PurchasesAndSales.Where(x => x.Type == EntryType.Sale).Select(groupedPurchaseAndSales => new[]
                {
                    new DataCell(groupedPurchaseAndSales.GroupName, DataCellType.Text),
                    new DataCell(groupedPurchaseAndSales.PurchaseAndSales.Sum(x => x.Value).ToString(),
                        DataCellType.Number),
                    DataCell.Empty
                }));

            data.AddRange(
                report.PurchasesAndSales.Where(x => x.Type == EntryType.Purchase).Select(groupedPurchaseAndSales => new[]
                {
                    new DataCell(groupedPurchaseAndSales.GroupName, DataCellType.Text),
                    DataCell.Empty,
                    new DataCell(groupedPurchaseAndSales.PurchaseAndSales.Sum(x => x.Value).ToString(),
                        DataCellType.Number)
                }));

            data.Add(new []
            {
                new DataCell("Sum", DataCellType.Heading),
                new DataCell(report.Sales.ToString(), DataCellType.ResultGood),
                new DataCell(report.Purchases.ToString(), DataCellType.ResultBad)
            });

            if (report.Result >= 0)
            {
                data.Add(new[]
                {
                    new DataCell("Profit", DataCellType.Heading),
                    new DataCell(report.Result.ToString(), DataCellType.ResultGood),
                    DataCell.Empty, 
                });
            }
            else
            {
                data.Add(new[]
                {
                    new DataCell("Loss", DataCellType.Heading),
                    DataCell.Empty, 
                    new DataCell(report.Result.ToString(), DataCellType.ResultBad)
                });
            }

            data.Add(new [] {DataCell.Empty, DataCell.Empty, DataCell.Empty});
            data.Add(new []{new DataCell("This report was exported using Scrooge", DataCellType.Text), DataCell.Empty, DataCell.Empty});

            return data.ToArray();
        }
    }
}