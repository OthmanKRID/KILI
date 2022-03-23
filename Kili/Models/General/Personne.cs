using System;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class Personne
    {
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        public string Nom { get; set; }

        [MaxLength(25)]
        [Required]
        public string Prenom { get; set; }

        [Required]
        public DateTime DateNaissance { get; set; }
    }
}