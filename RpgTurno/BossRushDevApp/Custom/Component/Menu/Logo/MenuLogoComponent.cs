using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite.Custom.Ui.Logo;

namespace RpgTurno.Custom.Component.Menu.Logo;

public class MenuLogoComponent : ImageComponent
{
    public MenuLogoComponent() : base(
        new MenuLogoSprite(), 717, 317)
    {
    }
}
