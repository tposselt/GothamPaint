using Clay_cs;
using Raylib_cs;
using System.Security.AccessControl;

namespace GothamPaint;

public static class Layout
{
    public static unsafe void InitializeText(string font)
    {
        Clay.SetMeasureTextFunction(RaylibClay.MeasureText);
    }

    private const int sidebarWidth = 200;

    public static void Sidebar()
    {
        using (Clay.Element(Clay.Id("FullContainer"), new()
        {
            layout = new()
            {
                sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow()),
                layoutDirection = Clay_LayoutDirection.CLAY_TOP_TO_BOTTOM,
            },
        }))
        {
            using (Clay.Element(Clay.Id("Taskbar"), new()
            {
                backgroundColor = new Clay_Color(36, 83, 97),
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(100)),
                    padding = Clay_Padding.All(5),
                    layoutDirection = Clay_LayoutDirection.CLAY_LEFT_TO_RIGHT,
                    childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_CENTER },
                    childGap = 5,
                }
            }))
            {
                using (Clay.Element(Clay.Id("Logo"), new()
                {
                    backgroundColor = new Clay_Color(10, 55, 73),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(sidebarWidth - 10), Clay_SizingAxis.Grow()),
                    }
                })) { }

                int toolButtonSize = 60;

                using (Clay.Element(Clay.Id("Tools"), new()
                {
                    backgroundColor = new Clay_Color(10, 55, 73),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Fixed((toolButtonSize * 3) + 40), Clay_SizingAxis.Grow()),
                        padding = Clay_Padding.All(10),
                        layoutDirection = Clay_LayoutDirection.CLAY_LEFT_TO_RIGHT,
                        childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_CENTER },
                        childGap = 10,
                    }
                }))
                {

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

                using (Clay.Element(Clay.Id("Colors"), new()
                {
                    backgroundColor = new Clay_Color(10, 55, 73),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow()),
                    }
                })) { }
            }

            using (Clay.Element(Clay.Id("Sidebar"), new()
            {
                backgroundColor = new Clay_Color(36, 83, 97),
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(sidebarWidth), Clay_SizingAxis.Grow()),
                    padding = new()
                    {
                        left = 5,
                        right = 5,
                        bottom = 5
                    },
                    layoutDirection = Clay_LayoutDirection.CLAY_TOP_TO_BOTTOM,
                    childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_CENTER, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP },
                    childGap = 5,
                }
            }))
            {
                // 9, 31, 46 for selected pallete background when we implement that
                foreach (Palette palette in Palettes.palettes)
                {
                    using (Clay.Element(Clay.Id(palette.name), new()
                    {
                        //backgroundColor = palette.palette[0],
                        backgroundColor = new Clay_Color(10, 55, 73),
                        layout = new()
                        {
                            sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(sidebarWidth / 2)),
                        }
                    })) { }
                }
                using (Clay.Element(Clay.Id("Filer"), new()
                {
                    backgroundColor = new Clay_Color(10, 55, 73),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow()),
                    }
                })) { }
            }
        }
    }
}
