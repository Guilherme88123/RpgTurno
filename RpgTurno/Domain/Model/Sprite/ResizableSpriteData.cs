using Domain.Enum.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Sprite;

public class ResizableSpriteData : SpriteData
{
    public ResizableSpriteType ResizableType { get; }

    private readonly int _fixedHorizontal;
    private readonly int _fixedVertical;

    private readonly int _borderHorizontal;
    private readonly int _borderVertical;

    public ResizableSpriteData(Texture2D texture, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical, 
        int borderHorizontal = 0, int borderVertical = 0) : base(texture)
    {
        ResizableType = resizableType;
        _fixedHorizontal = fixedHorizontal;
        _fixedVertical = fixedVertical;
        _borderHorizontal = borderHorizontal;
        _borderVertical = borderVertical;
    }

    public ResizableSpriteData(Texture2D texture, Rectangle sourceRect, ResizableSpriteType resizableType, int fixedHorizontal, int fixedVertical,
        int borderHorizontal = 0, int borderVertical = 0) : base(texture, sourceRect)
    {
        ResizableType = resizableType;
        _fixedHorizontal = fixedHorizontal;
        _fixedVertical = fixedVertical;
        _borderHorizontal = borderHorizontal;
        _borderVertical = borderVertical;
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

    }

    private void DrawVerticalResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {

    }

    private void DrawFullResizable(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {

    }
}
