namespace CustomNavigationPageMaui.Sample.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

	}

    void FadeClicked(System.Object sender, System.EventArgs e)
    {
		NavConfig.Duration = .5;
		NavConfig.Starting = TransitionType.Fade;
		NavConfig.Finished = TransitionType.Fade;
		App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void TopClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.SlideFromTop;
        NavConfig.Finished = TransitionType.SlideFromBottom;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void LeftClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.SlideFromLeft;
        NavConfig.Finished = TransitionType.SlideFromRight;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void FlipClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.Flip;
        NavConfig.Finished = TransitionType.Flip;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }

    void ScaleClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.Scale;
        NavConfig.Finished = TransitionType.Scale;
        App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }

    void NoneClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.None;
        NavConfig.Finished = TransitionType.None;
        App.Current.MainPage.Navigation.PushAsync(new NewPage2());
    }

    void DefaultClicked(System.Object sender, System.EventArgs e)
    {
        NavConfig.Duration = 1.5;
        NavConfig.Starting = TransitionType.Default;
        NavConfig.Finished = TransitionType.Default;
        App.Current.MainPage.Navigation.PushAsync(new NewPage3());
    }
}
