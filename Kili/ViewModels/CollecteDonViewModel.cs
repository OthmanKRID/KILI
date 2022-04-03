using Kili.Models.Dons;
using Kili.Models.General;
using System.Collections.Generic;

namespace Kili.ViewModels
{
    public class CollecteDonViewModel
    {
        
        public List<Collecte> listecollecte { get; set; }
        public List<Don> listedon { get; set; }

        public Association association { get; set; }
        public Collecte collecte { get; set; }
        public int montantglobalcollectes { get; set; } 

        public int montantdernierdon { get; set; }
        public bool Authentifie { get; set; }

    }
}
