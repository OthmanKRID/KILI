using Kili.Models.General;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kili.Models.General
{
    public class Association
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }
        [Required]
        public int? AdresseId { get; set; }
        public virtual Adresse Adresse { get; set; }
        [Required]
        public ThemeAssociation Theme { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public bool Actif { get; set; }
        //public int? UserAccountId { get; set; }
        //public virtual UserAccount UserAccount { get; set; }

        public int? AbonnementId { get; set; }
        public virtual Abonnement Abonnement { get; set; }

       
        public ICollection<Paiement> Paiements { get; set; }


        public Association()
        {
            Adresse = new Adresse();
            Abonnement = new Abonnement();
        }

    }



    public enum ThemeAssociation
    {
        Sport,
        [Display(Name = "Arts et culture")]
        Arts_et_culture,
        Environnement,
        [Display(Name = "Humanitaire - caritative")]
        Humanitaire_caritative,
        [Display(Name = "Club de loisirs")]
        club_de_loisirs,
        étudiante
    }

    public enum RechercheTheme
    {
        Tous,
        Sport,
        [Display(Name = "Arts et culture")]
        Arts_et_culture,
        Environnement,
        [Display(Name = "Humanitaire - caritative")]
        Humanitaire_caritative,
        [Display(Name = "Club de loisirs")]
        club_de_loisirs,
        étudiante
    }
}
