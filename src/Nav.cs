using System;

public static class Nav
{

    public static class Config
    {
        public static double Duration { get; set; } = .5;
        public static TransitionType Starting { get; set; } = TransitionType.Default;
        public static TransitionType Finished { get; set; } = TransitionType.Default;
    }
}
