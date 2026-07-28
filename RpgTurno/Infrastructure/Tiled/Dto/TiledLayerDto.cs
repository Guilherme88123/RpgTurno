namespace Infrastructure.Tiled.Dto;

public class TiledLayerDto 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<int> Data { get; set; }
    public int[,] Matrix => GetMatrixData();

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public float Opacity { get; set; }
    public bool Visible { get; set; }

    public string Type { get; set; }

    public List<TiledLayerCustomPropertyDto> Properties { get; set; } = new();

    private int[,] GetMatrixData()
    {
        var matrix = new int[Height, Width];

        if (Data is null || Data.Count == 0)
            return matrix;

        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            matrix[y, x] = Data[y * Width + x];

        return matrix;
    }
}
