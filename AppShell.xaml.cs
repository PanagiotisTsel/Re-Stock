using ReStock.Views;

namespace ReStock;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ReStockItemPage), typeof(ReStockItemPage));
    }
}
