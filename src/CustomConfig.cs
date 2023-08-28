using System;
namespace Plugins.CNPM
{
	public class CustomConfig
	{
		public double OpacityStart { get; set; } = 1;
		public double OpacityEnd { get; set; } = 1;

		public double XStart { get; set; } = 0;
		public double XEnd { get; set; } = 0;

		public double YStart { get; set; } = 0;
		public double YEnd { get; set; } = 0;

		public double ScaleStart { get; set; } = 1;
        public double ScaleEnd { get; set; } = 1;

		public double Duration { get; set; } = .5;

        public double RotationStart { get; set; } = 0;
        public double RotationEnd { get; set; } = 0;

        public ScreenType ScreenSelected { get; set; } = ScreenType.Next;
    }
}