namespace CustomNavigationPageMaui.Sample.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

	}

    void FadeClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
		Nav.Config.Duration = .5;
		Nav.Config.PushType = Nav.TransitionType.Fade;
		Nav.Config.PopType = Nav.TransitionType.Fade;
		App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void TopClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = 1.5;
        Nav.Config.PushType = Nav.TransitionType.SlideTop;
        Nav.Config.PopType = Nav.TransitionType.SlideTop;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void LeftClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = .5;
        Nav.Config.PushType = Nav.TransitionType.SlideRight;
        Nav.Config.PushInputType = Nav.InputType.In;
        Nav.Config.PopType = Nav.TransitionType.SlideLeft;
        Nav.Config.PopInputType = Nav.InputType.Out;

        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void FlipClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = 1.5;
        Nav.Config.PushType = Nav.TransitionType.Flip;
        Nav.Config.PopType = Nav.TransitionType.Flip;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void ScaleClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = .5;
        Nav.Config.PushType = Nav.TransitionType.Scale;
        Nav.Config.PopType = Nav.TransitionType.Scale;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void NoneClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = 1.5;
        Nav.Config.PushType = Nav.TransitionType.None;
        Nav.Config.PopType = Nav.TransitionType.None;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void DefaultClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.ResetConfig();
        Nav.Config.Duration = 1.5;
        Nav.Config.PushType = Nav.TransitionType.Default;
        Nav.Config.PopType = Nav.TransitionType.Default;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }
}
