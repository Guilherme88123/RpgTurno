using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Manager;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Stage;

namespace RpgTurno.Screen.Map.World.Player;

public class MapPlayerData
{
    public StageMapNode CurrentStage { get; set; }

    public Vector2 Position { get; set; }
    public CreatureStateType State { get; set; }
    public DirectionType Direction { get; set; } = DirectionType.Right;
    public SpriteEffects DrawEffect { get; set; }

    public bool IsMoving => TargetStage is not null;
    public StageMapNode TargetStage { get; private set; }

    private AnimationManager _animation = new();
    private int _size = 128;

    public MapPlayerData()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorRun);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        _animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        _animation.Add(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
    }

    public void SetCurrentStage(StageMapNode currentStage)
    {
        CurrentStage = currentStage;
        Position = currentStage.Position;
    }

    public void Update()
    {
        _animation.Update(State);
        UpdateDirectionEffect();
    }

    private void UpdateDirectionEffect()
    {
        DrawEffect = Direction == DirectionType.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    }

    public void StartWalking(StageMapNode targetStage)
    {
        State = CreatureStateType.Running;
        TargetStage = targetStage;
    }

    public void StopWalking()
    {
        State = CreatureStateType.Idle;
        TargetStage = null;
    }

    public void Draw()
    {
        var destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, _size, _size);
        _animation.Draw(destinationRectangle, Color.White, 0f, DrawEffect, GlobalVariablesDto.SpriteBatchEntities);
    }
}
