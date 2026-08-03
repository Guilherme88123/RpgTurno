using Application.Model.MenuElements.Button;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Application.Components.Button;
using Domain.Application.Components.Image;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Data;

namespace RpgTurno.Custom.Component.Map.Start;

public class StartBattleButtonComponent : ButtonIconComponent
{
    private const int Size = 160;

    public StartBattleButtonComponent(Action onClick) : base(new PlayIconSprite())
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
        var bounce = GlobalVariablesDto.GetBounceValue(bounceAmplitude: 0.07f);

        ScaleX += bounce;
        ScaleY -= bounce;
        OffsetY += bounce * 100;
    }
}
