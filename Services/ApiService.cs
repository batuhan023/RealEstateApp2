using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RealEstateApp2.Models;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Win32;
using System.Net.Http;

namespace RealEstateApp2.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler);
        }
        public async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };
           
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(AppSettings.ApiUrl + "api/Users/Register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public  async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(AppSettings.ApiUrl + "api/Users/Login", content);
            if (!response.IsSuccessStatusCode) return false;
            
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accesstoken", result.AccessToken);
            Preferences.Set("userid", result.UserId);
            Preferences.Set("username", result.UserName);

            return true;
        }

        public  async Task<List<Category>> GetCategories()
        {
           
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public  async Task<List<TrendingProperty>> GetTrendingProperties()
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/TrendingProperties");
            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }

        public async Task<List<SearchProperty>> FindProperties(string address)
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/SearchProperties?address=" + address);
            return JsonConvert.DeserializeObject<List<SearchProperty>>(response);
        }

        public async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyList?categoryId=" + categoryId);
            return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
        }

        public  async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyDetail?id=" + propertyId);
            return JsonConvert.DeserializeObject<PropertyDetail>(response);
        }

        public async Task<List<BookmarkList>> GetBookmarkList()
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Bookmarks");
            return JsonConvert.DeserializeObject<List<BookmarkList>>(response);
        }
        public async Task<bool> AddBookmark(AddBookmark addBookmark)
        {
           
            var json = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.PostAsync(AppSettings.ApiUrl + "api/Bookmarks", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<bool> DeleteBookmark(int bookmarkId)
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await _httpClient.DeleteAsync(AppSettings.ApiUrl + "api/Bookmarks/" + bookmarkId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
