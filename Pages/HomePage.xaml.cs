using RealEstateApp2.Models;
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

    void CvCategories_SelectionChanged(object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection == null) return;
        Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    }

    void CvTopPicks_SelectionChanged(object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}