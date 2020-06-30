using NewsSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NewsSite.Service
{
    public class NewsApiClient
    {

        private readonly string _apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
        
    
        public NewsApiClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        //Get live updates 
        public List<Article> GetLiveUpdates()
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var url = $"http://newsapi.org/v2/top-headlines?country=us&apiKey={_apiKey}";
            var content = client.DownloadString(url);
            var response = JsonConvert.DeserializeObject<APIResponseModel>(content);

            return (response.Articles);
        }

        //Get keyword from when user searches for a word
        public List<Article> SearchArticles(string query)
        {
            var client = new WebClient();
            var today = DateTime.Today;
            client.Encoding = Encoding.ASCII;
            var url = $"http://newsapi.org/v2/everything?q={query}&sortBy=popularity&apiKey={_apiKey}";
            var content = client.DownloadString(url);
            var response = JsonConvert.DeserializeObject<APIResponseModel>(content);

            return (response.Articles);
        }

        public List<Article> PopularSwedishPosts()
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var url = $"http://newsapi.org/v2/top-headlines?country=se&sortBy=popularity&apiKey={_apiKey}";
            var content = client.DownloadString(url);

            var response = JsonConvert.DeserializeObject<APIResponseModel>(content);
            return (response.Articles.Take(3).ToList());
        }  

        public List<Article> PopularUsNews()
        {
            var client = new WebClient();
            client.Encoding = Encoding.ASCII;
            var url = $"http://newsapi.org/v2/top-headlines?country=us&sortBy=popularity&apiKey={_apiKey}";
            var content = client.DownloadString(url);

            var response = JsonConvert.DeserializeObject<APIResponseModel>(content);
            return (response.Articles.Take(3).ToList());
        }

        public List<Article> Categories(string query)
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var url = $"https://newsapi.org/v2/top-headlines?country=us&category={query}&apiKey={_apiKey}";

            var content = client.DownloadString(url);
            var response = JsonConvert.DeserializeObject<APIResponseModel>(content);

            return (response.Articles);
        }
             
    }
}