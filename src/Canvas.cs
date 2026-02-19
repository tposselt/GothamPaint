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
    private Vector2 prevMousePos;
    private int BrushSize = 20;
    private Vector2[] prevCorners = new Vector2[2];

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

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            prevMousePos = mousePos;
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            int x = (int)mousePos.X;
            int y = (int)mousePos.Y;
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                if (BrushSize == 1)
                {
                    Raylib.ImageDrawLineV(ref canvasImage, prevMousePos, mousePos, Color.Black);
                }
                else
                {
                    Vector2 direction = Vector2.Normalize(Vector2.Subtract(mousePos, prevMousePos));
                    Vector2 perpendicular = new(-direction.Y * BrushSize / 2, direction.X * BrushSize / 2);
                    Vector2[] corners =
                    [
                        mousePos - perpendicular,
                        mousePos + perpendicular,
                    ];
                    DrawThickLine(prevCorners, corners);
                    prevCorners = corners;
                }
                prevMousePos = mousePos;
                refreshTexture = true;
            }
        }

        Raylib.DrawTexture(canvasTexture, 0, 0, Color.White);
    }

    public void DrawThickLine(Vector2[] start, Vector2[] end)
    {
        int totalArea = (int)MathF.Ceiling((start[1] - start[0]).Length() * (end[0] - start[1]).Length());
        int currentArea = 0;
        float minX = MathF.Min(MathF.Min(start[0].X, start[1].X), MathF.Min(end[0].X, end[1].X));
        float maxX = MathF.Max(MathF.Max(start[0].X, start[1].X), MathF.Max(end[0].X, end[1].X));
        float minY = MathF.Min(MathF.Min(start[0].Y, start[1].Y), MathF.Min(end[0].Y, end[1].Y));
        float maxY = MathF.Max(MathF.Max(start[0].Y, start[1].Y), MathF.Max(end[0].Y, end[1].Y));

        for (int y = (int)minY; y <= (int)maxY; y++)
        {
            for (int x = (int)minX; x <= (int)maxX; x++)
            {
                if (currentArea >= totalArea) return;

                if (Raylib.CheckCollisionPointPoly(new Vector2(x, y), [start[0], start[1], end[1], end[0]]))
                {
                    Raylib.ImageDrawPixel(ref canvasImage, x, y, Color.Black);
                    currentArea++;
                }
            }
        }
    }

    public void Dispose()
    {
        Raylib.UnloadTexture(canvasTexture);
        Raylib.UnloadImage(canvasImage);
    }
}