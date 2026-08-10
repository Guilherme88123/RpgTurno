using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Unit.Pawn;

namespace Domain.Application.Entity.Units.Enemy.EvilPawn;

public class EvilPawnEntity : BaseUnitEntity
{
    public PawnToolType LastUsedTool { get; private set; } = PawnToolType.None;

    public EvilPawnEntity(int level = 1) : base(stats: new EvilPawnStats(level), skillTree: new EvilPawnSkillTree())
    {
        Animation.Add((CreatureStateType.Idle, PawnToolType.None), new EvilPawnIdleSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Axe), new EvilPawnIdleAxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Knife), new EvilPawnIdleKnifeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Pickaxe), new EvilPawnIdlePickaxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Hammer), new EvilPawnIdleHammerSprite());

        Animation.Add((CreatureStateType.Running, PawnToolType.None), new EvilPawnRunSprite());
        Animation.Add((CreatureStateType.Running, PawnToolType.Axe), new EvilPawnRunAxeSprite());
        Animation.Add((CreatureStateType.Running, PawnToolType.Knife), new EvilPawnRunKnifeSprite());
        Animation.Add((CreatureStateType.Running, PawnToolType.Pickaxe), new EvilPawnRunPickaxeSprite());
        Animation.Add((CreatureStateType.Running, PawnToolType.Hammer), new EvilPawnRunHammerSprite());

        Animation.Add((CreatureStateType.Attacking, PawnToolType.Axe), new EvilPawnAttackAxeSprite());
        Animation.Add((CreatureStateType.Attacking, PawnToolType.Knife), new EvilPawnAttackKnifeSprite());
        Animation.Add((CreatureStateType.Attacking, PawnToolType.Pickaxe), new EvilPawnAttackPickaxeSprite());
        Animation.Add((CreatureStateType.Attacking, PawnToolType.Hammer), new EvilPawnAttackHammerSprite());

        Icon = new EvilPawnAvatarSprite();

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilPawnUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;
    }

    protected override void UpdateAnimation()
    {
        Animation.Update((CreatureState, LastUsedTool));
    }
    
    public void SetUsedTool(PawnToolType pawnTool)
    {
        LastUsedTool = pawnTool;
    }
}
