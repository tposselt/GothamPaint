using System.Numerics;
using Clay_cs;
using Raylib_cs;

namespace GothamPaint;

public enum Tool
{
    Pencil,
    Eraser,
    Flood,
    Stamp,
}

public class Canvas : IDisposable
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    private Image canvasImage;
    private Texture2D canvasTexture;
    private bool refreshTexture;
    public static Tool SelectedToolIndex { get; set; } = Tool.Pencil;
    private readonly PaintTool[] tools = [];

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        canvasImage = Raylib.GenImageColor(Width, Height, RaylibClay.ToColor(Palettes.backgroundColor));
        canvasTexture = Raylib.LoadTextureFromImage(canvasImage);
        tools = [new Pencil(canvasTexture), new Pencil(canvasTexture, true), new FloodFill(canvasTexture), new Stamp(canvasTexture)];
        refreshTexture = true;
    }

    public void ResizeCanvas(int width, int height)
    {
        Width = width;
        Height = height;
        Raylib.UnloadTexture(canvasTexture);
        Raylib.UnloadImage(canvasImage);
        canvasImage = Raylib.GenImageColor(Width, Height, RaylibClay.ToColor(Palettes.backgroundColor));
        canvasTexture = Raylib.LoadTextureFromImage(canvasImage);
        refreshTexture = true;
    }

    public unsafe void Draw(Vector2 mousePos)
    {
        if (refreshTexture)
        {
            Color* pixels = Raylib.LoadImageColors(canvasImage);
            Raylib.UpdateTexture(canvasTexture, pixels);
            refreshTexture = false;
        }

        int x = (int)mousePos.X;
        int y = (int)mousePos.Y;
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            tools[(int)SelectedToolIndex].Draw(ref canvasImage, mousePos, out refreshTexture);
        }

        Raylib.DrawRectangle(-5, -5, Width + 10, Height + 10, new Color(36, 83, 97)); // border
        Raylib.DrawTexture(canvasTexture, 0, 0, Color.White);
    }

    public void Dispose()
    {
        Raylib.UnloadTexture(canvasTexture);
        Raylib.UnloadImage(canvasImage);
    }
}