using Microsoft.Maui.Controls;

namespace ReStock
{
    public static class FontRegistrar
    {
        private static bool fontsRegistered = false;

        public static void RegisterFonts(IFontCollection fonts)
        {
            if (fontsRegistered)
                return;

            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

            fontsRegistered = true;
        }
    }
}
