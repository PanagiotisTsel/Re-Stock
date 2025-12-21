using SQLite;
using ReStock.Models;

namespace ReStock.Data;

public class ReStockItemDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public ReStockItemDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        //_database.DropTableAsync<ReStockItem>().Wait();
        _database.CreateTableAsync<ReStockItem>().Wait();
    }

    public Task<List<ReStockItem>> GetItemsAsync() =>
        _database.Table<ReStockItem>().ToListAsync();

    public Task<ReStockItem> GetItemAsync(int id) =>
        _database.Table<ReStockItem>().Where(i => i.ID == id).FirstOrDefaultAsync();

   public async Task<int> SaveItemAsync(ReStockItem item)
{
    if (item.ID != 0)
    {
        Console.WriteLine($"Updating item ID={item.ID}, Name={item.Name}");
        return await _database.UpdateAsync(item);
    }
    else
    {
        int result = await _database.InsertAsync(item);
        Console.WriteLine($"Inserted new item: ID={item.ID}, Name={item.Name}, Qty={item.Quantity}");
        return result;
    }
}


    public Task<int> DeleteItemAsync(ReStockItem item) =>
        _database.DeleteAsync(item);
}
