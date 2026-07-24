using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Infrastructure.ColorInfra;

public static class ColorHelper
{
    public static Color GetFadeColor(Color color1, Color color2, float progress)
    {
        return new Color(
            (int)MathHelper.Lerp(color1.R, color2.R, progress),
            (int)MathHelper.Lerp(color1.G, color2.G, progress),
            (int)MathHelper.Lerp(color1.B, color2.B, progress),
            (int)MathHelper.Lerp(color1.A, color2.A, progress));
    }
}
