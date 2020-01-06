﻿using Microsoft.AspNet.Identity;
using RedditClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedditClone.Controllers
{
    public class CoPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: 
        public ActionResult Index()
        {
            var posts = db.CoPosts.Include("User").Include("Community");

            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            ViewBag.CoPosts = posts;

            return View();
        }

        [Authorize(Roles = "User,Moderator,Administrator")]
        // GET: vizualizarea unei postari
        public ActionResult Show(int id)
        {
            CoPost post = db.CoPosts.Find(id);


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
            CoPost post = new CoPost();
            post.UserId = User.Identity.GetUserId();
            return View(post);
        }


        // POST: trimitem datele catre server pentru creare
        [HttpPost]
        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New(CoPost post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CoPosts.Add(post);
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
            CoPost post = db.CoPosts.Find(Id);
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
        public ActionResult Edit(int id, CoPost requestPost)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    CoPost post = db.CoPosts.Find(id);
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

    }
}