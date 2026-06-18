using Domain.Enum.Sprite;
using Domain.Model.Animation;
using Domain.Model.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.HealthBar;

public class HealthBarComponent
{
    private readonly ResizableSpriteData _baseSprite;
    private readonly ResizableSpriteData _fillSprite;
    private readonly int _sliceWidth;
    private readonly int _width;
    private readonly int _height;
    private readonly int _offsetY;

    public HealthBarComponent(Texture2D baseTexture, Texture2D fillTexture, int width, int height, int offsetY = 10, int sliceWidth = 8)
    {
        _baseSprite = new ResizableSpriteData(baseTexture, ResizableSpriteType.Horizontal, 16, 0, 
            borderHorizontal: 48, borderVertical: 16, piecesGap: 64);
        _fillSprite = new ResizableSpriteData(fillTexture, ResizableSpriteType.None, 0, 0);
        _width = width;
        _height = height;
        _offsetY = offsetY;
        _sliceWidth = sliceWidth;
    }

    public void Draw(Vector2 entityCenter, int currentHealth, int maxHealth, SpriteBatch spriteBatch)
    {
        var position = GetAbsolutePosition(entityCenter);

        DrawBaseSprite(position, spriteBatch);
        DrawFillSprite(currentHealth, maxHealth, position, spriteBatch);
    }

    private Point GetAbsolutePosition(Vector2 centerPosition)
    {
        int positionX = (int)(centerPosition.X - _width / 2f);
        int positionY = (int)(centerPosition.Y - _offsetY);

        return new (positionX, positionY);
    }

    private void DrawBaseSprite(Point postion, SpriteBatch spriteBatch)
    {
        var baseRect = new Rectangle(postion.X, postion.Y, _width, _height);
        _baseSprite.Draw(baseRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
    }

    private void DrawFillSprite(int currentValue, int maxValue, Point position, SpriteBatch spriteBatch)
    {
        float percent = (float)currentValue / currentValue;
        int fillWidth = (int)((_width - _sliceWidth * 2) * percent);
        if (fillWidth > 0)
        {
            var fillRect = new Rectangle(position.X + (int)(_sliceWidth / 1.5), position.Y - _height / 2, fillWidth + (int)(_sliceWidth / 1.4), _height * 2);
            _fillSprite.Draw(fillRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        }
    }
}