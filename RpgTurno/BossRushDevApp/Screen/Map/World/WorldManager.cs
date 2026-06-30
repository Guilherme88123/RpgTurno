using RpgTurno.Screen.Map.World.Player;
using RpgTurno.Screen.Map.World.Stage;

namespace RpgTurno.Screen.Map.World;

public class WorldManager
{
    public MapData Map { get; set; }
    public MapPlayerData Player { get; set; }

    public void Initialize()
    {
        Map = MapFactory.Create();

        Player = new();
        Player.SetCurrentStage(Map.StartStage);
    }

    public void Update()
    {
        Player.Update();
    }
}
