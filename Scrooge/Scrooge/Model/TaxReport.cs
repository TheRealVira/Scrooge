using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    public class TaxReport
    {
        public decimal SalesTax { get; set; }

        public decimal InputTax { get; set; }

        public decimal TaxPayable { get; set; }

        public decimal AdvancePayments { get; set; }

        public decimal OutstandingMoney { get; set; }
    }
}
