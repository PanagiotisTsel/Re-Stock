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

        // Optional: initial sample items
        Items.Add(new ReStockItem { Name = "Apples", Quantity = 5 });
        Items.Add(new ReStockItem { Name = "Oranges", Quantity = 2 });
        Items.Add(new ReStockItem { Name = "Bananas", Quantity = 10 });
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var items = await App.Database.GetItemsAsync();
        Items.Clear();
        foreach (var item in items)
            Items.Add(item);

        ApplySort(SortPicker.SelectedIndex >= 0 ? SortPicker.SelectedIndex : 0, Items.ToList());
    }

    void OnItemAdded(object sender, EventArgs e)
    {
        var newItem = new ReStockItem();
        
        Items.Add(newItem);
        App.Database.SaveItemAsync(newItem);
        Navigation.PushAsync(new ReStockItemPage(newItem));
    }


    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ReStockItem item)
            return;

        // Navigate to edit page
        Navigation.PushAsync(new ReStockItemPage { Item = item });

        // Deselect item to avoid multiple triggers
        ((CollectionView)sender).SelectedItem = null;
    }

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SortPicker.SelectedIndex < 0)
            return;

        ApplySort(SortPicker.SelectedIndex, Items.ToList());
    }
}
