using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Scrooge.Annotations;

namespace Scrooge.Model
{
    public class Acquisition : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}