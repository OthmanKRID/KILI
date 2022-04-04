using System.Collections.Generic;
using Kili.Models.General;

namespace Kili.Models.Vente
{
    public class Panier
    {
        public int PanierID { get; set; }
        public virtual List<Article> Articles { get; set; } = new List<Article>(); 

        public virtual ServiceBoutique ServiceBoutique { get; set; }

        public virtual MoyenPaiement MoyenPaiement { get; set; }

        //public virtual List<Livraison> Livraisons { get; set; }

        public bool HasArticles
        {
            get
            {
                return (Articles.Count > 0);
            }
        }


    }
}
