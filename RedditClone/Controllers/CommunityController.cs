using RedditClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedditClone.Controllers
{
    public class CommunityController : Controller
    {
        // GET: lista tuturor studentilor
        public ActionResult Index()
        {
            return View();
        }
        // GET: vizualizarea unui student
        public ActionResult Show(int id)
        {
            return View();
        }

        public ActionResult New(Community community)
        {
            return View();
        }


        // POST: trimitem datele studentului catre server pentru creare
        [HttpPost]
        public ActionResult Create(Community community)
        {
            // cod creare student
            // dupa ce am creat studentul luam ID-ul nou inserat din baza de date
            // redirectionam browser-ul catre studentul nou creat
            int id = 123;
            return Redirect("/students/" + id);
        }

        // GET: vrem sa editam un student
        public ActionResult Edit(int ID)
        {
            return View();
        }
    }
}