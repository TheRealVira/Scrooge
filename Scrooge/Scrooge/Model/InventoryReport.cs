namespace Scrooge.Model
{
    using System.Collections.ObjectModel;

    public class InventoryReport
    {
        public Collection<InventoryViewModel> Inventories { get; set; }

        public int Year { get; private set; }
    }
}
