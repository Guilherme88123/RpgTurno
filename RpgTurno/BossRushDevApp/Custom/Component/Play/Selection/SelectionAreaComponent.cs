using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite.Custom.Ui.Cursor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RpgTurno.Custom.CustomComponents.Play.Selection;

public class SelectionAreaComponent : BaseComponent
{
    private const int _marginX = 32;

    public SelectionAreaComponent()
    {
        AnimationManager.Add(true, new SelectionAreaSprite());
    }

    public void DrawOnFocusedUnits(List<BaseUnitEntity> focusedUnits, SpriteBatch spriteBatch)
    {
        foreach (var unit in focusedUnits)
        {
            var boundUnitRectangle = GetBounceRectangle(unit.Rectangle);
            AnimationManager.Draw(boundUnitRectangle, Color, Rotation, SpriteEffects, spriteBatch, Vector2.One, Vector2.Zero);
        }
    }

    private Rectangle GetBounceRectangle(Rectangle destinationRectangle)
    {
        var bounce = (int)GlobalVariablesDto.GetBounceValue();

        return new Rectangle(
            destinationRectangle.X - _marginX / 2 - bounce, 
            destinationRectangle.Y - _marginX / 2 - bounce,
            destinationRectangle.Width + _marginX + bounce * 2, 
            destinationRectangle.Height + _marginX + bounce * 2);
    }
}
