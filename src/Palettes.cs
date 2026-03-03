using Clay_cs;
using Raylib_cs;

public static class Palettes
{
    public static int stampIndex = 0;
    public static int selectedIndex = 0;
    public static readonly Clay_Color backgroundColor = new(88, 95, 97);

    public static readonly Palette batmanPalette = new("Batman", [
        new(0, 0, 0),
        new(255, 255, 0),
    ]);

    public static readonly Palette jokerPalette = new("Joker", [
        new(255, 255, 255),
        new(0, 255, 0),
    ]);

    public static readonly Palette[] palettes = [
        batmanPalette,
        jokerPalette,
    ];
    public static void Initialize()
    {
        foreach (Palette palette in palettes)
        {
            palette.Initialize();
        }
    }
}

public class Palette(string name, Clay_Color[] colors)
{
    public readonly string name = name;
    public readonly Clay_Color[] palette = colors;
    public int selectedColor = 0;
    public readonly Image stampImage = Raylib.LoadImage($"assets/stamps/{name.ToLower()}.png");
    public Texture2D stampIcon;
    public readonly Sound voiceLine = Raylib.LoadSound($"assets/audio/{name.ToLower()}.wav");

    public void Initialize()
    {
        Image smallImage = Raylib.LoadImage($"assets/stamps/{name.ToLower()}.png");
        Raylib.ImageResize(ref smallImage, 70, 70);
        stampIcon = Raylib.LoadTextureFromImage(smallImage);
    }

    public Color GetRaylibColor()
    {
        var clayColor = palette[selectedColor];
        return new Color((byte)clayColor.r, (byte)clayColor.g, (byte)clayColor.b, (byte)clayColor.a);
    }
}
