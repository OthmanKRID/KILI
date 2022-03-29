using Kili.Models.General;
using Microsoft.EntityFrameworkCore;
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
            return _bddContext.Associations.Include(A => A.Adresse).Include(A => A.Abonnement).ThenInclude(A => A.ServiceDon).ThenInclude(SD => SD.Collectes).ToList();
        }

        public List<Association> ObtenirAssociationsParLocalisation(string Localisation)
        {

            List<Association> listeTouteAssociation = ObtenirAssociations();
            List<Association> listeParVille = new List<Association>();

            foreach (Association association in listeTouteAssociation)
            {
                //Adresse adresse = _adresseService.ObtenirAdresse((int)association.AdresseId);
                if (association.Adresse.Ville.Equals(Localisation))
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
        public int CreerAssociation(string nomAsso, Adresse adresse, ThemeAssociation Theme, UserAccount compte/*, int? IdCompteAsso*/)
        {
            Association Association = new Association() { Nom = nomAsso, Adresse = adresse, Theme = Theme, Actif = false};         
            _bddContext.Associations.Add(Association);
            _bddContext.SaveChanges();
            compte.AssociationId = Association.Id;
            _UserAccount_Services.ModifierUserAccount(compte.Id, compte.Prenom, compte.Nom, compte.Mail, compte.Role, compte.AssociationId, compte.DonateurId);
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
            UserAccount compteAssocie = _bddContext.UserAccounts.Where(UA=>UA.AssociationId == Association.Id).FirstOrDefault();
            if (Association != null)
            {
                _bddContext.Associations.Remove(Association);
                _UserAccount_Services.ModifierUserAccount(compteAssocie.Id, compteAssocie.Prenom, compteAssocie.Nom, compteAssocie.Mail, compteAssocie.Role, null, compteAssocie.DonateurId);
                _bddContext.SaveChanges();              
            }
        }

          public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
