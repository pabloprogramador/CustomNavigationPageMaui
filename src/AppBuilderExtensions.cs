namespace Plugins.CNPM;
public static class AppBuilderExtensions
{

    public static MauiAppBuilder UseCustomNavigationPageMaui(this MauiAppBuilder builder)
    {
        
        builder.ConfigureMauiHandlers((handlers) =>
         {
#if ANDROID
             handlers.AddHandler<Shell, Plugins.CNPM.Platforms.Android.CustomShellRenderer>();
             //handlers.AddHandler(typeof(CustomNavigationPageMaui), typeof(Platforms.Android.CustomNavigationPageMauiHandler));

#elif IOS
             //handlers.AddHandler(typeof(CustomNavigationPageMaui), typeof(Platforms.iOS.CustomNavigationPageMauiRender));
             handlers.AddHandler<Shell, Plugins.CNPM.Platforms.iOS.CustomShellRenderer>();
#endif
         });
        return builder;
    }
}