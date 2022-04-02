using Kili.Models.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Kili.Models.Services;

using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services
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
            return _bddContext.Associations.Include(A => A.Adresse).Include(A => A.Abonnement).ThenInclude(A => A.serviceDon).ThenInclude(SD => SD.Collectes).ToList();
        }

        public List<Association> Obtenir3DernièresAssociations()
        {
            List<Association> associations = ObtenirAssociations();
            List<Association> Dernieresassociations = new List<Association>();

            int nb;
            if (associations.Count < 3)
            {
                nb = associations.Count;
            }
            else
            {
                nb = 3;
            }

            for (int i = associations.Count - nb; i < associations.Count ; i++)
            {
                Dernieresassociations.Add(associations[i]);
            }
            return Dernieresassociations;
        }

        public List<Association> ObtenirAssociationsParLocalisation(string Localisation)
        {
            return _bddContext.Associations.Where(a => a.Adresse.Ville.Contains(Localisation)).Include(A => A.Adresse).ToList();
        }

        public List<Association> ObtenirAssociationsParNom(string nom)
        {
            return _bddContext.Associations.Where(a => a.Nom.Contains(nom)).Include(A => A.Adresse).ToList();
        }

        //Fonction permettant d'obtenir la liste de tout les Associations associées à un thème
        public List<Association> ObtenirAssociationsParTheme(RechercheTheme Theme)
        {
            ThemeAssociation Themerecherche = (ThemeAssociation)Enum.Parse(typeof(ThemeAssociation), Theme.ToString());
            return _bddContext.Associations.Where(a => a.Theme.Equals(Themerecherche)).Include(A => A.Adresse).ToList();
        }

        //Fonction permettant d'obtenir la liste de tout les Associations associées à un thème et une ville
        public List<Association> ObtenirAssociationsParThemeEtVille(string Localisation, RechercheTheme Theme)
        {
            ThemeAssociation Themerecherche = (ThemeAssociation)Enum.Parse(typeof(ThemeAssociation), Theme.ToString());
            return _bddContext.Associations.Where(a => a.Theme.Equals(Themerecherche)).Where(a => a.Adresse.Ville.Equals(Localisation)).Include(A => A.Adresse).ToList();        
        }

        public List<Association> ObtenirAssociationsParThemeEtVilleEtNom(string Localisation, string nom, RechercheTheme Theme)
        {
            ThemeAssociation Themerecherche = (ThemeAssociation)Enum.Parse(typeof(ThemeAssociation), Theme.ToString());
            return _bddContext.Associations.Where(a => a.Theme.Equals(Themerecherche)).Where(a => a.Adresse.Ville.Equals(Localisation)).Include(A => A.Adresse).Where(a => a.Nom.Contains(nom)).ToList();
        }

        public List<Association> ObtenirAssociationsParThemeEtNom(string nom, RechercheTheme Theme)
        {
            ThemeAssociation Themerecherche = (ThemeAssociation)Enum.Parse(typeof(ThemeAssociation), Theme.ToString());
            return _bddContext.Associations.Where(a => a.Theme.Equals(Themerecherche)).Where(a => a.Nom.Contains(nom)).Include(A => A.Adresse).ToList();
        }

        public List<Association> ObtenirAssociationsParVilleEtNom(string Localisation, string nom)
        {
            List<Association> listetriee = _bddContext.Associations.Where(a => a.Adresse.Ville.Equals(Localisation)).Include(A => A.Adresse).Where(a => a.Nom.Contains(nom)).ToList();
            return listetriee;
        }


        //Fonction permettant d'obtenir une association à partir de son Id
        public Association ObtenirAssociation(int id)
        {
            return _bddContext.Associations.Include(a => a.Adresse).Include(a => a.Abonnement).ThenInclude(abo => abo.serviceBoutique).Include(a => a.Abonnement).ThenInclude(abo => abo.serviceAdhesion).Include(a => a.Abonnement).ThenInclude(abo => abo.serviceDon).ThenInclude(s => s.Collectes).FirstOrDefault(x => x.Id == id); ;
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
        public int CreerAssociation(string nomAsso, Adresse adresse, ThemeAssociation Theme, UserAccount compte)
        {
            Association Association = new Association() { Nom = nomAsso, Adresse = adresse, Theme = Theme, Actif = false, ImagePath = "/images/Associations/AssociationDefaut.png" };
            _bddContext.Associations.Add(Association);
            _bddContext.SaveChanges();
            compte.AssociationId = Association.Id;

            _UserAccount_Services.ModifierUserAccount(compte.Id, compte.Prenom, compte.Nom, compte.Mail, compte.Telephone, TypeRole.Association, compte.AssociationId, compte.DonateurId, compte.AdresseId, compte.ImagePath);

            return Association.Id;
        }

        //Fonction permettant de modifier une association
        public void ModifierAssociation(Association Association)
        {
            Association AssociationModifiee = _bddContext.Associations.Find(Association.Id);

            if (Association != null)
            {
                AssociationModifiee.Nom = Association.Nom;
                AssociationModifiee.Theme = Association.Theme;
                AssociationModifiee.Description = Association.Description;
                AssociationModifiee.ImagePath = Association.ImagePath;
                new Adresse_Services().ModifierAdresse(Association.AdresseId, Association.Adresse);
                _bddContext.SaveChanges();
            }
        }

        public void ModifierAssociationDescriptifImage(int id, string description, string image)
        {
            Association AssociationModifiee = _bddContext.Associations.Find(id);

            if (AssociationModifiee != null)
            {
                AssociationModifiee.Description = description;
                AssociationModifiee.ImagePath = image;
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
            UserAccount compteAssocie = _bddContext.UserAccounts.Where(UA => UA.AssociationId == Association.Id).FirstOrDefault();
            if (Association != null)
            {
                _bddContext.Associations.Remove(Association);

                _UserAccount_Services.ModifierUserAccount(compteAssocie.Id, compteAssocie.Prenom, compteAssocie.Nom, compteAssocie.Mail, compteAssocie.Telephone, compteAssocie.Role, null, compteAssocie.DonateurId, compteAssocie.AdresseId, compteAssocie.ImagePath);

                _bddContext.SaveChanges();              
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /*public void UploaderPhoto()
        {
            try
            {
                OpenFileDialog dialog
            }
            catch(Exception)
            {

            }
        }*/


    }
}
