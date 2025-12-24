using SQLite;

namespace ReStock.Models;

public class AppSettings
{
    [PrimaryKey]
    public int Id { get; set; } = 1; 

    public bool IsDarkMode { get; set; }
}
