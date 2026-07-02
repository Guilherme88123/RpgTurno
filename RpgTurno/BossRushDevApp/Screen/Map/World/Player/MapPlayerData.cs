using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Manager;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Stage.Node;

namespace RpgTurno.Screen.Map.World.Player;

public class MapPlayerData
{
    public MapNodeData CurrentNode { get; set; }

    public Vector2 Position { get; set; }
    public CreatureStateType State { get; set; }
    public DirectionType Direction { get; set; } = DirectionType.Right;
    public SpriteEffects DrawEffect { get; set; }

    public bool IsMoving => TargetStage is not null;
    public MapNodeData TargetStage { get; private set; }

    public MapPlayerData()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorRun);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);
    }

    public void SetCurrentStage(MapNodeData currentStage)
    {
        CurrentNode = currentStage;
        Position = currentStage.Position;
    }

    public void Update()
    {
        UpdateDirectionEffect();
    }

    private void UpdateDirectionEffect()
    {
        DrawEffect = Direction == DirectionType.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    }

    public void StartWalking(MapNodeData targetStage)
    {
        State = CreatureStateType.Running;
        TargetStage = targetStage;
    }

    public void StopWalking()
    {
        State = CreatureStateType.Idle;
        TargetStage = null;
    }
}
