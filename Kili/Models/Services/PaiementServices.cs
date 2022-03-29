using Kili.Models.Dons;
using Kili.Models.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kili.Models.Services
{
    public class PaiementServices
    {
        private BddContext _bddContext;
        private UserAccount_Services UserAccount_Services;
        public PaiementServices()
        {
            _bddContext = new BddContext();
            UserAccount_Services = new UserAccount_Services();
        }

        //Fonction permettant d'obtenir la liste de tous les Paiements
        public List<Paiement> ObtenirPaiements()
        {
            return _bddContext.Paiements.ToList();
        }

        //Fonction permettant d'obtenir un Paiement à partir de son Id
        public Paiement ObtenirPaiement(int id)
        {
            return _bddContext.Paiements.Include(p => p.Don).ThenInclude(Do=>Do.Collecte).ThenInclude(Co => Co.ServiceDon).ThenInclude(Sd => Sd.Abonnement).ThenInclude(Ab => Ab.Association).FirstOrDefault(p => p.Id == id);
        }


        //Fonction permettant d'otenir la liste des paiements du useraccount

        public ICollection<Paiement> ObtenirPaiementsDuCompteConnecte(string IdUSer)
        {
            ICollection < Paiement > paiements = new List<Paiement >();

            foreach (Don don in UserAccount_Services.ObtenirUserAccount(IdUSer).Donateur.Dons)
            {
                Paiement paiement = don.Paiement;
                paiements.Add(paiement);
            }

            return paiements;
        }


        //Fonction permettant de créer un Paiement
        public int CreerPaiement(int montant, int moyenPaiementID)
        {

            Paiement Paiement = new Paiement() { Montant = montant, MoyenPaiementId =moyenPaiementID,  DatePaiement = System.DateTime.Today };
            _bddContext.Paiements.Add(Paiement);
            _bddContext.SaveChanges();
            return Paiement.Id;
        }

        // Fonction permettant de modifier un Paiement. 
        public void ModifierPaiement(int id, int montant, int associationBeneficiaireID)
        {
            Paiement Paiement = _bddContext.Paiements.Find(id);
            if (Paiement != null)
            {
                Paiement.Montant = montant;
                Paiement.AssociationId = associationBeneficiaireID;
                _bddContext.SaveChanges();
            }
        }

        // Fonction permettant de supprimer un Paiement. 
        public void SupprimerPaiement(int id)
        {
            Paiement Paiement = _bddContext.Paiements.Find(id);

            if (Paiement != null)
            {
                _bddContext.Paiements.Remove(Paiement);
                _bddContext.SaveChanges();
            }
        }

        public List<MoyenPaiement> ObtenirMoyenPaiements()
        {
            return _bddContext.MoyenPaiements.ToList();
        }

        //Fonction permettant d'obtenir un MoyenPaiement à partir de son Id
        public MoyenPaiement ObtenirMoyenPaiement(int id)
        {
            return _bddContext.MoyenPaiements.FirstOrDefault(p => p.Id == id);
        }

        //Fonction permettant de créer un Paiement
        public int CreerMoyenPaiement(string identifiant)
        {

            MoyenPaiement moyenPaiement = new MoyenPaiement() { Identifiant = identifiant, moyenPaiement = MoyenPaiement.TypeMoyenPaiement.Paypal };
            _bddContext.MoyenPaiements.Add(moyenPaiement);
            _bddContext.SaveChanges();
            return moyenPaiement.Id;
        }

        public int CreerMoyenPaiement(string nom, string numero, string crypto, string date)
        {

            MoyenPaiement moyenPaiement = new MoyenPaiement() { NomTitulaire = nom, Numero = numero, Cryptogramme = crypto, DateExpiration = date, moyenPaiement = MoyenPaiement.TypeMoyenPaiement.CarteBancaire };
            _bddContext.MoyenPaiements.Add(moyenPaiement);
            _bddContext.SaveChanges();
            return moyenPaiement.Id;
        }


        // Fonction permettant de supprimer un don. 
        public void SupprimerMoyenPaiement(int id)
        {
            MoyenPaiement moyenPaiement = _bddContext.MoyenPaiements.Find(id);

            if (moyenPaiement != null)
            {
                _bddContext.MoyenPaiements.Remove(moyenPaiement);
                _bddContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }


    }
}
