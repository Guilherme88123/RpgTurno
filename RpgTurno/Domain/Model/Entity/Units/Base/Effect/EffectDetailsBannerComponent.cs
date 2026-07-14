using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.Effect.Base;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;
using Microsoft.Xna.Framework;

namespace RpgTurno.Custom.Component.Play.Banners;

public class EffectDetailsBannerComponent : FrameComponent
{
    private const int _sizeX = 256;
    private const int _sizeY = 320;

    private const int _iconSize = 64;

    private readonly TextComponent _nameText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _descriptionText = new(positionXByCenter: true);
    private readonly TextComponent _durationText = new(positionXByCenter: true, positionYByCenter: true);

    private readonly ImageComponent _effectIcon = new(new SwordIconSprite(), _iconSize, _iconSize);

    public EffectDetailsBannerComponent()
    {
        AnimationManager.Add(true, new PaperBannerSprite());

        AddChild(_nameText);
        AddChild(_descriptionText);
        AddChild(_durationText);
        AddChild(_effectIcon);

        Bounds = new(0, 0, _sizeX, _sizeY);
    }

    public void SetHoverSkillButton(BaseEffect effect, Rectangle rectangle)
    {
        var x = rectangle.X + rectangle.Width / 2 - _sizeX / 2;
        var y = rectangle.Y - _sizeY;

        SetSkill(effect);
        SetPosition(x, y);
    }

    private void SetSkill(BaseEffect effect)
    {
        _nameText.SetText(effect.Name);
        _descriptionText.SetText(effect.Description);
        _durationText.SetText($"Duration: {effect.Duration}");
        _effectIcon.SetImage(effect.Icon);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);

        _effectIcon.SetPosition(Bounds.X + Bounds.Width / 2 - _iconSize / 2, Bounds.Y + _iconSize / 2);

        SetFieldPositionByIndex(_nameText, 3);
        SetFieldPositionByIndex(_descriptionText, 4);
        SetFieldPositionByIndex(_durationText, 8);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();
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
