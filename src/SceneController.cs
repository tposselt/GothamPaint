using Clay_cs;
using GothamPaint;
using Raylib_cs;

public static class SceneController
{
    public static void DrawScene(Camera camera, Canvas canvas)
    {
        camera.Move();
        camera.Begin();

        canvas.canvasLocked = Layout.HoveringGUI;
        canvas.Draw(camera.WorldMousePosition());

        camera.End();
    }

    public static void DrawUI()
    {
        Clay.BeginLayout();

        Layout.Sidebar();

        var commands = Clay.EndLayout();
        RaylibClay.RenderCommands(commands);
    }
}
