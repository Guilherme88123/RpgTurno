using Domain.Dto.Global;
using Microsoft.Xna.Framework;

namespace Domain.Model.Components.Base;

public class HoverState
{
    public bool IsHover { get; private set; }

    public void Update(Rectangle bounds)
    {
        var mouse = GlobalVariablesDto.MouseState;

        IsHover = bounds.Contains(mouse.Position);
    }
}
