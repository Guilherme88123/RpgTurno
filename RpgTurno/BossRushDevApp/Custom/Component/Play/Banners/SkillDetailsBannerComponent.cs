using Domain.Dto.Global;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite;

namespace RpgTurno.Custom.Component.Play.Banners;

public class SkillDetailsBannerComponent : FrameComponent
{
    private const int _sizeX = 256;
    private const int _sizeY = 256;

    public SkillDetailsBannerComponent()
    {
        AnimationManager.Add(true, new PaperBannerSprite());

        Bounds = new(0, 0, _sizeX, _sizeY);
    }

    public void SetHoverSkillButton(SkillSelectButtonComponent button)
    {
        var x = button.Bounds.X + button.Bounds.Width / 2 - _sizeX / 2;
        var y = button.Bounds.Y - _sizeY;

        SetPosition(x, y);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();
        return baseValue - bounce;
    }
}
