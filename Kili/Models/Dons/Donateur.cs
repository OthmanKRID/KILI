using Kili.Models.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Dons
{
    public class Donateur
    {
        public int Id { get; set; }

        [Required]
        public int? AdresseID { get; set; }
        public virtual Adresse Adresse { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public ICollection<Don> Dons { get; set; }

    }
}
