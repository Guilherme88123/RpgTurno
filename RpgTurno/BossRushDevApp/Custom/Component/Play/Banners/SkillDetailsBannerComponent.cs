using Domain.Dto.Global;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;

namespace RpgTurno.Custom.Component.Play.Banners;

public class SkillDetailsBannerComponent : FrameComponent
{
    private const int _sizeX = 256;
    private const int _sizeY = 384;

    private readonly TextComponent _nameText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _descriptionText = new(positionXByCenter: true);
    private readonly TextComponent _targetTypeText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _targetAmountText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _cooldownText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _currentCooldownText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _manaCostText = new(positionXByCenter: true, positionYByCenter: true);

    public SkillDetailsBannerComponent()
    {
        AnimationManager.Add(true, new PaperBannerSprite());

        AddChild(_nameText);
        AddChild(_descriptionText);
        AddChild(_targetTypeText);
        AddChild(_targetAmountText);
        AddChild(_cooldownText);
        AddChild(_currentCooldownText);
        AddChild(_manaCostText);

        Bounds = new(0, 0, _sizeX, _sizeY);
    }

    public void SetHoverSkillButton(SkillSelectButtonComponent button)
    {
        var x = button.Bounds.X + button.Bounds.Width / 2 - _sizeX / 2;
        var y = button.Bounds.Y - _sizeY;

        SetSkill(button.GetSkill());
        SetPosition(x, y);
    }

    private void SetSkill(UnitSkill skill)
    {
        _nameText.SetText(skill.Definition.Name);
        _descriptionText.SetText(skill.Definition.Description);
        _targetTypeText.SetText($"Target Type: {skill.Definition.TargetType.ToString()}");
        _targetAmountText.SetText($"Target Amount: {skill.Definition.TargetAmount.ToString()}");
        _cooldownText.SetText($"Cooldown: {skill.Definition.Cooldown.ToString()}");
        _currentCooldownText.SetText($"Current Cooldown: {skill.CurrentCooldown.ToString()}");
        _manaCostText.SetText($"Mana Cost: {skill.Definition.ManaCost.ToString()}");

        _currentCooldownText.Color = skill.CurrentCooldown <= 0
            ? Microsoft.Xna.Framework.Color.Black
            : Microsoft.Xna.Framework.Color.Red;

        _manaCostText.Color = skill.OwnerUnit.Stats.CanSpendMana(skill.Definition.ManaCost)
            ? Microsoft.Xna.Framework.Color.Black 
            : Microsoft.Xna.Framework.Color.Red;
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);

        SetFieldPositionByIndex(_nameText, 1);
        SetFieldPositionByIndex(_descriptionText, 2);
        SetFieldPositionByIndex(_targetTypeText, 5);
        SetFieldPositionByIndex(_targetAmountText, 6);
        SetFieldPositionByIndex(_cooldownText, 7);
        SetFieldPositionByIndex(_currentCooldownText, 8);
        SetFieldPositionByIndex(_manaCostText, 9);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = (int)GlobalVariablesDto.GetBounceValue();
        return baseValue - bounce;
    }

    private void SetFieldPositionByIndex(TextComponent textComponent, int index)
    {
        var marginY = 32;
        var marginX = 32;
        var textHeight = 32;

        var positionY = Bounds.Y + marginY + textHeight * index;
        var positionX = textComponent.IsPositionXByCenter ? Bounds.X + Bounds.Width / 2 : Bounds.X + marginX;

        textComponent.SetPosition(positionX, positionY);
    }
}
