﻿using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public class XLSXDataSerializer : IDataSerializer
    {
        public void SerializeFinancialReport(FinancialReport report, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.FinancialReportToCellData(report);
            this.WriteToXLSX(cellData, filename);
        }

        private void WriteToXLSX(DataCell[][] cells, string filename)
        {
            var package = new ExcelPackage(new FileInfo(filename));
            var sheet = package.Workbook.Worksheets.Add("Content");

            foreach (var cell in new ExcelCellEnumerator(cells.GetLength(0), cells.Max(x => x.Length)))
            {
                var dataRow = cells[cell.Row - 1];
                if (cell.Column >= dataRow.Length) continue;
                var data = dataRow[cell.Column];
                var excelCell = sheet.Cells[cell.Value];
                excelCell.Value = data.Content;
                switch (data.Type)
                {
                    case DataCellType.HeadingBig:
                        excelCell.Style.Font.Bold = true;
                        excelCell.Style.Font.Size = 18;
                        sheet.Row(cell.Row).Merged = true;
                        break;
                    case DataCellType.Heading:
                        excelCell.Style.Font.Bold = true;
                        excelCell.Style.Font.Size = 12;
                        break;
                    case DataCellType.ResultGood:
                        XLSXDataSerializer.SetResultCell(excelCell, Color.Green);
                        break;
                    case DataCellType.ResultNeutral:
                        XLSXDataSerializer.SetResultCell(excelCell, Color.LightGoldenrodYellow);
                        break;
                    case DataCellType.ResultBad:
                        XLSXDataSerializer.SetResultCell(excelCell, Color.Red);
                        break;
                    case DataCellType.Number:
                        excelCell.Value = double.Parse(excelCell.Value.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                        break;
                }

                if (data.Outline.HasFlag(DataCellOutline.Top))
                {
                    excelCell.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    excelCell.Style.Border.Top.Color.SetColor(Color.Black);
                }

                if (data.Outline.HasFlag(DataCellOutline.Bottom))
                {
                    excelCell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                    excelCell.Style.Border.Bottom.Color.SetColor(Color.Black);
                }

                if (data.Outline.HasFlag(DataCellOutline.Left))
                {
                    excelCell.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    excelCell.Style.Border.Left.Color.SetColor(Color.Black);
                }

                if (data.Outline.HasFlag(DataCellOutline.Right))
                {
                    excelCell.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    excelCell.Style.Border.Right.Color.SetColor(Color.Black);
                }
            }

            for (var i = 1; i < sheet.Dimension.Columns + 1; i++)
            {
                sheet.Column(i).AutoFit(10.71);
                    // 10.71 represents the default excel cell width, we do not want to go below that
            }

            package.Save();
            package.Dispose();
        }

        private static void SetResultCell(ExcelRange excelCell, Color color)
        {
            excelCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelCell.Style.Fill.BackgroundColor.SetColor(color);
            excelCell.Style.Font.Size = 12;
            excelCell.Value = double.Parse(excelCell.Value.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
        }
    }
}