using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;

namespace Kili.Models
{
    public class Adresse_Services
    {
        private BddContext _bddContext;
        public Adresse_Services()
        {
            _bddContext = new BddContext();
        }


        //Fonction permettant d'obtenir la liste de tout les Associations
       public List<Adresse> ObtenirAdresses()
        {
            return _bddContext.Adresses.ToList();
        }

        //Fonction permettant d'obtenir une association à partir de son Id
        public Adresse ObtenirAdresse(int id)
        {
            return _bddContext.Adresses.Find(id);
        }

        //Fonction permettant d'obtenir une association à partir de son Id en format string
        public Adresse ObtenirAdresse(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirAdresse(id);
            }
            return null;
        }

        //Fonction permettant de créer une association
        /* public int CreerAssociation(string nomAsso, Adresse adresse, ThemeAssociation Theme, int? IdCompteAsso)
         {
             //UserAccount_Services compteAssoUserService = new UserAccount_Services();
             //int id = compteAssoUserService.CreerUserAccount(compteAsso.UserName, compteAsso.Password, compteAsso.Mail, TypeRole.Association);
             Association Association = new Association() { Nom = nomAsso, Adresse = adresse, Theme = Theme, Actif = false, UserAccountId= IdCompteAsso };

             _bddContext.Associations.Add(Association);
             _bddContext.SaveChanges();
             return Association.Id;
         }
        */
        //Fonction permettant de modifier une Adresse
        public void ModifierAdresse(int? id, Adresse NouvelleAdresse)
        {
            Adresse Adresse = _bddContext.Adresses.Find(id);

            if (Adresse != null)
            {
                Adresse.Numero = NouvelleAdresse.Numero;
                Adresse.Voie = NouvelleAdresse.Voie;
                Adresse.Complement = NouvelleAdresse.Complement;
                Adresse.CodePostal = NouvelleAdresse.CodePostal;
                Adresse.Ville = NouvelleAdresse.Ville;
                _bddContext.SaveChanges();
            }
        }
        /*
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
        */

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
