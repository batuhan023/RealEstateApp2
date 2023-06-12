using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _loginService = new ApiService();
    public LoginPage()
	{
		InitializeComponent();
	}

    async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        var response = await _loginService.Login(EntEmail.Text, EntPassword.Text);
        if (response)
        {
            Application.Current.MainPage = new CustomTabbed();
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong.", "Cancel");
        }
    }

    async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}