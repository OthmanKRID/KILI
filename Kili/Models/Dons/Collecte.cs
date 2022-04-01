using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Dons
{
    public class Collecte
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Nom { get; set; }
        public int MontantCollecte { get; set; }      
        public string  Descriptif { get; set; }
        
        public DateTime Date { get; set; }
        public int? ServiceDonId { get; set; }
        public virtual ServiceDon ServiceDon { get; set; }
        public ICollection<Don> Dons { get; set; }

    }
}
