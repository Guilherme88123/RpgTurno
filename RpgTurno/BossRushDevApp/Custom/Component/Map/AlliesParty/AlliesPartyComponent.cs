using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Player;
using SharpDX.MediaFoundation;
using System.Collections.Generic;

namespace RpgTurno.Custom.CustomComponents.Map.AlliesParty;

public class AlliesPartyComponent : BaseComponent
{
    private const int PartySpacing = 40;

    private List<BaseUnitEntity> _alliesParty = new();
    private int _positionX;
    private int _positionY;

    public void SetAlliesParty(List<BaseUnitEntity> alliesParty)
    {
        _alliesParty = alliesParty;
    }

    public void SetPositionByPlayer(MapPlayerData playerData, bool isInBattle)
    {
        SetPosition((int)playerData.Position.X, (int)playerData.Position.Y);
        SetAlliesPartyPosition((int)playerData.Position.X, (int)playerData.Position.Y);

        if (!isInBattle)
            SetAlliesState(playerData);
    }

    private void SetAlliesPartyPosition(int positionX, int positionY)
    {
        _positionX = positionX;
        _positionY = positionY;
    }

    private void SetAlliesState(MapPlayerData playerData)
    {
        _alliesParty.ForEach(x => x.CreatureState = playerData.State);
        _alliesParty.ForEach(x => x.Direction = playerData.Direction);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        UpdateAlliesParty();
    }

    private void UpdateAlliesParty()
    {
        _alliesParty.ForEach(x => x.Update());
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawAlliesParty();
    }

    private void DrawAlliesParty()
    {
        int offsetX = 0;

        foreach (var ally in _alliesParty)
        {
            ally.DrawMap(_positionX + offsetX, _positionY);

            offsetX += PartySpacing;
        }
    }
}
