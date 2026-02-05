using System.Numerics;
using Raylib_cs;

namespace Clay_cs;

public static class RaylibClay
{
    public static Font[] Fonts = new Font[10];

    private static Color ToColor(Clay_Color color) => new()
    {
        R = (byte)MathF.Round(color.r),
        G = (byte)MathF.Round(color.g),
        B = (byte)MathF.Round(color.b),
        A = (byte)MathF.Round(color.a),
    };

    public static unsafe Clay_Dimensions MeasureText(Clay_StringSlice slice, Clay_TextElementConfig* config, void* userData)
    {
        var text = slice.ToCSharpString();

        // valid font?
        if (config->fontId > Fonts.Length) return default;
        var font = Fonts[config->fontId];
        if (font.Glyphs == null) return default;

        var factor = (float)config->fontSize / font.BaseSize;


        float maxTextWidth = 0;
        float currentTextWidth = 0;

        for (int i = 0; i < slice.length; i++)
        {
            var ch = slice.chars[i];
            if (ch == '\n')
            {
                maxTextWidth = Math.Max(maxTextWidth, currentTextWidth);
                currentTextWidth = 0;
                continue;
            }

            int index = slice.chars[i] - 32;
            if (font.Glyphs[index].AdvanceX != 0) currentTextWidth += font.Glyphs[index].AdvanceX;
            else currentTextWidth += font.Recs[index].Width + font.Glyphs[index].OffsetX;
        }

        maxTextWidth = Math.Max(maxTextWidth, currentTextWidth);
        return new Clay_Dimensions
        {
            width = maxTextWidth * factor,
            height = config->fontSize,
        };
    }

    public static unsafe void RenderCommands(Clay_RenderCommandArray array)
    {
        for (int i = 0; i < array.length; i++)
        {
            var renderCommand = Clay.RenderCommandArrayGet(array, i);
            Clay_BoundingBox boundingBox = renderCommand->boundingBox;

            switch (renderCommand->commandType)
            {
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_NONE:
                    break;
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_RECTANGLE:
                    {
                        var config = renderCommand->renderData.rectangle;

                        var box = new Rectangle(boundingBox.x, boundingBox.y, boundingBox.width, boundingBox.height);
                        var color = ToColor(config.backgroundColor);

                        if (config.cornerRadius.topLeft > 0)
                        {
                            float radius = (config.cornerRadius.topLeft * 2) /
                                           (float)((boundingBox.width > boundingBox.height) ? boundingBox.height : boundingBox.width);
                            Raylib.DrawRectangleRounded(box, radius, 8, color);
                        }
                        else
                        {
                            Raylib.DrawRectangleRec(box, color);
                        }

                        break;
                    }
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_BORDER:
                    // throw new NotImplementedException();
                    break;
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_TEXT:
                    {
                        var asStr = renderCommand->renderData.text.stringContents.ToCSharpString();
                        var span = renderCommand->renderData.text.stringContents.ToSpanOwner();
                        var font = Fonts[renderCommand->renderData.text.fontId];

                        try
                        {
                            fixed (sbyte* spanptr = span.Span)
                            {
                                Raylib.DrawTextEx(
                                    font, spanptr,
                                    new Vector2(boundingBox.x, boundingBox.y),
                                    renderCommand->renderData.text.fontSize,
                                    renderCommand->renderData.text.letterSpacing,
                                    ToColor(renderCommand->renderData.text.textColor)
                                );
                            }
                        }
                        finally
                        {
                            span.Dispose();
                        }
                        break;
                    }
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_IMAGE:
                    {
                        Texture2D texture = *(Texture2D*)renderCommand->renderData.image.imageData;
                        Raylib.DrawTextureEx(texture,
                            new Vector2(boundingBox.x, boundingBox.y),
                            0,
                            boundingBox.width / boundingBox.width,
                            Color.White
                        );
                        break;
                    }
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_SCISSOR_START:
                    {
                        Raylib.BeginScissorMode((int)boundingBox.x, (int)boundingBox.y, (int)boundingBox.width,
                            (int)boundingBox.height);
                        break;
                    }
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_SCISSOR_END:
                    {
                        Raylib.EndScissorMode();
                        break;
                    }
                case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_CUSTOM:
                    // throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}