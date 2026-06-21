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

    private TextComponent _nameText = new();
    private TextComponent _healtText = new();
    private TextComponent _damageText = new();

    private ImageComponent _nameIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HammerIcon)), 32, 32);
    private ImageComponent _swordIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SwordIcon)), 32, 32);
    private ImageComponent _shieldIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ShieldIcon)), 32, 32);
    private ImageComponent _unitIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAvatar)), 128, 128);

    public UnitBannerComponent()
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PaperBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, new BorderDefinition(16, 16, 16, 16), 80)]));

        AddChild(_nameText);
        AddChild(_healtText);
        AddChild(_damageText);
        AddChild(_nameIcon);
        AddChild(_swordIcon);
        AddChild(_shieldIcon);
        AddChild(_unitIcon);

        Bounds = new Rectangle(0, 0, 300, 240);
    }

    public void SetFocusedUnit(BaseUnitEntity focusedEntity)
    {
        _nameText.SetText(focusedEntity.Name);
        _healtText.SetText($"{focusedEntity.Health}/{focusedEntity.MaxHealth}");
        _damageText.SetText(focusedEntity.Damage.ToString());
        _unitIcon.SetImage(focusedEntity.Icon);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _unitIcon.SetPosition(positionX + Bounds.Width / 2 - 64, positionY - 20);

        var textX = positionX + _fixedSlice + 10;
        var textY = positionY + 70;

        _nameIcon.SetPosition(textX - 37, textY + 30 - 5);
        _nameText.SetPosition(textX, textY + 30);

        _shieldIcon.SetPosition(textX - 37, textY + 60 - 5);
        _healtText.SetPosition(textX, textY + 60);

        _swordIcon.SetPosition(textX - 37, textY + 90 - 5);
        _damageText.SetPosition(textX, textY + 90);
    }
}
