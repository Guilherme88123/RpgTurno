using Domain.Application.Effect.Base;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

public class PoisonEffect : BaseEffect
{
    public override string Name => "Poison";
    public override string Description => "This unit is poisoned";
    public override SpriteData Icon => new PoisonIconSprite();

    public PoisonEffect() : base(duration: 3)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveAttack(7);
    }
}
