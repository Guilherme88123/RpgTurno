using Domain.Const.Sprite;

namespace Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;

public class TrollClubPart2Sprite : AnimationClip
{
    public TrollClubPart2Sprite() : base(
        SpriteConst.TrollClubPart2,
        framesX: 10,
        framesY: 1,
        frameTime: 0.1f,
        row: 1)
    {
    }
}
