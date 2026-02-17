using Raylib_cs;
using Clay_cs;
using GothamPaint;

public class Program
{
    public static unsafe void Main(string[] args)
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1200, 900, "Gotham Paint");
        Raylib.SetTargetFPS(30);

        int monitor = Raylib.GetCurrentMonitor();
        int monitorWidth = Raylib.GetMonitorWidth(monitor);
        int monitorHeight = Raylib.GetMonitorHeight(monitor) - 200; // Account for taskbar
        Console.WriteLine($"Monitor size: {monitorWidth}x{monitorHeight}");
        Raylib.SetWindowMaxSize(monitorWidth, monitorHeight);
        Raylib.SetWindowSize(Math.Min(1200, monitorWidth), Math.Min(900, monitorHeight));

        using var arena = Clay.CreateArena(Clay.MinMemorySize());
        Clay.Initialize(arena, new Clay_Dimensions(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), ErrorHandler);
        Clay.SetDebugModeEnabled(false);

        Layout.InitializeText("");
        using Canvas canvas = new(400, 400);
        Camera camera = new(canvas.Width, canvas.Height);

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
            canvas.Draw(camera.WorldMousePosition());
            camera.End();

            RaylibClay.RenderCommands(commands);
            Raylib.DrawFPS(Raylib.GetScreenWidth() - 100, Raylib.GetScreenHeight() - 20);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static void ErrorHandler(Clay_ErrorData data)
    {
        Console.WriteLine($"{data.errorType}: {data.errorText.ToCSharpString()}");
    }
}
