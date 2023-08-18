namespace CustomNavigationPageMaui.Sample.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

	}

    void FadeClicked(System.Object sender, System.EventArgs e)
    {
		Nav.Config.Duration = .5;
		Nav.Config.Starting = TransitionType.Fade;
		Nav.Config.Finished = TransitionType.Fade;
		App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void TopClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = 1.5;
        Nav.Config.Starting = TransitionType.SlideFromTop;
        Nav.Config.Finished = TransitionType.SlideFromBottom;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void LeftClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = 1.5;
        Nav.Config.Starting = TransitionType.SlideFromLeft;
        Nav.Config.Finished = TransitionType.SlideFromRight;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void FlipClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = 1.5;
        Nav.Config.Starting = TransitionType.FlipIn;
        Nav.Config.Finished = TransitionType.FlipOut;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void ScaleClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = .5;
        Nav.Config.Starting = TransitionType.ScaleIn;
        Nav.Config.Finished = TransitionType.ScaleOut;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void NoneClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = 1.5;
        Nav.Config.Starting = TransitionType.None;
        Nav.Config.Finished = TransitionType.None;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void DefaultClicked(System.Object sender, System.EventArgs e)
    {
        Nav.Config.Duration = 1.5;
        Nav.Config.Starting = TransitionType.Default;
        Nav.Config.Finished = TransitionType.Default;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }
}
