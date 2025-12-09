using ReStock.Data;
using ReStock.Models;
using ReStock.Views;

namespace ReStock.Views;

[QueryProperty("Item", "Item")]
public partial class ReStockItemPage : ContentPage
{
    public ReStockItem Item
    {
        get => BindingContext as ReStockItem;
        set => BindingContext = value;
    }

    // Default constructor to use when creating a new item, or when shows a sample item
    public ReStockItemPage()
    {
        InitializeComponent();

        // Provide a default item so the page shows content
        if (BindingContext == null)
        {
            BindingContext = new ReStockItem
            {
                Name = "Sample Item",
                Quantity = 1,
                Done = false
            };
        }
    }

    // Constructor to use when navigating from the list
    public ReStockItemPage(ReStockItem item)
    {
        InitializeComponent();
        BindingContext = item ?? new ReStockItem();
    }

    private void IncreaseQuantity(object sender, EventArgs e)
    {
        if (Item == null) return;
        Item.Quantity++;
        OnPropertyChanged(nameof(Item));
    }

    private void DecreaseQuantity(object sender, EventArgs e)
    {
        if (Item == null) return;
        if (Item.Quantity > 0)
            Item.Quantity--;
        OnPropertyChanged(nameof(Item));
    }

    async void OnSaveClicked(object sender, EventArgs e)
{
    await App.Database.SaveItemAsync(Item);
    await Shell.Current.GoToAsync("..");
}

async void OnDeleteClicked(object sender, EventArgs e)
{
    await App.Database.DeleteItemAsync(Item);
    await Shell.Current.GoToAsync("..");
}   


    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
