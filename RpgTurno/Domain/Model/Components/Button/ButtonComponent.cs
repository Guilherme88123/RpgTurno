using Application.Model.MenuElements.Base;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Button;

public class ButtonComponent : BaseComponent
{
    public Action Click { get; set; }

    public bool IsClick { get; set; }

    public override void Update()
    {
        base.Update();

        bool botaoPressionado = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;
        bool delayFinished = _currentDelay < 0;

        if (botaoPressionado && delayFinished && !GlobalVariablesDto.IsMouseDown && IsHover)
        {
            _currentDelay = Delay;
            _currentDelayClickAnimation = Delay;
            ClickSound?.Play(GlobalOptionsDto.SfxVolumeFloat, 0f, 0f);
            IsClick = true;
        }

        if (!HasClicked && IsClick)
        {
            IsClick = false;
            Click?.Invoke();
        }
    }
}
