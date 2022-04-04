using Kili.Models.Vente;
using System.Collections.Generic;

namespace Kili.ViewModels
{
    public class VenteViewModel
    {
        public List<Produit> Produits { get; set; }
        public Panier Panier { get; set; }
    }
}
