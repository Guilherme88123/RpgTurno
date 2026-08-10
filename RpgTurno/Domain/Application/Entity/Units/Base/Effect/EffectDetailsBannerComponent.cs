using Domain.Dto.Global;
using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.Effect.Base;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;
using Domain.Dto.Language;
using Domain.Const.Text;

namespace RpgTurno.Custom.Component.Play.Banners;

public class EffectDetailsBannerComponent : FrameComponent
{
    private const int Width = 384;
    private const int Height = 384;
    private const int Margin = 32;
    private const int TextHeight = 32;

    private const int IconSize = 64;

    private readonly TextComponent _nameText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _descriptionText = new(positionXByCenter: true);
    private readonly TextComponent _durationText = new(positionXByCenter: true, positionYByCenter: true);

    private readonly ImageComponent _effectIcon = new(new SwordIconSprite(), IconSize, IconSize);

    public EffectDetailsBannerComponent()
    {
        AnimationManager.Add(true, new PaperBannerSprite());

        AddChild(_nameText);
        AddChild(_descriptionText);
        AddChild(_durationText);
        AddChild(_effectIcon);

        Bounds = new(0, 0, Width, Height);
    }

    public void SetHoverSkillButton(BaseEffect effect, Rectangle rectangle)
    {
        var x = rectangle.X + rectangle.Width / 2 - Width / 2;
        var y = rectangle.Y - Height;

        SetSkill(effect);
        SetPosition(x, y);
    }

    private void SetSkill(BaseEffect effect)
    {
        _nameText.SetText(LanguageManager.Get(effect.Name));
        _descriptionText.SetWrapedText(LanguageManager.Get(effect.Description), Width - Margin * 2);
        _durationText.SetText($"{LanguageManager.Get(TextConst.Duration)}: {effect.Duration}");
        _effectIcon.SetImage(effect.Icon);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);

        _effectIcon.SetPosition(Bounds.X + Bounds.Width / 2 - IconSize / 2, Bounds.Y + IconSize / 2);

        SetFieldPositionByIndex(_nameText, 3);
        SetFieldPositionByIndex(_descriptionText, 4);

        _durationText.SetPosition(Bounds.Center.X, Bounds.Bottom - Margin - TextHeight);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = (int)GlobalVariablesDto.GetBounceValue();
        return baseValue - bounce;
    }

    private void SetFieldPositionByIndex(TextComponent textComponent, int index)
    {
        var positionY = Bounds.Y + Margin + TextHeight * index;
        var positionX = textComponent.IsPositionXByCenter ? Bounds.X + Bounds.Width / 2 : Bounds.X + Margin;

        textComponent.SetPosition(positionX, positionY);
    }
}
