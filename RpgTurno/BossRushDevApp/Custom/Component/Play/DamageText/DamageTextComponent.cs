using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.CustomComponents.Play.DamageText;

public class DamageTextComponent : BaseComponent
{
    private const float DelayDissapear = 1.0f;
    private float _currentDelayDissapear = DelayDissapear;

    public bool IsDestroyed { get; private set; }

    private readonly TextComponent _textComponent = new();

    public DamageTextComponent(int positionX, int positionY, string text)
    {
        positionX = GetRandomByPositionX(positionX);

        SetPosition(positionX, positionY);

        _textComponent.SetText(text);
    }

    private int GetRandomByPositionX(int positionX)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return positionX + 5 * bounce - 3;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _textComponent.Update(gameTime);

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
