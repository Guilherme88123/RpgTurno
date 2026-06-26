using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Base;
using Domain.Model.Components.Image;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Data;

namespace RpgTurno.CustomComponents.TurnQueue;

//TODO: Implmentar animação na passagem de turno
public class TurnQueueComponent : BaseComponent
{
    private const int MaxIconsCount = 7;
    private const int IconSize = 80;

    private List<SpriteData> _unitsList = new();

    private ImageComponent _queueBackground = new(
        new ResizableSpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRibbons), 
            ResizableSpriteType.Horizontal, 64, 0, new BorderDefinition(0, 576, 0, 0), 64), 
            GlobalOptionsDto.WidthSize / 3, 80);

    public TurnQueueComponent()
    {
        Bounds = new Rectangle(GlobalOptionsDto.WidthSize / 2, 16, 0, 0);
        _queueBackground.SetPosition(GlobalOptionsDto.WidthSize / 3, 16);
    }

    public void SetUnitsList(List<SpriteData> unitsList)
    {
        _unitsList = unitsList;
    }

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
            if (count > MaxIconsCount)
                return;

            var unitRectangle = new Rectangle(initialPosition - (IconSize * count), Bounds.Y, IconSize, IconSize);
            unit.Draw(unitRectangle, Color, Rotation, SpriteEffects, spriteBatch);

            count++;
        }
    }
}
