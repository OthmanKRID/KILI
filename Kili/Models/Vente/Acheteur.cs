using Kili.Models.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Vente
{
    public class Acheteur
    {
        public int AcheteurID { get; set; }

        public Adresse AdresseLivraison { get; set; }
        [MaxLength(10)]
        public Adresse AdresseFacuration { get; set; }
        [MaxLength(10)]
        public string Telephone { get; set; }
        
        public int? UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        public ICollection<Commande> Commandes { get; set; }
    }
}
