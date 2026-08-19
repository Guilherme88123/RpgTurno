using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect.Base;

public class BurnEffect : BaseEffect
{
    public override string Name => TextConst.BurnEffect;
    public override string Description => TextConst.BurnEffectDescription;
    public override SpriteData Icon => new PoisonIconSprite();

    public BurnEffect() : base(duration: 2)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveAttack(16);
    }
}
