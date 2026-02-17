using System.Numerics;
using Raylib_cs;

namespace GothamPaint;

public class Camera
{
    private Camera2D rlCamera;
    private Vector2 DragStart { get; set; }
    private Vector2 DragCurrent { get; set; }

    public Camera(int canvasWidth, int canvasHeight)
    {
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        rlCamera = new Camera2D
        {
            Target = Vector2.Zero,
            Offset = new Vector2(screenWidth / 2 - canvasWidth / 2, screenHeight / 2 - canvasHeight / 2),
            Rotation = 0,
            Zoom = 1
        };
    }

    public void Move()
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            DragStart = Raylib.GetMousePosition();
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            DragCurrent = Raylib.GetMousePosition();
            Vector2 delta = Vector2.Subtract(DragCurrent, DragStart);
            rlCamera.Target = Vector2.Subtract(rlCamera.Target, delta);
            DragStart = DragCurrent;
        }
    }

    public void Begin()
    {
        Raylib.BeginMode2D(rlCamera);
    }

    public void End()
    {
        Raylib.EndMode2D();
    }

    public Vector2 WorldMousePosition() => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), rlCamera);
}
