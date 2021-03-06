﻿using RedditClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace RedditClone.Controllers
{
    public class CommunityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: 
        public ActionResult Index()
        {
            var communities = db.Communities.Include("User");

            ViewBag.Communities = communities;

            return View();
        }

        [Authorize(Roles = "User,Moderator,Administrator")]
        // GET: vizualizarea unei comunitati
        public ActionResult Show(int id)
        {
            Community community = db.Communities.Find(id);

            ViewBag.afisareButoane = false;
            if (User.IsInRole("Editor") || User.IsInRole("Administrator"))
            {
                ViewBag.afisareButoane = true;
            }

            ViewBag.esteAdmin = User.IsInRole("Administrator");
            ViewBag.utilizatorCurent = User.Identity.GetUserId();

            return View(community);
        }
      
        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New()
        {
            Community community = new Community();
            community.UserId = User.Identity.GetUserId();
            return View(community);
        }


        // POST: trimitem datele catre server pentru creare
        [HttpPost]
        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New(Community community)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Communities.Add(community);
                    db.SaveChanges();
                    TempData["message"] = "O noua comunitate a fost adaugata!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(community);
                }
            }
            catch (Exception e)
            {
                return View(community);
            }
        }

        [Authorize(Roles = "Moderator,Administrator")]
        // GET: vrem sa editam un student
        public ActionResult Edit(int Id)
        {
            Community community = db.Communities.Find(Id);
            ViewBag.Community = community;
            if (community.UserId == User.Identity.GetUserId() || User.IsInRole("Administrator"))
            {
                return View(community);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei comunitati care nu va apartine!";
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Moderator,Administrator")]
        public ActionResult Edit(int id, Community requestCommunity)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Community community = db.Communities.Find(id);
                    if (community.UserId == User.Identity.GetUserId() ||
                        User.IsInRole("Administrator"))
                    {
                        if (TryUpdateModel(community))
                        {
                            community.Name = requestCommunity.Name;
                            community.Description = requestCommunity.Description;
                            db.SaveChanges();
                            TempData["message"] = "Datele comunitatii au fost modificate!";
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unei comunitati care nu va apartine!";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return View(requestCommunity);
                }

            }
            catch (Exception e)
            {
                return View(requestCommunity);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Moderator,Administrator")]
        public ActionResult Delete(int id)
        {
            Community community = db.Communities.Find(id);
            if (community.UserId == User.Identity.GetUserId() ||
                User.IsInRole("Administrator"))
            {
                db.Communities.Remove(community);
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

        public ActionResult GetSubscribedCommunities()
        {
            if (User.IsInRole("Guest"))
            {
                //list is most popular
            } else
            {
                //list is subscribed communities
            }
            return PartialView();
        }
    }
}