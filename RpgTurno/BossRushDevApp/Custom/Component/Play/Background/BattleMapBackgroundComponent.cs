using Domain.Application.Components.Image;
using Domain.Dto.Map.Building;
using Domain.Enum.Stage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Service.Stage;

namespace RpgTurno.Custom.CustomComponents.Play.Background;

public class BattleMapBackgroundComponent : ImageComponent
{
    private const float SizeFactor = 1.5f;

    private BuildingMapDto _buildDto;

    public BattleMapBackgroundComponent(StageCode stageCode)
    {
        InitializeDrawnableMap(stageCode);
    }

    #region Initialize

    private void InitializeDrawnableMap(StageCode stageCode)
    {
        _buildDto = BuildingMapFactory.Create(stageCode);

        SetImage(_buildDto.Background);
        Bounds = new(0, 0, (int)(_buildDto.Width * SizeFactor), (int)(_buildDto.Height * SizeFactor));
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        _buildDto.Sprites.ForEach(x => x.Update());
    }

    #endregion

    #region Draw

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawDecorations(spriteBatch);
    }

    private void DrawDecorations(SpriteBatch spriteBatch)
    {
        foreach (var treePosition in _buildDto.Decorations)
            treePosition.Sprite.Draw(
                new Rectangle(
                    (int)(treePosition.Point.X * SizeFactor),
                    (int)((treePosition.Point.Y - treePosition.Sprite.Height) * SizeFactor),
                    (int)(treePosition.Sprite.Width * SizeFactor),
                    (int)(treePosition.Sprite.Height * SizeFactor)),
                Color,
                Rotation,
                SpriteEffects,
                spriteBatch,
                Scale,
                Offset);
    }

    #endregion
}
