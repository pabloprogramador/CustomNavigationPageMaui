//using View = Android.Views.View;

using Android.Content.Res;
using Android.Views;
using AndroidX.Transitions;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace Plugins.CNPM.Platforms.Android
{

    public partial class CustomNavigationPageMauiHandler : Microsoft.Maui.Handlers.NavigationViewHandler
    {
        protected override void ConnectHandler(global::Android.Views.View platformView)
        {

            base.ConnectHandler(platformView);
        }

        

        protected override global::Android.Views.View CreatePlatformView()
        {
            var native = (global::AndroidX.Fragment.App.FragmentContainerView)base.CreatePlatformView();
            //global::AndroidX.Transitions.TransitionManager.BeginDelayedTransition(native);

            // Create custom slide transitions
            var enterTransition = new Slide((int)GravityFlags.End);
            var exitTransition = new Slide((int)GravityFlags.Start);

            TransitionManager.BeginDelayedTransition(native, new TransitionSet().AddTransition(enterTransition).AddTransition(exitTransition));
            //TransitionManager.EndTransitions(native);
            return native;
        }

        //protected override View CreatePlatformView()
        //{



        //    var navigationViw = (View)navigation; 
        //    //return (View)navigation;
        //    return base.CreatePlatformView();
        //}

    }
}
//#nullable enable
//using AndroidX.AppCompat.Widget;
//using Microsoft.Maui.Handlers;
//using Microsoft.Maui.Platform;
//using Plugins.CNPM;

//namespace MyMauiControl.Handlers
//{
//    public partial class CustomEntryHandler : ViewHandler<CustomNavigationPageMaui, NavigationPage>
//    {
//        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

//        protected override void ConnectHandler(AppCompatEditText platformView)
//        {
//            base.ConnectHandler(platformView);

//            // Perform any control setup here
//        }

//        protected override void DisconnectHandler(AppCompatEditText platformView)
//        {
//            // Perform any native view cleanup here
//            platformView.Dispose();
//            base.DisconnectHandler(platformView);
//        }

//        public static void MapText(CustomEntryHandler handler, CustomNavigationPageMaui view)
//        {
//            handler.PlatformView.Text = view.Text;
//            handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
//        }

//        public static void MapTextColor(CustomEntryHandler handler, CustomNavigationPageMaui view)
//        {
//            handler.PlatformView?.SetTextColor(view.TextColor.ToPlatform());
//        }
//    }
//}
//////using PlatformView = AndroidX.Navigation;
////using System;
//////using Android.Content;
////using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;

////using Microsoft.Maui.Controls;
////using Android.App;
////using Android.Graphics.Drawables;
////using Android.Content;
////using System.ComponentModel;

//////using Microsoft.Maui.Platform;
//////using static System.TimeZoneInfo;
//////using System.ComponentModel;
//////using Android.Support.V4.App;
//////using Microsoft.Maui.Controls.Handlers.Compatibility;
//////using AndroidX.Fragment.App;
//////using Android.Animation;
//////using TransitionNavigationPage.Droid.Renderers;
//////using TransitionNavigationPage.Enums;
//////using Xamarin.Forms;
//////using Xamarin.Forms.Platform.Android.AppCompat;


////namespace Plugins.CNPM.Platforms.Android
////{


////    public class CustomNavigationPageMauiRender : NavigationPageRenderer
////    {

////        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
////        {
////            base.OnElementPropertyChanged(sender, e);


////        }

////        public CustomNavigationPageMauiRender(Context context) : base(context)
////        {
////        }


////        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
////        //{
////        //    base.OnElementPropertyChanged(sender, e);
////        //}

////         void S_etupPageTransition(string transaction, bool isPush)
////        // protected override void SetupPageTransition(FragmentTransaction transaction, bool isPush)
////        {
////            //var fadeInAnimation = ObjectAnimator.OfFloat(null, "alpha", 0f, 1f);
////            //fadeInAnimation.SetDuration(1000); // Duração de 1 segundo

////            //var fadeOutAnimation = ObjectAnimator.OfFloat(null, "alpha", 1f, 0f);
////            //fadeOutAnimation.SetDuration(1000); // Duração de 1 segundo

////            //transaction.SetCustomAnimations((int)fadeInAnimation, (int)fadeOutAnimation);

////            return;
////            //var teste = Microsoft.Maui.Resource.Animation.;
////            //switch (_transitionType)
////            //{
////            //    case TransitionType.None:
////            //        return;
////            //    case TransitionType.Default:
////            //        return;
////            //    case TransitionType.Fade:
////            //        transaction.SetCustomAnimations(Resource.Animation.fade_in, Resource.Animation.fade_out,
////            //                                        Resource.Animation.fade_out, Resource.Animation.fade_in);
////            //        break;
////            //    case TransitionType.Flip:
////            //        transaction.SetCustomAnimations(Resource.Animation.flip_in, Resource.Animation.flip_out,
////            //                                        Resource.Animation.flip_out, Resource.Animation.flip_in);
////            //        break;
////            //    case TransitionType.Scale:
////            //        transaction.SetCustomAnimations(Resource.Animation.scale_in, Resource.Animation.scale_out,
////            //                                        Resource.Animation.scale_out, Resource.Animation.scale_in);
////            //        break;
////            //    case TransitionType.SlideFromLeft:
////            //        if (isPush)
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_left, Resource.Animation.exit_right,
////            //                                            Resource.Animation.enter_right, Resource.Animation.exit_left);
////            //        }
////            //        else
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_right, Resource.Animation.exit_left,
////            //                                            Resource.Animation.enter_left, Resource.Animation.exit_right);
////            //        }
////            //        break;
////            //    case TransitionType.SlideFromRight:
////            //        if (isPush)
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_right, Resource.Animation.exit_left,
////            //                                            Resource.Animation.enter_left, Resource.Animation.exit_right);
////            //        }
////            //        else
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_left, Resource.Animation.exit_right,
////            //                                            Resource.Animation.enter_right, Resource.Animation.exit_left);
////            //        }
////            //        break;
////            //    case TransitionType.SlideFromTop:
////            //        if (isPush)
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_top, Resource.Animation.exit_bottom,
////            //                                            Resource.Animation.enter_bottom, Resource.Animation.exit_top);
////            //        }
////            //        else
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_bottom, Resource.Animation.exit_top,
////            //                                            Resource.Animation.enter_top, Resource.Animation.exit_bottom);
////            //        }
////            //        break;
////            //    case TransitionType.SlideFromBottom:
////            //        if (isPush)
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_bottom, Resource.Animation.exit_top,
////            //                                            Resource.Animation.enter_top, Resource.Animation.exit_bottom);
////            //        }
////            //        else
////            //        {
////            //            transaction.SetCustomAnimations(Resource.Animation.enter_top, Resource.Animation.exit_bottom,
////            //                                            Resource.Animation.enter_bottom, Resource.Animation.exit_top);
////            //        }
////            //        break;
////            //    default:
////            //        return;
////        }

////    }
////}