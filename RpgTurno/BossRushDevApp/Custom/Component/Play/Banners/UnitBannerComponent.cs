using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.Entity.Units.Base;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;

namespace RpgTurno.Custom.CustomComponents.Play.Banners;

public class UnitBannerComponent : FrameComponent
{
    private const int _iconSize = 128;

    private readonly TextComponent _nameText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly TextComponent _healtText = new();
    private readonly TextComponent _manaText = new();
    private readonly TextComponent _defenseText = new();
    private readonly TextComponent _damageText = new();
    private readonly TextComponent _experienceText = new();
    private readonly TextComponent _speedText = new();

    private readonly ImageComponent _healthIcon = new(new HeartIconSprite(), 32, 32);
    private readonly ImageComponent _manaIcon = new(new ManaIconSprite(), 32, 32);
    private readonly ImageComponent _defenseIcon = new(new ShieldIconSprite(), 32, 32);
    private readonly ImageComponent _damageIcon = new(new SwordIconSprite(), 32, 32);
    private readonly ImageComponent _experienceIcon = new(new PurpleStarIconSprite(), 32, 32);
    private readonly ImageComponent _speedIcon = new(new BootIconSprite(), 32, 32);

    private readonly ImageComponent _unitIcon = new(new SwordIconSprite(), _iconSize, _iconSize);

    public UnitBannerComponent()
    {
        AnimationManager.Add(true, new ScrollBannerSprite());

        AddChild(_nameText);
        AddChild(_healtText);
        AddChild(_manaText);
        AddChild(_defenseText);
        AddChild(_damageText);
        AddChild(_experienceText);
        AddChild(_speedText);
        AddChild(_healthIcon);
        AddChild(_manaIcon);
        AddChild(_defenseIcon);
        AddChild(_damageIcon);
        AddChild(_experienceIcon);
        AddChild(_speedIcon);
        AddChild(_unitIcon);

        Bounds = new Rectangle(0, 0, 320, 512);
    }

    public void SetFocusedUnit(BaseUnitEntity focusedEntity, bool isEnemy)
    {
        _nameText.SetText($"{focusedEntity.Name} Lvl {focusedEntity.Stats.Level}");
        _healtText.SetText($"{focusedEntity.Stats.CurrentHealth}/{focusedEntity.Stats.MaxHealth}");
        _manaText.SetText($"{focusedEntity.Stats.CurrentMana}/{focusedEntity.Stats.MaxMana}");
        _defenseText.SetText(focusedEntity.Stats.Defense.ToString());
        _damageText.SetText(focusedEntity.Stats.Attack.ToString());
        _speedText.SetText(focusedEntity.Stats.Speed.ToString());
        _experienceText.SetText($"{focusedEntity.Stats.CurrentExperience}/{focusedEntity.Stats.MaxExperience}");
        _unitIcon.SetImage(focusedEntity.Icon);

        _experienceIcon.IsVisible = !isEnemy;
        _experienceText.IsVisible = !isEnemy;

        SetPosition(Bounds.X, Bounds.Y);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _unitIcon.SetPosition(Bounds.Center.X - _iconSize / 2, Bounds.Y + _iconSize / 2);

        SetFieldPositionByIndex(_nameText, null, 0);
        SetFieldPositionByIndex(_healtText, _healthIcon, 1);
        SetFieldPositionByIndex(_manaText, _manaIcon, 2);
        SetFieldPositionByIndex(_damageText, _damageIcon, 3);
        SetFieldPositionByIndex(_defenseText, _defenseIcon, 4);
        SetFieldPositionByIndex(_speedText, _speedIcon, 5);
        SetFieldPositionByIndex(_experienceText, _experienceIcon, 6);
    }

    private void SetFieldPositionByIndex(TextComponent textComponent, ImageComponent imageComponent, int index)
    {
        SetTextPositionByIndex(textComponent, index);

        if (imageComponent is not null)
            SetImagePositionByIndex(imageComponent, index);
    }

    private void SetTextPositionByIndex(TextComponent textComponent, int index)
    {
        var positionY = Bounds.Y + GetYOffsetByIndex(index);
        var positionX = textComponent.IsPositionXByCenter ? Bounds.X + Bounds.Width / 2 : Bounds.X + GetXOffset();

        textComponent.SetPosition(positionX, positionY);
    }

    private void SetImagePositionByIndex(ImageComponent imageComponent, int index)
    {
        var iconMarginX = 37;
        var iconMarginY = 5;

        var positionY = Bounds.Y + GetYOffsetByIndex(index) - iconMarginY;
        var positionX = Bounds.X + GetXOffset() - iconMarginX;

        imageComponent.SetPosition(positionX, positionY);
    }

    private int GetYOffsetByIndex(int index)
    {
        var marginY = 204;
        var textHeight = 32;

        return marginY + textHeight * index;
    }

    private int GetXOffset()
    {
        var marginX = 122;
        return marginX;
    }
}
