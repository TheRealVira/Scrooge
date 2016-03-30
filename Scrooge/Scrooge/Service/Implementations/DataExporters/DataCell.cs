namespace Scrooge.Service.Implementations.DataExporters
{
    public class DataCell
    {
        public static DataCell Empty = new DataCell("", DataCellType.Text);

        public string Content { get; private set; }

        public DataCellType Type { get; private set; }
        public DataCellOutline Outline { get; private set; }

        public DataCell(string content, DataCellType type, DataCellOutline outline = DataCellOutline.None)
        {
            this.Content = content;
            this.Type = type;
            this.Outline = outline;
        }
    }
}