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
        Config.PushType = TransitionType.Fade;
        Config.PopType = TransitionType.Fade;
        Config.CustomPush = new CustomConfig() { RotationEnd = 360};
        //Config.CustomPush = new CustomConfig() { Duration = .5 , OpacityEnd = 0, XEnd = 1, ScaleEnd = 1.5};
		App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void TopClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = 1.5;
        Config.PushType = TransitionType.SlideTop;
        Config.PopType = TransitionType.SlideTop;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void LeftClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = .5;
        Config.PushType = TransitionType.SlideRight;
        Config.PopType = TransitionType.SlideRight;

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void FlipClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = 1.5;
        Config.PushType = TransitionType.Flip;
        Config.PopType = TransitionType.Flip;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void ScaleClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = .5;
        Config.PushType = TransitionType.Scale;
        Config.PopType = TransitionType.Scale;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void NoneClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = 1.5;
        Config.PushType = TransitionType.None;
        Config.PopType = TransitionType.None;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void DefaultClicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = 1.5;
        Config.PushType = TransitionType.Default;
        Config.PopType = TransitionType.Default;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    //L Out / R Out
    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = .5;
        Config.PushType = TransitionType.SlideLeft;
        Config.PushInputType = InputType.Out;
        Config.PopType = TransitionType.SlideRight;
        Config.PopInputType = InputType.Out;

        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    //R Out / L In
    void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = .5;
        Config.PushType = TransitionType.SlideRight;
        Config.PushInputType = InputType.Out;
        Config.PopType = TransitionType.SlideLeft;
        Config.PopInputType = InputType.In;

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    //R In / L In
    void Button_Clicked_2(System.Object sender, System.EventArgs e)
    {
        Config.ResetConfig();
        Config.Duration = .5;
        Config.PushType = TransitionType.SlideRight;
        Config.PushInputType = InputType.In;
        Config.PopType = TransitionType.SlideLeft;
        Config.PopInputType = InputType.In;

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }
}
