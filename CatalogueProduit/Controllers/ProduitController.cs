using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CatalogueProduit.Models;
using System.Web.Mvc;
using System.IO;

namespace CatalogueProduit.Controllers
{
    public class ProduitController : Controller
    {
        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjoutProduit()
        {
            try
            {
                ViewBag.listeProduit = db.CAT_PRODUIT.ToList();
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                return View();
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }
        [HttpPost]
        public ActionResult AjoutProduit(CAT_PRODUIT produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0]; // le nom de notre fichier
                        if(file != null && file.ContentLength > 0) // si notre fichier est different de null et sa taille est superieur à 0 octet
                        {
                            var filename = Path.GetFileName(file.FileName); // recupérer le nom du fichier 
                            var path = Path.Combine(Server.MapPath("~/fichier"), filename); // récuperer le chemin d'accès où sera notre fichier
                            file.SaveAs(path);// enregistrer le tout sur le server

                            produit.IMAGE_PRODUIT = filename;
                            produit.URL_IMAGE_PRODUIT = "/fichier";
                        }
                    }

                    produit.DATE_SAISIE = DateTime.Now;
                    db.CAT_PRODUIT.Add(produit);
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }

        public ActionResult SupprimerProduit(int id)
        {
            try
            {
                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id); // recherche id categorie
                if (produit != null)
                {
                    db.CAT_PRODUIT.Remove(produit); // supprimer la categorie
                    db.SaveChanges(); // enregistrer le resultat
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }
        public ActionResult ModifierProduit(int id)
        {
            try
            {
                ViewBag.listeProduit = db.CAT_PRODUIT.ToList();
                ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
                CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id); // recherche id categorie
                if (produit != null)
                {
                    return View("AjoutProduit",produit);
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult ModifierProduit(CAT_PRODUIT produit)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Entry(produit).State = EntityState.Modified; // modification de notre categorie
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound(e.Message);
            }
        }
    }
}