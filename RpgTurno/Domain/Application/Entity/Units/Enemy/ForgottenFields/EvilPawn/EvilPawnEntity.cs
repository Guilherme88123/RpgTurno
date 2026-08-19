using Domain.Application.Entity.Units.Base;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Skill.Pawn;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Unit.Pawn;

namespace Domain.Application.Entity.Units.Enemy.EvilPawn;

public class EvilPawnEntity : BaseUnitEntity
{
    public PawnToolType LastUsedTool { get; private set; } = PawnToolType.None;

    public EvilPawnEntity(int level = 1) : base(
        stats: new EvilPawnStats(level), 
        skillTree: new EvilPawnSkillTree())
    {
        Animation.Add((CreatureStateType.Idle, PawnToolType.None), new EvilPawnIdleSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Axe), new EvilPawnIdleAxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Knife), new EvilPawnIdleKnifeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Pickaxe), new EvilPawnIdlePickaxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Hammer), new EvilPawnIdleHammerSprite());

        Animation.Add((CreatureStateType.Run, PawnToolType.None), new EvilPawnRunSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Axe), new EvilPawnRunAxeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Knife), new EvilPawnRunKnifeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Pickaxe), new EvilPawnRunPickaxeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Hammer), new EvilPawnRunHammerSprite());

        Animation.Add((CreatureStateType.Attack, PawnToolType.Axe), new EvilPawnAttackAxeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Knife), new EvilPawnAttackKnifeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Pickaxe), new EvilPawnAttackPickaxeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Hammer), new EvilPawnAttackHammerSprite());

        Icon = new EvilPawnAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.EvilPawnUnit;
    }

    protected override void UpdateAnimation()
    {
        Animation.Update((CreatureState, LastUsedTool));
    }

    public override float GetSkillExecutionDelay(UnitSkill skill)
    {
        return Animation.GetAnimationTime((CreatureState, LastUsedTool));
    }

    public override void BeforeSkillExecute(UnitSkill skill)
    {
        base.BeforeSkillExecute(skill);

        var tool = PawnToolType.None;

        if (skill.Definition is ImprovisedStrikeSkill)
            tool = GetRandomAttackSkillTool();

        if (skill.Definition is RepairSkill)
            tool = PawnToolType.Hammer;

        SetUsedTool(tool);
    }

    private PawnToolType GetRandomAttackSkillTool()
    {
        return (PawnToolType)Random.Shared.Next(1, 4);
    }

    public void SetUsedTool(PawnToolType pawnTool)
    {
        LastUsedTool = pawnTool;
    }
}
