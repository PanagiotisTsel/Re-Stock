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
            AddFontOnce("Font Awesome 7 Brands-Regular-400.otf", "BrandsRegular");
            AddFontOnce("Font Awesome 7 Free-Regular-400.otf", "FreeRegular");
            AddFontOnce("Font Awesome 7 Free-Solid-900.otf", "FreeSolid");;
        });

        builder.Services.AddSingleton<ReStockListPage>();
        builder.Services.AddTransient<ReStockItemPage>();
        builder.Services.AddSingleton<ReStockItemDatabase>();

        return builder.Build();
    }
}
