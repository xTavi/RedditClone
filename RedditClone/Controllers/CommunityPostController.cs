using RedditClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedditClone.Controllers
{
    public class CommunityPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Guest,User,Moderator,Administrator")]
        // GET: 
        public ActionResult Index()
        {
            

            return View();
        }

    }
}