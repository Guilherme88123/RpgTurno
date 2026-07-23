using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Entity.Units.Base;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Custom.Component.Play.Banners;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Custom.CustomComponents.Play.Banners;

public class SkillSelectBannerComponent : FrameComponent
{
    public Action<UnitSkill> OnSkillSelect { get; set; }

    private List<SkillSelectButtonComponent> _buttons = new();
    private SkillSelectButtonComponent _selectedButton = null;

    private SkillDetailsBannerComponent _detailsBanner = new();

    private ImageComponent _selectedSkillMark = new(new ConfirmIconSprite(), 48, 48);

    private const int MarginX = 64;
    private const int MarginY = 64;
    private const int Spacing = 32;
    private const int Columns = 2;

    public SkillSelectBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        Bounds = new Rectangle(0, 0, 512, 512);
    }

    public bool HasCursorHoveringButton()
    {
        var hoverButton = GetHoverButton();
        return hoverButton is not null;
    }

    public bool CanUseFocusedButton()
    {
        var hoverButton = GetHoverButton();

        if (hoverButton is null)
            return false;

        return hoverButton.CanUseSkill();
    }

    public SkillSelectButtonComponent GetHoverButton()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _buttons.FirstOrDefault(x => x.Bounds.Contains(mouse.Position));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        VerifyVisibility();

        if (HasCursorHoveringButton())
            SetDetailsBannerOnHoverButton();
        else
            SetDetailsBannerInvisible();
    }

    private void VerifyVisibility()
    {
        if (!IsVisible)
            _selectedButton = null;
    }

    private void SetDetailsBannerOnHoverButton()
    {
        _detailsBanner.IsVisible = true;

        var button = GetHoverButton();
        _detailsBanner.SetHoverSkillButton(button);
    }

    private void SetDetailsBannerInvisible()
    {
        _detailsBanner.IsVisible = false;
    }

    public void SetUnit(BaseUnitEntity unit)
    {
        _buttons.Clear();
        ClearChildren();

        int index = 0;
        foreach (var skill in unit.Skills)
        {
            var button = new SkillSelectButtonComponent(this, skill);

            var (positionX, positionY) = GetButtonPositionByIndex(index, button);
            button.SetPosition(positionX, positionY);

            _buttons.Add(button);
            AddChild(button);

            index++;
        }
    }

    private (int, int) GetButtonPositionByIndex(int index, SkillSelectButtonComponent button)
    {
        int column = index % Columns;
        int row = index / Columns;

        var positionX = Bounds.X + MarginX + column * (button.Bounds.Width + Spacing) + Spacing / 2;
        var positionY = Bounds.Y + MarginY + row * (button.Bounds.Height + Spacing) + Spacing / 2;

        return (positionX, positionY);
    }

    public void SelectSkill(UnitSkill skill, SkillSelectButtonComponent button)
    {
        if (!skill.CanUse())
            return;

        _selectedButton = button;
        _selectedSkillMark.SetPosition(button.Bounds.X - Spacing / 2, button.Bounds.Y - Spacing / 2);

        OnSkillSelect?.Invoke(skill);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (_selectedButton is not null)
            DrawSelectMark(spriteBatch);

        _detailsBanner.Draw(spriteBatch);
    }

    private void DrawSelectMark(SpriteBatch spriteBatch)
    {
        _selectedSkillMark.Draw(spriteBatch);
    }
}
