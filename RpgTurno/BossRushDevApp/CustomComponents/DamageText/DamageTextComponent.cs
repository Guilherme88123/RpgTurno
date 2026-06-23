using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.CustomComponents.DamageText;

public class DamageTextComponent : BaseComponent
{
    private readonly int _initialPositionX;
    private readonly int _initialPositionY;

    private const float DelayDissapear = 1.0f;
    private float _currentDelayDissapear = DelayDissapear;

    public bool IsDestroied { get; private set; }

    private readonly TextComponent _textComponent = new();

    public DamageTextComponent(int positionX, int positionY, string text)
    {
        _initialPositionX = positionX;
        _initialPositionY = positionY;

        SetPosition(positionX, positionY);

        _textComponent.SetText(text);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _textComponent.Update(gameTime);

        UpdateDelay();
    }

    private void UpdateDelay()
    {
        _currentDelayDissapear -= GlobalVariablesDto.DeltaTime;

        if (_currentDelayDissapear <= 0)
        {
            IsDestroied = true;
        }
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        _textComponent.SetPosition(positionX, positionY);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _textComponent.Draw(spriteBatch);
    }
}
