using Clay_cs;
using GothamPaint;
using Raylib_cs;

public static class SceneController
{
    public static void DrawScene(Camera camera, Canvas canvas)
    {
        camera.Move();
        camera.Begin();
        if (!Layout.IsResizeCanvasOpen)
        {
            canvas.Draw(camera.WorldMousePosition());
        }
        camera.End();
    }

    public static void DrawUI()
    {
        Clay.BeginLayout();
        Layout.Sidebar();
        var commands = Clay.EndLayout();

        if (Raylib.GetKeyPressed() == (int)KeyboardKey.C)
        {
            Layout.ToggleResizeCanvas();
        }

        RaylibClay.RenderCommands(commands);
    }
}
