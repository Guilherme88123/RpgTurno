using Application.Model.MenuElements.Base;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Switch;

public class SwitchComponent : BaseComponent
{
    public Action<bool> Click { get; set; }
    public bool Value { get; set; }

    public override void Update()
    {
        base.Update();

        var mouse = Mouse.GetState();

        bool botaoPressionado = mouse.LeftButton == ButtonState.Pressed;
        bool delayFinished = _currentDelay < 0;

        if (botaoPressionado && delayFinished && !GlobalVariablesDto.IsMouseDown && IsHover)
        {
            ClickSound?.Play(GlobalOptionsDto.SfxVolumeFloat, 0f, 0f);
            ToggleValue();
            Click?.Invoke(Value);
            _currentDelay = Delay;
            _currentDelayClickAnimation = Delay;
        }
    }

    public void ToggleValue()
    {
        Value = !Value;
    }

    protected override string GetText()
    {
        string valueText = Value ? "ON" : "OFF";
        return $"{Text}: {valueText}";
    }
}
