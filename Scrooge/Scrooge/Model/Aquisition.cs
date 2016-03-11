using System;

namespace Scrooge.Model
{
    public class Aquisition
    {
        public DateTime DateTime { get; private set; }
        public decimal Value { get; private set; }

        public Aquisition(DateTime dateTime, decimal value)
        {
            this.DateTime = dateTime;
            this.Value = value;
        }
    }
}
