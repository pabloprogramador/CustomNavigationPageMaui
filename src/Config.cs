using System;

namespace Plugins.CNPM
{
    public static class Config
    {
        private static CustomConfig _default = new CustomConfig { };
        
        public static void ResetConfig()
        {
            CustomPush = _default;
            CustomPop = _default;
        }

        public static CustomConfig CustomPush { get; set; } = _default;
        public static CustomConfig CustomPop { get; set; } = _default;

    }

}
