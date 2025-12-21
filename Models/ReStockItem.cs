using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReStock.Models;

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
        set => SetProperty(ref quantity, value);
    }

    public bool Done
    {
        get => done;
        set => SetProperty(ref done, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
