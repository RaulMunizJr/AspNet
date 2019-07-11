using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;//inject instane to db
        }

        [Route("")]//url .../ !!Only if it matches this exact 'route'
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();//retrieve posts from db

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]//order doesn't have to match, url match date
        public IActionResult Post(int year, int month, string key)//int? nullable integer, if failed -> null
        {
            //if null.. etc.
            //new post -> model
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);
            return View(post);
        }

        [Authorize]
        [HttpGet, Route("create")]//display
        public IActionResult Create()//to create an html form
        {
            return View();
        }

        [Authorize]
        [HttpPost, Route("create")]//handle the form
        public IActionResult Create(Post post)//'override'
        {
            if (!ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name; //done regardless
            post.Posted = DateTime.Now;        //each time

            _db.Posts.Add(post);
            _db.SaveChanges(); //Make sure to save to db! ->sqlServer in VS to check

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
