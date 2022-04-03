using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Dons
{
    public class Collecte
    {
        public int Id { get; set; }
        [MaxLength(300)]
        [Required]
        public string Nom { get; set; }
        public int MontantCollecte { get; set; }      
        public string  Descriptif { get; set; }

        public bool Actif { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public DateTime Date { get; set; }
        public int? ServiceDonId { get; set; }
        public virtual ServiceDon ServiceDon { get; set; }
        public ICollection<Don> Dons { get; set; }

    }
}
