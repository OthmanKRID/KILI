
using System;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class Paiement
    {
        public int Id { get; set; }
        [Required]
        public int Montant { get; set; }
        [Required]
        public DateTime DatePaiement { get; set; }

    }
}

