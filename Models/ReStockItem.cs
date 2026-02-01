using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace ReStock.Models
{
    public class ReStockItem : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string name;
        private string notes;
        private int quantity;
        private bool done;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (SetProperty(ref quantity, value))
                    OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public bool Done
        {
            get => done;
            set => SetProperty(ref done, value);
        }

        [Ignore]
        public Color BackgroundColor
        {
            get
            {
                var settings = App.Database.GetSettingsAsync().Result;
                if (settings == null) return Colors.Transparent;
                return Quantity <= settings.LowStockThreshold ? settings.LowStockColor : Colors.Transparent;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Add public method for external calls
        public void NotifyPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
