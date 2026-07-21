using Domain.Model.Effect.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Model.Effect;

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
