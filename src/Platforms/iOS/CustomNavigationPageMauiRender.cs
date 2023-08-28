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
using Plugins.CNPM;
using System.Runtime.InteropServices;

namespace Plugins.CNPM.Platforms.iOS
{

    public class CustomNavigationPageMauiRender : NavigationRenderer
    {
        private UIView _previousContentView;
        private bool _isPush;

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            _isPush = true;

            PreviousContent();

            if (Config.CustomPush.SelectedForAnimation == ScreenType.CurrentPage)
                CustomCurrentPageAnimation(View, Config.CustomPush);

            if (Config.CustomPush.SelectedForAnimation == ScreenType.NextPage)
                CustomNextPageAnimation(View, Config.CustomPush);

            base.PushViewController(viewController, false);
        }


        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            if (!_isPush)
            {
                _isPush = true;
                return null;
            }
            PopViewController(animated);
            return null;
        }

        public override UIViewController PopViewController(bool animated)
        {
            _isPush = false;
            PreviousContent();

            if (Config.CustomPop.SelectedForAnimation == ScreenType.CurrentPage)
                CustomCurrentPageAnimation(View, Config.CustomPop);

            if (Config.CustomPop.SelectedForAnimation == ScreenType.NextPage)
                CustomNextPageAnimation(View, Config.CustomPop);

            return base.PopViewController(false);
        }

        //TODO Talvez no futuro
        //private void FlipInAnimation(UIView view, double duration = 0.5)
        //{

        //    FixToStart(view, duration);

        //    var m34 = (nfloat)(-1 * 0.001);
        //    var initialTransform = CATransform3D.Identity;
        //    initialTransform.M34 = m34;
        //    initialTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, 1.0f, 0.0f);

        //    view.Subviews[0].Layer.Transform = initialTransform;
        //    UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
        //        () =>
        //        {
        //            view.Subviews[0].Layer.AnchorPoint = new CGPoint(.5f, 0.5f);
        //            var newTransform = CATransform3D.Identity;
        //            newTransform.M34 = m34;
        //            view.Subviews[0].Layer.Transform = newTransform;
        //        },
        //        null
        //    );
        //}

        //private void FlipOutAnimation(UIView view, double duration = 0.5)
        //{

        //    FixToStart(view, duration);

        //    view.AddSubview(_previousContentView);

        //    var m34 = (nfloat)(-1 * 0.001);
        //    var initialTransform = CATransform3D.Identity;
        //    initialTransform.M34 = m34;

        //    view.Subviews[1].Layer.Opacity = 0;
        //    _previousContentView.Layer.Transform = initialTransform;
        //    _previousContentView.Layer.ZPosition = 999;
        //    _previousContentView.Layer.AnchorPoint = new CGPoint(.5f, .5f);

        //    UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
        //        () =>
        //        {
        //            var newTransform = initialTransform.Rotate((nfloat)(1 * Math.PI * 0.5), 0.0f, -1.0f, 0.0f);
        //            newTransform.M34 = m34;
        //            _previousContentView.Layer.Transform = newTransform;
        //            view.Subviews[1].Layer.Opacity = 1;

        //        },
        //        () =>
        //        {
        //            _previousContentView.RemoveFromSuperview();
        //        }
        //    );
        //}

        private void CustomNextPageAnimation(UIView view, CustomConfig config)
        {

            FixToStart(view, config.Duration);

            view.Layer.Opacity = (float)config.OpacityStart;
            view.Subviews[0].Transform = CGAffineTransform.MakeWithComponents(
                new CoreFoundation.CGAffineTransformComponents()
                {
                    Scale = new CGSize((float)config.ScaleStart, (float)config.ScaleStart),
                    Translation = new CGVector(PosX(config.XStart), PosY(config.YStart)),
                    Rotation = PosRotation(config.RotationStart)
                });
            view.Subviews[1].Layer.Opacity = 0;

            UIView.Animate(config.Duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    view.Subviews[0].Transform = CGAffineTransform.MakeWithComponents(
                        new CoreFoundation.CGAffineTransformComponents()
                        {
                            Scale = new CGSize((float)config.ScaleEnd, (float)config.ScaleEnd),
                            Translation = new CGVector(PosX(config.XEnd), PosY(config.YEnd)),
                            Rotation = PosRotation(config.RotationEnd)
                        });
                    view.Layer.Opacity = (float)config.OpacityEnd;
                    view.Subviews[1].Layer.Opacity = 1;
                }, null
            );


        }

        private void CustomCurrentPageAnimation(UIView view, CustomConfig config)
        {

            if (_previousContentView == null) return;

            FixToStart(view, config.Duration);
            view.AddSubview(_previousContentView);
            _previousContentView.Layer.Opacity = (float)config.OpacityStart;
            _previousContentView.Transform = CGAffineTransform.MakeWithComponents(
                new CoreFoundation.CGAffineTransformComponents()
                {
                    Scale = new CGSize((float)config.ScaleStart, (float)config.ScaleStart),
                    Translation = new CGVector(PosX(config.XStart), PosY(config.YStart)),
                    Rotation = PosRotation(config.RotationStart)
                });
            view.Subviews[1].Layer.Opacity = 0;


            UIView.Animate(config.Duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    _previousContentView.Transform = CGAffineTransform.MakeWithComponents(new CoreFoundation.CGAffineTransformComponents()
                    {
                        Scale = new CGSize((float)config.ScaleEnd, (float)config.ScaleEnd),
                        Translation = new CGVector(PosX(config.XEnd), PosY(config.YEnd)),
                        Rotation = PosRotation(config.RotationEnd)
                    });
                    _previousContentView.Layer.Opacity = (float)config.OpacityEnd;
                    view.Subviews[1].Layer.Opacity = 1;
                },
                () =>
                {
                    if (_previousContentView != null)
                    {
                        _previousContentView.Layer.RemoveAllAnimations();
                        _previousContentView.RemoveFromSuperview();
                    }
                    _previousContentView = null;
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

        private void PreviousContent()
        {
            bool hasBar = true;
            var mainPage = Application.Current.MainPage;

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
        }

        private float PosRotation(double value)
        {
            float result = (float)(2 * Math.PI * value);
            return result;
        }

        private float PosX(double value)
        {
            float result = (float)(View.Bounds.Width * value);
            return result;
        }

        private float PosY(double value)
        {
            float result = (float)(View.Bounds.Height * value);
            return result;
        }

    }

}
