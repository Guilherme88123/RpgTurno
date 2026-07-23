using Domain.Dto.Global;
using Domain.Model.Sound.Base;
using Domain.Model.Sound.Ui;
using Microsoft.Xna.Framework;

namespace Domain.Model.Components.Base;

public class HoverState
{
    public bool IsHover { get; private set; }

    public Action OnHoverIn { get; set; }
    public Action OnHoverOut { get; set; }

    private bool _wasHover;

    public void Update(Rectangle bounds)
    {
        var mouse = GlobalVariablesDto.MouseState;

        IsHover = bounds.Contains(mouse.Position);

        if (IsHover && !_wasHover)
            OnHoverIn?.Invoke();

        if (!IsHover && _wasHover)
            OnHoverOut?.Invoke();

        _wasHover = IsHover;
    }
}
