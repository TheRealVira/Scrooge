namespace Scrooge.Service.Implementations.DataExporters
{
    public class DataCell
    {
        public static DataCell Empty = new DataCell("", DataCellType.Text);

        public string Content { get; private set; }

        public DataCellType Type { get; private set; }

        public DataCell(string content, DataCellType type)
        {
            this.Content = content;
            this.Type = type;
        }
    }
}
