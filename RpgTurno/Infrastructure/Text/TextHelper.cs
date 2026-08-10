using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.Text;

public static class TextHelper
{
    public static string WrapText(this string text, float maxWidth, SpriteFont font)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        var words = text.Split(' ');
        var lines = new List<string>();

        var currentLine = string.Empty;

        foreach (var word in words)
        {
            var testLine = string.IsNullOrEmpty(currentLine)
                ? word
                : $"{currentLine} {word}";

            var width = font.MeasureString(testLine).X;

            if (width <= maxWidth)
            {
                currentLine = testLine;
                continue;
            }

            if (!string.IsNullOrEmpty(currentLine))
                lines.Add(currentLine);

            currentLine = word;
        }

        if (!string.IsNullOrEmpty(currentLine))
            lines.Add(currentLine);

        return string.Join("\n", lines);
    }
}
