using Domain.Model.Components.Base;
using Domain.Model.Entity.Units.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Player;
using System.Collections.Generic;

namespace RpgTurno.CustomComponents.Map.AlliesParty;

public class AlliesPartyComponent : BaseComponent
{
    private List<BaseUnitEntity> _alliesParty = new();

    public void SetAlliesParty(List<BaseUnitEntity> alliesParty)
    {
        _alliesParty = alliesParty;
    }

    public void SetPositionByPlayer(MapPlayerData playerData)
    {
        SetPosition((int)playerData.Position.X, (int)playerData.Position.Y);
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
        _alliesParty.ForEach(x => x.DrawMap());
    }
}
