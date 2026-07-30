using Domain.Enum.Language;

namespace Domain.Dto.Global;

public static class GlobalOptionsDto
{
    public static int MusicVolume { get; set; } = 60;
    public static int SfxVolume { get; set; } = 80;

    public static bool Fullscreen { get; set; } = false;
    public static bool ShowFps { get; set; } = false;

    public const int WidthSize = 1920;
    public const int HeightSize = 1080;

    public static int RealWidthSize { get; set; } = 1280;
    public static int RealHeightSize { get; set; } = 720;

    public static LanguageType Language { get; set; } = LanguageType.English;

    public static float MusicVolumeFloat => VolumeToFloat(MusicVolume);
    public static float SfxVolumeFloat => VolumeToFloat(SfxVolume);

    private static float VolumeToFloat(int slider)
    {
        float t = Math.Clamp(slider / 100f, 0f, 1f);
        return MathF.Pow(t, 2.2f);
    }
}
