using System;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace Plugins.CNPM.Platforms.iOS
{
    public class CustomShellItemTransition : IShellItemTransition
    {
        public Task Transition(IShellItemRenderer oldRenderer, IShellItemRenderer newRenderer)
        {
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            var oldView = oldRenderer.ViewController.View;
            var newView = newRenderer.ViewController.View;

            oldView.Layer.RemoveAllAnimations();
            newView.Alpha = 0f;

            oldView.Superview.InsertSubviewAbove(newView, oldView);

            UIView.Animate(2.5, 0, UIViewAnimationOptions.BeginFromCurrentState, () => newView.Alpha = 1, () =>
            {
                task.TrySetResult(true);
            });

            return task.Task;
        }
    }
}

