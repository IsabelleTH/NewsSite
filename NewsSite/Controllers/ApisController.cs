using System.Collections.Generic;
using System.Web.Mvc;
using NewsSite.Service;
using NewsSite.Models;

namespace NewsSite.Controllers
{
    public class ApisController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
            var headLines = client.GetLiveUpdates();
            return View(headLines);
        }


        public ActionResult GetHeadLines()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
            var headLines = client.GetLiveUpdates();
            return View(headLines);
        }

        public ActionResult ImageSlide()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
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
                NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
                var searchArticles = client.SearchArticles(query);
                return View(searchArticles);
            }
        }

        public ActionResult GetPopularPosts()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
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
                NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
                var category = client.Categories(query);

                return View(category);
            }
        
        }

        public ActionResult SlideShow()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
            var popularPosts = client.PopularSwedishPosts();

            return View(popularPosts);
        }

        public ActionResult GetUsNews()
        {
            NewsApiClient client = new NewsApiClient("25e1ebfe8acb430a9a5cfa7388a0b288");
            var usNews = client.PopularUsNews();

            return View(usNews);
        }
    }
}