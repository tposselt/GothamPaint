using Raylib_cs;
using Clay_cs;
using GothamPaint;

public class Program
{
    public static unsafe void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Gotham Paint");
        Raylib.SetTargetFPS(60);

        using var arena = Clay.CreateArena(Clay.MinMemorySize());
        Clay.Initialize(arena, new Clay_Dimensions(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), ErrorHandler);
        Clay.SetDebugModeEnabled(false);

        Layout.InitializeText("");
        Camera camera = new();

        while (!Raylib.WindowShouldClose())
        {
            Clay.SetLayoutDimensions(new Clay_Dimensions(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
            Clay.SetPointerState(Raylib.GetMousePosition(), Raylib.IsMouseButtonDown(0));
            Clay.UpdateScrollContainers(true, Raylib.GetMouseWheelMoveV(), Raylib.GetFrameTime());

            camera.Move();

            Clay.BeginLayout();
            Layout.Sidebar();
            var commands = Clay.EndLayout();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.GetColor(0x2C3E50FF));

            camera.Begin();
            Raylib.DrawRectangle(0, 0, 200, 200, Raylib.GetColor(0xECF0F1FF));
            camera.End();

            RaylibClay.RenderCommands(commands);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static void ErrorHandler(Clay_ErrorData data)
    {
        Console.WriteLine($"{data.errorType}: {data.errorText.ToCSharpString()}");
    }
}
