using RealEstateApp2.Pages;

namespace RealEstateApp2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var accessToken = Preferences.Get("accesstoken", string.Empty);
		if (string.IsNullOrEmpty(accessToken))
		{
			MainPage = new LoginPage();
		}
		else
		{
			MainPage = new CustomTabbed();
		}
        
    }
}
