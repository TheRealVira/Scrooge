using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scrooge.Model
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _name;
        private int _dateOfAcquisition;
        private float _costValue;
        private float _balanceValue;
        private float _access;
        private float _duration;
        private float _percentage;
        private byte _months;
        private float _value;
        private float _derecognition;
        private float _assetValue;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int DateOfAcquisition
        {
            get { return _dateOfAcquisition; }
            set
            {
                if (_dateOfAcquisition == value) return;
                _dateOfAcquisition = value;
                OnPropertyChanged();
            }
        }

        public float CostValue
        {
            get { return _costValue; }
            set
            {
                if (_costValue == value) return;
                _costValue = value;
                OnPropertyChanged();
            }
        }

        public float BalanceValue
        {
            get { return _balanceValue; }
            set
            {
                if (_balanceValue == value) return;
                _balanceValue = value;
                OnPropertyChanged();
            }
        }

        public float Access
        {
            get { return _access; }
            set
            {
                if (_access == value) return;
                _access = value;
                OnPropertyChanged();
            }
        }

        public float Duration
        {
            get { return _duration; }
            set
            {
                if (_duration == value) return;
                _duration = value;
                OnPropertyChanged();
            }
        }

        public float Percentage
        {
            get { return _percentage; }
            set
            {
                if (_percentage == value) return;
                _percentage = value;
                OnPropertyChanged();
            }
        }

        public byte Months
        {
            get { return _months; }
            set
            {
                if (_months == value) return;
                _months = value;
                OnPropertyChanged();
            }
        }

        public float Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public float Derecognition
        {
            get { return _derecognition; }
            set
            {
                if (_derecognition == value) return;
                _derecognition = value;
                OnPropertyChanged();
            }
        }

        public float AssetValue
        {
            get { return _assetValue; }
            set
            {
                if (_assetValue == value) return;
                _assetValue = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
