using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.ProgressBar;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Base.HealthBar;

public class HealthBarComponent : ProgressBarComponent
{
    private const int _sliceWidth = 16;

    public HealthBarComponent(int maxValue, int currentValue) : base(GetFillSprite(), maxValue, currentValue, _sliceWidth)
    {
        var baseTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarBase);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(baseTexture, ResizableSpriteType.Horizontal, _sliceWidth, 0, new BorderDefinition(16, 16, 48, 48), piecesGap: 64)]));

        Bounds = new Rectangle(0, 0, 120, 32);
    }

    private static SpriteData GetFillSprite()
    {
        var fillTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarFill);
        return new SpriteData(fillTexture, new BorderDefinition(16, 16, 0, 0));
    }
}