using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.Vente
{
    public class Catalogue
    {
        public int CatalogueID { get; set; }

        [Required, StringLength(100), Display(Name = "Catalogue")]
        public string CatalogueName { get; set; }

        [Display(Name = "Déscription")]
        public string Description { get; set; }

        public bool Actif { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }
    }
}
