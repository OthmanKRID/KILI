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
                // cart does not exist so we create it before adding item with product inside
                cartId = PanierService_Services.CreateCart();
                PanierService_Services.AddItem(cartId, new Item { ServiceId = id, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                // cart exist then we just add item on it
                PanierService panier = PanierService_Services.GetCart(cartId);
                int res = ServiceExisteDansPanier(panier, id);
                if (res != -1)
                {
                    PanierService_Services.UpdateItemQuantity(res);
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


        private int ServiceExisteDansPanier(PanierService panier, int ServiceId)
        {
            foreach (var item in panier.Items)
            {
                if (item.Id == ServiceId)
                {
                    return item.Id;
                }
            }
            return -1;
        }

    }
}
