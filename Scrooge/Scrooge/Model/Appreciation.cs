using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge.Model
{
    public class Appreciation
    {
        // Explicit constructor needed for serialization, do not remove!
        public Appreciation()
        {
        }

        [Key]
        public int ID { get; set; }

        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }

        public uint InventoryViewModelForeignKey { get; set; }

        [ForeignKey("InventoryViewModelForeignKey")]
        public InventoryViewModel InventoryViewModel { get; set; }

        public Appreciation(DateTime dateTime, decimal value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value + "";
        }
    }
}