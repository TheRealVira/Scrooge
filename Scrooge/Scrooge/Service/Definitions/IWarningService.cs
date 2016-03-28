using System.Collections.Generic;
using Scrooge.Model;

namespace Scrooge.Service.Definitions
{
    public interface IWarningService
    {
        void SetAllWarningsRead();

        IEnumerable<WarningViewModel> GetWarnings();

        IWarningService Push(WarningViewModel warning);

        bool ShouldShowNewWarningsDialog { get; }
    }
}
