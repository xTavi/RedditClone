using RedditClone.Models;
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

        [Authorize(Roles = "Guest,User,Moderator,Administrator")]
        // GET: 
        public ActionResult Index()
        {
            var communities = db.Communities;

            ViewBag.Communities = communities;

            return View();
        }

        [Authorize(Roles = "Guest,User,Moderator,Administrator")]
        // GET: vizualizarea unei comunitati
        public ActionResult Show(int id)
        {
            Community community = db.Communities.Find(id);

            //This will be uncommented when 
            //ViewBag.afisareButoane = false;
            //if (User.IsInRole("Editor") || User.IsInRole("Administrator"))
            //{
            //    ViewBag.afisareButoane = true;
            //}

            //ViewBag.esteAdmin = User.IsInRole("Administrator");
            //ViewBag.utilizatorCurent = User.Identity.GetUserId();

            return View(community);
        }
      
        [Authorize(Roles = "User,Moderator,Administrator")]
        public ActionResult New()
        {
            Community community = new Community();
            return View(community);
        }


        // POST: trimitem datele studentului catre server pentru creare
        [HttpPost]
        [Authorize(Roles = "Moderator,Administrator")]
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
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui articol care nu va apartine!";
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
                TempData["message"] = "Articolul a fost sters!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un articol care nu va apartine!";
                return RedirectToAction("Index");
            }

        }
    }
}