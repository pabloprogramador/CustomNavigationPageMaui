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

            if (Config.PushType == TransitionType.None)
            {
                base.PushViewController(viewController, false);
                return;
            }
            else if (Config.PushType == TransitionType.Default)
            {
                base.PushViewController(viewController, animated);
                return;
            }
            else if (Config.PushType == TransitionType.Fade)
            {
                CustomAnimation(View, Config.CustomPush);
            }
            else if (Config.PushType == TransitionType.Flip && Config.PushInputType == InputType.In)
            {
                FlipInAnimation(View, Config.Duration);
            }
            else if (Config.PushType == TransitionType.Flip && Config.PushInputType == InputType.Out)
            {
                FlipOutAnimation(View, Config.Duration);
            }
            else if (Config.PushType == TransitionType.Scale && Config.PushInputType == InputType.In)
            {
                ScaleInAnimation(View, Config.Duration);
            }
            else if (Config.PushType == TransitionType.Scale && Config.PushInputType == InputType.Out)
            {
                ScaleOutAnimation(View, Config.Duration);
            }
            else
            {
                Slide(View, Config.PushType, Config.PushInputType);
            }

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

            if (Config.PopType == TransitionType.None)
            {
                return base.PopViewController(false);
            }
            if (Config.PopType == TransitionType.Default)
            {
                return base.PopViewController(animated);
            }
            if (Config.PopType == TransitionType.Fade)
            {
                //var transition = CATransition.CreateAnimation();
                //transition.Duration = Config.Duration;
                //transition.Type = CAAnimation.TransitionFade;
                //View.Layer.AddAnimation(transition, null);
                CustomAnimation(View, Config.CustomPush);
            }
            else if (Config.PopType == TransitionType.Flip && Config.PopInputType == InputType.In)
            {
                FlipInAnimation(View, Config.Duration);
            }
            else if (Config.PopType == TransitionType.Flip && Config.PopInputType == InputType.Out)
            {
                FlipOutAnimation(View, Config.Duration);
            }
            else if (Config.PopType == TransitionType.Scale && Config.PopInputType == InputType.In)
            {
                ScaleInAnimation(View, Config.Duration);
            }
            else if (Config.PopType == TransitionType.Scale && Config.PopInputType == InputType.Out)
            {
                ScaleOutAnimation(View, Config.Duration);
            }
            else
            {
                Slide(View, Config.PopType, Config.PopInputType);
            }

            return base.PopViewController(false);
        }

        private void Slide(UIView view, TransitionType transition, InputType input)
        {
            if (input == InputType.In)
            {
                switch (transition)
                {
                    case TransitionType.SlideBottom:
                        SlideInAnimation(view, Config.Duration, 0, (float)View.Bounds.Height);
                        break;
                    case TransitionType.SlideLeft:
                        SlideInAnimation(view, Config.Duration, -(float)View.Bounds.Width, 0);
                        break;
                    case TransitionType.SlideRight:
                        SlideInAnimation(view, Config.Duration, (float)View.Bounds.Width, 0);
                        break;
                    case TransitionType.SlideTop:
                        SlideInAnimation(view, Config.Duration, 0, -(float)View.Bounds.Height);
                        break;
                }
            }
            else if (input == InputType.Out)
            {
                switch (transition)
                {
                    case TransitionType.SlideBottom:
                        SlideOutAnimation(view, Config.Duration, 0, (float)View.Bounds.Height);
                        break;
                    case TransitionType.SlideLeft:
                        SlideOutAnimation(view, Config.Duration, -(float)View.Bounds.Width, 0);
                        break;
                    case TransitionType.SlideRight:
                        SlideOutAnimation(view, Config.Duration, (float)View.Bounds.Width, 0);
                        break;
                    case TransitionType.SlideTop:
                        SlideOutAnimation(view, Config.Duration, 0, -(float)View.Bounds.Height);
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

        private void CustomAnimation(UIView view, CustomConfig config)
        {

            if (_previousContentView == null) return;
            System.Diagnostics.Debug.WriteLine(":::::");
            FixToStart(view, config.Duration);
            view.AddSubview(_previousContentView);
            _previousContentView.Layer.Opacity = (float)config.OpacityStart;
            _previousContentView.Transform = CGAffineTransform.MakeWithComponents(
                new CoreFoundation.CGAffineTransformComponents()
                {
                    Scale = new CGSize((float)config.ScaleStart, (float)config.ScaleStart),
                    Translation = new CGVector(PosX(config.XStart), PosX(config.YStart)),
                    Rotation = 0
                });
            view.Subviews[1].Layer.Opacity = 0;


            UIView.Animate(config.Duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () =>
                {
                    _previousContentView.Transform = CGAffineTransform.MakeWithComponents(new CoreFoundation.CGAffineTransformComponents()
                    {
                        Scale = new CGSize((float)config.ScaleEnd, (float)config.ScaleEnd),
                        Translation = new CGVector(PosX(config.XEnd), PosX(config.YEnd)),
                        Rotation = 360
                    });
                    //_previousContentView.Layer.Opacity = 0;
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
