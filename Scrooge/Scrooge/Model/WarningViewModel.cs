using System;
using System.ComponentModel.DataAnnotations;

namespace Scrooge.Model
{
    public class WarningViewModel
    {
        // Explicit constructor needed for serialization, do not remove!
        public WarningViewModel()
        {
        }

        public WarningViewModel(string title, string message)
        {
            this.Title = title;
            this.Message = message;
            this.Read = false;
            this.Issued = DateTime.Today;
        }

        [Key]
        public uint ID { get; set; }

        public string Title { get; protected set; }

        public string Message { get; protected set; }

        public bool Read { get; set; }

        public DateTime Issued { get; protected set; }
    }
}