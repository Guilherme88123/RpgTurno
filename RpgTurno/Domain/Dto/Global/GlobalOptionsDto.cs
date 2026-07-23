namespace Domain.Dto.Global;

public static class GlobalOptionsDto
{
    public static int MusicVolume { get; set; } = 80;
    public static int SfxVolume { get; set; } = 80;

    public static bool Fullscreen { get; set; } = true;
    public static bool ShowFps { get; set; } = false;

    public static int WidthSize { get; set; } = 1920;
    public static int HeightSize { get; set; } = 1080;

    public static float MusicVolumeFloat => VolumeToFloat(MusicVolume);
    public static float SfxVolumeFloat => VolumeToFloat(SfxVolume);

    private static float VolumeToFloat(int slider)
    {
        float t = Math.Clamp(slider / 100f, 0f, 1f);
        return MathF.Pow(t, 2.2f);
    }
}
