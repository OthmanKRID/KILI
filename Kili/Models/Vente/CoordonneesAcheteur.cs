using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kili.Models.General;

namespace Kili.Models.Vente
{
    public class CoordonneesAcheteur
    {
        public int CoordonneesAcheteurID { get; set; }
        public UserAccount Useraccount { get; set; }
        public virtual Adresse AdresseLivraison { get; set; }
        public virtual Adresse AdresseFacturation { get; set; }

    }
}
