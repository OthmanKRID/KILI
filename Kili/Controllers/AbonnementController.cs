using CartSession.Helpers;
using Microsoft.AspNetCore.Mvc;
using Kili.Models.Services;
using Kili.Models.General;
using System.Collections.Generic;

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
            return View("../Association/PanierService", ObtenirPanierService());
        }


        public IActionResult SupprimerServiceDansPanier(int id)
        {
            var panierId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            new PanierService_Services().RemoveItem(panierId, id);
            return View("../Association/PanierService", ObtenirPanierService());
        }

        private void CreerPanier()
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            PanierService_Services PanierService_Services = new PanierService_Services();

            if (cartId == 0)
            {
                // Le panier n'existe pas donc on le crée en ajoutant l'item dedans
                cartId = PanierService_Services.CreateCart();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
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
