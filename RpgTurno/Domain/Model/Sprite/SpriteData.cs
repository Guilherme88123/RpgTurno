using Domain.Dto.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Sprite;

public class SpriteData
{
    private readonly Texture2D _texture;
    private readonly Rectangle _sourceRectangle;

    public int Width => _sourceRectangle.Width;
    public int Height => _sourceRectangle.Height;

    public SpriteData(Texture2D texture)
    {
        _texture = texture;
        _sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
    }

    public SpriteData(Texture2D texture, Rectangle sourceRect)
    {
        _texture = texture;
        _sourceRectangle = sourceRect;
    }

    public virtual void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        var scaleX = (float)destinationRectangle.Width / _sourceRectangle.Width;
        var scaleY = (float)destinationRectangle.Height / _sourceRectangle.Height;

        Vector2? cameraOffset = GlobalVariablesDto.GetTransform(spriteBatch);

        Vector2 position = new Vector2(destinationRectangle.X, destinationRectangle.Y);

        spriteBatch.Draw(
            _texture,
            position - (cameraOffset ?? Vector2.Zero),
            _sourceRectangle,
            color,
            rotation,
            Vector2.Zero,
            new Vector2(scaleX, scaleY),
            drawEffect,
            0f);
    }
}
