using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

namespace RpgTurno.Custom.CustomComponents.Play.TurnQueue;

public class CurrentUnitTurnIndicatorComponent : ImageComponent
{
    private const int _idicatorSize = 48;

    public CurrentUnitTurnIndicatorComponent() : base(new IndicatorIconSprite(), _idicatorSize, _idicatorSize)
    {
    }

    public void SetCurrentTurnUnit(BaseUnitEntity unit)
    {
        var positionX = unit.PositionX + unit.SizeX / 2 - _idicatorSize / 2;
        var positionY = unit.PositionY - _idicatorSize * 1.5;

        SetPosition((int)positionX, (int)positionY);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();
        return baseValue - bounce;
    }
}
