using ReStock.Models;
using ReStock.Data;

namespace ReStock.Views;

public partial class ReStockItemPage : ContentPage
{
    private readonly ReStockItem _item;
    private bool _isNewItem;

    public ReStockItemPage(ReStockItem item)
    {
        InitializeComponent();
        _item = item;
        BindingContext = _item;

        // Determine if this is a new item
        _isNewItem = _item.ID == 0;

        // Hook PropertyChanged for live save
        _item.PropertyChanged += Item_PropertyChanged;
    }

    private async void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (_isNewItem)
        {
            // Insert new item once
            await App.Database.SaveItemAsync(_item);
            _isNewItem = false; // now has ID
        }
        else
        {
            // Update existing item
            await App.Database.SaveItemAsync(_item);
        }

        Console.WriteLine($"Auto-saved: {_item.ID}, {_item.Name}, {_item.Quantity}, {_item.Done}");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // If this was a new item and Cancel is pressed, delete it
        if (_item.ID != 0 && _isNewItem)
        {
            await App.Database.DeleteItemAsync(_item);
            Console.WriteLine($"Cancelled new item, deleted ID={_item.ID}");
        }

        await Navigation.PopAsync();
    }

    private void IncreaseQuantity(object sender, EventArgs e)
    {
        if (BindingContext is ReStockItem item)
            item.Quantity++;
    }

    private void DecreaseQuantity(object sender, EventArgs e)
    {
        if (BindingContext is ReStockItem item && item.Quantity > 0)
            item.Quantity--;
    }


//you will have to remove it eventually
    private void OnSaveClicked(object sender, EventArgs e)
{
    // No action needed: all changes are already auto-saved
    DisplayAlert("Info", "Changes are automatically saved.", "OK");
}


    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var item = (ReStockItem)BindingContext;

        if (item.ID == 0)
        {
            await DisplayAlert("Error", "Item has not been saved yet.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Delete", "Are you sure?", "Yes", "Cancel");
        if (!confirm) return;

        await App.Database.DeleteItemAsync(item);
        Console.WriteLine($"Deleted item ID={item.ID}");

        await Navigation.PopAsync();
    }
}
