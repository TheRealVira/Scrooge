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
                    new DataCell("Financial report - " + report.Year, DataCellType.HeadingBig),
                    DataCell.Empty,
                    DataCell.Empty
                },
                new[] {DataCell.Empty, DataCell.Empty, DataCell.Empty},
                new[]
                {
                    new DataCell("Name", DataCellType.Heading, DataCellOutline.Bottom),
                    new DataCell("Income", DataCellType.Heading, DataCellOutline.Bottom),
                    new DataCell("Expenses", DataCellType.Heading, DataCellOutline.Bottom)
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
                report.PurchasesAndSales.Where(x => x.Type == EntryType.Purchase)
                    .Select(groupedPurchaseAndSales => new[]
                    {
                        new DataCell(groupedPurchaseAndSales.GroupName, DataCellType.Text),
                        DataCell.Empty,
                        new DataCell(groupedPurchaseAndSales.PurchaseAndSales.Sum(x => x.Value).ToString(),
                            DataCellType.Number)
                    }));

            data.Add(new[]
            {
                new DataCell("Sum", DataCellType.Heading, DataCellOutline.Top),
                new DataCell(report.Sales.ToString(), DataCellType.ResultGood, DataCellOutline.Top),
                new DataCell(report.Purchases.ToString(), DataCellType.ResultBad, DataCellOutline.Top)
            });

            if (report.Result >= 0)
            {
                data.Add(new[]
                {
                    new DataCell("Profit", DataCellType.Heading, DataCellOutline.Top),
                    new DataCell(report.Result.ToString(), DataCellType.ResultGood, DataCellOutline.Top),
                    new DataCell("", DataCellType.Text, DataCellOutline.Top)
                });
            }
            else
            {
                data.Add(new[]
                {
                    new DataCell("Loss", DataCellType.Heading, DataCellOutline.Top),
                    new DataCell("", DataCellType.Text, DataCellOutline.Top),
                    new DataCell(report.Result.ToString(), DataCellType.ResultBad, DataCellOutline.Top)
                });
            }

            return data.ToArray();
        }

        public DataCell[][] TaxReportToCellData(TaxReport report)
        {
            var data = new List<DataCell[]>
            {
                new[]
                {
                    new DataCell("Tax report - " + report.Year, DataCellType.HeadingBig),
                    DataCell.Empty
                },
                new[] {DataCell.Empty, DataCell.Empty},
                new[] {new DataCell("Sales", DataCellType.Text), new DataCell(report.Sales.ToString(), DataCellType.Number)},
                new[] {new DataCell("Net sales", DataCellType.Text), new DataCell(report.NetSales.ToString(), DataCellType.Number)},
                new[] {new DataCell("Sales tax", DataCellType.Text), new DataCell(report.SalesTax.ToString(), DataCellType.Number, DataCellOutline.Top)},
                new[] {new DataCell("- Input tax", DataCellType.Text), new DataCell(report.InputTax.ToString(), DataCellType.Number)},
                new[] {new DataCell("Tax payable", DataCellType.Text), new DataCell(report.TaxPayable.ToString(), DataCellType.Number, DataCellOutline.Top)},
                new[] {new DataCell("- Advanced tax payments", DataCellType.Text), new DataCell(report.AdvanceTaxPayements.ToString(), DataCellType.Number)},
                new[] {new DataCell(report.OutstandingMoney >= 0 ? "Liability" : "Claim", DataCellType.Text), new DataCell(report.OutstandingMoney.ToString(), report.OutstandingMoney >= 0 ? DataCellType.ResultBad : DataCellType.ResultGood, DataCellOutline.Top)},
            };

            return data.ToArray();
        }
    }
}