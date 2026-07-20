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

    public bool AffectCursor { get; set; } = true;

    private const float DelayPressed = 0.2f;
    private float _currentDelay = DelayPressed;

    public readonly TextComponent Text = new(positionXByCenter: true, positionYByCenter: true);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Text.Update(gameTime);

        AnimationManager.Update(State);

        if (State == ButtonInteractionState.Pressed)
        {
            UpdatePressedDelay();
            return;
        }

        if (!CanClick())
            return;

        CursorManager.RequestHover();

        if (IsTryingClick())
            ExecuteClick();
    }

    private bool CanClick()
    {
        if (!IsVisible)
            return false;

        if (State == ButtonInteractionState.Pressed)
            return false;

        if (!HoverState.IsHover)
            return false;

        if (GlobalVariablesDto.PreviousMouseDown)
            return false;

        return true;
    }

    private bool IsTryingClick()
    {
        return GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;
    }

    private void ExecuteClick()
    {
        State = ButtonInteractionState.Pressed;
        _currentDelay = DelayPressed;
        SetPositionText();
    }

    private void UpdatePressedDelay()
    {
        _currentDelay -= GlobalVariablesDto.DeltaTime;

        if (_currentDelay < 0)
        {
            Click?.Invoke();

            State = ButtonInteractionState.Regular;
            AnimationManager.Update(State);
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

    public void SetText(string text)
    {
        Text.SetText(text);
    }
}
