using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Button;

//TODO: Refatorar Button quando tiver uma oportunidade de testar
public class ButtonComponent : BaseComponent
{
    public Action Click { get; set; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        bool botaoPressionado = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;

        if (botaoPressionado && !GlobalVariablesDto.IsMouseDown && HoverState.IsHover)
        {
            Click?.Invoke();
        }
    }
}
