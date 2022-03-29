using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kili.Models.General;

namespace Kili.Models.Vente
{
    public class CoordonneesAcheteur
    {
        public int CoordonneesAcheteurID { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public virtual Adresse AdresseLivraison { get; set; }
        public virtual Adresse AdresseFacturation { get; set; }

        [MaxLength(10)]
        public string Telephone { get; set; }
    }
}
