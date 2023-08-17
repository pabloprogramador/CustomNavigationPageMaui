using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.Generic;
using Microsoft.Maui.Platform;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

namespace src.Platforms.iOS
{
    public class CustomNavigationPageMauiRender : NavigationRenderer
    {

        public override async void PushViewController(UIViewController viewController, bool animated)
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

                var transition = CATransition.CreateAnimation();
                transition.Duration = NavConfig.Duration;
                transition.Type = CAAnimation.TransitionFade;

                View.Layer.AddAnimation(transition, null);
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
                transition.Type = CAAnimation.TransitionMoveIn;

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
            base.PushViewController(viewController, false);
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
                var transition = CATransition.CreateAnimation();
                transition.Duration = NavConfig.Duration;
                transition.Type = CAAnimation.TransitionFade;
                View.Layer.AddAnimation(transition, null);
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

        //private void FadeAnimation(UIView view, double duration = 1.0)
        //{
        //    view.Alpha = 0.0f;
        //    view.Transform = CGAffineTransform.MakeIdentity();

        //    UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseOut,
        //        () =>
        //        {
        //            view.Alpha = 1.0f;
        //        },
        //        null
        //    );
        //}

        public async void FlipAnimation(UIView view, double duration = 0.5)
        {
            //CALayer layer0 = view.Layer.Sublayers[0];
            //CALayer layer1 = view.Layer.Sublayers[1];


            //view.Layer.InsertSublayer(layer1, 0);
            //view.Layer.InsertSublayer(layer0, 1);

            //view.SetNeedsDisplay();
            //view.Superview.Superview.L.Transform = CATransform3D.MakeTranslation((nfloat)0, (nfloat)300, (nfloat)0);
            //view.Superview.Superview.BringSubviewToFront(view);

            //view.Superview.ExchangeSubview(0, 2);
            //view.SetNeedsDisplay();
            //base.NavigationController.View.Hidden = true;

            // var topVC = GetTopViewController();
            //view.BringSubviewToFront(topVC.View);
            // base.WillMoveToParentViewController(view.InputViewController);
            //var topVC = UIApplication.SharedApplication.KeyWindow.RootViewController;
            //var xview = ViewController.View;
            //if (xview != null) xview.Hidden = true;
            //view.Superview.Layer.Sublayers[1].Hidden = true;


            //view.Superview.Superview.Layer.Sublayers[0].Sublayers[0].Sublayers[3].Transform = CATransform3D.MakeTranslation((nfloat)0, (nfloat)300, (nfloat)0); ;
            //view.Superview.Superview.Layer.Transform = CATransform3D.MakeTranslation((nfloat)0, (nfloat)300, (nfloat)0); ;
            //view.Superview.Subviews[0].Transform =
            //_ = view.Superview.Subviews;

            var transition = CATransition.CreateAnimation();
            transition.Duration = NavConfig.Duration;
            transition.Type = CAAnimation.TransitionMoveIn;
            transition.Subtype = CAAnimation.TransitionFromRight;
            transition.StartProgress = (float)0.5;
            transition.EndProgress = (float)0.5;
            View.Layer.AddAnimation(transition, null);

            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;

            initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, -2.0f, 0.0f);
            //initialTransform = initialTransform.Translate(0, 0, 0);
            // view.Alpha = 0.0f;
            // view.Layer.AnchorPoint = new CGPoint(2, 0.5);
            //view.Layer.Transform = initialTransform;
            //view.Layer.Sublayers[0].Transform
            view.BackgroundColor = Colors.Transparent.ToPlatform();
            view.Subviews[0].Transform = CGAffineTransform.MakeTranslation((nfloat)100, (nfloat)0);
            //CATransform3D.MakeTranslation((nfloat)100, (nfloat)0, (nfloat)0);

            //view.ExchangeSubview(0, 1);
            UIView.Animate(NavConfig.Duration, () =>
            {
                // view.AwakeFromNib();

                // view.Layer.Sublayers[0].Sublayers[1].Transform = CATransform3D.MakeTranslation((nfloat)0, (nfloat)0, (nfloat)0);
            });
            //UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
            //    () =>
            //    {
            //        view.Layer.AnchorPoint = new CGPoint((nfloat)0, 0.5f);
            //        var newTransform = CATransform3D.Identity;
            //        newTransform.M34 = m34;
            //        view.Layer.Transform = newTransform;
            //       // view.Alpha = 1.0f;
            //    },
            //    null
            //);

        }

        private void FixToStart(UIView view, double duration = 0.5)
        {
            var transition = CATransition.CreateAnimation();

            transition.Duration = duration;
            transition.Type = CAAnimation.TransitionMoveIn;
            transition.Subtype = CAAnimation.TransitionFromRight;
            transition.StartProgress = (float)0.5;
            transition.EndProgress = (float)0.5;
            View.Layer.AddAnimation(transition, null);
            view.BackgroundColor = Colors.Transparent.ToPlatform();
            view.ClipsToBounds = false;

            var fixAnimation = CABasicAnimation.FromKeyPath("transform.translation.x");
            fixAnimation.From = Foundation.NSNumber.FromDouble(-view.Bounds.Width / 2);
            fixAnimation.To = Foundation.NSNumber.FromDouble(-view.Bounds.Width / 2);
            fixAnimation.Duration = duration;
            fixAnimation.AnimationStopped += (sender, e) =>
            {
                view.Layer.Transform = CATransform3D.MakeTranslation(0, 0, 0);
            };
            view.Layer.AddAnimation(fixAnimation, null);
        }

        private void ScaleAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            view.Layer.Opacity = 0;
            view.Subviews[0].Transform = CGAffineTransform.MakeScale((nfloat).5, (nfloat).5);
            view.Subviews[1].Layer.Opacity = 0;

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Subviews[0].Transform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);
                    view.Subviews[1].Layer.Opacity = 1;
                    view.Layer.Opacity = 1;
                },
                null
            );
        }


    }
}
