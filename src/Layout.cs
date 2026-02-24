using Clay_cs;

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
                    }))
                    {
                        Clay.OnHover((id, pointer, userData) =>
                        {
                            if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_PRESSED_THIS_FRAME)
                            {
                                Canvas.SelectedToolIndex = Tool.Pencil;
                            }
                        });
                    }

                    using (Clay.Element(Clay.Id("EraserButton"), new()
                    {
                        backgroundColor = new Clay_Color(255, 255, 255),
                        layout = new()
                        {
                            sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(toolButtonSize), Clay_SizingAxis.Fixed(toolButtonSize)),
                        }
                    }))
                    {
                        Clay.OnHover((id, pointer, userData) =>
                        {
                            if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_PRESSED_THIS_FRAME)
                            {
                                Canvas.SelectedToolIndex = Tool.Eraser;
                            }
                        });
                    }

                    using (Clay.Element(Clay.Id("FloodButton"), new()
                    {
                        backgroundColor = new Clay_Color(255, 255, 255),
                        layout = new()
                        {
                            sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(toolButtonSize), Clay_SizingAxis.Fixed(toolButtonSize)),
                        }
                    }))
                    {
                        Clay.OnHover((id, pointer, userData) =>
                        {
                            if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_PRESSED_THIS_FRAME)
                            {
                                Canvas.SelectedToolIndex = Tool.Flood;
                            }
                        });
                    }
                }

                using (Clay.Element(Clay.Id("Colors"), new()
                {
                    backgroundColor = new Clay_Color(10, 55, 73),
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow()),
                        padding = Clay_Padding.All(10),
                        layoutDirection = Clay_LayoutDirection.CLAY_LEFT_TO_RIGHT,
                        childAlignment = new() { x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT, y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_CENTER },
                        childGap = 10,
                    }
                }))
                {
                    Palette palette = Palettes.palettes[Palettes.selectedIndex];
                    for (int i = 0; i < palette.palette.Length; i++)
                    {
                        Clay_Color color = palette.palette[i];
                        int currentColorIndex = i;
                        float modifier = i == palette.selectedColor ? 1 : 0.8f;
                        using (Clay.Element(Clay.Id(palette.name + currentColorIndex), new()
                        {
                            backgroundColor = color,
                            layout = new()
                            {
                                sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(toolButtonSize * modifier), Clay_SizingAxis.Fixed(toolButtonSize * modifier)),
                            }
                        }))
                        {
                            Clay.OnHover((id, pointer, userData) =>
                            {
                                if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_PRESSED_THIS_FRAME)
                                {
                                    Palettes.palettes[Palettes.selectedIndex].selectedColor = currentColorIndex;
                                }
                            });
                        }
                    }
                }
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
                for (int i = 0; i < Palettes.palettes.Length; i++)
                {
                    Palette palette = Palettes.palettes[i];
                    int currentPaletteIndex = i;
                    using (Clay.Element(Clay.Id(palette.name), new()
                    {
                        backgroundColor = i == Palettes.selectedIndex ? new Clay_Color(9, 31, 46) : new Clay_Color(10, 55, 73),
                        layout = new()
                        {
                            sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(sidebarWidth / 2)),
                        }
                    }))
                    {
                        Clay.OnHover((id, pointer, userData) =>
                        {
                            if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_PRESSED_THIS_FRAME)
                            {
                                Palettes.selectedIndex = currentPaletteIndex;
                            }
                        });
                    }
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
