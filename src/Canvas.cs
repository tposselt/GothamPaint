using System.Numerics;
using Raylib_cs;

namespace GothamPaint;

public class Canvas : IDisposable
{
    public int Width { get; private init; }
    public int Height { get; private init; }
    private Image canvasImage;
    private Texture2D canvasTexture;
    private bool refreshTexture;

    public Canvas(int width, int height)
    {
        Width = width;
        Height = height;
        canvasImage = Raylib.GenImageColor(Width, Height, Color.White);
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

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            int x = (int)mousePos.X;
            int y = (int)mousePos.Y;
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                Raylib.ImageDrawPixel(ref canvasImage, x, y, Color.Black);
                refreshTexture = true;
            }
        }

        Raylib.DrawTexture(canvasTexture, 0, 0, Color.White);
    }

    public void Dispose()
    {
        Raylib.UnloadTexture(canvasTexture);
        Raylib.UnloadImage(canvasImage);
    }
}