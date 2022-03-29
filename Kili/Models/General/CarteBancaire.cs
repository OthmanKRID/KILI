using System;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class CarteBancaire
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string NomTitulaire { get; set; }
        public string Numero { get; set; }
        public string Cryptogrammt { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}
