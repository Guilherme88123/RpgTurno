using Domain.Model.Entity.Units.Base;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite.Custom.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RpgTurno.Custom.Component.Play.Banners;
using System;
using System.Collections.Generic;

namespace RpgTurno.Custom.CustomComponents.Play.Banners;

public class SkillSelectBannerComponent : FrameComponent
{
    private List<SkillSelectButtonComponent> _buttons = new();

    public Action<UnitSkill> OnSkillSelect { get; set; }

    public SkillSelectBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        Bounds = new Rectangle(0, 0, 512, 384);
    }

    public void SetUnit(BaseUnitEntity unit)
    {
        _buttons.Clear();
        ClearChildren();

        int index = 0;
        foreach (var skill in unit.Skills)
        {
            var button = new SkillSelectButtonComponent(this, skill);

            var (positionX, positionY) = GetButtonPositionByIndex(index);
            button.SetPosition(positionX, positionY);

            _buttons.Add(button);
            AddChild(button);

            index++;
        }
    }

    private (int, int) GetButtonPositionByIndex(int index)
    {
        var initialPositionX = Bounds.X;
        var initialPositionY = Bounds.Y;

        var marginX = 64;
        var marginY = 64;

        var positionX = initialPositionX + marginX;
        var positionY = initialPositionY + marginY;

        return (positionX, positionY);
    }
}
