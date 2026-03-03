using Clay_cs;
using Raylib_cs;
using System.Numerics;

namespace GothamPaint;

public class Pencil(Texture2D canvasTexture, bool eraser = false) : PaintTool(canvasTexture)
{
    private Vector2 prevMousePos;
    private Vector2[] prevCorners = new Vector2[2];
    private int BrushSize { get; set; } = 10;

    public override void Draw(ref Image canvasImage, Vector2 mousePos, out bool updateCanvas)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            prevMousePos = mousePos;
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            Color selectedColor = ( eraser ?
                RaylibClay.ToColor(Palettes.backgroundColor) :
                Palettes.palettes[Palettes.selectedIndex].GetRaylibColor() 
            );
            if (BrushSize == 1)
            {
                Raylib.ImageDrawLineV(ref canvasImage, prevMousePos, mousePos, selectedColor);
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
                DrawThickLine(ref canvasImage, prevCorners, corners, selectedColor);
                prevCorners = corners;
            }
        }

        prevMousePos = mousePos;
        updateCanvas = true;
    }

    private void DrawThickLine(ref Image canvasImage, Vector2[] start, Vector2[] end, Color selectedColor)
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
                    Raylib.ImageDrawPixel(ref canvasImage, x, y, selectedColor);
                    currentArea++;
                }
            }
        }
    }
}