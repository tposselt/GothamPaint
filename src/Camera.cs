using System.Numerics;
using Raylib_cs;

namespace GothamPaint;

public class Camera
{
    private Camera2D rlCamera;
    private Vector2 DragStart { get; set; }
    private Vector2 DragCurrent { get; set; }

    public Camera()
    {
        rlCamera = new Camera2D
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(0, 0),
            Rotation = 0,
            Zoom = 1
        };
    }

    public void Move()
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            DragStart = Raylib.GetMousePosition();
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
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
}
