namespace CustomNavigationPageMaui.Sample.Pages;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
		App.Current.MainPage.Navigation.PopAsync();
    }
}
