using Kili.Models.General;
using Kili.Models.Vente;

namespace Kili.ViewModels
{
    public class PaiementViewModel
    {

        public Paiement Paiement { get; set; }
        public MoyenPaiement MoyenPaiement { get; set; }
        public int ActionID { get; set; }
        public TypeAction Action { get; set; }


        public string NomAcheteur { get; set; }
        public string NomVendeur { get; set; }
        public Adresse adresseAcheteur { get; set; }
        public Adresse adresseVendeur { get; set; }

        public enum TypeAction
        {
            Commande,
            Cotisation,
            Don
        }

    }
}
