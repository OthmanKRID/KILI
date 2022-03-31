using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kili.Models.General;


namespace Kili.Models.Services
{
    public class PanierService_Services
    {
        private BddContext _context;
        public PanierService_Services()
        {
            _context = new BddContext();
        }

        public int CreateCart()
        {
            PanierService PanierService = new PanierService() { Items = new List<Item>() };
            _context.PaniersServices.Add(PanierService);
            _context.SaveChanges();
            return PanierService.Id;
        }

        public void DeleteCart(int id)
        {
            PanierService PanierService = new PanierService() { Items = new List<Item>() };
            PanierService = _context.PaniersServices.Find(id);
            _context.PaniersServices.Remove(PanierService);
            _context.SaveChanges();
        }
        public PanierService GetCart(int panierId)
        {

            return _context.PaniersServices.Include(c => c.Items).ThenInclude(i=>i.Service).Where(c => c.Id == panierId).FirstOrDefault();
        }

        public void AddItem(int CartId, Item service)
        {
            PanierService panier = _context.PaniersServices.Find(CartId);
            panier.Items.Add(service);



            _context.SaveChanges();
        }

        public void UpdateItemQuantity(int ItemId)
        {
            var item = _context.Items.Find(ItemId);
            if (item != null)
            {
                item.Quantity += 1;
                _context.SaveChanges();
            }
        }

        public void RemoveItem(int cartId, int itemId)
        {
            PanierService PanierService = GetCart(cartId);
            Item item = PanierService.Items.Where(it => it.Id == itemId).FirstOrDefault();

            PanierService.Items.Remove(item);

            _context.SaveChanges();
        }
    }
}

