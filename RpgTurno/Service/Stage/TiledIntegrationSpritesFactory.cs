using Domain.Const.Tiled;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Terrain.Bushes;
using Domain.Model.Texture.Sprite.Custom.Terrain.Rocks;
using Domain.Model.Texture.Sprite.Custom.Terrain.Trees;

namespace Service.Stage;

public static class TiledIntegrationSpritesFactory
{
    public static AnimationClip GetSprite(string tiledIntegrationCode)
    {
        return tiledIntegrationCode switch
        {
            TiledIntegrationCodesConst.Tree1 => new Tree1Sprite(),
            TiledIntegrationCodesConst.Bushe1 => new Bushe1Sprite(),
            TiledIntegrationCodesConst.Bushe2 => new Bushe2Sprite(),
            TiledIntegrationCodesConst.Bushe3 => new Bushe3Sprite(),
            TiledIntegrationCodesConst.Bushe4 => new Bushe4Sprite(),
            TiledIntegrationCodesConst.Rock1 => new(new Rock1Sprite()),
            TiledIntegrationCodesConst.Rock2 => new(new Rock2Sprite()),
            TiledIntegrationCodesConst.Rock3 => new(new Rock3Sprite()),
            TiledIntegrationCodesConst.Rock4 => new(new Rock4Sprite()),

            TiledIntegrationCodesConst.Terrain => throw new ArgumentException("Terrain layer has not sprite!"),
            _ => throw new ArgumentException("Invalid tiled integration code!"),
        };
    }
}
