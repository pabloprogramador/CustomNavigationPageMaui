using Plugins.CNPM;

namespace CustomNavigationPageMaui.Sample.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    void FadeClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.CustomPush = new CustomConfig() {OpacityStart = 0, XStart = 1};
        Config.CustomPop = new CustomConfig() { OpacityStart = .5, RotationStart = .5, ScaleStart = 1.5};
		App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void TopClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void LeftClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void FlipClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void ScaleClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void NoneClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void DefaultClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    //L Out / R Out
    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        

        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    //R Out / L In
    void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    //R In / L In
    void Button_Clicked_2(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }
}
