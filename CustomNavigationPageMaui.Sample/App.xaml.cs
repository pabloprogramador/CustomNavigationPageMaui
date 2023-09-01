using Plugins.CNPM;
namespace CustomNavigationPageMaui.Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new NavigationPage(new Pages.MainPage());

		//MainPage = new Plugins.CNPM.CustomNavigationPageMaui(new Pages.MainPage());

		MainPage = new AppShell();
	}
}

