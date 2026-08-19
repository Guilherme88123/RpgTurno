using Domain.Application.Effect.Base;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect;

public class BleedEffect : BaseEffect
{
    public override string Name => TextConst.BleedEffect;
    public override string Description => TextConst.BleedEffectDescription;
    public override SpriteData Icon => new PoisonIconSprite();

    public BleedEffect() : base(duration: 4)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveAttack(9);
    }
}
