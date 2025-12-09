using SQLite;
using ReStock.Models;

namespace ReStock.Data;

public class ReStockItemDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public ReStockItemDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        // Creates the table if it doesn't exist
        _database.CreateTableAsync<ReStockItem>().Wait();
    }

    public Task<List<ReStockItem>> GetItemsAsync() =>
        _database.Table<ReStockItem>().ToListAsync();

    public Task<ReStockItem> GetItemAsync(int id) =>
        _database.Table<ReStockItem>().Where(i => i.ID == id).FirstOrDefaultAsync();

    public Task<int> SaveItemAsync(ReStockItem item) =>
        item.ID != 0 ? _database.UpdateAsync(item) : _database.InsertAsync(item);

    public Task<int> DeleteItemAsync(ReStockItem item) =>
        _database.DeleteAsync(item);
}
