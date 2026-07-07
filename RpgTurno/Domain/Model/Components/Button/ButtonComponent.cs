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
    private const float DelayPressed = 0.2f;
    private float _currentDelay = DelayPressed;

    public Action Click { get; set; }

    public readonly TextComponent Text = new(positionXByCenter: true, positionYByCenter: true);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Text.Update(gameTime);

        AnimationManager.Update(State);

        bool botaoPressionado = GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;

        if (botaoPressionado && !GlobalVariablesDto.PreviousMouseDown && HoverState.IsHover)
        {
            State = ButtonInteractionState.Pressed;
            _currentDelay = DelayPressed;
            SetPositionText();

            Click?.Invoke();

            return;
        }

        if (State == ButtonInteractionState.Pressed)
            UpdatePressedDelay();
    }

    private void UpdatePressedDelay()
    {
        _currentDelay -= GlobalVariablesDto.DeltaTime;

        if (_currentDelay < 0)
        {
            State = ButtonInteractionState.Regular;
            SetPositionText();
        }
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        SetPositionText(positionX, positionY);
    }

    private void SetPositionText()
    {
        SetPositionText(Bounds.X, Bounds.Y);
    }

    private void SetPositionText(int positionX, int positionY)
    {
        if (State == ButtonInteractionState.Pressed)
            positionY += 10;

        Text.SetPosition(positionX + Bounds.Width / 2, positionY + Bounds.Height / 2);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Text.Draw(spriteBatch);
    }
}
