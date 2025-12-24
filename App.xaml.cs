using ReStock.Data;
using ReStock.Views;

namespace ReStock;

public partial class App : Application
{
    public static ReStockItemDatabase Database { get; private set; }

    public App()
    {
        InitializeComponent();

        // Automatic database creation path
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "ReStockSQLite.db3");
        Console.WriteLine($"DB path file: {dbPath}");
        Database = new ReStockItemDatabase(dbPath);

        MainPage = new NavigationPage(new AppShell());
    }
}
