using ReStock.Models;
using ReStock.Data;

namespace ReStock.Views;

public partial class ReStockSettings : ContentPage
{
    
 public ReStockSettings()
    {
        InitializeComponent(); 
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var settings = await App.Database.GetSettingsAsync();
        DarkModeSwitch.IsToggled = settings?.IsDarkMode ?? false;
    }

    async void DarkModeMode(object sender, ToggledEventArgs e)
    {
        App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;

        var settings = new AppSettings { Id = 1, IsDarkMode = e.Value };
        await App.Database.SaveSettingsAsync(settings);
    }



}

