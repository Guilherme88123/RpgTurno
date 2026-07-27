namespace Infrastructure.Tiled.Dto;

public class TiledMapDto
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int TileWidth { get; set; }
    public int TileHeight { get; set; }

    public int RealWidth => Width * TileWidth;
    public int RealHeight => Height * TileHeight;

    public bool Infinite { get; set; }

    public int NextLayerId { get; set; }
    public int NextObjectId { get; set; }

    public string Orientation { get; set; }
    public string RenderOrder { get; set; }
    public string Type { get; set; }

    public string Version { get; set; }
    public string TiledVersion { get; set; }

    public List<TiledTilesetDto> Tilesets { get; set; }
    public List<TiledLayerDto> Layers { get; set; }
}
