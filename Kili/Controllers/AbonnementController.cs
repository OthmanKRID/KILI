using Kili.Helpers;
using Microsoft.AspNetCore.Mvc;
using Kili.Models.Services;
using Kili.Models.General;
using System.Collections.Generic;
using Kili.ViewModels;
using Kili.Models;

namespace Kili.Controllers
{
    public class AbonnementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PanierService ObtenirPanierService()
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            PanierService cart;
            if (cartId != 0)
            {
                cart = new PanierService_Services().GetCart(cartId);
            }
            else
            {
                cart = new PanierService() { Items = new List<Item>() };
            }
            return cart;
        }

        public IActionResult ConfirmerPaiementService()
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            PanierService cart;
            cart = new PanierService_Services().GetCart(cartId);


            foreach (Item item in cart.Items)
            {
                new Abonnement_Services().AjouterService(new UserAccount_Services().ObtenirUserAccountConnecte(User.Identity.Name).Association.Id, item.Service);
            }

            SupprimerPanier();
            return View("ConfirmationPaiement");
        }

        public IActionResult AjouterService(int id)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            PanierService_Services PanierService_Services = new PanierService_Services();

            if (cartId == 0)
            {
                // Le panier n'existe pas donc on le crée en ajoutant l'item dedans
                cartId = PanierService_Services.CreateCart();
                PanierService_Services.AddItem(cartId, new Item { ServiceId = id, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                // Le panier existe deja, on ajoute alors l'item
                PanierService panier = PanierService_Services.GetCart(cartId);
                int res = ServiceExisteDansPanier(panier, id);
                if (res != -1)
                {
                //    PanierService_Services.UpdateItemQuantity(res);
                }
                else
                {
                    PanierService_Services.AddItem(cartId, new Item { ServiceId = id, Quantity = 1 });
                }
            }
            //return View("../Association/GererServices", new ServicesViewModel());
            //return RedirectToAction("PanierServices");
            return RedirectToAction("GererServices", "Association");
        }

        public IActionResult PanierServices()
        {
            return View(ObtenirPanierService());
        }

        public IActionResult SupprimerServiceDansPanier(int id)
        {
            var panierId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            new PanierService_Services().RemoveItem(panierId, id);
            
            return RedirectToAction("PanierServices");

        }


        public IActionResult SupprimerPanier()
        {
            var panierId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            new PanierService_Services().DeleteCart(panierId);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", 0);
            return RedirectToAction("GererServices", "Association");
        }


        private int ServiceExisteDansPanier(PanierService panier, int ServiceId)
        {
            foreach (var item in panier.Items)
            {
                if (item.Service.Id == ServiceId)
                {
                    return item.Service.Id;
                }
            }
            return -1;
        }

    }
}
