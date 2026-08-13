using Domain.Dto.Global;
using Domain.Application.Components.Image;
using Domain.Application.Entity.Units.Base;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Custom.Component.Play.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RpgTurno.Custom.CustomComponents.Play.Banners;

public class SkillSelectBannerComponent : FrameComponent
{
    public Action<UnitSkill> OnSkillSelect { get; set; }

    private List<SkillSelectButtonComponent> _buttons = new();
    private SkillSelectButtonComponent _selectedButton = null;

    private SkillDetailsBannerComponent _detailsBanner = new();

    private ImageComponent _selectedSkillMark = new(new ConfirmIconSprite(), 48, 48);

    private const int SpacingX = 16;
    private const int SpacingY = 32;
    private const int Columns = 2;

    public SkillSelectBannerComponent()
    {
        AnimationManager.Add(true, new WoodBannerSprite());

        Bounds = new Rectangle(0, 0, 600, 512);
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
        var mouse = GlobalVariablesDto.MousePoint;
        return _buttons.FirstOrDefault(x => x.Bounds.Contains(mouse));
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
        _selectedButton = null;

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

        var positionX = GetPositionXByColumn(column, button);
        var positionY = GetPositionYByRow(row, button);

        return (positionX, positionY);
    }

    private int GetPositionXByColumn(int column, SkillSelectButtonComponent button)
    {
        int columnGap = column == 0 ? - (button.Bounds.Width + SpacingX / 2) : SpacingX / 2;

        return Bounds.Center.X + columnGap;
    }

    private int GetPositionYByRow(int row, SkillSelectButtonComponent button)
    {
        int rowGap = row switch
        {
            0 => - (button.Bounds.Height + SpacingY / 2),
            1 => 0,
            2 => button.Bounds.Height + SpacingY / 2
        };

        return Bounds.Center.Y + rowGap - button.Bounds.Height / 2;
    }

    public void SelectSkill(UnitSkill skill, SkillSelectButtonComponent button)
    {
        if (!skill.CanUse())
            return;

        _selectedButton = button;
        _selectedSkillMark.SetPosition(button.Bounds.X - 16, button.Bounds.Y - 16);

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
