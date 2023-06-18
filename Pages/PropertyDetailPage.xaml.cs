using RealEstateApp2.Models;
using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private readonly ApiService _apiService = new ApiService();
    private string phoneNumber;
    private bool IsBookmarkEnabled;
    private int propertyId;
    private int bookmarkId;

    public PropertyDetailPage(int propertyId)
    {
        InitializeComponent();
        GetPropertyDetail(propertyId);
        this.propertyId = propertyId;
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await _apiService.GetPropertyDetail(propertyId);
        LblPrice.Text = "$ " + property.Price;
        LblDescription.Text = property.Detail;
        lblAddress.Text = property.Address;
        ImgProperty.Source = property.FullImageUrl;
        phoneNumber = property.Phone;

        if (property.Bookmarks == null)
        {
            ImgbookmarkBtn.Source = "bookmark_empty_icon";
            IsBookmarkEnabled = false;
        }
        else
        {
            ImgbookmarkBtn.Source = property.Bookmarks.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
            bookmarkId = property.Bookmarks.Id;
            IsBookmarkEnabled = true;
        }
    }

    private void TapMessage_Tapped(object sender, EventArgs e)
    {
        var message = new SmsMessage("Hi! I am interested in your property", phoneNumber);
        Sms.ComposeAsync(message);
    }

    private void TapCall_Tapped(object sender, EventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void ImgbookmarkBtn_Clicked(object sender, EventArgs e)
    {
        if (IsBookmarkEnabled == false)
        {
            //Add a bookmark
            var addBookmark = new AddBookmark()
            {
                User_Id = 9,
                PropertyId = propertyId
            };
            var response = await _apiService.AddBookmark(addBookmark);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_fill_icon";
                IsBookmarkEnabled = true;
            }
        }
        else
        {
            //Delete a bookmark
            var response = await _apiService.DeleteBookmark(bookmarkId);
            if (response)
            {
                ImgbookmarkBtn.Source = "bookmark_empty_icon";
                IsBookmarkEnabled = false;
            }
        }
    }
}