using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class Adresse
    {
        public int Id { get; set; }
        [Required]
        public uint Numero { get; set; }
        [MaxLength(40)]
        [Required]
        public string Voie { get; set; }
        [MaxLength(40)]
        public string Complement { get; set; }
        [Required]
        public uint CodePostal { get; set; }
        [Required]
        [MaxLength(30)]
        public string Ville { get; set; }

        public virtual UserAccount Compte { get; set; }
    }
}
