using Domain.Application.Effect.Base;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect;

public class RegenerationEffect : BaseEffect
{
    public override string Name => TextConst.RegenerationEffect;
    public override string Description => TextConst.RegenerationEffectDescription;
    public override SpriteData Icon => new HeartIconSprite();

    public RegenerationEffect() : base(duration: 2)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveHeal(5);
    }
}
