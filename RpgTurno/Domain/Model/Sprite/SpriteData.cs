using Domain.Dto.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices.Marshalling;

namespace Domain.Model.Sprite;

public class SpriteData
{
    protected readonly Texture2D Texture;
    protected readonly Rectangle SourceRectangle;

    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public SpriteData(Texture2D texture, int borderHorizontal = 0, int borderVertical = 0)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(borderHorizontal, borderVertical, 
            texture.Width - borderHorizontal * 2, texture.Height - borderVertical * 2);
    }

    public SpriteData(Texture2D texture, Rectangle rawSourceRect, int borderHorizontal = 0, int borderVertical = 0)
    {
        Texture = texture;

        var sourceRectangle = new Rectangle(rawSourceRect.X + borderHorizontal, rawSourceRect.Y + borderVertical,
            rawSourceRect.Width - borderHorizontal * 2, rawSourceRect.Height - borderVertical * 2);

        SourceRectangle = sourceRectangle;
    }

    public virtual void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        DrawBySource(SourceRectangle, destinationRectangle, color, rotation, drawEffect, spriteBatch);
    }

    protected virtual void DrawBySource(Rectangle sourceRectangle, Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        var scaleX = (float)destinationRectangle.Width / sourceRectangle.Width;
        var scaleY = (float)destinationRectangle.Height / sourceRectangle.Height;

        Vector2? cameraOffset = GlobalVariablesDto.GetTransform(spriteBatch);

        Vector2 position = new Vector2(destinationRectangle.X, destinationRectangle.Y);

        spriteBatch.Draw(
            Texture,
            position - (cameraOffset ?? Vector2.Zero),
            sourceRectangle,
            color,
            rotation,
            Vector2.Zero,
            new Vector2(scaleX, scaleY),
            drawEffect,
            0f);
    }
}
