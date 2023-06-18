using RealEstateApp2.Models;
using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class BookmarksPage : ContentPage
{
    private readonly ApiService _bookmarkService = new ApiService();

    public BookmarksPage()
	{
		InitializeComponent();
		GetPropertiesList();
	}

    private async void GetPropertiesList()
    {
        var properties = await _bookmarkService.GetBookmarkList();
        CvProperties.ItemsSource = properties;
    }
    void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
        ((CollectionView)sender).SelectedItem = null;
    }
}