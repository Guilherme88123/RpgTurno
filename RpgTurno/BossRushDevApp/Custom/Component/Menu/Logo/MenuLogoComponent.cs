using Domain.Dto.Global;
using Domain.Application.Components.Image;
using Domain.Application.Texture.Sprite.Custom.Ui.Logo;
using Microsoft.Xna.Framework;

namespace RpgTurno.Custom.Component.Menu.Logo;

public class MenuLogoComponent : ImageComponent
{
    public MenuLogoComponent() : base(
        new MenuLogoSprite(), 708, 296)
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateBounceEffect();
    }

    private void UpdateBounceEffect()
    {
        var bounce = GlobalVariablesDto.GetBounceValue(bounceAmplitude: 0.0008f, bounceSpeed: 2f);

        ScaleX += bounce;
        ScaleY += bounce;
        OffsetY -= bounce * 200;
    }
}
