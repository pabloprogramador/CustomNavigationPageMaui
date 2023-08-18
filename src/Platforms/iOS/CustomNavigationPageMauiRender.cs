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
        private UIView _previousContentView;

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            var mainPage = Application.Current.MainPage;

            bool hasBar = true;

            if (mainPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage is ContentPage contentPage)
                {
                    hasBar = NavigationPage.GetHasNavigationBar(contentPage);
                }
            }

            if (hasBar)
            {
                _previousContentView = View.Subviews[0].SnapshotView(false);
            }
            else
            {
                _previousContentView = View.SnapshotView(false);
            }

            if (Nav.Config.PushType == Nav.TransitionType.None)
            {
                base.PushViewController(viewController, false);
                return;
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Default)
            {
                base.PushViewController(viewController, animated);
                return;
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Fade)
            {

                var transition = CATransition.CreateAnimation();
                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionFade;

                View.Layer.AddAnimation(transition, null);
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Flip && Nav.Config.PushInputType == Nav.InputType.In)
            {
                FlipInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Flip && Nav.Config.PushInputType == Nav.InputType.Out)
            {
                FlipOutAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Scale && Nav.Config.PushInputType == Nav.InputType.In)
            {
                ScaleInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PushType == Nav.TransitionType.Scale && Nav.Config.PushInputType == Nav.InputType.Out)
            {
                ScaleOutAnimation(View, Nav.Config.Duration);
            }
            else
            {
                Slide(View, Nav.Config.PushType, Nav.Config.PushInputType);
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
            _previousContentView = View.Subviews[0].SnapshotView(false);

            if (Nav.Config.PopType == Nav.TransitionType.None)
            {
                return base.PopViewController(false);
            }
            if (Nav.Config.PopType == Nav.TransitionType.Default)
            {
                return base.PopViewController(animated);
            }
            if (Nav.Config.PopType == Nav.TransitionType.Fade)
            {
                var transition = CATransition.CreateAnimation();
                transition.Duration = Nav.Config.Duration;
                transition.Type = CAAnimation.TransitionFade;
                View.Layer.AddAnimation(transition, null);
            }
            else if (Nav.Config.PopType == Nav.TransitionType.Flip && Nav.Config.PopInputType == Nav.InputType.In)
            {
                FlipInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PopType == Nav.TransitionType.Flip && Nav.Config.PopInputType == Nav.InputType.Out)
            {
                FlipOutAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PopType == Nav.TransitionType.Scale && Nav.Config.PopInputType == Nav.InputType.In)
            {
                ScaleInAnimation(View, Nav.Config.Duration);
            }
            else if (Nav.Config.PopType == Nav.TransitionType.Scale && Nav.Config.PopInputType == Nav.InputType.Out)
            {
                ScaleOutAnimation(View, Nav.Config.Duration);
            }
            else
            {
                Slide(View, Nav.Config.PopType, Nav.Config.PopInputType);
            }

            return base.PopViewController(false);
        }

        private void Slide(UIView view, Nav.TransitionType transition, Nav.InputType input)
        {
            if (input == Nav.InputType.In)
            {
                switch (transition)
                {
                    case Nav.TransitionType.SlideBottom:
                        SlideInAnimation(view, Nav.Config.Duration, 0, (float)View.Bounds.Height);
                        break;
                    case Nav.TransitionType.SlideLeft:
                        SlideInAnimation(view, Nav.Config.Duration, -(float)View.Bounds.Width, 0);
                        break;
                    case Nav.TransitionType.SlideRight:
                        SlideInAnimation(view, Nav.Config.Duration, (float)View.Bounds.Width, 0);
                        break;
                    case Nav.TransitionType.SlideTop:
                        SlideInAnimation(view, Nav.Config.Duration, 0, -(float)View.Bounds.Height);
                        break;
                }
            }
            else if (input == Nav.InputType.Out)
            {
                switch (transition)
                {
                    case Nav.TransitionType.SlideBottom:
                        SlideOutAnimation(view, Nav.Config.Duration, 0, (float)View.Bounds.Height);
                        break;
                    case Nav.TransitionType.SlideLeft:
                        SlideOutAnimation(view, Nav.Config.Duration, -(float)View.Bounds.Width, 0);
                        break;
                    case Nav.TransitionType.SlideRight:
                        SlideOutAnimation(view, Nav.Config.Duration, (float)View.Bounds.Width, 0);
                        break;
                    case Nav.TransitionType.SlideTop:
                        SlideOutAnimation(view, Nav.Config.Duration, 0, -(float)View.Bounds.Height);
                        break;
                }
            }

        }

        private void SlideInAnimation(UIView view, double duration = 0.5, float pX = 0, float pY = 0)
        {
            FixToStart(view, duration);

            view.Subviews[0].Transform = CGAffineTransform.MakeTranslation(pX, pY);
            view.Subviews[1].Layer.Opacity = 0;

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Subviews[0].Transform = CGAffineTransform.MakeTranslation(0, 0);
                    view.Subviews[1].Layer.Opacity = 1;
                },
                null
            );
        }

        private void SlideOutAnimation(UIView view, double duration = 0.5, float pX = 0, float pY = 0)
        {
            FixToStart(view, duration);

            view.AddSubview(_previousContentView);

            _previousContentView.Transform = CGAffineTransform.MakeTranslation(0, 0);
            view.Subviews[1].Layer.Opacity = 0;

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    _previousContentView.Transform = CGAffineTransform.MakeTranslation(pX, pY);
                    view.Subviews[1].Layer.Opacity = 1;
                },
                () =>
                {
                    _previousContentView.RemoveFromSuperview();
                }
            );
        }

        private void FlipInAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;
            initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, 1.0f, 0.0f);

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

        private void FlipOutAnimation(UIView view, double duration = 0.5)
        {

            FixToStart(view, duration);

            view.AddSubview(_previousContentView);

            var m34 = (nfloat)(-1 * 0.001);
            var initialTransform = CATransform3D.Identity;
            initialTransform.M34 = m34;

            view.Subviews[1].Layer.Opacity = 0;
            _previousContentView.Layer.Transform = initialTransform;
            _previousContentView.Layer.ZPosition = 999;
            _previousContentView.Layer.AnchorPoint = new CGPoint(.5f, .5f);

            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    var newTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, -1.0f, 0.0f);
                    newTransform.M34 = m34;
                    _previousContentView.Layer.Transform = newTransform;
                    view.Subviews[1].Layer.Opacity = 1;

                },
                () =>
                {
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

            view.AddSubview(_previousContentView);
            _previousContentView.Layer.Opacity = 1;
            _previousContentView.Transform = CGAffineTransform.MakeScale(1f, 1f);
            view.Subviews[1].Layer.Opacity = 0;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    _previousContentView.Transform = CGAffineTransform.MakeScale(1.5f, 1.5f);
                    _previousContentView.Layer.Opacity = 0;
                    view.Subviews[1].Layer.Opacity = 1;
                },
                () =>
                {
                    _previousContentView.RemoveFromSuperview();
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
