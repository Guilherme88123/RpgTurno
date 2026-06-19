using Domain.Enum.Sprite;
using Domain.Model.Animation;
using Domain.Model.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.HealthBar;

public class HealthBarComponent
{
    private readonly ResizableSpriteData _baseSprite;
    private readonly SpriteData _fillSprite;
    private readonly int _width;
    private readonly int _height;
    private readonly int _offsetY;

    private readonly int _sliceWidth = 16;

    public HealthBarComponent(Texture2D baseTexture, Texture2D fillTexture, int width, int height, int offsetY = 10)
    {
        _baseSprite = new ResizableSpriteData(baseTexture, ResizableSpriteType.Horizontal, _sliceWidth, 0, new BorderDefinition(16, 16, 48, 48), piecesGap: 64);
        _fillSprite = new SpriteData(fillTexture);
        _width = width;
        _height = height;
        _offsetY = offsetY;
    }

    public void Draw(Rectangle entityRectangle, int currentHealth, int maxHealth, SpriteBatch spriteBatch)
    {
        var position = GetAbsolutePosition(entityRectangle);

        DrawBaseSprite(position, spriteBatch);
        DrawFillSprite(currentHealth, maxHealth, position, spriteBatch);
    }

    private Point GetAbsolutePosition(Rectangle entityRectangle)
    {
        int positionX = (int)(entityRectangle.X + entityRectangle.Width / 2 - _width / 2f);
        int positionY = (int)(entityRectangle.Y + entityRectangle.Height - _offsetY);

        return new (positionX, positionY);
    }

    private void DrawBaseSprite(Point postion, SpriteBatch spriteBatch)
    {
        var baseRect = new Rectangle(postion.X, postion.Y, _width, _height);
        _baseSprite.Draw(baseRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
    }

    private void DrawFillSprite(int currentValue, int maxValue, Point position, SpriteBatch spriteBatch)
    {
        float percent = (float)currentValue / maxValue;
        int fillWidth = (int)((_width - _sliceWidth * 2) * percent);
        if (fillWidth > 0)
        {
            var fillRect = new Rectangle(position.X + (int)(_sliceWidth / 1.5), position.Y - _height / 2, fillWidth + (int)(_sliceWidth / 1.4), _height * 2);
            _fillSprite.Draw(fillRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        }
    }
}