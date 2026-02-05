using Clay_cs;
using Raylib_cs;

namespace GothamPaint;

public static class Layout
{
    public static unsafe void InitializeText(string font)
    {
        Clay.SetMeasureTextFunction(RaylibClay.MeasureText);
    }

    public static void Sidebar()
    {
        using (Clay.Element(Clay.Id("Container"), new()
        {
            backgroundColor = new Clay_Color(25, 0, 25),
            layout = new()
            {
                sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(100), Clay_SizingAxis.Grow()),
                padding = new Clay_Padding { left = 10, right = 10, top = 10, bottom = 10 },
                layoutDirection = Clay_LayoutDirection.CLAY_TOP_TO_BOTTOM,
                childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_CENTER, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP }
            }
        }))
        {
            using (Clay.Element(Clay.Id("Paint Brush Button"), new()
            {
                backgroundColor = new Clay_Color(255, 255, 255),
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(50), Clay_SizingAxis.Fixed(50)),
                }
            }))
            {

            }
        }
    }
}
