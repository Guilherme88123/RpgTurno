using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.Entity.Units.Base;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Domain.Model.Components.Custom.Banners;

//TODO: Adicionar propriedade de velocidade do unit
//TODO: Só mostrar o XP quando for aliado
public class UnitBannerComponent : FrameComponent
{
    private const int _fixedSlice = 112;

    private readonly TextComponent _nameText = new(positionByCenter: true);
    private readonly TextComponent _healtText = new();
    private readonly TextComponent _defenseText = new();
    private readonly TextComponent _damageText = new();
    private readonly TextComponent _experienceText = new();

    private readonly ImageComponent _healthIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HeartIcon)), 32, 32);
    private readonly ImageComponent _defenseIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ShieldIcon)), 32, 32);
    private readonly ImageComponent _damageIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SwordIcon)), 32, 32);
    private readonly ImageComponent _experienceIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.StarIcon)), 32, 32);

    private readonly ImageComponent _unitIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAvatar)), 128, 128);

    public UnitBannerComponent()
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ScrollBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, new BorderDefinition(16, 16, 16, 16), 80)]));

        AddChild(_nameText);
        AddChild(_healtText);
        AddChild(_defenseText);
        AddChild(_damageText);
        AddChild(_experienceText);
        AddChild(_healthIcon);
        AddChild(_defenseIcon);
        AddChild(_damageIcon);
        AddChild(_experienceIcon);
        AddChild(_unitIcon);

        Bounds = new Rectangle(0, 0, 300, 335);
    }

    public void SetFocusedUnit(BaseUnitEntity focusedEntity)
    {
        _nameText.SetText($"{focusedEntity.Name} Lvl {focusedEntity.Stats.Level}");
        _healtText.SetText($"{focusedEntity.Stats.CurrentHealth}/{focusedEntity.Stats.MaxHealth}");
        _defenseText.SetText(focusedEntity.Stats.Defense.ToString());
        _damageText.SetText(focusedEntity.Stats.Attack.ToString());
        _experienceText.SetText($"{focusedEntity.Stats.CurrentExperience}/{focusedEntity.Stats.MaxExperience}");
        _unitIcon.SetImage(focusedEntity.Icon);

        SetPosition(Bounds.X, Bounds.Y);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _unitIcon.SetPosition(positionX + Bounds.Width / 2 - 64, positionY - 20);

        var textX = positionX + _fixedSlice + 10;
        var textY = positionY + 105;

        _nameText.SetPosition(positionX + Bounds.Width / 2, textY);

        var iconMarginX = 37;
        var iconMarginY = 5;

        _healthIcon.SetPosition(textX - iconMarginX, textY + 35 - iconMarginY);
        _healtText.SetPosition(textX, textY + 35);

        _defenseIcon.SetPosition(textX - iconMarginX, textY + 65 - iconMarginY);
        _defenseText.SetPosition(textX, textY + 65);

        _damageIcon.SetPosition(textX - iconMarginX, textY + 95 - iconMarginY);
        _damageText.SetPosition(textX, textY + 95);

        _experienceIcon.SetPosition(textX - iconMarginX, textY + 125 - iconMarginY);
        _experienceText.SetPosition(textX, textY + 125);
    }
}
