using Kili.Models.General;

namespace Kili.ViewModels
{
    public class PaiementViewModel
    {

        public Paiement Paiement { get; set; }
        public MoyenPaiement MoyenPaiement { get; set; }
        public int ActionID { get; set; }
        public TypeAction Action { get; set; }
   
        
        public enum TypeAction
        {
            Commande,
            Cotisation,
            Don
        }

    }
}
