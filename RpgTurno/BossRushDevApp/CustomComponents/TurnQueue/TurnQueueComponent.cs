using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.CustomComponents.TurnQueue;

public class TurnQueueComponent : BaseComponent
{
    private const int IconSize = 64;

    private List<SpriteData> _unitsList = new();

    public TurnQueueComponent()
    {
        Bounds = new Rectangle(GlobalOptionsDto.WidthSize / 2, 16, 0, 0);
    }

    public void SetUnitsList(List<SpriteData> unitsList)
    {
        _unitsList = unitsList;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawUnitsList(spriteBatch);
    }

    private void DrawUnitsList(SpriteBatch spriteBatch)
    {
        int unitsTotalWidth = _unitsList.Count * IconSize;
        var initialPosition = Bounds.X - unitsTotalWidth / 2;

        int count = 0;
        foreach (var unit in _unitsList)
        {
            var unitRectangle = new Rectangle(initialPosition + (IconSize * count), Bounds.Y, IconSize, IconSize);
            unit.Draw(unitRectangle, Color, Rotation, SpriteEffects, spriteBatch);
            count++;
        }
    }
}
