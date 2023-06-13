using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _registerService = new ApiService();
    public RegisterPage()
	{
		InitializeComponent();
	}

    async void BtnRegister_Clicked(object sender, EventArgs e)
    {
		var response = await _registerService.RegisterUser(EntFullName.Text, EntEmail.Text, EntPassword.Text, EntPhone.Text);
		if (response)
		{
			await DisplayAlert("", "Your account has been created", "Alright");
            await Navigation.PushAsync(new LoginPage());
        }
		else
		{
			await DisplayAlert("", "Oops something went wrong", "Cancel");
		}
    }

    async void TapLogin_Tapped(object sender, TappedEventArgs e)
    {
        //await Navigation.PushAsync(new LoginPage());
         await Navigation.PopModalAsync();
    }
}