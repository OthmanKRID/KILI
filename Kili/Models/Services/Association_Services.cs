using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;

namespace Kili.Models
{
    public class Association_Services
    {
        private BddContext _bddContext;
        private Adresse_Services _adresseService;
        private UserAccount_Services _UserAccount_Services;
        public Association_Services()
        {
            _bddContext = new BddContext();
            _adresseService = new Adresse_Services();
            _UserAccount_Services = new UserAccount_Services();
        }

        //Fonction permettant d'obtenir la liste de tout les Associations
       public List<Association> ObtenirAssociations()
        {
            return _bddContext.Associations.ToList();
        }

        public Association ObtenirAssociationDuCompteConnecte(string idUSer)
        {
            UserAccount compteConnecte = _UserAccount_Services.ObtenirUserAccountConnecte(idUSer);
            return ObtenirAssociations().Where(r => r.UserAccountId == compteConnecte.Id).FirstOrDefault();
        }

        //Fonction permettant d'obtenir la liste de tout les Associations dans une ville
        public List<Association> ObtenirAssociationsParLocalisation(string Localisation)
        {

            List<Association> listeTouteAssociation = ObtenirAssociations();
            List<Association> listeParVille = new List<Association>();

            foreach (Association association in listeTouteAssociation)
            {
                Adresse adresse = _adresseService.ObtenirAdresse((int)association.AdresseId);
                if (adresse.Ville.Equals(Localisation))
                {
                    listeParVille.Add(association);
                }
            }
            return listeParVille;
        }

        //Fonction permettant d'obtenir la liste de tout les Associations associées à un thème
        public List<Association> ObtenirAssociationsParTheme(RechercheTheme Theme)
        {
            string sTheme = Theme.ToString();
            List<Association> listesAssociation = _bddContext.Associations.ToList();
            List<Association> listeTriee = new List<Association>();
            foreach (Association association in listesAssociation)
            {
                if (association.Theme.ToString().Equals(sTheme))
                {
                    listeTriee.Add(association);
                }

            }
            return listeTriee;
        }

        //Fonction permettant d'obtenir la liste de tout les Associations associées à un thème et une ville
        public List<Association> ObtenirAssociationsParThemeEtVille(string Localisation, RechercheTheme Theme)
        {
            string sTheme = Theme.ToString();
            List<Association> listetriee = new List<Association>();
            List<Association> listeParVille = ObtenirAssociationsParLocalisation(Localisation);
            foreach (Association association in listeParVille)
            {
                if (association.Theme.ToString().Equals(sTheme))
                {
                    listetriee.Add(association);
                }

            }
            return listetriee;
        }

        //Fonction permettant d'obtenir une association à partir de son Id
        public Association ObtenirAssociation(int id)
        {
            return _bddContext.Associations.Find(id);
        }

        //Fonction permettant d'obtenir une association à partir de son Id en format string
        public Association ObtenirAssociation(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirAssociation(id);
            }
            return null;
        }

        //Fonction permettant de créer une association
        public int CreerAssociation(string nomAsso, Adresse adresse, ThemeAssociation Theme, int? IdCompteAsso)
        {
            Association Association = new Association() { Nom = nomAsso, Adresse = adresse, Theme = Theme, Actif = false, UserAccountId= IdCompteAsso };
            Abonnement_Services abonnement_services = new Abonnement_Services();
            _bddContext.Associations.Add(Association);
            _bddContext.SaveChanges();
            return Association.Id;
        }

        //Fonction permettant de modifier une association
        public void ModifierAssociation(int id, string nomAsso, Adresse adresse, ThemeAssociation Theme)
        {
            Association Association = _bddContext.Associations.Find(id);

            if (Association != null)
            {
                Association.Nom = nomAsso;
                Association.Theme = Theme;

                Adresse_Services Adresse_Services = new Adresse_Services();
                Adresse_Services.ModifierAdresse(Association.AdresseId, adresse);
                //Association.Adresse = adresse;
                _bddContext.SaveChanges();
            }
        }

        //Fonction permettant d'activer une association
        public void ActiverAssociation(int id)
        {
            Association Association = _bddContext.Associations.Find(id);

            if (Association != null)
            {
                Association.Actif = true;
                _bddContext.SaveChanges();
            }
        }

        //Fonction permettant de désactiver une association
        public void DésactiverAssociation(int id)
        {
            Association Association = _bddContext.Associations.Find(id);

            if (Association != null)
            {
                Association.Actif = false;
                _bddContext.SaveChanges();
            }
        }

        public void SupprimerAssociation(int id)
        {
            Association Association = _bddContext.Associations.Find(id);

            if (Association != null)
            {
                _bddContext.Associations.Remove(Association);
                _bddContext.SaveChanges();
            }
        }

          public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
