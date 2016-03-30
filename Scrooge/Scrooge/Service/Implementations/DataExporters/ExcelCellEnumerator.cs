using System.Collections;
using System.Collections.Generic;

namespace Scrooge.Service.Implementations.DataExporters
{
    public class ExcelCellEnumerator : IEnumerable<ExcelCellEnumerator.CellCoordinate>,
        IEnumerator<ExcelCellEnumerator.CellCoordinate>
    {
        private readonly int columns;
        private readonly int rows;
        private int currentColumn;
        private int currentRow;

        public ExcelCellEnumerator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.Reset();
        }

        public IEnumerator<CellCoordinate> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.currentColumn++;
            if (this.currentColumn < this.columns) return true;
            this.currentColumn = 0;
            this.currentRow++;
            return this.currentRow < this.rows;
        }

        public void Reset()
        {
            this.currentRow = 0;
            this.currentColumn = -1;
        }

        public CellCoordinate Current => new CellCoordinate(this.currentRow, this.currentColumn);

        object IEnumerator.Current => this.Current;

        public struct CellCoordinate
        {
            public int Row { get; }
            public int Column { get; }
            public string Value => ((char) ('A' + (char) (this.Column))).ToString() + this.Row;

            public CellCoordinate(int row, int column)
            {
                this.Row = row + 1;
                this.Column = column;
            }
        }
    }
}