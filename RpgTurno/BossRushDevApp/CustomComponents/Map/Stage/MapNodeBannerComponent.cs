using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Stage.Node;
using RpgTurno.Screen.Play.Battle.Stage.Factory;
using System;
using System.Diagnostics;

namespace RpgTurno.CustomComponents.Map.Stage;

public class MapNodeBannerComponent : FrameComponent
{
    private const int _fixedSlice = 64;
    private const int _sizeX = 256;
    private const int _sizeY = 256;
    private const int _marginY = 200;

    private const int _iconSize = 32;
    private const int _difficultyIconSize = 32;

    private readonly TextComponent _nameText = new(positionByCenter: true);
    private readonly TextComponent _difficultyText = new(positionByCenter: true);
    private readonly TextComponent _clearedText = new(positionByCenter: true);

    private readonly ImageComponent _stageStatusIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.CloseIcon)), _iconSize, _iconSize);

    private readonly ImageComponent _difficulty1StarIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon)), _difficultyIconSize, _difficultyIconSize);
    private readonly ImageComponent _difficulty2StarIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon)), _difficultyIconSize, _difficultyIconSize);
    private readonly ImageComponent _difficulty3StarIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon)), _difficultyIconSize, _difficultyIconSize);
    private readonly ImageComponent _difficulty4StarIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon)), _difficultyIconSize, _difficultyIconSize);
    private readonly ImageComponent _difficulty5StarIcon = new(new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon)), _difficultyIconSize, _difficultyIconSize);

    private int _starsCount = 0;

    public MapNodeBannerComponent()
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PaperBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, null, 64)]));

        AddChild(_nameText);
        AddChild(_difficultyText);
        AddChild(_clearedText);
        AddChild(_stageStatusIcon);

        AddChild(_difficulty1StarIcon);
        AddChild(_difficulty2StarIcon);
        AddChild(_difficulty3StarIcon);
        AddChild(_difficulty4StarIcon);
        AddChild(_difficulty5StarIcon);

        Bounds = new Rectangle(0, 0, _sizeX, _sizeY);
    }

    public void SetCurrentMapNode(StageMapNode mapNode)
    {
        SetPositionByMapNode(mapNode);

        _nameText.SetText(mapNode.Name);
        _difficultyText.SetText("Difficulty:");
        _clearedText.SetText("Defeated:");
        _stageStatusIcon.SetImage(mapNode.Cleared 
            ? new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ConfirmIcon))
            : new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.CloseIcon)));

        _starsCount = mapNode.Difficulty;
    }

    private void SetPositionByMapNode(MapNodeData mapNode)
    {
        var positionX = mapNode.Position.X - _sizeX / 2;
        var positionY = mapNode.Position.Y - _sizeY - _marginY;

        SetPosition((int)positionX, (int)positionY);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);

        _nameText.SetPosition(positionX + Bounds.Width / 2, bouncedPositionY + 70);

        _difficultyText.SetPosition(positionX + Bounds.Width / 2, bouncedPositionY + 105);
        FixDificultyIconsPosition();

        _clearedText.SetPosition(positionX + Bounds.Width / 3 + 20, bouncedPositionY + 190);
        _stageStatusIcon.SetPosition(positionX + Bounds.Width / 3 * 2 - _iconSize / 2 + 10, bouncedPositionY + 190 - 3 - _iconSize / 2);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return baseValue - bounce;
    }

    private void FixDificultyIconsPosition()
    {
        var startX = Bounds.X + Bounds.Width / 2 - (_difficultyIconSize * _starsCount) / 2;

        var positionY = Bounds.Y + 125;

        for (int i = 0; i < _starsCount; i++)
        {
            var icon = GetDifficultyIconByIndex(i);
            icon.IsVisible = true;
            icon.SetPosition(startX + i * _difficultyIconSize, positionY);
        }

        for (int i = _starsCount; i < 5; i++)
        {
            var icon = GetDifficultyIconByIndex(i);
            icon.IsVisible = false;
        }
    }

    private ImageComponent GetDifficultyIconByIndex(int index)
    {
        return index switch
        {
            0 => _difficulty1StarIcon,
            1 => _difficulty2StarIcon,
            2 => _difficulty3StarIcon,
            3 => _difficulty4StarIcon,
            4 => _difficulty5StarIcon,
            _ => throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 4.")
        };
    }

    private int GetCountStarsByDificulty(int difficulty)
    {
        return difficulty switch
        {
            _ when difficulty <= 200 => 1,
            _ when difficulty <= 500 => 2,
            _ when difficulty <= 900 => 3,
            _ when difficulty <= 1400 => 4,
            _ when difficulty > 1400 => 5,
        };
    }
}
