namespace Plugins.CNPM;
public static class AppBuilderExtensions
{

    public static MauiAppBuilder UseCustomNavigationPageMaui(this MauiAppBuilder builder)
    {
        
        builder.ConfigureMauiHandlers((handlers) =>
         {
#if ANDROID
                handlers.AddHandler(typeof(CustomNavigationPageMaui), typeof(Platforms.Android.CustomNavigationPageMauiHandler));
             
#elif IOS
                handlers.AddHandler(typeof(CustomNavigationPageMaui), typeof(Platforms.iOS.CustomNavigationPageMauiRender));
#endif
         });
        return builder;
    }
}