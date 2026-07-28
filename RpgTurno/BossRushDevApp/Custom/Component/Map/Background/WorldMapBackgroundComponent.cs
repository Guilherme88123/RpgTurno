using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Terrain.Bushes;
using Domain.Model.Texture.Sprite.Custom.Terrain.Rocks;
using Domain.Model.Texture.Sprite.Custom.Terrain.Trees;
using Domain.Model.Texture.Sprite.Custom.Ui.Maps;
using Infrastructure.Tiled;
using Infrastructure.Tiled.Dto;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RpgTurno.Custom.CustomComponents.Map.Background;

public class WorldMapBackgroundComponent : ImageComponent
{
    private const int TreeLayerId = 4;
    private const int RockLayerId = 5;
    private const int BusheLayerId = 6;

    private readonly Vector2 Tree1SpriteId = new(457, 457);
    private readonly Vector2 Bushe1SpriteId = new(465, 472);
    private readonly Vector2 Bushe2SpriteId = new(473, 480);
    private readonly Vector2 Bushe3SpriteId = new(481, 488);
    private readonly Vector2 Bushe4SpriteId = new(489, 496);
    private readonly Vector2 Rock1SpriteId = new(497, 497);
    private readonly Vector2 Rock2SpriteId = new(498, 498);
    private readonly Vector2 Rock3SpriteId = new(499, 499);
    private readonly Vector2 Rock4SpriteId = new(500, 500);

    private static Tree1Sprite _tree1Sprite = new();
    private static Bushe1Sprite _bushe1Sprite = new();
    private static Bushe2Sprite _bushe2Sprite = new();
    private static Bushe3Sprite _bushe3Sprite = new();
    private static Bushe4Sprite _bushe4Sprite = new();
    private static Rock1Sprite _rock1Sprite = new();
    private static Rock2Sprite _rock2Sprite = new();
    private static Rock3Sprite _rock3Sprite = new();
    private static Rock4Sprite _rock4Sprite = new();

    private readonly List<PositionSpriteRecord> _decorations = new();

    public WorldMapBackgroundComponent() : base(new WorldMapBackgroundSprite(), GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize)
    {
        InitializePositions();
    }

    #region Initialize

    private void InitializePositions()
    {
        var dto = GetTiledDto();

        Bounds = new(0, 0, dto.RealWidth, dto.RealHeight);

        InitializeLayerDecorationsPositions(dto, dto.Layers.First(x => x.Id == RockLayerId));
        InitializeLayerDecorationsPositions(dto, dto.Layers.First(x => x.Id == BusheLayerId));
        InitializeLayerDecorationsPositions(dto, dto.Layers.First(x => x.Id == TreeLayerId));
    }

    private TiledMapDto GetTiledDto()
    {
        string filename = Path.Combine(GlobalVariablesDto.Content.RootDirectory, SpriteConst.WorldMapBackground + ".json");

        return TiledManagerService.ParseTiledMap(filename);
    }

    private void InitializeLayerDecorationsPositions(TiledMapDto tiledDto, TiledLayerDto layerDto)
    {
        for (var y = 0; y < layerDto.Height; y++)
        for (var x = 0; x < layerDto.Width; x++)
        {
            if (layerDto.Matrix[y, x] == 0)
                continue;

            _decorations.Add(new(GetSpriteById(layerDto.Matrix[y, x]), new Point(x * tiledDto.TileWidth, y * tiledDto.TileHeight)));
        }
    }

    private AnimationClip GetSpriteById(int spriteId)
    {
        if (TestSpriteId(Tree1SpriteId, spriteId))
            return _tree1Sprite;

        if (TestSpriteId(Bushe1SpriteId, spriteId))
            return _bushe1Sprite;

        if (TestSpriteId(Bushe2SpriteId, spriteId))
            return _bushe2Sprite;

        if (TestSpriteId(Bushe3SpriteId, spriteId))
            return _bushe3Sprite;

        if (TestSpriteId(Bushe4SpriteId, spriteId))
            return _bushe4Sprite;

        if (TestSpriteId(Rock1SpriteId, spriteId))
            return new([_rock1Sprite]);

        if (TestSpriteId(Rock2SpriteId, spriteId))
            return new([_rock2Sprite]);

        if (TestSpriteId(Rock3SpriteId, spriteId))
            return new([_rock3Sprite]);

        if (TestSpriteId(Rock4SpriteId, spriteId))
            return new([_rock4Sprite]);

        throw new ArgumentException("Invalid Tiled Sprite ID!");
    }

    private bool TestSpriteId(Vector2 idRange, int candidateId)
    {
        return candidateId >= idRange.X && candidateId <= idRange.Y;
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _tree1Sprite.Update();
        _bushe1Sprite.Update();
        _bushe2Sprite.Update();
        _bushe3Sprite.Update();
        _bushe4Sprite.Update();
    }

    #endregion

    #region Draw

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawTrees(spriteBatch);
    }

    private void DrawTrees(SpriteBatch spriteBatch)
    {
        foreach (var treePosition in _decorations)
            treePosition.Sprite.Draw(
                new Rectangle(treePosition.Point.X, 
                treePosition.Point.Y - treePosition.Sprite.Height, 
                treePosition.Sprite.Width, 
                treePosition.Sprite.Height),
                Color, 
                Rotation, 
                SpriteEffects, 
                spriteBatch, 
                Scale, 
                Offset);
    }

    #endregion
}

public record PositionSpriteRecord(AnimationClip Sprite, Point Point);