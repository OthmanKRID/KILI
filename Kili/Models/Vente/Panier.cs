using System.Collections.Generic;

namespace Kili.Models.Vente
{
    public class Panier
    {
        public int PanierID { get; set; }
        public virtual List<Article> Articles { get; set; }

        //public virtual List<Livraison> Livraisons { get; set; }


    }
}
