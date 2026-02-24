using System.Numerics;
using Raylib_cs;

public class FloodFill(Texture2D canvasTexture) : PaintTool(canvasTexture)
{
    private readonly Stack<Span> spanStack = [];

    public override void Draw(ref Image canvasImage, Vector2 mousePos, out bool updateCanvas)
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Palette selectedPalette = Palettes.palettes[Palettes.selectedIndex];
            Vector2 seedPoint = new(mousePos.X, mousePos.Y);
            Color targetColor = Raylib.GetImageColor(canvasImage, (int)seedPoint.X, (int)seedPoint.Y);
            Color replacementColor = selectedPalette.GetRaylibColor();

            if (!targetColor.Equals(replacementColor))
            {
                FloodFillAlgorithm(ref canvasImage, seedPoint, targetColor, replacementColor);
            }

            updateCanvas = true;
        }
        else
        {
            updateCanvas = false;
        }
    }

    private void FloodFillAlgorithm(ref Image image, Vector2 point, Color targetColor, Color replacementColor)
    {
        spanStack.Push(FillSpan(ref image, point, targetColor, replacementColor));

        while (spanStack.Count > 0)
        {
            Span span = spanStack.Pop();
            for (int i = span.lx; i <= span.rx; i++)
            {
                if (span.y + 1 < (int)image.Dimensions.Y && Raylib.GetImageColor(image, i, span.y + 1).Equals(targetColor))
                {
                    spanStack.Push(FillSpan(ref image, new Vector2(i, span.y + 1), targetColor, replacementColor));
                }

                if (span.y - 1 >= 0 && Raylib.GetImageColor(image, i, span.y - 1).Equals(targetColor))
                {
                    spanStack.Push(FillSpan(ref image, new Vector2(i, span.y - 1), targetColor, replacementColor));
                }
            }
        }
    }

    private unsafe Span FillSpan(ref Image image, Vector2 point, Color targetColor, Color replacementColor)
    {
        int lx = (int)point.X;
        int rx = (int)point.X;
        int y = (int)point.Y;

        Color lxColor = Raylib.GetImageColor(image, Math.Max(lx - 1, 0), y);
        while (lxColor.Equals(targetColor))
        {
            Raylib.ImageDrawPixel(ref image, lx, y, replacementColor);
            if (lx <= 0) break;

            lx--;
            lxColor = Raylib.GetImageColor(image, lx, y);
        }

        Color rxColor = Raylib.GetImageColor(image, Math.Min(rx + 1, (int)image.Dimensions.X - 1), y);
        while (rxColor.Equals(targetColor))
        {
            Raylib.ImageDrawPixel(ref image, rx, y, replacementColor);
            if (rx >= (int)image.Dimensions.X - 1) break;

            rx++;
            rxColor = Raylib.GetImageColor(image, rx, y);
        }

        Color* pixels = Raylib.LoadImageColors(image);
        Raylib.UpdateTexture(canvasTexture, pixels);

        return new(lx, rx, y);
    }
}

struct Span(int lx, int rx, int y)
{
    public readonly int lx = lx;
    public readonly int rx = rx;
    public readonly int y = y;
}