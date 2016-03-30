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
                    new DataCell((-report.Result).ToString(), DataCellType.ResultBad, DataCellOutline.Top)
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
                new[]
                {new DataCell("Sales", DataCellType.Text), new DataCell(report.Sales.ToString(), DataCellType.Number)},
                new[]
                {
                    new DataCell("Net sales", DataCellType.Text),
                    new DataCell(report.NetSales.ToString(), DataCellType.Number)
                },
                new[]
                {
                    new DataCell("Sales tax", DataCellType.Text, DataCellOutline.Top),
                    new DataCell(report.SalesTax.ToString(), DataCellType.Number, DataCellOutline.Top)
                },
                new[]
                {
                    new DataCell("- Input tax", DataCellType.Text),
                    new DataCell(report.InputTax.ToString(), DataCellType.Number)
                },
                new[]
                {
                    new DataCell("Tax payable", DataCellType.Text, DataCellOutline.Top),
                    new DataCell(report.TaxPayable.ToString(), DataCellType.Number, DataCellOutline.Top)
                },
                new[]
                {
                    new DataCell("- Advanced tax payments", DataCellType.Text),
                    new DataCell(report.AdvanceTaxPayements.ToString(), DataCellType.Number)
                },
                new[]
                {
                    new DataCell(report.OutstandingMoney >= 0 ? "Liability" : "Claim", DataCellType.Text,
                        DataCellOutline.Top),
                    new DataCell(
                        report.OutstandingMoney >= 0
                            ? report.OutstandingMoney.ToString()
                            : (-report.OutstandingMoney).ToString(),
                        report.OutstandingMoney >= 0 ? DataCellType.ResultBad : DataCellType.ResultGood,
                        DataCellOutline.Top)
                }
            };

            return data.ToArray();
        }

        public DataCell[][] InventoryReportToCellData(IEnumerable<InventoryViewModel> report, int year)
        {
            var data = new List<DataCell[]>
            {
                new[]
                {
                    new DataCell("Inventory report - " + year, DataCellType.HeadingBig),
                    DataCell.Empty,
                    DataCell.Empty,
                    DataCell.Empty,
                    DataCell.Empty,
                    DataCell.Empty
                },
                new[]
                {
                    DataCell.Empty, DataCell.Empty, DataCell.Empty, DataCell.Empty, DataCell.Empty, DataCell.Empty
                },
                new[]
                {
                    new DataCell("Item", DataCellType.Heading, DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Aquisitions value", DataCellType.Heading,
                        DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Asset value (1.1.)", DataCellType.Heading,
                        DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Appreciation", DataCellType.Heading, DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Depreciation", DataCellType.Heading, DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Disposal", DataCellType.Heading, DataCellOutline.Bottom | DataCellOutline.Right),
                    new DataCell("Asset value (31.12.)", DataCellType.Heading, DataCellOutline.Bottom)
                }
            };

            data.AddRange(report.Select(item => new[]
            {
                new DataCell(item.Name, DataCellType.Text, DataCellOutline.Right),
                new DataCell(item.AcquisitionValue.ToString(), DataCellType.Number, DataCellOutline.Right),
                new DataCell(item.BalanceValue.ToString(), DataCellType.Number, DataCellOutline.Right),
                new DataCell(item.AppreciationList.Sum(x => x.DateTime.Year == year ? x.Value : 0).ToString(),
                    DataCellType.Number, DataCellOutline.Right),
                new DataCell(item.Deprecation.ToString(), DataCellType.Number, DataCellOutline.Right),
                new DataCell(item.Disposal.ToString(), DataCellType.Number, DataCellOutline.Right),
                new DataCell(item.AssetValue.ToString(), DataCellType.Number)
            }));

            data.Add(new[]
            {
                new DataCell("Sum", DataCellType.Heading, DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(report.Sum(x => x.AcquisitionValue).ToString(), DataCellType.ResultNeutral,
                    DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(report.Sum(x => x.BalanceValue).ToString(), DataCellType.ResultNeutral,
                    DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(
                    report.Sum(x => x.AppreciationList.Sum(y => y.DateTime.Year == year ? y.Value : 0)).ToString(),
                    DataCellType.ResultNeutral, DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(report.Sum(x => x.Deprecation).ToString(), DataCellType.ResultNeutral,
                    DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(report.Sum(x => x.Disposal).ToString(), DataCellType.ResultNeutral,
                    DataCellOutline.Top | DataCellOutline.Right),
                new DataCell(report.Sum(x => x.AssetValue).ToString(), DataCellType.ResultNeutral, DataCellOutline.Top),
            });

            return data.ToArray();
        }
    }
}