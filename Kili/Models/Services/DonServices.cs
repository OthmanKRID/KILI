    using Kili.Models.Dons;
using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kili.Models.Services
{
    public class DonServices
    {
        private BddContext _bddContext;
        private UserAccount_Services UserAccount_Services;

        public DonServices() {
        
            _bddContext = new BddContext();
            UserAccount_Services = new UserAccount_Services();
        
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

        public ICollection<Don> ObtenirDonsDuCompteConnecte(string IdUSer)
        {
            return UserAccount_Services.ObtenirUserAccount(IdUSer).Donateur.Dons;
        }


        //Fonction permettant de créer un don
        public int CreerDon(int montant, TypeRecurrence recurrence, int? donateurId, int? collecteId)
        {
            
            Don don = new Don() { Montant = montant, Recurrence = recurrence, DonateurId = donateurId , CollecteId = collecteId, Date = System.DateTime.Today };

            Collecte collecte = _bddContext.Collectes.Find(collecteId);
            if (collecte != null)
            {
                collecte.MontantCollecte += montant;            
                _bddContext.SaveChanges();
            }

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

        public void ModifierDonPaiementID(int id, int paiementID)
        {
            Don don = _bddContext.Dons.Find(id);
            if (don != null)
            {
                don.PaiementId = paiementID;
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

        public Donateur ObtenirDonateurDuCompteConnecte(string IdUSer)
        {
            return UserAccount_Services.ObtenirUserAccount(IdUSer).Donateur;
        }

        //Fonction permettant de créer un donateur
        public int CreerDonateur(int? adresseID)
        {

            Donateur donateur = new Donateur() { AdresseID = adresseID};
            _bddContext.Donateurs.Add(donateur);
            _bddContext.SaveChanges();
            
            return donateur.Id;
        }

        // Fonction permettant de modifier un donateur. 
        public void ModifierDonateur(int id, int? adresseID)
        {
            Donateur donateur = _bddContext.Donateurs.Find(id);
            if (donateur != null)
            {
                donateur.AdresseID = adresseID;             
                _bddContext.SaveChanges();
            }
        }

        // Fonction permettant de supprimer un donateur. 
        public void SupprimerDonateur(int id)
        {
            Donateur donateur = _bddContext.Donateurs.Find(id);            

            if (donateur != null)
            {
                UserAccount compte = _bddContext.UserAccounts.Where(v => v.DonateurId == donateur.Id).FirstOrDefault();
                _bddContext.Donateurs.Remove(donateur);

                UserAccount_Services.ModifierUserAccount(compte.Id, compte.Prenom, compte.Nom, compte.Mail, compte.Telephone, compte.Role, compte.AssociationId, null, compte.AdresseId, compte.ImagePath);

                _bddContext.SaveChanges();
            }
        }

        public List<Collecte> ObtenirCollectes()
        {
            return _bddContext.Collectes.ToList();

        }


        public List<Collecte> Obtenir3DernièresCollectes()
        {
            List<Collecte> collectes = ObtenirCollectes();
            List<Collecte> Dernierescollectes = new List<Collecte>();

            int nb;
            if (collectes.Count <3) {
                nb = collectes.Count;
                    }
            else {
                nb = 3;
                    }

            for (int i = collectes.Count - nb; i < collectes.Count; i++)
            {
                Dernierescollectes.Add(collectes[i]);
            }
            return Dernierescollectes;
        }

        //Fonction permettant d'obtenir une collecte à partir de son Id
        public Collecte ObtenirCollecte(int id)
        {
            return _bddContext.Collectes.Find(id);
        }


        //Fonction permettant de créer une collecte
        public int CreerCollecte(string nom, int montant, string descriptif, int? servicedonID) // , DateTime date
        {

            Collecte collecte = new Collecte() { Nom = nom, MontantCollecte = montant, Descriptif = descriptif, Date = DateTime.Today, ServiceDonId = servicedonID };
            _bddContext.Collectes.Add(collecte);
            _bddContext.SaveChanges();
            return collecte.Id;
        }

        // Fonction permettant de modifier une collecte 
        public void ModifierCollecte(int id, string nom, int montant, string descriptif, DateTime date)
        {
            Collecte collecte = _bddContext.Collectes.Find(id);
            if (collecte != null)
            {
                collecte.Nom = nom;
                collecte.MontantCollecte = montant;
                collecte.Descriptif = descriptif;
                collecte.Date = date;
                _bddContext.SaveChanges();
            }
        }

        // Fonction permettant de supprimer une collecte. 
        public void SupprimerCollecte(int id)
        {
            Collecte collecte = _bddContext.Collectes.Find(id);

            if (collecte != null)
            {
                _bddContext.Collectes.Remove(collecte);
                _bddContext.SaveChanges();
            }
        }



        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
