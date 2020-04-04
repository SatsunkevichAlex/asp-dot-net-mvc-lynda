using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index(int page = 0)
        {
            int pageSize = 2;
            int totalPosts = _db.Posts.Count();
            int totalPages = (int)Math.Ceiling(Convert.ToDouble(totalPosts / pageSize));
            int previousPage = page - 1;
            int nextPage = page + 1;

            ViewBag.PreviousePage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            Post[] posts = _db.Posts.OrderByDescending(x => x.Posted)
                                    .Skip(pageSize * page)
                                    .Take(pageSize)
                                    .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(posts);
            }

            return View(posts);
        }

        [Route("{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            Post post = _db.Posts.FirstOrDefault(x => x.Key == key);

            return View(post);
        }


        [Authorize]
        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid || post == null)
            {
                return View();
            }

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;            

            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}