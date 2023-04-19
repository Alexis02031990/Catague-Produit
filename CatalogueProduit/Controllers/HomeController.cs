using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CatalogueProduit.Models;

namespace CatalogueProduit.Controllers
{
    public class HomeController : Controller
    {
        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList().OrderBy(r => r.LIBELLE_CATEGORIE);// Liste des categories rangée par ordre alphabetique
            return View();
        }
    }
}