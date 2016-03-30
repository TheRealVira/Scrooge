using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class KilometerEntryViewModel : INotifyPropertyChanged, IEquatable<KilometerEntryViewModel>
    {
        private DateTime date;
        private string drivenRoute;
        private string purpose;
        private bool isSelected;
        private decimal startedKilometerCount;
        private decimal newKilometerCount;

        [Key]
        public uint ID { get; set; }

        // Explicit constructor needed for serialization, do not remove!
        public KilometerEntryViewModel()
        {
        }

        [NotMapped]
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected == value) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if (this.date == value) return;
                this.date = value;
                this.OnPropertyChanged();
            }
        }

        public decimal StartedKilometerCount
        {
            get { return this.startedKilometerCount; }
            set
            {
                if (this.startedKilometerCount == value) return;
                this.startedKilometerCount = value;
                this.OnPropertyChanged();
            }
        }

        public decimal NewKilometerCount
        {
            get { return this.newKilometerCount; }
            set
            {
                if (this.newKilometerCount == value) return;
                this.newKilometerCount = value;
                this.OnPropertyChanged();
            }
        }

        public string Purpose
        {
            get { return this.purpose; }
            set
            {
                if (this.purpose == value) return;
                this.purpose = value;
                this.OnPropertyChanged();
            }
        }

        public decimal DrivenKilometers => NewKilometerCount - StartedKilometerCount; // TODO

        public string DrivenRoute
        {
            get { return this.drivenRoute; }
            set
            {
                if (this.drivenRoute == value) return;
                this.drivenRoute = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(KilometerEntryViewModel other) => this.ID == other.ID;
    }
}