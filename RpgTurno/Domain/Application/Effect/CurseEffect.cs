using Domain.Application.Effect.Base;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

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
