using Domain.Model.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.HealthBar;

public class HealthBarComponent
{
    private readonly AnimationModel _baseAnimation;
    private readonly AnimationModel _fillAnimation;
    private readonly int _sliceWidth;
    private readonly int _width;
    private readonly int _height;
    private readonly int _offsetY;

    public HealthBarComponent(Texture2D baseTexture, Texture2D fillTexture, int width, int height, int offsetY = 10, int sliceWidth = 8)
    {
        _baseAnimation = new AnimationModel(baseTexture);
        _fillAnimation = new AnimationModel(fillTexture);
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
        //_baseAnimation.Draw(baseRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        DrawBaseBar(spriteBatch, _baseAnimation.Texture, baseRect, Color.White);

        // Fill
        float percent = (float)currentHealth / maxHealth;
        int fillWidth = (int)((_width - _sliceWidth * 2) * percent);
        if (fillWidth > 0)
        {
            var fillRect = new Rectangle(posX + _sliceWidth, posY, fillWidth, _height);
            _fillAnimation.Draw(fillRect, Color.White, 0f, SpriteEffects.None, spriteBatch);
        }
    }

    private void DrawBaseBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle destRect, Color color)
    {
        int sliceWidth = texture.Height; // usa a altura como largura das bordas

        // Borda esquerda
        var leftSource = new Rectangle(0, 0, sliceWidth, texture.Height);
        var leftDest = new Rectangle(destRect.X, destRect.Y, sliceWidth, destRect.Height);

        // Meio esticado
        var midSource = new Rectangle(sliceWidth, 0, texture.Width - sliceWidth * 2, texture.Height);
        var midDest = new Rectangle(destRect.X + sliceWidth, destRect.Y, destRect.Width - sliceWidth * 2, destRect.Height);

        // Borda direita
        var rightSource = new Rectangle(texture.Width - sliceWidth, 0, sliceWidth, texture.Height);
        var rightDest = new Rectangle(destRect.X + destRect.Width - sliceWidth, destRect.Y, sliceWidth, destRect.Height);

        spriteBatch.Draw(texture, leftDest, leftSource, color);
        spriteBatch.Draw(texture, midDest, midSource, color);
        spriteBatch.Draw(texture, rightDest, rightSource, color);
    }
}