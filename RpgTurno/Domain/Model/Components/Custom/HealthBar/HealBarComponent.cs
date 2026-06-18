using Domain.Enum.Sprite;
using Domain.Model.Animation;
using Domain.Model.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.HealthBar;

public class HealthBarComponent
{
    private readonly ResizableSpriteData _baseAnimation;
    private readonly AnimationClip _fillAnimation;
    private readonly int _sliceWidth;
    private readonly int _width;
    private readonly int _height;
    private readonly int _offsetY;

    public HealthBarComponent(Texture2D baseTexture, Texture2D fillTexture, int width, int height, int offsetY = 10, int sliceWidth = 8)
    {
        _baseAnimation = new ResizableSpriteData(baseTexture, ResizableSpriteType.Horizontal, 16, 0, 
            borderHorizontal: 48, borderVertical: 16, piecesGap: 64);
        _fillAnimation = new AnimationClip(fillTexture);
        _width = width;
        _height = height;
        _offsetY = offsetY;
        _sliceWidth = sliceWidth;
    }

    public void Draw(Vector2 entityCenter, int currentHealth, int maxHealth, SpriteBatch spriteBatch)
    {
        int posX = (int)(entityCenter.X - _width / 2f);
        int posY = (int)(entityCenter.Y - _offsetY);

        // Base
        var baseRect = new Rectangle(posX, posY, _width, _height);
        _baseAnimation.Draw(baseRect, Color.White, 0f, SpriteEffects.None, spriteBatch);

        //_baseAnimation.Draw(baseRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        //DrawBaseBar(spriteBatch, _baseAnimation.Texture, baseRect, Color.White);

        // Fill
        float percent = (float)currentHealth / maxHealth;
        int fillWidth = (int)((baseRect.Width - _sliceWidth * 2) * percent);
        if (fillWidth > 0)
        {
            var fillRect = new Rectangle(posX + (int)(_sliceWidth / 1.5), posY - _height / 2, fillWidth + (int)(_sliceWidth / 1.4), _height * 2);
            _fillAnimation.Draw(fillRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        }
    }
}