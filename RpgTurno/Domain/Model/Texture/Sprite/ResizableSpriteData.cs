using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data;

namespace Domain.Model.Texture.Sprite;

//TODO: alterar para sempre usar o full resizable, pois funciona para quando é horizontal/vertical
public class ResizableSpriteData : SpriteData
{
    public ResizableSpriteType ResizableType { get; }

    private readonly int _fixedHorizontalSlice;
    private readonly int _fixedVerticalSlice;

    private readonly int _piecesGap;

    public ResizableSpriteData(Texture2D texture, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical,
        BorderDefinition border = null, int piecesGap = 0) : base(texture, border)
    {
        ResizableType = resizableType;
        _fixedHorizontalSlice = fixedHorizontal;
        _fixedVerticalSlice = fixedVertical;
        _piecesGap = piecesGap;
    }

    public ResizableSpriteData(Texture2D texture, Rectangle sourceRect, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical,
        BorderDefinition border = null, int piecesGap = 0) : base(texture, sourceRect, border)
    {
        ResizableType = resizableType;
        _fixedHorizontalSlice = fixedHorizontal;
        _fixedVerticalSlice = fixedVertical;
        _piecesGap = piecesGap;
    }

    public override void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        if (ResizableType == ResizableSpriteType.Full)
        {
            DrawFullResizable(destinationRectangle, color, rotation, drawEffect, spriteBatch);
            return;
        }

        if (ResizableType == ResizableSpriteType.Horizontal)
        {
            DrawHorizontalResizable(destinationRectangle, color, rotation, drawEffect, spriteBatch);
            return;
        }

        if (ResizableType == ResizableSpriteType.Vertical)
        {
            DrawVerticalResizable(destinationRectangle, color, rotation, drawEffect, spriteBatch);
            return;
        }

        base.Draw(destinationRectangle, color, rotation, drawEffect, spriteBatch);
    }

    private void DrawHorizontalResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        var sourceLeft = new Rectangle(SourceRectangle.X, SourceRectangle.Y, _fixedHorizontalSlice, SourceRectangle.Height);
        var sourceMid = new Rectangle(SourceRectangle.X + _fixedHorizontalSlice + _piecesGap, SourceRectangle.Y, SourceRectangle.Width - _fixedHorizontalSlice * 2 - _piecesGap * 2, SourceRectangle.Height);
        var sourceRight = new Rectangle(SourceRectangle.X + SourceRectangle.Width - _fixedHorizontalSlice, SourceRectangle.Y, _fixedHorizontalSlice, SourceRectangle.Height);

        var destLeft = new Rectangle(destinationRectangle.X, destinationRectangle.Y, _fixedHorizontalSlice, destinationRectangle.Height);
        var destMid = new Rectangle(destinationRectangle.X + _fixedHorizontalSlice, destinationRectangle.Y, destinationRectangle.Width - _fixedHorizontalSlice * 2, destinationRectangle.Height);
        var destRight = new Rectangle(destinationRectangle.X + destinationRectangle.Width - _fixedHorizontalSlice, destinationRectangle.Y, _fixedHorizontalSlice, destinationRectangle.Height);

        DrawBySource(sourceLeft, destLeft, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceMid, destMid, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceRight, destRight, color, rotation, drawEffect, spriteBatch);
    }

    private void DrawVerticalResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
    }

    private void DrawFullResizable(
        Rectangle destinationRectangle,
        Color color,
        float rotation,
        SpriteEffects drawEffect,
        SpriteBatch spriteBatch)
    {
        int sx = SourceRectangle.X;
        int sy = SourceRectangle.Y;
        int sw = SourceRectangle.Width;
        int sh = SourceRectangle.Height;

        int fh = _fixedHorizontalSlice;
        int fv = _fixedVerticalSlice;

        int g = _piecesGap;

        int dx = destinationRectangle.X;
        int dy = destinationRectangle.Y;
        int dw = destinationRectangle.Width;
        int dh = destinationRectangle.Height;

        int sourceCenterWidth = sw - fh * 2 - g * 2;
        int sourceCenterHeight = sh - fv * 2 - g * 2;

        var sourceTopLeft =
            new Rectangle(
                sx,
                sy,
                fh,
                fv);

        var sourceTopCenter =
            new Rectangle(
                sx + fh + g,
                sy,
                sourceCenterWidth,
                fv);

        var sourceTopRight =
            new Rectangle(
                sx + sw - fh,
                sy,
                fh,
                fv);


        var sourceMidLeft =
            new Rectangle(
                sx,
                sy + fv + g,
                fh,
                sourceCenterHeight);

        var sourceMidCenter =
            new Rectangle(
                sx + fh + g,
                sy + fv + g,
                sourceCenterWidth,
                sourceCenterHeight);

        var sourceMidRight =
            new Rectangle(
                sx + sw - fh,
                sy + fv + g,
                fh,
                sourceCenterHeight);


        var sourceDownLeft =
            new Rectangle(
                sx,
                sy + sh - fv,
                fh,
                fv);

        var sourceDownCenter =
            new Rectangle(
                sx + fh + g,
                sy + sh - fv,
                sourceCenterWidth,
                fv);

        var sourceDownRight =
            new Rectangle(
                sx + sw - fh,
                sy + sh - fv,
                fh,
                fv);

        var destTopLeft =
            new Rectangle(
                dx,
                dy,
                fh,
                fv);

        var destTopCenter =
            new Rectangle(
                dx + fh,
                dy,
                dw - fh * 2,
                fv);

        var destTopRight =
            new Rectangle(
                dx + dw - fh,
                dy,
                fh,
                fv);


        var destMidLeft =
            new Rectangle(
                dx,
                dy + fv,
                fh,
                dh - fv * 2);

        var destMidCenter =
            new Rectangle(
                dx + fh,
                dy + fv,
                dw - fh * 2,
                dh - fv * 2);

        var destMidRight =
            new Rectangle(
                dx + dw - fh,
                dy + fv,
                fh,
                dh - fv * 2);


        var destDownLeft =
            new Rectangle(
                dx,
                dy + dh - fv,
                fh,
                fv);

        var destDownCenter =
            new Rectangle(
                dx + fh,
                dy + dh - fv,
                dw - fh * 2,
                fv);

        var destDownRight =
            new Rectangle(
                dx + dw - fh,
                dy + dh - fv,
                fh,
                fv);

        DrawBySource(sourceTopLeft, destTopLeft, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceTopCenter, destTopCenter, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceTopRight, destTopRight, color, rotation, drawEffect, spriteBatch);

        DrawBySource(sourceMidLeft, destMidLeft, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceMidCenter, destMidCenter, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceMidRight, destMidRight, color, rotation, drawEffect, spriteBatch);

        DrawBySource(sourceDownLeft, destDownLeft, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceDownCenter, destDownCenter, color, rotation, drawEffect, spriteBatch);
        DrawBySource(sourceDownRight, destDownRight, color, rotation, drawEffect, spriteBatch);
    }
}
