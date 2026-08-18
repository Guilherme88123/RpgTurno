using Domain.Application.Entity.Units.Base;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Skill.Pawn;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Pawn;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Unit.Pawn;

namespace Domain.Application.Entity.Units.Ally.Pawn;

public class PawnEntity : BaseUnitEntity
{
    public PawnToolType LastUsedTool { get; private set; } = PawnToolType.None;

    public PawnEntity(int level = 1) : base(
        stats: new PawnStats(level), 
        skillTree: new PawnSkillTree())
    {
        Animation.Add((CreatureStateType.Idle, PawnToolType.None), new PawnIdleSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Axe), new PawnIdleAxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Knife), new PawnIdleKnifeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Pickaxe), new PawnIdlePickaxeSprite());
        Animation.Add((CreatureStateType.Idle, PawnToolType.Hammer), new PawnIdleHammerSprite());

        Animation.Add((CreatureStateType.Run, PawnToolType.None), new PawnRunSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Axe), new PawnRunAxeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Knife), new PawnRunKnifeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Pickaxe), new PawnRunPickaxeSprite());
        Animation.Add((CreatureStateType.Run, PawnToolType.Hammer), new PawnRunHammerSprite());

        Animation.Add((CreatureStateType.Attack, PawnToolType.Axe), new PawnAttackAxeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Knife), new PawnAttackKnifeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Pickaxe), new PawnAttackPickaxeSprite());
        Animation.Add((CreatureStateType.Attack, PawnToolType.Hammer), new PawnAttackHammerSprite());

        Icon = new PawnAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.PawnUnit;
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
