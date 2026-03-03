using Clay_cs;
using Raylib_cs;

namespace GothamPaint;

public static class Layout
{
    public static bool IsResizeCanvasOpen { get; private set; } = false;
    public delegate void OnResizeCanvas(int width, int height);
    public static OnResizeCanvas ResizeCallback = delegate { };

    private static Clay_Color BackgroundColor = new(10, 55, 73);
    private static Clay_Color BackgroundColorLight = new(36, 83, 97);

    private static Image logoImage = Raylib.LoadImage("assets/images/logo.png");
    private static Texture2D logoTexture = Raylib.LoadTextureFromImage(logoImage);

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
                backgroundColor = BackgroundColorLight,
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
                    backgroundColor = BackgroundColor,
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(sidebarWidth - 10), Clay_SizingAxis.Grow()),
                    }
                })) { }

                int toolButtonSize = 60;

                using (Clay.Element(Clay.Id("Tools"), new()
                {
                    backgroundColor = BackgroundColor,
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
                    backgroundColor = BackgroundColor,
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

                    using (Clay.Element(Clay.Id(palette.name + "stamp"), new()
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
                                Palettes.stampIndex = Palettes.selectedIndex;
                                Canvas.SelectedToolIndex = Tool.Stamp;
                            }
                        });
                    }
                }
            }

            using (Clay.Element(Clay.Id("Sidebar"), new()
            {
                backgroundColor = BackgroundColorLight,
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
                        backgroundColor = i == Palettes.selectedIndex ? new Clay_Color(9, 31, 46) : BackgroundColor,
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
                                Raylib.PlaySound(Palettes.palettes[currentPaletteIndex].voiceLine);
                                Palettes.selectedIndex = currentPaletteIndex;
                            }
                        });
                    }
                }
                using (Clay.Element(Clay.Id("Filer"), new()
                {
                    backgroundColor = BackgroundColor,
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Grow()),
                    }
                })) { }
            }
        }

        if (IsResizeCanvasOpen)
        {
            using (Clay.Element(Clay.Id("ResizeCanvasBox"), new()
            {
                backgroundColor = BackgroundColor,
                border = new()
                {
                    color = BackgroundColorLight,
                    width = new() { left = 5, top = 5, right = 5, bottom = 5 },
                },
                floating = new()
                {
                    offset = new() { x = 0, y = 0 },
                    attachTo = Clay_FloatingAttachToElement.CLAY_ATTACH_TO_ROOT,
                    attachPoints = new()
                    {
                        element = Clay_FloatingAttachPointType.CLAY_ATTACH_POINT_CENTER_CENTER,
                        parent = Clay_FloatingAttachPointType.CLAY_ATTACH_POINT_CENTER_CENTER,
                    }
                },
                layout = new()
                {
                    sizing = new Clay_Sizing(Clay_SizingAxis.Fixed(400), Clay_SizingAxis.Fixed(400)),
                    padding = new() { left = 5, top = 5, right = 5, bottom = 5 },
                    layoutDirection = Clay_LayoutDirection.CLAY_TOP_TO_BOTTOM,
                    childAlignment = new()
                    {
                        x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_CENTER,
                        y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP,
                    },
                    childGap = 25,
                }
            }))
            {
                using (Clay.Element(Clay.Id("ResizeCanvasText"), new()
                {
                    layout = new()
                    {
                        sizing = new Clay_Sizing(Clay_SizingAxis.Grow(), Clay_SizingAxis.Fixed(50)),
                        childAlignment = new()
                        {
                            x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT,
                            y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP,
                        }
                    }
                }))
                {
                    Clay.TextElement("Resize Canvas", new()
                    {
                        fontSize = 40,
                        letterSpacing = 4,
                        textColor = BackgroundColorLight,
                    });
                }

                using (Clay.Element(Clay.Id("SmallCanvasButton"), new()
                {
                    layout = new()
                    {
                        sizing = new(Clay_SizingAxis.Fixed(150), Clay_SizingAxis.Fixed(40)),
                        padding = new() { left = 10, right = 10, top = 0, bottom = 0 },
                    },
                    border = new()
                    {
                        color = BackgroundColorLight,
                        width = new() { left = 5, top = 5, right = 5, bottom = 5 },
                    }
                }))
                {
                    Clay.TextElement("Small", new()
                    {
                        fontSize = 40,
                        letterSpacing = 2,
                        textColor = BackgroundColorLight,
                    });

                    Clay.OnHover((id, pointer, userData) =>
                    {
                        if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_RELEASED_THIS_FRAME)
                        {
                            ResizeCallback.Invoke(400, 400);
                            IsResizeCanvasOpen = false;
                        }
                    });
                }

                using (Clay.Element(Clay.Id("MediumCanvasButton"), new()
                {
                    layout = new()
                    {
                        sizing = new(Clay_SizingAxis.Fixed(150), Clay_SizingAxis.Fixed(40)),
                        padding = new() { left = 10, right = 10, top = 0, bottom = 0 },
                    },
                    border = new()
                    {
                        color = BackgroundColorLight,
                        width = new() { left = 5, top = 5, right = 5, bottom = 5 },
                    }
                }))
                {
                    Clay.TextElement("Medium", new()
                    {
                        fontSize = 40,
                        letterSpacing = 2,
                        textColor = BackgroundColorLight,
                    });

                    Clay.OnHover((id, pointer, userData) =>
                    {
                        if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_RELEASED_THIS_FRAME)
                        {
                            ResizeCallback.Invoke(800, 600);
                            IsResizeCanvasOpen = false;
                        }
                    });
                }

                using (Clay.Element(Clay.Id("LargeCanvasButton"), new()
                {
                    layout = new()
                    {
                        sizing = new(Clay_SizingAxis.Fixed(150), Clay_SizingAxis.Fixed(40)),
                        padding = new() { left = 10, right = 10, top = 0, bottom = 0 },
                    },
                    border = new()
                    {
                        color = BackgroundColorLight,
                        width = new() { left = 5, top = 5, right = 5, bottom = 5 },
                    }
                }))
                {
                    Clay.TextElement("Large", new()
                    {
                        fontSize = 40,
                        letterSpacing = 2,
                        textColor = BackgroundColorLight,
                    });

                    Clay.OnHover((id, pointer, userData) =>
                    {
                        if (pointer.state == Clay_PointerDataInteractionState.CLAY_POINTER_DATA_RELEASED_THIS_FRAME)
                        {
                            ResizeCallback.Invoke(1200, 800);
                            IsResizeCanvasOpen = false;
                        }
                    });
                }
            }
        }
    }

    public static void ToggleResizeCanvas()
    {
        IsResizeCanvasOpen = !IsResizeCanvasOpen;
    }
}
