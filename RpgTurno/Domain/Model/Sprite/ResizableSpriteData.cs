using Domain.Enum.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data;

namespace Domain.Model.Sprite;

public class ResizableSpriteData : SpriteData
{
    public ResizableSpriteType ResizableType { get; }

    private readonly int _fixedHorizontal;
    private readonly int _fixedVertical;

    public ResizableSpriteData(Texture2D texture, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical,
        int borderHorizontal = 0, int borderVertical = 0) : base(texture, borderHorizontal, borderVertical)
    {
        ResizableType = resizableType;
        _fixedHorizontal = fixedHorizontal;
        _fixedVertical = fixedVertical;
    }

    public ResizableSpriteData(Texture2D texture, Rectangle sourceRect, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical,
        int borderHorizontal = 0, int borderVertical = 0) : base(texture, sourceRect, borderHorizontal, borderVertical)
    {
        ResizableType = resizableType;
        _fixedHorizontal = fixedHorizontal;
        _fixedVertical = fixedVertical;
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
        var sourceLeft = new Rectangle(SourceRectangle.X, SourceRectangle.Y, _fixedHorizontal, SourceRectangle.Height);
        var sourceMid = new Rectangle(SourceRectangle.X + _fixedHorizontal, SourceRectangle.Y, SourceRectangle.Width - _fixedHorizontal * 2, SourceRectangle.Height);
        var sourceRight = new Rectangle(SourceRectangle.X + SourceRectangle.Width - _fixedHorizontal, SourceRectangle.Y, _fixedHorizontal, SourceRectangle.Height);

        var destLeft = new Rectangle(destinationRectangle.X, destinationRectangle.Y, _fixedHorizontal, destinationRectangle.Height);
        var destMid = new Rectangle(destinationRectangle.X + _fixedHorizontal, destinationRectangle.Y, destinationRectangle.Width - _fixedHorizontal * 2, destinationRectangle.Height);
        var destRight = new Rectangle(destinationRectangle.X + destinationRectangle.Width - _fixedHorizontal, destinationRectangle.Y, _fixedHorizontal, destinationRectangle.Height);
    }

    private void DrawVerticalResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {

    }

    private void DrawFullResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {

    }
}
