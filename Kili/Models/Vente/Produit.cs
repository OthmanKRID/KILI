using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.Vente
{
    public class Produit
    {
        public int ProduitID { get; set; }

        public string DateCreation { get; set; }

        [Required, StringLength(100), Display(Name = "Désignation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Format")]
        public string Format { get; set; }

        [StringLength(10000), Display(Name = "Déscription"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }


        [Display(Name = "Prix")]
        public double PrixUnitaire { get; set; }

        [Required]
        [Display(Name = "Devise")]
        public string Devise { get; set; }

        [Display(Name = "Catalogue")]
        public int CatalogueID { get; set; }

        public virtual Catalogue Catalogue { get; set; }
    }


}
