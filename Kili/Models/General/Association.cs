using Kili.Models.General;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class Association
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nom { get; set; }
        [Required]
        public int? AdresseId { get; set; }
        public virtual Adresse Adresse { get; set; }
        [Required]
        public ThemeAssociation Theme { get; set; }

        public bool Actif { get; set; }
        //public int? UserAccountId { get; set; }
        //public virtual UserAccount UserAccount { get; set; }

        public int? AbonnementId { get; set; }
        public virtual Abonnement Abonnement { get; set; }

        public Association()
        {
            Adresse = new Adresse();
            Abonnement = new Abonnement();
        }

    }



    public enum ThemeAssociation
    {
        Sport,
        [DefaultValue("Arts et culture")]
        Arts_et_culture,
        Environnement,
        [DefaultValue(" Humanitaire-caritative")]
        Humanitaire_caritative,
        [DefaultValue("Club deloisirs")]
        club_de_loisirs,
        étudiante
    }

    public enum RechercheTheme
    {
        Tous,
        Sport,
        [DefaultValue("Arts et culture")]
        Arts_et_culture,
        Environnement,
        [DefaultValue(" Humanitaire-caritative")]
        Humanitaire_caritative,
        [DefaultValue("Club deloisirs")]
        club_de_loisirs,
        étudiante
    }
}
