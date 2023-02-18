using Modrinth.JsonConverters;
using Newtonsoft.Json;

namespace Modrinth.Models;

[JsonConverter(typeof(ColorConverter))]
public struct Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public System.Drawing.Color color;
    
    public Color(long color)
    {
        R = (byte)(color >> 24);
        G = (byte)(color >> 16);
        B = (byte)(color >> 8);
        A = (byte)(color);
        this.color = System.Drawing.Color.FromArgb(A, R, G, B);
    }
}