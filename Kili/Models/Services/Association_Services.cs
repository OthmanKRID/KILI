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
        public Association_Services()
        {
            _bddContext = new BddContext();
        }


        //Fonction permettant d'obtenir la liste de tout les Associations
       public List<Association> ObtenirAssociations()
        {
            return _bddContext.Associations.ToList();
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
            //UserAccount_Services compteAssoUserService = new UserAccount_Services();
            //int id = compteAssoUserService.CreerUserAccount(compteAsso.UserName, compteAsso.Password, compteAsso.Mail, TypeRole.Association);
            Association Association = new Association() { Nom = nomAsso, Adresse = adresse, Theme = Theme, Actif = false, UserAccountId= IdCompteAsso };

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
