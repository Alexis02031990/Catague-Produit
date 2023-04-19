using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CatalogueProduit.Models;

namespace CatalogueProduit.Controllers
{
    public class CategorieController : Controller
    {

        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjoutCategorie()
        {
            try 
            {
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch(Exception ex)
            {
               return HttpNotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AjoutCategorie(CAT_CATEGORIE categorie) //enregistrer
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categorie.DATE_SAISIE = DateTime.Now;
                    db.CAT_CATEGORIE.Add(categorie);
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        public ActionResult SupprimerCategorie(int id)
        {
            try
            {
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id); // recherche id categorie
                if(categorie != null)
                {
                    db.CAT_CATEGORIE.Remove(categorie); // supprimer la categorie
                    db.SaveChanges(); // enregistrer le resultat
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch(Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }

        public ActionResult ModifierCategorie(int id)
        {
            try
            {
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id); // recherche id categorie
                if (categorie != null)
                {
                    return View("AjoutCategorie",categorie);
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult ModifierCategorie(CAT_CATEGORIE categorie)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    db.Entry(categorie).State = EntityState.Modified; // modification de notre categorie
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }

    }
}