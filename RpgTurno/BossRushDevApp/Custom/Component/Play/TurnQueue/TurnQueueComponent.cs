using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons.Small;
using Domain.Model.Texture.Sprite.CustomSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Custom.CustomComponents.Play.TurnQueue;

public class TurnQueueComponent : BaseComponent
{
    private const int MaxIconsCount = 7;
    private const int IconSize = 80;

    private int InitialPositionX => _queueBackground.Bounds.X + _queueBackground.Bounds.Width - IconSize / 2;

    private Dictionary<BaseUnitEntity, UnitIconComponent> _icons = new();
    private List<UnitIconComponent> _unitsIconList = new();
    private BaseUnitEntity _focusedUnit;

    private List<BaseUnitEntity> _pendingUnitsList;

    private ImageComponent _queueBackground = new(new BlueSmallRibbonSprite(), GlobalOptionsDto.WidthSize / 3, IconSize);

    private SmallDustAnimation _dustEffect = new();

    private bool _isInTransition = false;
    private float _transitionOffset;

    public TurnQueueComponent()
    {
        Bounds = new Rectangle(GlobalOptionsDto.WidthSize / 2, 16, 0, 0);
        _queueBackground.SetPosition(GlobalOptionsDto.WidthSize / 3, 16);
    }

    public void StartTransition()
    {
        _isInTransition = true;
        _dustEffect.Reset();
    }

    public void SetUnitsList(List<BaseUnitEntity> unitsList)
    {
        if (_isInTransition)
        {
            _pendingUnitsList = unitsList;
            return;
        }

        ApplyUnitsList(unitsList);
    }

    private void ApplyUnitsList(List<BaseUnitEntity> unitsList)
    {
        foreach (var unit in unitsList)
        {
            if (!_icons.ContainsKey(unit))
                _icons.Add(unit, new UnitIconComponent(unit));
        }

        _unitsIconList = unitsList
            .Select(x => _icons[x])
            .ToList();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_isInTransition)
            UpdateTransition();

        UpdateIconsPosition();
    }

    private void UpdateIconsPosition()
    {
        int count = 1;

        foreach (var unit in _unitsIconList)
        {
            unit.Rectangle = new Rectangle(
                InitialPositionX - (IconSize * count) + (int)_transitionOffset, 
                Bounds.Y, 
                IconSize, 
                IconSize);

            if (unit.Unit == _focusedUnit)
                unit.Rectangle = GetBoucedRectangle(unit.Rectangle);

            count++;
        }
    }

    private Rectangle GetBoucedRectangle(Rectangle rectangle)
    {
        var bounce = GetBounceValue();

        return new Rectangle(
            rectangle.X - bounce,
            rectangle.Y - bounce,
            rectangle.Width + (bounce * 2),
            rectangle.Height + (bounce * 2));
    }

    private void UpdateTransition()
    {
        _transitionOffset += IconSize * 2 * GlobalVariablesDto.DeltaTime;

        _dustEffect.Update();

        if (_transitionOffset > IconSize)
            FinishTransition();
    }

    private void FinishTransition()
    {
        _transitionOffset = 0;
        _isInTransition = false;

        if (_pendingUnitsList is not null)
        {
            ApplyUnitsList(_pendingUnitsList);
            _pendingUnitsList = null;
        }
    }

    #region Draw

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (!IsVisible)
            return;

        DrawQueueBackground(spriteBatch);
        DrawUnitsList(spriteBatch);
    }

    private void DrawQueueBackground(SpriteBatch spriteBatch)
    {
        _queueBackground.Draw(spriteBatch);
    }

    private void DrawUnitsList(SpriteBatch spriteBatch)
    {
        int count = 1;
        foreach (var unit in _unitsIconList)
        {
            if (_isInTransition && count == 1)
            {
                DrawDustEffect(spriteBatch);
                count++;
                continue;
            }

            if (count > MaxIconsCount)
                return;

            unit.Draw(spriteBatch);

            count++;
        }
    }

    private void DrawDustEffect(SpriteBatch spriteBatch)
    {
        var dustRectangle = new Rectangle(
                InitialPositionX - IconSize,
                Bounds.Y,
                IconSize,
                IconSize);

        _dustEffect.Draw(dustRectangle, Color, Rotation, SpriteEffects, spriteBatch);
    }

    #endregion

    #region Hover Units

    public bool HasCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _unitsIconList.Any(x => x.Rectangle.Contains(mouse.X, mouse.Y));
    }

    public BaseUnitEntity GetCursorHoveringEntity()
    {
        var mouse = GlobalVariablesDto.MouseState;
        return _unitsIconList.First(x => x.Rectangle.Contains(mouse.X, mouse.Y)).Unit;
    }

    #endregion

    #region Focused Units

    public void SetFocusedUnit(BaseUnitEntity unit)
    {
        if (!_icons.ContainsKey(unit))
            return;

        _focusedUnit = unit;
    }

    public void ClearFocusedUnit()
    {
        _focusedUnit = null;
    }

    #endregion

    #region Bounce

    private int GetBounceValue()
    {
        return GlobalVariablesDto.GetBounceValue();
    }

    #endregion
}

public class UnitIconComponent
{
    public BaseUnitEntity Unit { get; set; }
    public Rectangle Rectangle { get; set; }

    public UnitIconComponent(BaseUnitEntity unit)
    {
        Unit = unit;
        Rectangle = Rectangle.Empty;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Unit.Icon.Draw(
            Rectangle,
            Color.White,
            0,
            SpriteEffects.None,
            spriteBatch);
    }
}