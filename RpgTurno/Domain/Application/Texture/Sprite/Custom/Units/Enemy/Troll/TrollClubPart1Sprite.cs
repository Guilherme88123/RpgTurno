using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollClubPart1Sprite : AnimationClip
{
    public TrollClubPart1Sprite() : base(
        SpriteConst.TrollClubPart1,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
