using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Screen.Map.World.Player;
using RpgTurno.Screen.Map.World.Stage;
using System.Linq;
using System.Runtime.Serialization;

namespace RpgTurno.Screen.Map.World;

public class WorldManager
{
    public MapData Map { get; set; }
    public MapPlayerData Player { get; set; }

    private const float WalkingSpeed = 500f;

    private Keys _confirmKey1 = Keys.Space;
    private Keys _confirmKey2 = Keys.Enter;
    private Keys _nextKey1 = Keys.D;
    private Keys _nextKey2 = Keys.Right;
    private Keys _previousKey1 = Keys.A;
    private Keys _previousKey2 = Keys.Left;

    #region Initialize

    public void Initialize()
    {
        Map = MapFactory.Create();

        Player = new();
        Player.SetCurrentStage(Map.StartStage);
    }

    #endregion

    #region Update

    public void Update()
    {
        Player.Update();
        UpdateMovement();
        UpdateInputs();
    }

    #region Player Movement

    private void UpdateInputs()
    {
        if (Player.IsMoving)
            return;

        VerifyWalkingBetweenStages();
        VerifyEnteringStage();
    }

    private void UpdateMovement()
    {
        if (!Player.IsMoving)
            return;

        UpdateWalkingToTargetStage();
    }

    public void VerifyEnteringStage()
    {
        var teclado = Keyboard.GetState();

        if (IsConfirmPressed())
            GlobalVariablesDto.PushScreen(ScreenConst.PlayScreen);
    }

    public void VerifyWalkingBetweenStages()
    {
        var teclado = GlobalVariablesDto.KeyboardState;

        if (IsNextPressed())
            TryWalkTo(Player.CurrentStage.GetNextNode());

        if (IsPreviousPressed())
            TryWalkTo(Player.CurrentStage.PreviousNode);
    }

    private void TryWalkTo(StageMapNode targetStage)
    {
        if (IsAbleToWalkTo(targetStage))
            StartWalking(targetStage);
    }

    private bool IsAbleToWalkTo(StageMapNode targetStage)
    {
        if (targetStage is null)
            return false;

        //TODO: Comentado pois lógica de desbloqueio de fase ainda não foi implementada
        //if (!targetStage.Cleared && !Player.CurrentStage.Cleared)
        //    return false;

        return true;
    }

    private void StartWalking(StageMapNode targetStage)
    {
        Player.StartWalking(targetStage);
    }

    private void UpdateWalkingToTargetStage()
    {
        var direction = Player.TargetStage.Position - Player.Position;

        Player.Direction =
            direction.X > 0
            ? DirectionType.Right
            : DirectionType.Left;

        var distance = direction.Length();

        var nextStep = WalkingSpeed * GlobalVariablesDto.DeltaTime;

        if (distance <= nextStep)
        {
            FinishWalking();
            return;
        }

        direction.Normalize();
        Player.Position += direction * nextStep;
    }

    private void FinishWalking()
    {
        Player.SetCurrentStage(Player.TargetStage);
        Player.StopWalking();
    }

    #endregion

    #region Inputs 

    private bool IsNextPressed()
    {
        var teclado = GlobalVariablesDto.KeyboardState;
        return teclado.IsKeyDown(_nextKey1) || teclado.IsKeyDown(_nextKey2);
    }

    private bool IsPreviousPressed()
    {
        var teclado = GlobalVariablesDto.KeyboardState;
        return teclado.IsKeyDown(_previousKey1) || teclado.IsKeyDown(_previousKey2);
    }

    private bool IsConfirmPressed()
    {
        var teclado = GlobalVariablesDto.KeyboardState;
        return teclado.IsKeyDown(_confirmKey1) || teclado.IsKeyDown(_confirmKey2);
    }

    #endregion

    #endregion
}
