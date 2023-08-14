public static class AppBuilderExtensions
{

    public static MauiAppBuilder UseCustomNavigationPageMaui(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers((handlers) =>
         {
#if ANDROID
               // handlers.AddHandler(typeof(NavigationPage), typeof(XamarinCustomRenderer.Droid.Renderers.PressableViewRenderer));
#elif IOS
                handlers.AddHandler(typeof(NavigationPage), typeof(src.Platforms.iOS.CustomNavigationPageMauiRender));
#endif
         });
        return builder;
    }
}