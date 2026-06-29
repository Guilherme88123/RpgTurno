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

public class UnitBannerComponent : FrameComponent
{
    private const int _fixedSlice = 112;

    private readonly TextComponent _nameText = new();
    private readonly TextComponent _levelText = new();
    private readonly TextComponent _healtText = new();
    private readonly TextComponent _defenseText = new();
    private readonly TextComponent _damageText = new();
    private readonly TextComponent _experienceText = new();

    private readonly ImageComponent _nameIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HammerIcon)), 32, 32);
    private readonly ImageComponent _levelIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PlayIcon)), 32, 32);
    private readonly ImageComponent _healthIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.MeatIcon)), 32, 32);
    private readonly ImageComponent _defenseIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ShieldIcon)), 32, 32);
    private readonly ImageComponent _damageIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SwordIcon)), 32, 32);
    private readonly ImageComponent _experienceIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.GoldIcon)), 32, 32);

    private readonly ImageComponent _unitIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAvatar)), 128, 128);

    public UnitBannerComponent()
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PaperBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, new BorderDefinition(16, 16, 16, 16), 80)]));

        AddChild(_nameText);
        AddChild(_levelText);
        AddChild(_healtText);
        AddChild(_defenseText);
        AddChild(_damageText);
        AddChild(_experienceText);
        AddChild(_nameIcon);
        AddChild(_levelIcon);
        AddChild(_healthIcon);
        AddChild(_defenseIcon);
        AddChild(_damageIcon);
        AddChild(_experienceIcon);
        AddChild(_unitIcon);

        Bounds = new Rectangle(0, 0, 300, 335);
    }

    public void SetFocusedUnit(BaseUnitEntity focusedEntity)
    {
        _nameText.SetText(focusedEntity.Name);
        _levelText.SetText($"Level {focusedEntity.Stats.Level}");
        _healtText.SetText($"{focusedEntity.Stats.CurrentHealth}/{focusedEntity.Stats.MaxHealth}");
        _defenseText.SetText(focusedEntity.Stats.Defense.ToString());
        _damageText.SetText(focusedEntity.Stats.Attack.ToString());
        _experienceText.SetText($"{focusedEntity.Stats.CurrentExperience}/{focusedEntity.Stats.MaxExperience}");
        _unitIcon.SetImage(focusedEntity.Icon);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _unitIcon.SetPosition(positionX + Bounds.Width / 2 - 64, positionY - 20);

        var textX = positionX + _fixedSlice + 10;
        var textY = positionY + 65;

        _nameIcon.SetPosition(textX - 37, textY + 30 - 5);
        _nameText.SetPosition(textX, textY + 30);

        _levelIcon.SetPosition(textX - 37, textY + 60 - 5);
        _levelText.SetPosition(textX, textY + 60);

        _healthIcon.SetPosition(textX - 37, textY + 90 - 5);
        _healtText.SetPosition(textX, textY + 90);

        _defenseIcon.SetPosition(textX - 37, textY + 120 - 5);
        _defenseText.SetPosition(textX, textY + 120);

        _damageIcon.SetPosition(textX - 37, textY + 150 - 5);
        _damageText.SetPosition(textX, textY + 150);

        _experienceIcon.SetPosition(textX - 37, textY + 180 - 5);
        _experienceText.SetPosition(textX, textY + 180);
    }
}
