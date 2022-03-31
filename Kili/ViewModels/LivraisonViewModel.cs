using Kili.Models.Vente;
using System.Collections.Generic;

namespace Kili.ViewModels
{
    public class LivraisonViewModel
    {
        public List<Livraison> Livraisons { get; set; }
        public CoordonneesAcheteur CoordonneesAcheteur { get; set; }

        public Panier Panier { get; set; }
    }
}
