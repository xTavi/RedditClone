using Microsoft.AspNet.Identity;
using RedditClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedditClone.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: 
        public ActionResult Index()
        {
            var posts = db.Posts.Include("User").Include("Community");

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            ViewBag.Posts = posts;

            return View();
        }

        [Authorize(Roles = "User,Moderator,Administrator")]
        // GET: vizualizarea unei postari
        public ActionResult Show(int id)
        {
            Post post = db.Posts.Find(id);


            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Administrator");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();

            return View(post);
        }

        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New()
        {
            Post post = new Post();
            post.UserId = User.Identity.GetUserId();
            return View(post);
        }


        // POST: trimitem datele catre server pentru creare
        [HttpPost]
        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New(Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                    TempData["message"] = "O noua postare a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Something went wrong!";
                    return View(post);
                }
            }
            catch (Exception e)
            {
                TempData["message"] = "Something went wrong!";
                return View(post);
            }
        }

        [Authorize(Roles = "Moderator,Administrator")]
        // GET: vrem sa editam un student
        public ActionResult Edit(int Id)
        {
            Post post = db.Posts.Find(Id);
            ViewBag.Post = post;
            if (post.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(post);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei postari care nu va apartine!";
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Moderator,Administrator")]
        public ActionResult Edit(int id, Post requestPost)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Post post = db.Posts.Find(id);
                    if (post.UserId == User.Identity.GetUserId() ||
                        User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(post))
                        {
                            post.Title = requestPost.Title;
                            post.Content = requestPost.Content;
                            db.SaveChanges();
                            TempData["message"] = "Datele postarii au fost modificate!";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei postari care nu va apartine!";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return View(requestPost);
                }

            }
            catch (Exception e)
            {
                return View(requestPost);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Moderator,Administrator")]
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            if (post.UserId == User.Identity.GetUserId() ||
                User.IsInRole("Administrator"))
            {
                db.Posts.Remove(post);
                db.SaveChanges();
                TempData["message"] = "Comunitatea a fost sters!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti unei comunitati care nu va apartine!";
                return RedirectToAction("Index");
            }

        }

    }
}