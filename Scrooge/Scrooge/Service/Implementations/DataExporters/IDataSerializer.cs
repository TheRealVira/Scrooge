﻿using Scrooge.Model;

namespace Scrooge.Service.Implementations.DataExporters
{
    public interface IDataSerializer
    {
        void SerializeFinancialReport(FinancialReport report, string filename);
    }
}
