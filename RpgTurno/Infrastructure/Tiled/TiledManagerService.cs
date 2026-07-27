using Infrastructure.Tiled.Dto;
using System.Text.Json;

namespace Infrastructure.Tiled;

public static class TiledManagerService
{
    public static TiledMapDto ParseTiledMap(string filename)
    {
        var content = File.ReadAllText(filename);

        return ParseTiledMapByContent(content);
    }

    private static TiledMapDto ParseTiledMapByContent(string content)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("Tiled JSON content must be non null!");

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<TiledMapDto>(content, options);
    }
}
