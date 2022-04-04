

using Kili.Models.Dons;
using Kili.Models.Vente;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General

    {
        public class Paiement
        {
            public int Id { get; set; }           
            [Required]
            public double Montant { get; set; }
            [Required]
            public DateTime DatePaiement { get; set; }
 
            public int? AssociationId { get; set; }    
            public virtual Association Association { get; set; }

            public int? MoyenPaiementId { get; set; }
            public virtual MoyenPaiement MoyenPaiement { get; set; }

            public virtual Don Don { get; set; }

            public virtual Panier Panier { get; set; }


        }
    }


