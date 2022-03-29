    using Kili.Models.Dons;
using Kili.Models.General;
using System.Collections.Generic;
using System.Linq;

namespace Kili.Models.Services
{
    public class DonServices
    {
        private BddContext _bddContext;

        public DonServices() {
        
            _bddContext = new BddContext();
        
        }

        //Fonction permettant d'obtenir la liste de tous les dons
        public List<Don> ObtenirDons()
        {
            return _bddContext.Dons.ToList();
        }

        //Fonction permettant d'obtenir un Don à partir de son Id
        public Don ObtenirDon(int id)
        {
            return _bddContext.Dons.Find(id);
        }

        //Fonction permettant de créer un don
        public int CreerDon(int montant, TypeRecurrence recurrence, int? donateurId)
        {
            
            Don don = new Don() { Montant = montant, Recurrence = recurrence, DonateurId = donateurId , Date = System.DateTime.Today };
            _bddContext.Dons.Add(don);
            _bddContext.SaveChanges();
            return don.Id;
        }

        // Fonction permettant de modifier un don. 
        public void ModifierDon(int id, int montant, TypeRecurrence recurrence)
        {
            Don don = _bddContext.Dons.Find(id);
            if (don != null)
            {
                don.Montant = montant;
                don.Recurrence = recurrence;
                _bddContext.SaveChanges();
            }
        }

        // Fonction permettant de supprimer un don. 
        public void SupprimerDon(int id)
        {
            Don don = _bddContext.Dons.Find(id);

            if (don != null)
            {
                _bddContext.Dons.Remove(don);
                _bddContext.SaveChanges();
            }
        }

        public List<Donateur> ObtenirDonateurs()
        {
            return _bddContext.Donateurs.ToList();
        }

        //Fonction permettant d'obtenir un donateurs à partir de son Id
        public Donateur ObtenirDonateur(int id)
        {
            return _bddContext.Donateurs.Find(id);
        }

        //Fonction permettant de créer un donateur
        public int CreerDonateur(Adresse adresse, string telephone, int? useraccountId)
        {

            Donateur donateur = new Donateur() { AdresseFacuration = adresse, Telephone = telephone, UserAccountId = useraccountId };
            _bddContext.Donateurs.Add(donateur);
            _bddContext.SaveChanges();
            return donateur.Id;
        }

        // Fonction permettant de modifier un donateur. 
        public void ModifierDonateur(int id, Adresse adresse, string telephone)
        {
            Donateur donateur = _bddContext.Donateurs.Find(id);
            if (donateur != null)
            {
                donateur.AdresseFacuration = adresse;
                donateur.Telephone = telephone;
                _bddContext.SaveChanges();
            }
        }

        // Fonction permettant de supprimer un donateur. 
        public void SupprimerDonateur(int id)
        {
            Donateur donateur = _bddContext.Donateurs.Find(id);

            if (donateur != null)
            {
                _bddContext.Donateurs.Remove(donateur);
                _bddContext.SaveChanges();
            }
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
