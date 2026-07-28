namespace Infrastructure.Tiled.Dto;

public class TiledGroupLayerDto 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public float Opacity { get; set; }
    public bool Visible { get; set; }

    public string Type { get; set; }

    public List<TiledLayerDto> Layers { get; set; } = new();
}
