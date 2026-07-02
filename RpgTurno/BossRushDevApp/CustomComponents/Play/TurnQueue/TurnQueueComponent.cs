using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Entity.Units.Base.Particle;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.CustomComponents.TurnQueue;

//TODO: Interligar com Hover no personagem para mostrar detalhes do personagem:
// Ou seja, quando passar o mouse no ícone do personagem, mostrar detalhes no banner, e quando hover normalmente, mostrar 
// onde ele está na fila

//TODO: Animação de transição da fila
public class TurnQueueComponent : BaseComponent
{
    private const int MaxIconsCount = 7;
    private const int IconSize = 80;

    private List<SpriteData> _unitsList = new();

    private ImageComponent _queueBackground = new(
        new ResizableSpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRibbons),
            ResizableSpriteType.Horizontal, 64, 0, new BorderDefinition(0, 576, 0, 0), 64),
            GlobalOptionsDto.WidthSize / 3, 80);

    private SmallDustEffect _dustEffect = new();

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

    public void SetUnitsList(List<SpriteData> unitsList)
    {
        if (_isInTransition)
            return;

        _unitsList = unitsList;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_isInTransition)
            UpdateTransition();
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
        var initialPosition = _queueBackground.Bounds.X + _queueBackground.Bounds.Width - IconSize / 2;

        int count = 1;
        foreach (var unit in _unitsList)
        {
            if (_isInTransition && unit == _unitsList.First())
            {
                DrawDustEffect(spriteBatch, initialPosition);
                count++;
                continue;
            }

            if (count > MaxIconsCount)
                return;

            var unitRectangle = new Rectangle(initialPosition - (IconSize * count) + (int)_transitionOffset, Bounds.Y, IconSize, IconSize);
            unit.Draw(unitRectangle, Color, Rotation, SpriteEffects, spriteBatch);

            count++;
        }
    }

    private void DrawDustEffect(SpriteBatch spriteBatch, int initialPosition)
    {
        var dustRectangle = new Rectangle(initialPosition - IconSize, Bounds.Y, IconSize, IconSize);
        _dustEffect.Draw(dustRectangle, Color, Rotation, SpriteEffects, spriteBatch);
    }

    #endregion
}
