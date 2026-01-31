using Microsoft.Maui.Graphics;
using ReStock.Data;
using ReStock.Models;

namespace ReStock.Views;

public partial class ReStockSettings : ContentPage
{
    private Color selectedColor;

    public ReStockSettings()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var settings = await App.Database.GetSettingsAsync();
        DarkModeSwitch.IsToggled = settings?.IsDarkMode ?? false;
        ThresholdEntry.Text = settings?.LowStockThreshold.ToString() ?? "2";
        selectedColor = settings?.LowStockColor ?? Colors.LightPink;
    }

    void DarkModeMode(object sender, ToggledEventArgs e)
    {
        App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    void ColorSelected(object sender, EventArgs e)
    {
        if (sender is Button btn)
            selectedColor = btn.BackgroundColor;
    }

    async void SaveSettings(object sender, EventArgs e)
    {
        if (!int.TryParse(ThresholdEntry.Text, out int threshold))
            threshold = 2;

        var settings = new AppSettings
        {
            Id = 1,
            LowStockThreshold = threshold,
            LowStockColor = selectedColor,
            IsDarkMode = DarkModeSwitch.IsToggled
        };

        await App.Database.SaveSettingsAsync(settings);

        await DisplayAlert("Settings", "Low stock settings saved!", "OK");

        // Refresh list page colors
        var page = Application.Current.Windows[0].Page;
        if (page is NavigationPage navPage &&
            navPage.CurrentPage is ReStockListPage listPage)
        {
            foreach (var item in listPage.Items)
                item.NotifyPropertyChanged(nameof(item.BackgroundColor));
        }
    }
}
