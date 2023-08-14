using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Microsoft.Maui.Controls.Handlers.Compatibility;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

namespace src.Platforms.iOS
{
    public class CustomNavigationPageMauiRender : NavigationRenderer
    {

        public override void PushViewController(UIViewController viewController, bool animated)
        {
           // base.BeginAppearanceTransition(false, false);
           
            
            if (NavConfig.Starting == TransitionType.None)
            {
                base.PushViewController(viewController, false);
                return;
            }
            else if (NavConfig.Starting == TransitionType.Default)
            {
                base.PushViewController(viewController, animated);
                return;
            }
            else if (NavConfig.Starting == TransitionType.Fade)
            {
                FadeAnimation(View, NavConfig.Duration);
            }
            else if (NavConfig.Starting == TransitionType.Flip)
            {
                FlipAnimation(View, NavConfig.Duration);
            }
            else if (NavConfig.Starting == TransitionType.Scale)
            {
                ScaleAnimation(View, NavConfig.Duration);
            }
            else
            {
                var transition = CATransition.CreateAnimation();
                transition.Duration = NavConfig.Duration;
                transition.Type = CAAnimation.TransitionPush;

                switch (NavConfig.Starting)
                {
                    case TransitionType.SlideFromBottom:
                        transition.Subtype = CAAnimation.TransitionFromBottom;
                        break;
                    case TransitionType.SlideFromLeft:
                        transition.Subtype = CAAnimation.TransitionFromLeft;
                        break;
                    case TransitionType.SlideFromRight:
                        transition.Subtype = CAAnimation.TransitionFromRight;
                        break;
                    case TransitionType.SlideFromTop:
                        transition.Subtype = CAAnimation.TransitionFromTop;
                        break;
                }

                View.Layer.AddAnimation(transition, null);
            }

            base.PushViewController(viewController, true);
        }

        public override UIViewController PopViewController(bool animated)
        {
            if (NavConfig.Finished == TransitionType.None)
            {
                return base.PopViewController(false);
            }
            if (NavConfig.Finished == TransitionType.Default)
            {
                return base.PopViewController(animated);
            }
            if (NavConfig.Finished == TransitionType.Fade)
            {
                FadeAnimation(View, NavConfig.Duration);
            }
            else if (NavConfig.Finished == TransitionType.Flip)
            {
                FlipAnimation(View, NavConfig.Duration);
            }
            else if (NavConfig.Finished == TransitionType.Scale)
            {
                ScaleAnimation(View, NavConfig.Duration);
            }
            else
            {
                var transition = CATransition.CreateAnimation();
                transition.Duration = NavConfig.Duration;
                transition.Type = CAAnimation.TransitionPush;

                switch (NavConfig.Finished)
                {
                    case TransitionType.SlideFromBottom:
                        transition.Subtype = CAAnimation.TransitionFromBottom;
                        break;
                    case TransitionType.SlideFromLeft:
                        transition.Subtype = CAAnimation.TransitionFromLeft;
                        break;
                    case TransitionType.SlideFromRight:
                        transition.Subtype = CAAnimation.TransitionFromRight;
                        break;
                    case TransitionType.SlideFromTop:
                        transition.Subtype = CAAnimation.TransitionFromTop;
                        break;
                }

                View.Layer.AddAnimation(transition, null);
            }

            return base.PopViewController(false);
        }

        private void FadeAnimation(UIView view, double duration = 1.0)
        {
            view.Alpha = 0.0f;
            view.Transform = CGAffineTransform.MakeIdentity();

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = 1.0f;
                },
                null
            );
        }

        public void FlipAnimation(UIView view, double duration = 0.5)
        {
            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;
            initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, 1.0f, 0.0f);

            view.Alpha = 0.0f;
            view.Layer.Transform = initialTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Layer.AnchorPoint = new CGPoint((nfloat)0.5, 0.5f);
                    var newTransform = CATransform3D.Identity;
                    newTransform.M34 = m34;
                    view.Layer.Transform = newTransform;
                    view.Alpha = 1.0f;
                },
                null
            );
        }

        private void ScaleAnimation(UIView view, double duration = 0.5)
        {
            view.Alpha = 0.0f;
            view.Transform = CGAffineTransform.MakeScale((nfloat)0.5, (nfloat)0.5);

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Alpha = 1.0f;
                    view.Transform = CGAffineTransform.MakeScale((nfloat)1.0, (nfloat)1.0);
                },
                null
            );
        }
    }
}
