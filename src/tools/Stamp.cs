using Clay_cs;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;

namespace GothamPaint;

public class Stamp(Texture2D canvasTexture) : PaintTool(canvasTexture)
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
            Color selectedColor = Palettes.palettes[Palettes.selectedIndex].GetRaylibColor();
            Image stampImage = Palettes.palettes[Palettes.stampIndex].stampImage;

            Raylib.ImageDraw(
                ref canvasImage, 
                stampImage, 
                new Rectangle(0, 0, stampImage.Width, stampImage.Height), 
                new Rectangle(
                    mousePos.X - (stampImage.Width/2), 
                    mousePos.Y - (stampImage.Height/2), 
                    stampImage.Width, stampImage.Height
                ), 
                selectedColor
            );
        }

        prevMousePos = mousePos;
        updateCanvas = true;
    }
}