using Domain.Enum;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Terrain.Clouds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Custom.Component.Menu.Background;

public class MenuBackgroundComponent : BaseComponent
{
    private readonly List<SpriteData> _spritesBase = new()
    {
        new Cloud1Sprite(),
        new Cloud2Sprite(),
        new Cloud3Sprite(),
        new Cloud4Sprite(),
        new Cloud5Sprite(),
        new Cloud6Sprite(),
        new Cloud7Sprite(),
        new Cloud8Sprite(),
    };
    private Queue<SpriteData> _lastSprites = new();
    private List<SpriteData> _validBaseSprites => _spritesBase.Where(x => !_lastSprites.Contains(x)).ToList();

    private readonly List<DirectionType> _directions = new()
    {
        //DirectionType.Left,
        DirectionType.Right,
    };

    private readonly List<float> _spritesSpeeds = new()
    {
        50f, 45f, 40f,
    };

    private readonly List<int> _spritesYPositions = new()
    {
        0, 160, 320, 480, 640, 800
    };
    private Queue<int> _lastYPositions = new();
    private List<int> _validSpritesYPositions => _spritesYPositions.Where(x => !_lastYPositions.Contains(x)).ToList();

    private readonly List<int> _spritesXPositions = new()
    {
        0, 160, 320, 480, 640, 800, 960, 1120, 1280
    };
    private Queue<int> _lastXPositions = new();
    private List<int> _validSpritesXPositions => _spritesXPositions.Where(x => !_lastXPositions.Contains(x)).ToList();

    private const int MaxSpritesCount = 23;
    private const int InitialSpritesCount = 17;

    private readonly List<MovingSpriteComponent> _movingSprites = new();

    private const float SpriteSpawnDelay = 2.3f;
    private float _currentDelay;

    private bool CanSpawnNext => _currentDelay <= 0 && _movingSprites.Count < MaxSpritesCount;

    public MenuBackgroundComponent()
    {
        while (_movingSprites.Count < InitialSpritesCount)
            SpawnSprite(withInitialXPosition: true);
    }

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateDelay(gameTime);

        if (CanSpawnNext)
        {
            ResetDelay();
            SpawnSprite();
        }

        UpdateMovingSprites(gameTime);
    }

    private void UpdateDelay(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _currentDelay = Math.Max(0, _currentDelay - deltaTime);
    }

    private void ResetDelay()
    {
        _currentDelay = SpriteSpawnDelay;
    }

    private void SpawnSprite(bool withInitialXPosition = false)
    {
        var sprite = _validBaseSprites.Shuffle().First();

        _lastSprites.Enqueue(sprite);
        if (_lastSprites.Count >= _spritesBase.Count - 3)
            _lastSprites.Dequeue();

        var positionX = -sprite.Width;
        if (withInitialXPosition)
        {
            positionX = _validSpritesXPositions.Shuffle().First();

            _lastXPositions.Enqueue(positionX);
            if (_lastXPositions.Count >= _spritesXPositions.Count - 3)
                _lastXPositions.Dequeue();
        }

        var positionY = _validSpritesYPositions.Shuffle().First();

        _lastYPositions.Enqueue(positionY);
        if (_lastYPositions.Count >= _spritesYPositions.Count - 3)
            _lastYPositions.Dequeue();

        var newMovingSprite = new MovingSpriteComponent(
            sprite,
            _spritesSpeeds.Shuffle().First(),
            _directions.Shuffle().First(),
            positionX,
            positionY);

        _movingSprites.Add(newMovingSprite);
    }

    private void UpdateMovingSprites(GameTime gameTime)
    {
        _movingSprites.ForEach(x => x.Update(gameTime));
        _movingSprites.RemoveAll(x => x.IsDestroyed);
    }

    #endregion

    #region Draw

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawMovingSprites(spriteBatch);
    }

    private void DrawMovingSprites(SpriteBatch spriteBatch)
    {
        _movingSprites.ForEach(x => x.Draw(spriteBatch));
    }

    #endregion
}
