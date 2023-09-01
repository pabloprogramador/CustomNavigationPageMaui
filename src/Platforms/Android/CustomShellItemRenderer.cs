using System;
using AndroidX.Fragment.App;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui;

namespace Plugins.CNPM.Platforms.Android
{
	public class CustomShellItemRenderer : ShellItemRenderer
	{
        public CustomShellItemRenderer(IShellContext context) : base(context)
        {
        }

        protected override void SetupAnimation(ShellNavigationSource navSource, FragmentTransaction t, Page page)
        {
            switch (navSource)
            {
                case ShellNavigationSource.Push:
                    t.SetCustomAnimations(
                        Microsoft.Maui.Controls.Resource.Animation.enterfromleft,
                        Microsoft.Maui.Controls.Resource.Animation.exittoleft
                        );
                    break;

                case ShellNavigationSource.Pop:
                case ShellNavigationSource.PopToRoot:
                    t.SetCustomAnimations(Microsoft.Maui.Controls.Resource.Animation.enterfromleft,
                        Microsoft.Maui.Controls.Resource.Animation.exittoleft);
                    break;

                case ShellNavigationSource.ShellSectionChanged:
                    break;
            }
            
        }
    }
}

