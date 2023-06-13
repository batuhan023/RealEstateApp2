using RealEstateApp2.Services;

namespace RealEstateApp2.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private readonly ApiService _propertyDetailService = new ApiService();
    public PropertyDetailPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetail(propertyId);
	}

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await _propertyDetailService.GetPropertyDetail(propertyId);
        LblPrice.Text ="$ " + property.Price;
        LblDescription.Text = property.Detail;
        lblAddress.Text = property.Address;
        ImgProperty.Source = property.FullImageUrl;
        phoneNumber = property.Phone;
    }

    private void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
        var messeage = new SmsMessage("Hi! I am interested in your property", phoneNumber);
        Sms.ComposeAsync(messeage);
    }

    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
        //PhoneDialer.Open(phoneNumber);
        if (PhoneDialer.Default.IsSupported)
            PhoneDialer.Default.Open(phoneNumber);
    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}