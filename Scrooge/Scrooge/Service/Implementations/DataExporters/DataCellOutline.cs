using System;

namespace Scrooge.Service.Implementations.DataExporters
{
    [Flags]
    public enum DataCellOutline : byte
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8
    }
}