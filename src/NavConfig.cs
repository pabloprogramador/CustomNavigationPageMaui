using System;

public static class NavConfig
{
    public static double Duration { get; set; } = .5;
    public static TransitionType Starting { get; set; } = TransitionType.Fade;
    public static TransitionType Finished { get; set; } = TransitionType.Fade;
}