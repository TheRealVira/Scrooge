using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    using System.Collections.ObjectModel;

    public class InventoryReport
    {
        public Collection<Inventory> Inventories { get; set; }

        public int Year { get; private set; }
    }
}
