using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite.Custom.Terrain.Trees;
using Domain.Model.Texture.Sprite.Custom.Ui.Maps;
using Infrastructure.Tiled;
using Infrastructure.Tiled.Dto;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RpgTurno.Custom.CustomComponents.Map.Background;

public class WorldMapBackgroundComponent : ImageComponent
{
    private const int TreeLayerId = 4;
    private const int RockLayerId = 5;
    private const int BusheLayerId = 6;

    private static Tree1Sprite _treeSprite = new();

    private List<Point> _treePositions;

    public WorldMapBackgroundComponent() : base(new WorldMapBackgroundSprite(), GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize)
    {
        InitializePositions();
    }

    private void InitializePositions()
    {
        var dto = GetTiledDto();

        Bounds = new(0, 0, dto.RealWidth, dto.RealHeight);

        InitializeTreePositions(dto);
    }

    private TiledMapDto GetTiledDto()
    {
        string filename = Path.Combine(GlobalVariablesDto.Content.RootDirectory, SpriteConst.WorldMapBackground + ".json");

        return TiledManagerService.ParseTiledMap(filename);
    }

    private void InitializeTreePositions(TiledMapDto tiledDto)
    {
        var treeLayer = tiledDto.Layers.First(x => x.Id == TreeLayerId);

        _treePositions = new();

        for (var y = 0; y < treeLayer.Height; y++)
        for (var x = 0; x < treeLayer.Width; x++)
        {
            if (treeLayer.Matrix[y, x] == 0)
                continue;

            _treePositions.Add(new Point(x * tiledDto.TileWidth, y * tiledDto.TileHeight));
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _treeSprite.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawTrees(spriteBatch);
    }

    private void DrawTrees(SpriteBatch spriteBatch)
    {
        foreach (var treePosition in _treePositions)
            _treeSprite.Draw(
                new Rectangle(treePosition.X, treePosition.Y - _treeSprite.Height, _treeSprite.Width, _treeSprite.Height),
                Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }
}
