using Application.Model.MenuElements.Button;
using Domain.Dto.Language;
using Domain.Enum.Component.Button;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Microsoft.Xna.Framework;
using RpgTurno.Custom.CustomComponents.Play.Banners;

namespace RpgTurno.Custom.Component.Play.Banners;

public class SkillSelectButtonComponent : ButtonComponent
{
    private const int Width = 224;
    private const int Height = 96;
    private const int Margin = 16;

    private readonly SkillSelectBannerComponent _banner;
    private readonly UnitSkill _skill;

    public SkillSelectButtonComponent(SkillSelectBannerComponent parentBanner, UnitSkill skill)
    {
        _banner = parentBanner;
        _skill = skill;
        Text.SetWrapedText(LanguageManager.Get(skill.Definition.Name), Width - Margin * 2);

        var canUse = skill.CanUse();

        Color = canUse ? Color.White : Color.Gray;
        IsEnable = canUse;

        AnimationManager.Add(ButtonInteractionState.Regular, new BlueButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new BlueButtonPressedSprite());

        Bounds = new Rectangle(0, 0, Width, Height);

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
