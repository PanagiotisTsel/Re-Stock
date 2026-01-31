using Microsoft.Maui.Graphics;

namespace ReStock.Models
{
    public class ColorPicker
    {
        public Color Color { get; set; }

        public string Name { get; set; } = "";

        public bool IsSelected { get; set; } = false;
    }
}
