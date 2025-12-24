using System.Collections.ObjectModel;
using ReStock.Models;
using ReStock.Views;

namespace ReStock.Views;

public partial class ReStockListPage : ContentPage
{
    public ObservableCollection<ReStockItem> Items { get; set; } = new();


    public ReStockListPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

        
    private void ApplySort(int index, List<ReStockItem> items)
    {
        IEnumerable<ReStockItem> sorted = items;

        switch (index)
        {
            case 0: // Quantity Low-High
                sorted = items.OrderBy(i => i.Quantity);
                break;
            case 1: // Quantity High-Low
                sorted = items.OrderByDescending(i => i.Quantity);
                break;
            case 2: // Name A-Z
                sorted = items.OrderBy(i => i.Name);
                break;
            case 3: // Name Z-A
                sorted = items.OrderByDescending(i => i.Name);
                break;
        }

        Items.Clear();
        foreach (var item in sorted)
            Items.Add(item);
    }
    private int _currentSortIndex = 0;

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SortPicker.SelectedIndex < 0)
            return;

        _currentSortIndex = SortPicker.SelectedIndex;
        ApplySort(_currentSortIndex, Items.ToList());
    }
    

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var items = await App.Database.GetItemsAsync();
        Items.Clear();
        foreach (var item in items)
            Items.Add(item);

        ApplySort(SortPicker.SelectedIndex >= 0 ? SortPicker.SelectedIndex : 0, Items.ToList());
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        var newItem = new ReStockItem();
        await Navigation.PushAsync(new ReStockItemPage(newItem));
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ReStockItem item)
            return;

        // Navigate to edit page
        await Navigation.PushAsync(new ReStockItemPage(item));


        // Deselect item to avoid multiple triggers
        ((CollectionView)sender).SelectedItem = null;
    }

     private async void IncreaseQuantity(object sender, EventArgs e)
    {
        if (sender is Button button &&
            button.BindingContext is ReStockItem item)
        {
            item.Quantity++;
            await App.Database.SaveItemAsync(item);
            ApplySort(_currentSortIndex, Items.ToList());
        }
    }

    private async void DecreaseQuantity(object sender, EventArgs e)
    {
        if (sender is Button button &&
            button.BindingContext is ReStockItem item &&
            item.Quantity > 0)
        {
            item.Quantity--;
            await App.Database.SaveItemAsync(item);
            ApplySort(_currentSortIndex, Items.ToList());
        }
    }

}
