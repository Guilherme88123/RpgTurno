using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite.Custom.Sprite;
using Microsoft.Xna.Framework;

namespace RpgTurno.Custom.CustomComponents.Play.Selection;

public class SelectionAreaComponent : BaseComponent
{
    private const int _marginX = 32;

    public SelectionAreaComponent()
    {
        AnimationManager.Add(true, new SelectionAreaSprite());
    }

    public void SetDestinationRectangle(Rectangle destinationRectangle)
    {
        Bounds = FixRectangleWithFixedSlice(destinationRectangle);
    }

    private Rectangle FixRectangleWithFixedSlice(Rectangle destinationRectangle)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return new Rectangle(
            destinationRectangle.X - _marginX / 2 - bounce, 
            destinationRectangle.Y - _marginX / 2 - bounce,
            destinationRectangle.Width + _marginX + bounce * 2, 
            destinationRectangle.Height + _marginX + bounce * 2);
    }
}
