using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Components.Base;
using Domain.Model.Components.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Button;

public class ButtonComponent : BaseComponent
{
    public ButtonInteractionState State { get; set; }

    public Action Click { get; set; }

    public readonly TextComponent Text = new(positionByCenter: true);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Text.Update(gameTime);

        AnimationManager.Update(State);

        bool botaoPressionado = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;

        if (botaoPressionado && !GlobalVariablesDto.PreviousMouseDown && HoverState.IsHover)
        {
            State = ButtonInteractionState.Pressed;
            Click?.Invoke();

            return;
        }

        State = ButtonInteractionState.Regular;
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        Text.SetPosition(positionX + Bounds.Width / 2, positionY + Bounds.Height / 2);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Text.Draw(spriteBatch);
    }
}
