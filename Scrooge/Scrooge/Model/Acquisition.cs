using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrooge.Model
{
    public class Acquisition
    {
        // Explicit constructor needed for serialization, do not remove!
        public Acquisition()
        {
        }

        [Key]
        public int ID { get; set; }

        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }

        public uint InventoryViewModelForeignKey { get; set; }
        [ForeignKey("InventoryViewModelForeignKey")]
        public InventoryViewModel InventoryViewModel { get; set; }

        public Acquisition(DateTime dateTime, decimal value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }
    }
}
