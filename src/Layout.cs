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
        using (Clay.Element(Clay.Id("FullContainer"), new()
        {
            layout = new()
            {
                sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow())
            },
        }))
        {
            using (Clay.Element(Clay.Id("Sidebar"), new()
            {
                backgroundColor = new Clay_Color(25, 0, 25),
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(200), Clay_SizingAxis.Grow()),
                    padding = Clay_Padding.All(10),
                    layoutDirection = Clay_LayoutDirection.CLAY_TOP_TO_BOTTOM,
                    childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_CENTER, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP },
                    childGap = 10,
                }
            }))
            {
                foreach (Palette palette in Palettes.palettes)
                {
                    using (Clay.Element(Clay.Id(palette.name), new()
                    {
                        backgroundColor = palette.palette[0],
                        layout = new()
                        {
                            sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(100)),
                        }
                    })) { }
                }
            }

            using (Clay.Element(Clay.Id("Taskbar"), new()
            {
                backgroundColor = new Clay_Color(25, 0, 25),
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(100)),
                    padding = Clay_Padding.All(10),
                    layoutDirection = Clay_LayoutDirection.CLAY_LEFT_TO_RIGHT,
                    childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_CENTER },
                    childGap = 10,
                }
            }))
            {
                int toolButtonSize = 60;
                using (Clay.Element(Clay.Id("PaintBrushButton"), new()
                {
                    backgroundColor = new Clay_Color(255, 255, 255),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(toolButtonSize), Clay_SizingAxis.Fixed(toolButtonSize)),
                    }
                })) { }

                using (Clay.Element(Clay.Id("EraserBrushButton"), new()
                {
                    backgroundColor = new Clay_Color(255, 255, 255),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(toolButtonSize), Clay_SizingAxis.Fixed(toolButtonSize)),
                    }
                })) { }
            }
        }
    }
}
