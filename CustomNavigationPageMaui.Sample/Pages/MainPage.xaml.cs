namespace CustomNavigationPageMaui.Sample.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

	}

    void FadeClicked(System.Object sender, System.EventArgs e)
    {
		NavConfig.Duration = 1;
		NavConfig.Starting = TransitionType.Fade;
		NavConfig.Finished = TransitionType.Flip;
		App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    }
}
