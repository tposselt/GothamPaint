using System.Numerics;
using Raylib_cs;

public abstract class PaintTool(Texture2D canvasTexture)
{
    protected Texture2D canvasTexture = canvasTexture;
    public abstract void Draw(ref Image canvas, Vector2 mousePos, out bool updateCanvas);
}