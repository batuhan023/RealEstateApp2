using RealEstateApp2.Models;
using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class PropertiesListPage : ContentPage
{
    private readonly ApiService _propertiesService = new ApiService();
    public PropertiesListPage(int categoryId, string categoryName)
	{
		InitializeComponent();
		Title = categoryName;
		GetPropertiesList(categoryId);
	}

    private async void GetPropertiesList(int categoryId)
    {
       var properties = await _propertiesService.GetPropertyByCategory(categoryId);
        CvProperties.ItemsSource = properties;
    }

    void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as PropertyByCategory;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}