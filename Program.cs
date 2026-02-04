using Raylib_cs;

Raylib.InitWindow(800, 600, "Gotham Paint");
Raylib.SetTargetFPS(60);

while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Raylib.GetColor(0x2C3E50FF));
  Raylib.EndDrawing();
}

Raylib.CloseWindow();
