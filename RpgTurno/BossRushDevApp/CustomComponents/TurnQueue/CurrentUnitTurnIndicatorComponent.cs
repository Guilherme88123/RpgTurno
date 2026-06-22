using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace RpgTurno.CustomComponents.TurnQueue;

public class CurrentUnitTurnIndicatorComponent : BaseComponent
{
    private const int IndicatorSize = 48;

    private readonly ImageComponent CurrentUnitIndicator;

    public CurrentUnitTurnIndicatorComponent()
    {
        var texture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ReturnIcon);
        CurrentUnitIndicator = new ImageComponent(new SpriteData(texture), IndicatorSize, IndicatorSize);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        CurrentUnitIndicator.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        CurrentUnitIndicator.Draw(spriteBatch);
    }

    public void SetCurrentTurnUnit(BaseUnitEntity unit)
    {
        var positionX = unit.PositionX + unit.SizeX / 2 - IndicatorSize / 2;
        var positionY = unit.PositionY - IndicatorSize * 1.5;

        SetPosition((int)positionX, (int)positionY);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        var bouncedPositionY = ApplyBounce(positionY);

        CurrentUnitIndicator.SetPosition(positionX, bouncedPositionY);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return baseValue - bounce;
    }
}
