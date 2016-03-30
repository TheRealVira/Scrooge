using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public class PDFDataSerializer : IDataSerializer
    {
        public void SerializeFinancialReport(FinancialReport report, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.FinancialReportToCellData(report);
            PDFDataSerializer.WriteToPDF(cellData, filename);
        }

        public void SerializeTaxReport(TaxReport report, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.TaxReportToCellData(report);
            PDFDataSerializer.WriteToPDF(cellData, filename);
        }

        public void SerializeInventoryReport(IEnumerable<InventoryViewModel> report, int year, string filename)
        {
            var cellData = Singleton<DataSerializationHelper>.Instance.InventoryReportToCellData(report, year);
            PDFDataSerializer.WriteToPDF(cellData, filename);
        }

        private static void WriteToPDF(DataCell[][] dataCells, string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            // Create a new MigraDoc document

            var document = new Document();
            document.Styles["Normal"].Font.Name = "Arial";
            document.DefaultPageSetup.LeftMargin -= Unit.FromCentimeter(1);
            var section = document.AddSection();

            var rightStyle = document.AddStyle("RightAligned", "Normal");
            rightStyle.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            var headingStyle = document.AddStyle("Heading", "Normal");
            headingStyle.Font.Size = Unit.FromPoint(11);

            var headingBigStyle = document.AddStyle("HeadingBig", "Normal");
            headingBigStyle.Font.Size = Unit.FromPoint(16);

            var table = section.AddTable();
            table.Style = "Table";
            table.Rows.LeftIndent = 0;

            var columnCount = dataCells.Max(x => x.Length);

            for (var i = 0; i < columnCount; i++)
            {
                var column = table.AddColumn((18.0/columnCount) + "cm");
                column.Format.Alignment = ParagraphAlignment.Left;
            }

            foreach (var row in dataCells)
            {
                var pdfRow = table.AddRow();

                int counter = 0;
                foreach (var dataCell in row)
                {
                    var pdfCell = pdfRow.Cells[counter];

                    pdfCell.AddParagraph(dataCell.Content);

                    if (dataCell.Outline.HasFlag(DataCellOutline.Top))
                    {
                        pdfCell.Borders.Top.Visible = true;
                        pdfCell.Borders.Top.Style = BorderStyle.Single;
                        pdfCell.Borders.Top.Width = Unit.FromMillimeter(0.5);
                        pdfCell.Borders.Top.Color = new Color(0, 0, 0);
                    }

                    if (dataCell.Outline.HasFlag(DataCellOutline.Bottom))
                    {
                        pdfCell.Borders.Bottom.Visible = true;
                        pdfCell.Borders.Bottom.Style = BorderStyle.Single;
                        pdfCell.Borders.Bottom.Width = Unit.FromMillimeter(0.5);
                        pdfCell.Borders.Bottom.Color = new Color(0, 0, 0);
                    }

                    if (dataCell.Outline.HasFlag(DataCellOutline.Left))
                    {
                        pdfCell.Borders.Left.Visible = true;
                        pdfCell.Borders.Left.Style = BorderStyle.Single;
                        pdfCell.Borders.Left.Width = Unit.FromMillimeter(0.5);
                        pdfCell.Borders.Left.Color = new Color(0, 0, 0);
                    }

                    if (dataCell.Outline.HasFlag(DataCellOutline.Right))
                    {
                        pdfCell.Borders.Right.Visible = true;
                        pdfCell.Borders.Right.Style = BorderStyle.Single;
                        pdfCell.Borders.Right.Width = Unit.FromMillimeter(0.5);
                        pdfCell.Borders.Right.Color = new Color(0, 0, 0);
                    }

                    switch (dataCell.Type)
                    {
                        case DataCellType.HeadingBig:
                            pdfCell.Style = "HeadingBig";
                            pdfCell.MergeRight = columnCount - 1;
                            break;
                        case DataCellType.Heading:
                            pdfCell.Style = "Heading";
                            break;
                        case DataCellType.Number:
                            pdfCell.Style = "RightAligned";
                            break;
                        case DataCellType.ResultGood:
                            PDFDataSerializer.SetCellResultStyle(pdfCell, new Color(0xBC, 0xED, 0x91));
                            break;
                        case DataCellType.ResultNeutral:
                            PDFDataSerializer.SetCellResultStyle(pdfCell, new Color(0xFF, 0xEC, 0xB3));
                            break;
                        case DataCellType.ResultBad:
                            PDFDataSerializer.SetCellResultStyle(pdfCell, new Color(0xFF, 0x8A, 0x65));
                            break;
                    }

                    counter++;
                }
            }


            var pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Automatic) {Document = document};
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filename);
        }

        private static void SetCellResultStyle(Cell pdfCell, Color color)
        {
            pdfCell.Style = "RightAligned";
            pdfCell.Shading.Visible = true;
            pdfCell.Shading.Color = color;
        }
    }
}