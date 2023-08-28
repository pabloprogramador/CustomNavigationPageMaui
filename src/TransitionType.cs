using System;
namespace Plugins.CNPM
{
    public enum TransitionType
    {
        /// <summary>
        /// Do not animate the transition.
        /// </summary>
        None = -1,

        /// <summary>
        /// Let the OS decide how to animate the transition.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Show a fade transition animation.
        /// </summary>
        Fade = 1,

        /// <summary>
        /// Show a flip transition animation.
        /// </summary>
        Flip = 2,

        /// <summary>
        /// Show a scale transition animation.
        /// </summary>
        Scale = 4,

        /// <summary>
        /// Show a slide left transition animation.
        /// </summary>
        SlideLeft = 6,

        /// <summary>
        /// Show a slide right transition animation.
        /// </summary>
        SlideRight = 7,

        /// <summary>
        /// Show a slide top transition animation.
        /// </summary>
        SlideTop = 8,

        /// <summary>
        /// Show a slide bottom transition animation.
        /// </summary>
        SlideBottom = 9
    }
}