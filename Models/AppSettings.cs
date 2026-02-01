using SQLite;

namespace ReStock.Models;

public class AppSettings
{
    [PrimaryKey]
    public int Id { get; set; } = 1; 

    public bool IsDarkMode { get; set; }

    // Threshold value for low stock
    public int LowStockThreshold { get; set; } = 2;

    // Store color as hex for SQLite
    public string LowStockColorHex { get; set; } = Colors.LightPink.ToHex();

    [Ignore]
    public Color LowStockColor
    {
        get => Color.FromArgb(LowStockColorHex);
        set => LowStockColorHex = value.ToHex();
    }
}
