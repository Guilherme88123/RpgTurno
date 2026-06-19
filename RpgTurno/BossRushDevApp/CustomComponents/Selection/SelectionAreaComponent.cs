using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.CustomComponents.Selection;

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
        return new Rectangle(destinationRectangle.X - _fixedSlice / 2, destinationRectangle.Y - _fixedSlice / 2,
            destinationRectangle.Width + _fixedSlice, destinationRectangle.Height + _fixedSlice);
    }
}
