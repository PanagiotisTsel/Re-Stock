using SQLite;

namespace ReStock.Models;

public class ReStockItem
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public int Quantity {get; set;}
    public bool Done { get; set; }
}
