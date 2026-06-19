using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite;

public class SpriteData
{
    protected readonly Texture2D Texture;
    protected readonly Rectangle SourceRectangle;

    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public SpriteData(Texture2D texture, BorderDefinition border = null)
    {
        Texture = texture;

        var fullTextureRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        SourceRectangle = CreateRectangleByTextureAndBorder(fullTextureRectangle, border);
    }

    public SpriteData(Texture2D texture, Rectangle rawSourceRect, BorderDefinition border = null)
    {
        Texture = texture;
        SourceRectangle = CreateRectangleByTextureAndBorder(rawSourceRect, border);
    }

    private Rectangle CreateRectangleByTextureAndBorder(Rectangle originalRectangle, BorderDefinition border)
    {
        if (border is null)
            return originalRectangle;

        int positionX = originalRectangle.X + border.Left;
        int positionY = originalRectangle.Y + border.Top;
        int width = originalRectangle.Width - border.Left - border.Right;
        int height = originalRectangle.Height - border.Top - border.Down;

        return new Rectangle(positionX, positionY, width, height);
    }

    public virtual void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        DrawBySource(SourceRectangle, destinationRectangle, color, rotation, drawEffect, spriteBatch);
    }

    protected virtual void DrawBySource(Rectangle sourceRectangle, Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        if (destinationRectangle.Width < 0 || destinationRectangle.Height < 0)
            return;

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
