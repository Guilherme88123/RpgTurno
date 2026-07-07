using Domain.Dto.Global;
using Domain.Model.Components.Text;
using Microsoft.Xna.Framework;

namespace RpgTurno.Custom.CustomComponents.Play.DamageText;

public class DamageTextComponent : TextComponent
{
    private const float DelayDissapear = 1.0f;
    private float _currentDelayDissapear = DelayDissapear;

    public bool IsDestroyed { get; private set; }

    public DamageTextComponent(int positionX, int positionY, string text, Color color)
    {
        positionX = GetRandomByPositionX(positionX);

        SetPosition(positionX, positionY);
        SetText(text);

        Color = color;
    }

    private int GetRandomByPositionX(int positionX)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return positionX + 5 * bounce - 3;
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
}
