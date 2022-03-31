using Kili.Models.General;
using Kili.Models.GestionAdhesion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services
{
    public class Adhesion_Services
    {
        private BddContext _bddContext;
        public Adhesion_Services()
        {
            _bddContext = new BddContext();
        }

       //  Fonctions sur les types d'adhésions             

       //Fonction permettant d'obtenir les type d'adhésion associé à un service Adhésion
       public List<Adhesion> ObtenirTypeAdhesion(ServiceAdhesion serviceAdhesion)
       {
           return _bddContext.Adhesions.Where(m => m.ServiceAdhesionId == serviceAdhesion.Id).ToList();
       }

        //Fonction permettant d'obtenir une adhesion à partir de son Id
        public Adhesion ObtenirTypeAdhesion(int id)
        {
            return _bddContext.Adhesions.Find(id);
        }

        //Fonction permettant d'obtenir la liste de toutes les cotisations à un type d'adhésion
        public List<Cotisation> ObtenirCotisationsPourAdhesion(Adhesion adhesion)
        {
            return _bddContext.Cotisations.Where(m => m.AdhesionId == adhesion.Id).ToList();
        }

        //Fonction permettant d'obtenir la liste de tout les Adhérents à une cotisation
        public List<Adherent> ObtenirAdherentPourAdhesion(List<Cotisation> listecotisation)
        {
            List<Adherent> listeAdherents = new List<Adherent>();
            foreach (Cotisation c in listecotisation)
            {
                listeAdherents.Add(_bddContext.Adherents.Find(c.AdherentId));
            }
            return listeAdherents;
        }

        //Fonction permettant d'obtenir la liste de tout les Adhérents à un type d'adhésion
        public List<Adherent> ObtenirAdhérentsPourUnTypeAdhesion(Adhesion adhesion)
        {
            return ObtenirAdherentPourAdhesion(ObtenirCotisationsPourAdhesion(adhesion));
        }

        //Fonction permettant d'obtenir une association à partir de son Id
        public int CreerAdherent(uint numeroAdherent, Adresse adresse, string telephone)
        {
            Adherent Adherent = new Adherent() { NumeroAdherent = numeroAdherent, Adresse = adresse, Actif = true, Telephone = telephone };

            _bddContext.Adherents.Add(Adherent);
            _bddContext.SaveChanges();
            return Adherent.Id;
        }

        //Fonction permettant de faire adhérer un adhérent à un type d'adhésion
        public int AdhererAdhesion(int adherentId, Adhesion adhesion)
        {
            Cotisation cotisation = new Cotisation() { AdherentId = adherentId, AdhesionId = adhesion.Id, dateAdhesion = DateTime.Today, dateFin = DateTime.Now.AddDays(adhesion.duree) };

            _bddContext.Cotisations.Add(cotisation);
            _bddContext.SaveChanges();
            return cotisation.Id;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
