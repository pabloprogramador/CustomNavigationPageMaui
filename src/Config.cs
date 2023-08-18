using System;

namespace Nav
{
    public static class Config
    {
        public static double Duration { get; set; } = .5;
        public static TransitionType PushType { get; set; } = TransitionType.Default;
        public static InputType PushInputType { get; set; } = InputType.In;
        public static TransitionType PopType { get; set; } = TransitionType.Default;
        public static InputType PopInputType { get; set; } = InputType.Out;

        public static void ResetConfig()
        {
            Duration = .5;
            PushType = TransitionType.Default;
            PushInputType = InputType.In;
            PopType = TransitionType.Default;
            PopInputType = InputType.Out;
        }
    }

}
