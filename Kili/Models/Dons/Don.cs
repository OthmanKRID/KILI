using Kili.Models.General;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Dons
{
    public class Don
    {
        
        public int Id { get; set; }
        [Required]
        public int Montant { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        public TypeRecurrence Recurrence { get; set; }
        public int? DonateurId { get; set; }
        public virtual Donateur Donateur { get; set; }
        public int? CollecteId { get; set; }
        public virtual Collecte Collecte { get; set; }
        public int? PaiementId { get; set; }
        public virtual Paiement Paiement { get; set; }

    }

    public enum TypeRecurrence
    {
        Unique,
        Mensuel,
        Trimestriel,
        Annuel
    }


}

