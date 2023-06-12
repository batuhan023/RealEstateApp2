using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class HomePage : ContentPage
{
    private readonly ApiService _homeService = new ApiService();
    public HomePage()
	{
		InitializeComponent();
		LblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);
		GetCategories();
        GetTrendingProperties();
	}

    private async void GetTrendingProperties()
    {
        var properties = await _homeService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
    {
       var categories = await _homeService.GetCategories();
       CvCategories.ItemsSource = categories;
    }
}