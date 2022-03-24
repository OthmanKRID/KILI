using Kili.Models.General;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;
using Microsoft.AspNetCore.Http;

namespace Kili.Models
{
    public class Abonnement_Services
    {
        private BddContext _bddContext;

        private UserAccount_Services UserAccount_Services;
        private Association_Services Association_Services;
        public Abonnement_Services()
        {
            _bddContext = new BddContext();
            UserAccount_Services = new UserAccount_Services();
            Association_Services = new Association_Services();
        }

        
       //Fonction permettant de récupérer l'abonnement d'une Association
       public Abonnement ObtenirAbonnement(Association association)
        {
            return _bddContext.Abonnements.Where(m => m.Id == association.AbonnementId).FirstOrDefault();
        }
        public Abonnement ObtenirAbonnementDuCompteConnecte(string IdUSer)
        {
            UserAccount compteConnecte = UserAccount_Services.ObtenirUserAccount(IdUSer);
            Association association = Association_Services.ObtenirAssociations().Where(r => r.UserAccountId == compteConnecte.Id).FirstOrDefault();
            return ObtenirAbonnement(association);
        }
        //Fonction permettant de créer un abonnement
        public int CreerAbonnement()
         {
             Abonnement abonnement = new Abonnement();                     
             _bddContext.Abonnements.Add(abonnement);
             _bddContext.SaveChanges();
             return abonnement.Id;
         }

        //Supprimer un abonnement à partir de son ID
        public void SupprimerAbonnement(int id)
        {
            Abonnement Abonnement = _bddContext.Abonnements.Find(id);

            if (Abonnement != null)
            {
                _bddContext.Abonnements.Remove(Abonnement);
                _bddContext.SaveChanges();
            }
        }

        //Ajouter une service Adhésion à un abonnement
        // On passe en argument l'ID de l'abonnnement et la duree de l'adhésion au service
        public void AjouterServiceAdhesion(int id, int duree)
        {

            ServiceAdhesion adhesion = new ServiceAdhesion() { IsActive = true, duree = duree, dateAbonnement = DateTime.Today, dateFinAbonnement = DateTime.Today.AddDays(duree) };
            _bddContext.ServicesAdhesion.Add(adhesion);
            _bddContext.SaveChanges();
            Abonnement Abonnement = _bddContext.Abonnements.Find(id);
            if (Abonnement != null)
            {
                Abonnement.ServiceAdhesionId = adhesion.Id;             
            }
            _bddContext.SaveChanges();
        }

        //Supprimer un service d'un Abonnnemnt donnée
        public void SupprimerServiceAdhesion(int id)
        {
            Abonnement Abonnement = _bddContext.Abonnements.Find(id);
            ServiceAdhesion adhesion = _bddContext.ServicesAdhesion.Find(Abonnement.ServiceAdhesionId);
            _bddContext.ServicesAdhesion.Remove(adhesion);
            Abonnement.ServiceAdhesionId = null;
            _bddContext.SaveChanges();
        }


        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
