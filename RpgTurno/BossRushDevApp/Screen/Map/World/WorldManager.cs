using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Dto.Map;
using Domain.Dto.Map.Node;
using Domain.Dto.Session;
using Domain.Enum;
using Domain.Enum.Stage;
using Domain.Application.Sound.Unit.Footsteps.Walk;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Screen.Map.World.Player;
using System;

namespace RpgTurno.Screen.Map.World;

public class WorldManager
{
    public MapData Map { get; set; }
    public MapPlayerData Player { get; set; }

    public Action<StageCode> OnPlayScreenEntry;

    private const float WalkingSpeed = 500f;

    private Keys _confirmKey1 = Keys.Space;
    private Keys _confirmKey2 = Keys.Enter;
    private Keys _nextKey1 = Keys.D;
    private Keys _nextKey2 = Keys.Right;
    private Keys _previousKey1 = Keys.A;
    private Keys _previousKey2 = Keys.Left;

    private DirtWalkSoundMix _dirtWalkMix = new();

    #region Initialize

    public void Initialize(MapData map)
    {
        Map = map;

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

    private void VerifyEnteringStage()
    {
        if (IsConfirmPressed())
            TryEnterMapNode();
    }

    public void TryEnterMapNode()
    {
        switch (Player.CurrentNode)
        {
            case StageMapNode stageNode:
                GoToPlayStage(stageNode);
                break;
            case StartMapNode:
                break;
        };
    }

    public bool CanPlayerEnterAtStage()
    {
        return Player.CurrentNode is StageMapNode && !Player.IsMoving;
    }

    private void GoToPlayStage(StageMapNode stageNode)
    {
        OnPlayScreenEntry?.Invoke(stageNode.StageCode);
        GlobalVariablesDto.PushScreen(ScreenConst.PlayScreen);
    }

    private void VerifyWalkingBetweenStages()
    {
        if (IsNextPressed())
            TryWalkToNextNode();

        if (IsPreviousPressed())
            TryWalkToPreviousNode();
    }

    public void TryWalkToNextNode()
    {
        TryWalkTo(Player.CurrentNode.GetNextNode());
    }

    public void TryWalkToPreviousNode()
    {
        TryWalkTo(Player.CurrentNode.PreviousNode);
    }

    private void TryWalkTo(MapNodeData targetStage)
    {
        if (IsAbleToWalkTo(targetStage))
            StartWalking(targetStage);
    }

    public bool IsAbleToWalkToNext()
    {
        return IsAbleToWalkTo(Player.CurrentNode.GetNextNode()) && !Player.IsMoving;
    }

    public bool IsAbleToWalkToPrevious()
    {
        return IsAbleToWalkTo(Player.CurrentNode.PreviousNode) && !Player.IsMoving;
    }

    private bool IsAbleToWalkTo(MapNodeData targetNode)
    {
        if (targetNode is null)
            return false;

        if (targetNode is StageMapNode stageNode &&
            !stageNode.Cleared &&
            Player.CurrentNode is StageMapNode currentStageNode &&
            !currentStageNode.Cleared)
            return false;

        return true;
    }

    private void StartWalking(MapNodeData targetStage)
    {
        Player.StartWalking(targetStage);
    }

    private void UpdateWalkingToTargetStage()
    {
        _dirtWalkMix.Update();

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
        _dirtWalkMix.Reset();
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

    #region Stage Cleared

    public void OnStageCleared()
    {
        if (Player.CurrentNode is not StageMapNode stageNode)
            return;

        stageNode.Cleared = true;
    }

    #endregion

    #endregion
}
