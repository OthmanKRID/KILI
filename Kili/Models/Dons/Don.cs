using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Dons
{
    public class Don
    {
        
        public int Id { get; set; }
        public int Montant { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TypeRecurrence Recurrence { get; set; }
        public int? DonateurId { get; set; }
        public virtual Donateur Donateur { get; set; }

    }

    public enum TypeRecurrence
    {
        Unique,
        Mensuel,
        Trimestriel,
        Annuel
    }


}

