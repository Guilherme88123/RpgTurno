using Domain.Application.Components.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Microsoft.Xna.Framework;
using System;

namespace RpgTurno.Custom.Component.Map.Button;

public class PreviousBattleButtonComponent : ButtonIconComponent
{
    private const int Size = 160;

    public PreviousBattleButtonComponent(Action onClick) : base(new ReturnIconSprite())
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new SmallBlueRoundButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new SmallBlueRoundButtonPressedSprite());

        Click += onClick;

        Bounds = new(0, 0, Size, Size);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateBounceEffect();
    }

    private void UpdateBounceEffect()
    {
        var bounce = GlobalVariablesDto.GetBounceValue(bounceAmplitude: 0.05f);

        ScaleX += bounce;
        ScaleY -= bounce;
    }
}
