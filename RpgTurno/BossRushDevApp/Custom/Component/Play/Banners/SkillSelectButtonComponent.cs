using Application.Model.MenuElements.Button;
using Domain.Enum.Component.Button;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Buttons;
using Microsoft.Xna.Framework;
using RpgTurno.Custom.CustomComponents.Play.Banners;

namespace RpgTurno.Custom.Component.Play.Banners;

public class SkillSelectButtonComponent : ButtonComponent
{
    private readonly SkillSelectBannerComponent _banner;
    private readonly UnitSkill _skill;

    public SkillSelectButtonComponent(SkillSelectBannerComponent parentBanner, UnitSkill skill)
    {
        _banner = parentBanner;
        _skill = skill;
        Text.SetText(skill.Definition.Name);

        var canUse = skill.CanUse();

        Color = canUse ? Color.White : Color.Gray;
        IsEnable = canUse;

        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Bounds = new Rectangle(0, 0, 192, 128);

        Click = OnSkillButtonSelect;
    }

    public bool CanUseSkill()
    {
        return _skill.CanUse();
    }

    public UnitSkill GetSkill()
    {
        return _skill;
    }

    public void OnSkillButtonSelect()
    {
        _banner.SelectSkill(_skill, this);
    }
}
