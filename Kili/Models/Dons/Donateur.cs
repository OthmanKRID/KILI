using Kili.Models.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Dons
{
    public class Donateur
    {
        public int Id { get; set; }
        public Adresse AdresseFacuration { get; set; }
        [MaxLength(10)]
        public string Telephone { get; set; }
        //[EmailAddress(ErrorMessage = "L'adresse email saisie n'est pas valide")]
        //public string Mail { get; set; }
        public int? UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        public ICollection<Don> Dons { get; set; }

    }
}
