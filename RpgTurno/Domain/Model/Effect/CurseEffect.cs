using Domain.Model.Effect.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Model.Effect;

public class CurseEffect : BaseEffect
{
    public override string Name => "Cursed";
    public override string Description => "This unit is cursed";
    public override SpriteData Icon => new CurseIconSprite();

    public CurseEffect() : base(duration: 3)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveAttack(12);
    }
}
