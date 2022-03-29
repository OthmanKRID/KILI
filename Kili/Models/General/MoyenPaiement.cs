using System;
using System.ComponentModel.DataAnnotations;

namespace Kili.Models.General
{
    public class MoyenPaiement
    {

        public int Id { get; set; }
        [MaxLength(40)]
        public string NomTitulaire { get; set; }
        [MaxLength(16)]
        [RegularExpression(@"^(?:4[0-9]{15})?$", ErrorMessage = "Le numero de carte bancaire doit commencer par un 4 et être suivi de 15 chiffres")]
        public string Numero { get; set; }
        [MaxLength(3)]
        [RegularExpression(@"^(?:[0-9]{3})?$", ErrorMessage = "Le crytgramme de votre carte doit comporter 3 chiffres")]
        public string Cryptogramme { get; set; }

        [RegularExpression(@"^(?:0[1-9]|1[0-2])\/?([0 - 9]{4}|[0-9]{2})?$", ErrorMessage = "La date de validité ne respecte pas la syntaxe attendue : mm/aa")]
        public string DateExpiration { get; set; }
        [MaxLength(50)]
        public string Identifiant { get; set; }

        public virtual Paiement Paiement { get; set; }
        
        public TypeMoyenPaiement moyenPaiement { get; set; }

        public enum TypeMoyenPaiement
        {
            CarteBancaire,
            Paypal,
        }

    }
}
