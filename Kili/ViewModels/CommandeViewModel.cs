using Kili.Models.General;
using Kili.Models.Vente;
using System;
using System.Collections.Generic;

namespace Kili.ViewModels
{
    public class CommandeViewModel
    {
        public Livraison Livraison { get; set; }

        public Panier Panier { get; set; }

        public CoordonneesAcheteur coordonneesAcheteur { get; set; }
    }
}
