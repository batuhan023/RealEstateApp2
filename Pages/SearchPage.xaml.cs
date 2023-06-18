using RealEstateApp2.Models;
using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class SearchPage : ContentPage
{
    private readonly ApiService _SearchService = new ApiService();
    public SearchPage()
	{
		InitializeComponent();
	}

    void ImgBackBtn_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    async void SbProperty_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue == null) return;
        var propertiesResult = await _SearchService.FindProperties(e.NewTextValue);
        CvSearch.ItemsSource = propertiesResult;
    }

    void CvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as SearchProperty;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}