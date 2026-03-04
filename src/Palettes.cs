using Clay_cs;
using Raylib_cs;

public static class Palettes
{
    public static Texture2D batman;
    public static Texture2D joker;
    public static Texture2D harley;
    public static Texture2D posion;
    public static Texture2D freeze;
    public static Texture2D bane;
    
    public static int stampIndex = 0;
    public static int selectedIndex = 0;
    public static readonly Clay_Color backgroundColor = new(88, 95, 97);

    public static Palette batmanPalette = new(
        "Batman",
        [
            new(0, 0, 0),
            new(54, 54, 54),
            new(255, 239, 207),
            new(214, 186, 0)
        ],
        "Assets/batman.png"
    );

    public static Palette jokerPalette = new(
        "Joker",
        [
            new(48, 130, 0),
            new(179, 50, 36),
            new(255, 230, 227),
            new(190, 51, 255),
        ],
        "Assets/joker.png"
    );

    public static Palette harleyPalette = new(
        "Harley Quinn",
        [
            new(140, 14, 0),
            new(25, 25, 26),
            new(194, 194, 194),
            new(255, 255, 217)
        ],
        "Assets/harleyquinn.png"
    );

    public static Palette posionPalette = new(
        "Posion Ivy",
        [
            new(244, 225, 217),
            new(120, 212, 99),
            new(30, 79, 19),
            new(201, 46,  18)
        ],
        "Assets/posionivy.png"
    );

    public static Palette freezePalette = new(
        "Mr Freeze",
        [
            new(133, 116, 161),
            new(169, 231, 235),
            new(230, 117, 255),
            new(94, 83, 0)
        ],
        "Assets/mrfreeze.png"
    );

    public static Palette banePalette = new(
        "Bane",
        [
            new(176, 0, 47),
            new(48, 27, 0),
            new(171, 171, 171),
            new(242, 178, 94)
        ],
        "Assets/bane.png"
    );

    public static Palette[] palettes =
    {
        batmanPalette,
        jokerPalette,
        harleyPalette,
        posionPalette,
        freezePalette,
        banePalette
    };

     public static void Initialize()
    {
        foreach (Palette palette in palettes)
        {
            palette.Initialize();
        }
    }
    
    public static void LoadIcons()
    {
        for (int i = 0; i < palettes.Length; i++)
        {
            palettes[i].icon = Raylib.LoadTexture(palettes[i].iconPath);
        }
    }

    public static void UnloadIcons()
    {
        foreach (var palette in palettes)
            Raylib.UnloadTexture(palette.icon);
    }
}

public struct Palette
{
    public readonly string name;
    public readonly Clay_Color[] colors;
    public readonly string iconPath;
    public Texture2D icon;

    public Palette(string name, Clay_Color[] colors, string iconPath)
    {
        this.name = name;
        this.colors = colors;
        this.iconPath = iconPath;
        this.icon = default;
    }
    public int selectedColor = 0;
    public readonly Image stampImage = Raylib.LoadImage($"assets/stamps/{name.ToLower().Replace(" ", "")}.png");
    public Texture2D stampIcon;
    public readonly Sound voiceLine = Raylib.LoadSound($"assets/audio/{name.ToLower().Replace(" ", "")}.wav");

    public void Initialize()
    {
        Image smallImage = Raylib.LoadImage($"assets/stamps/{name.ToLower().Replace(" ", "")}.png");
        Raylib.ImageResize(ref smallImage, 70, 70);
        stampIcon = Raylib.LoadTextureFromImage(smallImage);
        Raylib.UnloadImage(smallImage);
    }

    public Color GetRaylibColor()
    {
        var clayColor = colors[selectedColor];
        return new Color((byte)clayColor.r, (byte)clayColor.g, (byte)clayColor.b, (byte)clayColor.a);
    }
}
