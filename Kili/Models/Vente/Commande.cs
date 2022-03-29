using Kili.Models.General;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Vente
{
    public class Commande
    {
        public int CommandeID { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Id")]
        public int? Id { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        [ForeignKey("LivraisonID")]
        public int? LivraisonID { get; set; }
        

        public virtual Livraison Livraison { get; set; }

        [ForeignKey("PanierID")]
        public int? PanierID { get; set; }

        public virtual Panier Panier { get; set; }
    }
}
