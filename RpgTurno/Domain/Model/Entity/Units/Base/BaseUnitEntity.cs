using Domain.Dto.Global;
using Domain.Model.Effect.Base;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Base.HealthBar;
using Domain.Model.Entity.Units.Base.Skill.SkillTree;
using Domain.Model.Entity.Units.Base.Stats;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite;
using Domain.Model.Texture.Sprite.CustomSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Base;

public class BaseUnitEntity : BaseEntity
{
    public string Name { get; set; }

    public BaseUnitStats Stats { get; }

    private BaseSkillTree _skillTree;
    public List<UnitSkill> Skills { get; private set; }

    public List<BaseEffect> Effects { get; } = new();

    public SpriteData Icon { get; protected set; }

    private readonly HealthBarComponent _healthBar;

    public bool IsDead { get; protected set; }

    private const float DelayDamageTakenFlash = 0.1f;
    private float _currentDelayDamageTakenFlash;
    private bool HasTakeDamage => _currentDelayDamageTakenFlash > 0;

    private const float DelayHealTakenFlash = 0.1f;
    private float _currentDelayHealTakenFlash;
    private bool HasTakeHeal => _currentDelayHealTakenFlash > 0;

    private const float DelayDeadAnimation = 1f;
    private float _currentDelayDeadAnimation;
    private bool HasDeadAnimationFinished => _currentDelayDeadAnimation == 0;
    private LargeDustAnimation _deadAnimation = new();

    private const float DelayLevelUpAnimation = 1.1f;
    private float _currentDelayLevelUpAnimation;
    private bool HasLevelUpAnimation => _currentDelayLevelUpAnimation > 0;
    private LevelUpAnimation _levelUpAnimation = new();

    public BaseUnitEntity(BaseUnitStats stats, BaseSkillTree skillTree)
    {
        Stats = stats;
        Stats.OnLevelUp += HasLevelUp;

        _skillTree = skillTree;
        ReloadSkills();

        _healthBar = new HealthBarComponent(Stats.MaxHealth, Stats.CurrentHealth);
    }

    #region Update

    public override void Update()
    {
        base.Update();

        UpdateDelays();
        UpdateHealthBarComponent();
        UpdateColorEffect();

        if (IsDead)
            VerifyDeadDelayFinish();

        if (HasLevelUpAnimation)
            UpdateLevelUpAnimation();
    }

    private void UpdateDelays()
    {
        _currentDelayDamageTakenFlash = Math.Max(0, _currentDelayDamageTakenFlash - GlobalVariablesDto.DeltaTime);
        _currentDelayHealTakenFlash = Math.Max(0, _currentDelayHealTakenFlash - GlobalVariablesDto.DeltaTime);
        _currentDelayDeadAnimation = Math.Max(0, _currentDelayDeadAnimation - GlobalVariablesDto.DeltaTime);
        _currentDelayLevelUpAnimation = Math.Max(0, _currentDelayLevelUpAnimation - GlobalVariablesDto.DeltaTime);
    }

    private void UpdateHealthBarComponent()
    {
        _healthBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY);
        _healthBar.SetValues(Stats.MaxHealth, Stats.CurrentHealth);
        _healthBar.Update(GlobalVariablesDto.GameTime);
    }

    private void UpdateColorEffect()
    {
        Color = true switch
        {
            _ when HasTakeDamage => Color.Red,
            _ when HasTakeHeal => Color.Green,
            _ => Color.White,
        };
    }

    private void VerifyDeadDelayFinish()
    {
        _deadAnimation.Update();

        if (HasDeadAnimationFinished)
            Destroy();
    }

    private void UpdateLevelUpAnimation()
    {
        _levelUpAnimation.Update();
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        if (IsDead)
        {
            DrawDeadAnimation();
            return;
        }

        base.Draw();
        DrawHealthBar();
        DrawEffects();

        if (HasLevelUpAnimation)
            DrawLevelUpAnimation();
    }

    public void DrawMap(int positionX, int positionY)
    {
        float scale = 1.5f;

        int width = (int)(AnimationRectangle.Width / scale);
        int height = (int)(AnimationRectangle.Height / scale);

        int pivotX = (int)(FeetOffset.X / scale);
        int pivotY = (int)(FeetOffset.Y / scale);

        Animation.Draw(
            new Rectangle(
                positionX - pivotX,
                positionY - pivotY,
                width,
                height),
            Color,
            ActualAngle,
            DrawEffect,
            GlobalVariablesDto.SpriteBatchEntities);
    }

    protected virtual void DrawHealthBar()
    {
        _healthBar.Draw(GlobalVariablesDto.SpriteBatchEntities);
    }

    private void DrawEffects()
    {
        var index = 0;
        foreach (var effect in Effects)
        {
            DrawEffectIconByIndex(effect.Icon, index);
            index++;
        }
    }

    private void DrawEffectIconByIndex(SpriteData icon, int index)
    {
        var iconSize = 24;
        var indexMargin = iconSize * index;

        var effectRectangle = new Rectangle((int)(PositionX + indexMargin), (int)(PositionY + SizeY + 32), iconSize, iconSize);

        new ScrollMiddleBannerSprite().Draw(effectRectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchEntities);
        icon.Draw(effectRectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchEntities);
    }

    protected virtual void DrawDeadAnimation()
    {
        _deadAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, GlobalVariablesDto.SpriteBatchEntities);
    }

    protected virtual void DrawLevelUpAnimation()
    {
        _levelUpAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, GlobalVariablesDto.SpriteBatchEntities);
    }

    #endregion

    #region Functions

    #region Take Damage

    public int RecieveAttack(int damage)
    {
        var damageTaken = Stats.RecieveAttack(damage);

        TickDamageRecieved();

        return damageTaken;
    }

    public int RecieveTrueDamage(int damage)
    {
        var damageTaken = Stats.RecieveTrueDamage(damage);

        TickDamageRecieved();

        return damageTaken;
    }

    private void TickDamageRecieved()
    {
        if (Stats.IsDead)
            MakeDead();
        else
            ResetTakeDamageDelay();
    }

    private void ResetTakeDamageDelay()
    {
        _currentDelayDamageTakenFlash = DelayDamageTakenFlash;
    }

    #endregion

    #region Take Heal

    public int RecieveHeal(int healAmount)
    {
        var damageTaken = Stats.HealHealth(healAmount);

        ResetTakeHealDelay();

        return damageTaken;
    }

    private void ResetTakeHealDelay()
    {
        _currentDelayHealTakenFlash = DelayHealTakenFlash;
    }

    #endregion

    #region Death

    private void MakeDead()
    {
        if (IsDead)
            return;

        IsDead = true;

        ResetDelayDeadAnimation();

        _deadAnimation.Reset();
    }

    private void ResetDelayDeadAnimation()
    {
        _currentDelayDeadAnimation = DelayDeadAnimation;
    }

    #endregion

    #region Level Up

    private void HasLevelUp()
    {
        StartLevelUpAnimation();
        ReloadSkills();
    }

    private void StartLevelUpAnimation()
    {
        _currentDelayLevelUpAnimation = DelayLevelUpAnimation;
        _levelUpAnimation.Reset();
    }

    private void ReloadSkills()
    {
        Skills = _skillTree.GetAvaliableSkills(Stats.Level);
    }

    #endregion

    #region Turn Start

    public void OnTurnStart()
    {
        Tick();
        ApplyEffectOnTurnStart();
        RemoveExpiredEffects();
    }

    public void Tick()
    {
        Skills.ForEach(x => x.TickCooldown());
        Effects.ForEach(x => x.TickDuration());
    }

    public void ApplyEffectOnTurnStart()
    {
        Effects.ForEach(x => x.OnTurnStart(this));
    }

    public void RemoveExpiredEffects()
    {
        var effectsToRemove = Effects.Where(x => x.HasFinished).ToList();
        
        foreach (var effect in effectsToRemove)
            Effects.Remove(effect);
    }

    #endregion

    #region Effects

    public void AddEffect(BaseEffect effect)
    {
        Effects.Add(effect);
        effect.OnApply(this);
    }

    public void ApplyReciveAttackEffects(SkillContext context)
    {
        foreach (var effect in Effects)
            effect.OnReceiveAttack(context);
    }

    public void ApplyExecuteAttackEffects(SkillContext context)
    {
        foreach (var effect in Effects)
            effect.OnAttack(context);
    }

    #endregion

    #endregion
}
