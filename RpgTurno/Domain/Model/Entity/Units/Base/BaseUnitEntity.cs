using Domain.Dto.Global;
using Domain.Model.Effect.Base;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Base.Bar;
using Domain.Model.Entity.Units.Base.Skill.SkillTree;
using Domain.Model.Entity.Units.Base.Skill.Text;
using Domain.Model.Entity.Units.Base.Stats;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite;
using Domain.Model.Texture.Sprite.CustomSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Custom.Component.Play.Banners;

namespace Domain.Model.Entity.Units.Base;

public class BaseUnitEntity : BaseEntity
{
    public string Name { get; set; }

    public BaseUnitStats Stats { get; }

    private BaseSkillTree _skillTree;
    public List<UnitSkill> Skills { get; private set; }

    public List<UnitEffect> Effects { get; } = new();

    public SpriteData Icon { get; protected set; }

    private readonly HealthBarComponent _healthBar;
    private readonly ManaBarComponent _manaBar;
    private readonly EffectDetailsBannerComponent _effectBanner;

    public bool IsDead { get; protected set; }

    private const float DelayDamageTakenFlash = 0.1f;
    private float _currentDelayDamageTakenFlash;
    private bool HasTakeDamage => _currentDelayDamageTakenFlash > 0;

    private const float DelayHealTakenFlash = 0.1f;
    private float _currentDelayHealTakenFlash;
    private bool HasTakeHeal => _currentDelayHealTakenFlash > 0;

    private LargeDustAnimation _deadAnimation;

    private const float DelayLevelUpAnimation = 1.1f;
    private float _currentDelayLevelUpAnimation;
    private bool HasLevelUpAnimation => _currentDelayLevelUpAnimation > 0;
    private LevelUpAnimation _levelUpAnimation = new();

    private List<SkillResultTextComponent> _skillResultTexts = new();

    public BaseUnitEntity(BaseUnitStats stats, BaseSkillTree skillTree)
    {
        Stats = stats;
        Stats.OnLevelUp += HasLevelUp;

        _skillTree = skillTree;
        ReloadSkills();

        _healthBar = new HealthBarComponent(Stats.MaxHealth, Stats.CurrentHealth);
        _manaBar = new ManaBarComponent(Stats.MaxMana, Stats.CurrentMana);
        _effectBanner = new();

        _deadAnimation = new();
        _deadAnimation.IsLoop = false;
    }

    #region Update

    public override void Update()
    {
        base.Update();

        UpdateDelays();
        UpdateHealthBarComponent();
        UpdateManaBarComponent();
        UpdateColorEffect();
        UpdateSkillTexts();
        UpdateEffects();

        if (IsDead)
            VerifyDeadDelayFinish();

        if (HasLevelUpAnimation)
            UpdateLevelUpAnimation();
    }

    private void UpdateDelays()
    {
        _currentDelayDamageTakenFlash = Math.Max(0, _currentDelayDamageTakenFlash - GlobalVariablesDto.DeltaTime);
        _currentDelayHealTakenFlash = Math.Max(0, _currentDelayHealTakenFlash - GlobalVariablesDto.DeltaTime);
        _currentDelayLevelUpAnimation = Math.Max(0, _currentDelayLevelUpAnimation - GlobalVariablesDto.DeltaTime);
    }

    private void UpdateHealthBarComponent()
    {
        _healthBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY);
        _healthBar.SetValues(Stats.MaxHealth, Stats.CurrentHealth);
        _healthBar.Update(GlobalVariablesDto.GameTime);
    }

    private void UpdateManaBarComponent()
    {
        _manaBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY + 18);
        _manaBar.SetValues(Stats.MaxMana, Stats.CurrentMana);
        _manaBar.Update(GlobalVariablesDto.GameTime);
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

        if (_deadAnimation.IsFinished)
            Destroy();
    }

    private void UpdateLevelUpAnimation()
    {
        _levelUpAnimation.Update();
    }

    private void UpdateSkillTexts()
    {
        _skillResultTexts.ForEach(x => x.Update(GlobalVariablesDto.GameTime));
        _skillResultTexts.RemoveAll(x => x.IsDestroyed);
    }

    private void UpdateEffects()
    {
        UpdateEffectsRectangle();
        UpdateEffectsHover();
    }

    private void UpdateEffectsRectangle()
    {
        var iconSize = 24;
        var margin = 2;

        var index = 0;
        foreach (var unitEffect in Effects)
        {
            var indexMargin = (iconSize + margin) * index;

            unitEffect.Rectangle = new Rectangle((int)(PositionX + indexMargin), (int)(PositionY + SizeY + 50), iconSize, iconSize);

            index++;
        }
    }

    private void UpdateEffectsHover()
    {
        var mousePosition = GlobalVariablesDto.MouseState.Position;
        var isHovering = IsHoveringEffect(mousePosition);

        _effectBanner.IsVisible = isHovering;

        if (!isHovering)
            return;

        var unitEffect = GetHoveringEffect(mousePosition);
        _effectBanner.SetHoverSkillButton(unitEffect.Effect, unitEffect.Rectangle);
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        if (IsDead)
        {
            DrawDeadAnimation();
            DrawSkillTexts();
            return;
        }

        base.Draw();
        DrawHealthBar();
        DrawManaBar();
        DrawEffects();

        if (HasLevelUpAnimation)
            DrawLevelUpAnimation();

        DrawSkillTexts();
        DrawEffectBanner();
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

    protected virtual void DrawManaBar()
    {
        _manaBar.Draw(GlobalVariablesDto.SpriteBatchEntities);
    }

    private void DrawEffects()
    {
        foreach (var unitEffect in Effects)
        {
            new ScrollMiddleBannerSprite().Draw(unitEffect.Rectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchEntities);
            unitEffect.Effect.Icon.Draw(unitEffect.Rectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchEntities);
        }
    }

    protected virtual void DrawDeadAnimation()
    {
        _deadAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, GlobalVariablesDto.SpriteBatchEntities);
    }

    protected virtual void DrawLevelUpAnimation()
    {
        _levelUpAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, GlobalVariablesDto.SpriteBatchEntities);
    }

    private void DrawSkillTexts()
    {
        _skillResultTexts.ForEach(x => x.Draw(GlobalVariablesDto.SpriteBatchEntities));
    }

    private void DrawEffectBanner()
    {
        if (_effectBanner.IsVisible)
            _effectBanner.Draw(GlobalVariablesDto.SpriteBatchInterface);
    }

    #endregion

    #region Functions

    #region Hover

    public bool IsHovering(Point mousePosition)
    {
        return IsHoveringUnit(mousePosition) || IsHoveringEffect(mousePosition);
    }

    private bool IsHoveringUnit(Point mousePosition)
    {
        return Rectangle.Contains(mousePosition);
    }

    private bool IsHoveringEffect(Point mousePosition)
    {
        return Effects.Any(x => x.Rectangle.Contains(mousePosition));
    }

    private UnitEffect GetHoveringEffect(Point mousePosition)
    {
        return Effects.First(x => x.Rectangle.Contains(mousePosition));
    }

    #endregion

    #region Take Damage

    public int RecieveAttack(int damage)
    {
        var damageTaken = Stats.RecieveAttack(damage);

        TickDamageRecieved();
        AddAttackSkillText(damageTaken);

        return damageTaken;
    }

    public int RecieveTrueDamage(int damage)
    {
        var damageTaken = Stats.RecieveTrueDamage(damage);

        TickDamageRecieved();
        AddAttackSkillText(damageTaken);

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
        var trueHealAmount = Stats.HealHealth(healAmount);

        ResetTakeHealDelay();
        AddHealthSkillText(trueHealAmount);

        return trueHealAmount;
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

        _deadAnimation.Reset();
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
        Skills = _skillTree.GetAvaliableSkills(this, Stats.Level);
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
        Effects.ForEach(x => x.Effect.TickDuration());
        Stats.RecoveryMana(Stats.ManaRegen);
    }

    public void ApplyEffectOnTurnStart()
    {
        Effects.ForEach(x => x.Effect.OnTurnStart(this));
    }

    public void RemoveExpiredEffects()
    {
        var effectsToRemove = Effects.Where(x => x.Effect.HasFinished).ToList();
        
        foreach (var effect in effectsToRemove)
            Effects.Remove(effect);
    }

    #endregion

    #region Effects

    #region Apply

    public void AddEffect(BaseEffect effect)
    {
        Effects.Add(new(effect));
        effect.OnApply(this);
    }

    public void ApplyReciveAttackEffects(SkillContext context)
    {
        foreach (var unitEffect in Effects)
            unitEffect.Effect.OnReceiveAttack(context);
    }

    public void ApplyExecuteAttackEffects(SkillContext context)
    {
        foreach (var unitEffect in Effects)
            unitEffect.Effect.OnAttack(context);
    }

    #endregion

    #region Hover

    //public bool IsHovering

    #endregion

    #endregion

    #region Skill Result Text

    private void AddAttackSkillText(int damage)
    {
        AddSkillText($"-{damage}", Color.Red);
    }

    private void AddHealthSkillText(int healthAmount)
    {
        AddSkillText($"+{healthAmount}", Color.Green);
    }

    public void AddSkillText(string text, Color color)
    {
        _skillResultTexts.Add(new((int)Center.X, (int)Center.Y, text, color));
    }

    #endregion

    #endregion
}

public class UnitEffect
{
    public BaseEffect Effect { get; set; }
    public Rectangle Rectangle { get; set; }

    public UnitEffect(BaseEffect effect, Rectangle rectangle)
    {
        Effect = effect;
        Rectangle = rectangle;
    }

    public UnitEffect(BaseEffect effect)
    {
        Effect = effect;
        Rectangle = Rectangle.Empty;
    }
}