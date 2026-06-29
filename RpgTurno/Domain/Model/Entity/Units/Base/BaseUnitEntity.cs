using Domain.Dto.Global;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Base.HealthBar;
using Domain.Model.Entity.Units.Base.Stats;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;

namespace Domain.Model.Entity.Units.Base;

//TODO: Adicionar animação ao ser Destruído
public class BaseUnitEntity : BaseEntity
{
    public string Name { get; set; }

    public BaseUnitStats Stats { get; }

    public bool IsRanged { get; protected set; }

    public SpriteData Icon { get; protected set; }

    private readonly HealthBarComponent _healthBar;

    private const float DelayDamageTakenFlash = 0.1f;
    private float _currentDelayDamageTakenFlash;
    private bool HasTakeDamage => _currentDelayDamageTakenFlash > 0;

    public BaseUnitEntity(BaseUnitStats stats)
    {
        Stats = stats;

        _healthBar = new HealthBarComponent(Stats.MaxHealth, Stats.CurrentHealth);
    }

    #region Update

    public override void Update()
    {
        base.Update();

        UpdateDelays();
        UpdateHealthBarComponent();
        UpdateTakeDamageEffect();
    }

    private void UpdateDelays()
    {
        _currentDelayDamageTakenFlash = Math.Max(0, _currentDelayDamageTakenFlash - GlobalVariablesDto.DeltaTime);
    }

    private void UpdateHealthBarComponent()
    {
        _healthBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY);
        _healthBar.SetValues(Stats.MaxHealth, Stats.CurrentHealth);
        _healthBar.Update(GlobalVariablesDto.GameTime);
    }

    private void UpdateTakeDamageEffect()
    {
        if (HasTakeDamage)
            Color = Color.Red;
        else 
            Color = Color.White;
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        base.Draw();


        DrawHealthBar();
    }

    protected virtual void DrawHealthBar()
    {
        _healthBar.Draw(GlobalVariablesDto.SpriteBatchEntities);
    }

    #endregion

    #region Functions

    #region Take Damage

    public int RecieveAttack(BaseUnitEntity sender)
    {
        var damage = Stats.RecieveAttack(sender.Stats);

        if (Stats.HasHealthFinished())
            Destroy();

        ResetTakeDamageDelay();

        return damage;
    }

    private void ResetTakeDamageDelay()
    {
        _currentDelayDamageTakenFlash = DelayDamageTakenFlash;
    }

    #endregion

    #endregion
}
