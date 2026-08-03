using Domain.Dto.Global;
using Domain.Application.Components.Text;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Base.Skill.Text;

public class SkillResultTextComponent : TextComponent
{
    private const float DelayDissapear = 1.0f;
    private float _currentDelayDissapear = DelayDissapear;

    private bool _isCritical;
    private readonly SpriteData _criticalSimbol = new CriticalIconSprite();
    private const int _criticalSimbolSize = 16;

    public bool IsDestroyed { get; private set; }

    public SkillResultTextComponent(int positionX, int positionY, string text, Color color, bool isCritical = false)
    {
        positionX = GetRandomByPositionX(positionX);

        SetPosition(positionX, positionY);
        SetText(text);

        Color = color;

        _isCritical = isCritical;
    }

    private int GetRandomByPositionX(int positionX)
    {
        return new Random().Next(positionX, positionX + 64);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateDelay();

        SetPosition(Bounds.X, Bounds.Y - 1);
    }

    private void UpdateDelay()
    {
        _currentDelayDissapear -= GlobalVariablesDto.DeltaTime;

        if (_currentDelayDissapear <= 0)
        {
            IsDestroyed = true;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (_isCritical)
            DrawCriticalSimbol(spriteBatch);
    }

    private void DrawCriticalSimbol(SpriteBatch spriteBatch)
    {
        var textWidth = (int)Font.MeasureString(Text).X;

        var criticalSimbolRectangle = new Rectangle(Bounds.X + textWidth, Bounds.Y, _criticalSimbolSize, _criticalSimbolSize);

        _criticalSimbol.Draw(criticalSimbolRectangle, Color.White, Rotation, SpriteEffects, spriteBatch, Vector2.One, Vector2.Zero);
    }
}
