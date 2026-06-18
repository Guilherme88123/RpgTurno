using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data;

namespace Domain.Model.Sprite;

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
        //TODO: Implementar sprite redimensionavel vertical
    }

    private void DrawFullResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        //TODO: Implementar sprite redimensionavel completo
    }
}
