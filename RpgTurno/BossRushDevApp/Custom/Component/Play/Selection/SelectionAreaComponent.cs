using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RpgTurno.Custom.CustomComponents.Play.Selection;

public class SelectionAreaComponent : BaseComponent
{
    private const int _fixedSlice = 32;

    public SelectionAreaComponent()
    {
        var selectionArea = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SelectionArea);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(selectionArea, ResizableSpriteType.Full, _fixedSlice, _fixedSlice)]));

        Bounds = new(0, 0, selectionArea.Width, selectionArea.Height);
    }

    public void SetDestinationRectangle(Rectangle destinationRectangle)
    {
        Bounds = FixRectangleWithFixedSlice(destinationRectangle);
    }

    private Rectangle FixRectangleWithFixedSlice(Rectangle destinationRectangle)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return new Rectangle(
            destinationRectangle.X - _fixedSlice / 2 - bounce, 
            destinationRectangle.Y - _fixedSlice / 2 - bounce,
            destinationRectangle.Width + _fixedSlice + bounce * 2, 
            destinationRectangle.Height + _fixedSlice + bounce * 2);
    }
}
