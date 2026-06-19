using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Switch;

//TODO: Refatorar Switch quando tiver uma oportunidade de testar
public class SwitchComponent : BaseComponent
{
    public string Text { get; set; }

    public Action<bool> Click { get; set; }
    public bool Value { get; set; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var mouse = Mouse.GetState();

        bool botaoPressionado = mouse.LeftButton == ButtonState.Pressed;

        if (botaoPressionado && !GlobalVariablesDto.IsMouseDown && HoverState.IsHover)
        {
            ToggleValue();
            Click?.Invoke(Value);
        }
    }

    public void ToggleValue()
    {
        Value = !Value;
    }

    protected string GetText()
    {
        string valueText = Value ? "ON" : "OFF";
        return $"{Text}: {valueText}";
    }
}
