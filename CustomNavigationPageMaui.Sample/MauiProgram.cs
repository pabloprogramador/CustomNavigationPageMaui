using Microsoft.Extensions.Logging;
using Plugins.CNPM;

namespace CustomNavigationPageMaui.Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseCustomNavigationPageMaui()
            //.ConfigureMauiHandlers(handlers =>
            //{

            //    handlers.AddHandler<Shell, Plugins.CNPM.Platforms.Android.CustomShellRenderer>();

            //})
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static void Teste()
	{

    }
}

