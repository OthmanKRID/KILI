using Kili.Models.General;
using Kili.Models.GestionAdhesion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;
using static Kili.Models.GestionAdhesion.Adhesion;

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

        public List<Adhesion> ObtenirTypeAdhesions(ServiceAdhesion serviceAdhesion)
        {
            return _bddContext.Adhesions.Where(m => m.ServiceAdhesionId == serviceAdhesion.Id).ToList();
        }

        public List<Adhesion> ObtenirTypeAdhesions()
        {
            return _bddContext.Adhesions.ToList();
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

        
        public int CreerAdherent(uint numeroAdherent, int adresseId)
        {
            Adherent Adherent = new Adherent() { NumeroAdherent = numeroAdherent, AdresseID = adresseId, Actif = true };

            _bddContext.Adherents.Add(Adherent);
            _bddContext.SaveChanges();
            return Adherent.Id;
        }

        public void ModifierAdherent(int id, uint numeroAdherent, bool activation, int? adresseId)
        {
            Adherent Adherent = _bddContext.Adherents.Find(id);
            if (Adherent != null)
            {
                Adherent.NumeroAdherent = numeroAdherent;
                Adherent.Actif = activation;
                Adherent.AdresseID = adresseId;
                _bddContext.SaveChanges();
            }

        }

        public void SupprimerAdherent(int id)
        {
            Adherent Adherent = _bddContext.Adherents.Find(id);

            if (Adherent != null)
            {
                _bddContext.Adherents.Remove(Adherent);
                _bddContext.SaveChanges();
            }
        }

        public int CreerAdhesion(string nom, double prix, int duree, string description, TypeDureeAdhesion typeAdhesion, int? serviceAdhesionId)
        {
            Adhesion adhesion = new Adhesion() { nom = nom, prix = prix, duree = duree, description = description, typeAdhesion = typeAdhesion, ServiceAdhesionId = serviceAdhesionId };

            _bddContext.Adhesions.Add(adhesion);
            _bddContext.SaveChanges();
            return adhesion.Id;

        }

        public void ModifierAdhesion(int id, string nom, double prix, int duree, string description, TypeDureeAdhesion typeAdhesion)
        {
            Adhesion Adhesion = _bddContext.Adhesions.Find(id);
            if (Adhesion != null)
            {
                Adhesion.nom = nom;
                Adhesion.prix = prix;
                Adhesion.duree = duree;
                Adhesion.description = description;
                Adhesion.typeAdhesion = typeAdhesion;
                _bddContext.SaveChanges();
            }

        }

        public void SupprimerAdhesion(int id)
        {
            Adhesion Adhesion = _bddContext.Adhesions.Find(id);

            if (Adhesion != null)
            {
                _bddContext.Adhesions.Remove(Adhesion);
                _bddContext.SaveChanges();
            }
        }

        //Fonction permettant de faire adhérer un adhérent à un type d'adhésion
        public int AdhererAdhesion(int adherentId, Adhesion adhesion)
        {
            Cotisation cotisation = new Cotisation() { AdherentId = adherentId, AdhesionId = adhesion.Id, dateAdhesion = DateTime.Today, dateFin = DateTime.Now.AddDays(adhesion.duree) };

            _bddContext.Cotisations.Add(cotisation);
            _bddContext.SaveChanges();
            return cotisation.Id;
        }

        public void SupprimerCotisation(int id)
        {
            Cotisation cotisation = _bddContext.Cotisations.Find(id);

            if (cotisation != null)
            {
                _bddContext.Cotisations.Remove(cotisation);
                _bddContext.SaveChanges();
            }
        }



        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
