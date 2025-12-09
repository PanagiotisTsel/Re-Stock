using ReStock.Data;
using ReStock.Views;

namespace ReStock;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var registeredFonts = new HashSet<string>();

        builder
            .UseMauiApp<App>();

        builder.ConfigureFonts(fonts =>
        {
            void AddFontOnce(string file, string alias)
            {
                if (!registeredFonts.Contains(alias))
                {
                    fonts.AddFont(file, alias);
                    registeredFonts.Add(alias);
                }
            }

            AddFontOnce("OpenSans-Regular.ttf", "OpenSansRegular");
            AddFontOnce("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        builder.Services.AddSingleton<ReStockListPage>();
        builder.Services.AddTransient<ReStockItemPage>();
        builder.Services.AddSingleton<ReStockItemDatabase>();

        return builder.Build();
    }
}
