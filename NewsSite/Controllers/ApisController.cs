using System.Collections.Generic;
using System.Web.Mvc;
using NewsSite.Service;
using NewsSite.Models;
using System;

namespace NewsSite.Controllers
{
    public class ApisController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var headLines = client.GetLiveUpdates();
            return View(headLines);
        }


        public ActionResult GetHeadLines()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var headLines = client.GetLiveUpdates();
            return View(headLines);
        }

        public ActionResult ImageSlide()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var getData = client.GetLiveUpdates();
            return View(getData);
        }

        public ActionResult GetUserSearchArticles(string query = "")
        {
            if(query == "")
            {
                return View(new List<Article>());
            } else
            {
                var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
                NewsApiClient client = new NewsApiClient(apiKey);
                var searchArticles = client.SearchArticles(query);
                return View(searchArticles);
            }
        }

        public ActionResult GetPopularPosts()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var popularPosts = client.PopularSwedishPosts();

            return View(popularPosts);

        }

        public ActionResult GetCategory(string query = "")
        {
            if(query != null)
            {
                ViewBag.Message = "Articles for " + query;  
            }
            else
            {
                ViewBag.Message = "";
            }
            if (query == "")
            {
               
                return View(new List<Article>());
            } else
            {
                var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
                NewsApiClient client = new NewsApiClient(apiKey);
                var category = client.Categories(query);

                return View(category);
            }
        
        }

        public ActionResult SlideShow()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var popularPosts = client.PopularSwedishPosts();

            return View(popularPosts);
        }

        public ActionResult GetUsNews()
        {
            var apiKey = Environment.GetEnvironmentVariable("NEWS_API_KEY");
            NewsApiClient client = new NewsApiClient(apiKey);
            var usNews = client.PopularUsNews();

            return View(usNews);
        }
    }
}