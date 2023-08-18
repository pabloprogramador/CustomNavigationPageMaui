using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.Generic;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls;
using Foundation;
using CoreAudioKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

namespace src.Platforms.iOS
{

    public class CustomNavigationPageMauiRender : NavigationRenderer
    {
        private UIView _previousBarView;
        private UIView _previousContentView;

        public override void PushViewController(UIViewController viewController, bool animated)
        {

            _previousBarView = View.Subviews[1].SnapshotView(false);
            _previousContentView = View.Subviews[0].SnapshotView(false);

            if (Nav.Config.Starting == TransitionType.None)
            {
                base.PushViewController(viewController, false);
                return;
            }
            else if (Nav.Config.Starting == TransitionType.Default)
            {
                base.PushViewController(viewController, animated);
                return;
            }
            else if (Nav.Config.Starting == TransitionType.Fade)
            {

                var transition = CATransition.CreateAnimation();
                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionFade;

                View.Layer.AddAnimation(transition, null);
            }
            else if (Nav.Config.Starting == TransitionType.FlipIn)
            {
                FlipInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Starting == TransitionType.FlipOut)
            {
                FlipOutAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Starting == TransitionType.ScaleIn)
            {
                ScaleInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Starting == TransitionType.ScaleOut)
            {
                ScaleOutAnimation(View, Nav.Config.Duration);
            }
            else
            {
                var transition = CATransition.CreateAnimation();

                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionMoveIn;

                switch (Nav.Config.Starting)
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


        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            PopViewController(animated);
            return null;
        }

        public override UIViewController PopViewController(bool animated)
        {
            _previousBarView = View.Subviews[1].SnapshotView(false);
            _previousContentView = View.Subviews[0].SnapshotView(false);

            if (Nav.Config.Finished == TransitionType.None)
            {
                return base.PopViewController(false);
            }
            if (Nav.Config.Finished == TransitionType.Default)
            {
                return base.PopViewController(animated);
            }
            if (Nav.Config.Finished == TransitionType.Fade)
            {
                var transition = CATransition.CreateAnimation();
                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionFade;
                View.Layer.AddAnimation(transition, null);
            }
            else if (Nav.Config.Finished == TransitionType.FlipIn)
            {
                FlipInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Finished == TransitionType.FlipOut)
            {
                FlipOutAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Finished == TransitionType.ScaleIn)
            {
                ScaleInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.Finished == TransitionType.ScaleOut)
            {
                ScaleOutAnimation(View, Nav.Config.Duration);
            }
            else
            {
                var transition = CATransition.CreateAnimation();
                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionPush;

                switch (Nav.Config.Finished)
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



        public void FlipInAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;
            initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, 1.0f, 0.0f);

            //view.Alpha = 0.0f;
            view.Subviews[0].Layer.Transform = initialTransform;
            //view.Subviews[1].Layer.Opacity = 0;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Subviews[0].Layer.AnchorPoint = new CGPoint(.5f, 0.5f);
                    var newTransform = CATransform3D.Identity;
                    newTransform.M34 = m34;
                    view.Subviews[0].Layer.Transform = newTransform;
                    //view.Subviews[1].Layer.Opacity = 1;
                },
                null
            );
        }

        public void FlipOutAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            //view.AddSubview(_previousBarView);
            view.AddSubview(_previousContentView);

            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;
            
            //initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, -1.0f, 0.0f);

            //_previousBarView.Layer.Opacity = 1;
            _previousContentView.Layer.Transform = initialTransform;
            _previousContentView.Layer.ZPosition = 999;
            _previousContentView.Layer.AnchorPoint = new CGPoint(1f, .5f);
            _previousContentView.Layer.Transform = CATransform3D.MakeTranslation(_previousContentView.Bounds.Width / 2, 0, 0);

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    var newTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, 1.0f, 0.0f);
                    //newTransform.Translate((nfloat)_previousContentView.Bounds.Width / 2, 0, 0);
                    newTransform.M34 = m34;
                   // _previousBarView.Layer.Opacity = 0;
                    _previousContentView.Layer.Transform = newTransform;
                    _previousContentView.Layer.Transform = CATransform3D.MakeTranslation(_previousContentView.Bounds.Width / 2, 0, 0);
                },
                () =>
                {
                    //_previousBarView.RemoveFromSuperview();
                    _previousContentView.RemoveFromSuperview();
                }
            );
        }

        private void ScaleInAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            view.Layer.Opacity = 0;
            view.Subviews[0].Transform = CGAffineTransform.MakeScale((nfloat)1.5, (nfloat)1.5);
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

        private void ScaleOutAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            view.AddSubview(_previousBarView);
            view.AddSubview(_previousContentView);
            _previousContentView.Layer.Opacity = 1;
            _previousContentView.Transform = CGAffineTransform.MakeScale(1f, 1f);
            _previousBarView.Layer.Opacity = 1;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    _previousContentView.Transform = CGAffineTransform.MakeScale(1.5f, 1.5f);
                    _previousContentView.Layer.Opacity = 0;
                    _previousBarView.Layer.Opacity = 0;
                },
                () =>
                {
                    _previousContentView.RemoveFromSuperview();
                    _previousBarView.RemoveFromSuperview();
                }
            );
        }

        private void FixToStart(UIView view, double duration = 0.5)
        {
            var transition = CATransition.CreateAnimation();

            transition.Duration = duration;
            transition.Type = CAAnimation.TransitionMoveIn;
            transition.Subtype = CAAnimation.TransitionFromRight;
            transition.StartProgress = (float)0.5;
            transition.EndProgress = (float)0.5;
            view.Layer.AddAnimation(transition, null);
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

    }

}
