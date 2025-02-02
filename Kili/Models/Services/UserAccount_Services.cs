﻿using Kili.Models.Dons;
using Kili.Models.General;
using Kili.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services
{
    public class UserAccount_Services
    {
        private BddContext _bddContext;
        public UserAccount_Services()
        {
            _bddContext = new BddContext();
        }


        //Fonction permettant d'obtenir la liste de tout les UserAccount
       public List<UserAccount> ObtenirUserAccounts()
        {
            return _bddContext.UserAccounts.ToList();
        }

        //Fonction permettant d'obtenir un UserAccount à partir de son Id
        public UserAccount ObtenirUserAccount(int id)
        {

            return _bddContext.UserAccounts.Include(UA => UA.Association).ThenInclude(A => A.Adresse).Include(UA => UA.Association).ThenInclude(A => A.Abonnement).ThenInclude(A => A.serviceAdhesion).Include(UA => UA.Association).ThenInclude(A => A.Abonnement).ThenInclude(A => A.serviceBoutique).Include(UA => UA.Association).ThenInclude(A => A.Abonnement).ThenInclude(A => A.serviceDon).ThenInclude(SD => SD.Collectes).Include(UA => UA.Donateur).ThenInclude(DO => DO.Dons).FirstOrDefault(UA => UA.Id == id);

        }

        //Fonction permettant d'obtenir un UserAccount à partir de son Id
        public UserAccount ObtenirUserAccountConnecte(string idUser)
        {
            return ObtenirUserAccount(idUser);
        }

        //Fonction permettant d'obtenir un UserAccount à partir de son Id en format string
        public UserAccount ObtenirUserAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirUserAccount(id);
            }
            return null;
        }

        //Fonction permettant d'encoder un mot de passe
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Kili" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        //Fonction permettant de créer un UserAccount
        public int CreerUserAccount(string prenom, string nom, string password, string email, TypeRole role)
        {
            string motDePasse = EncodeMD5(password);

            UserAccount userAccount = new UserAccount() { Prenom = prenom, Nom = nom, Password = motDePasse, Mail = email, DateCreation = System.DateTime.Today, Actif = true, Role = role, ImagePath = "/images/UserAccount/logo.png" };

            _bddContext.UserAccounts.Add(userAccount);
            _bddContext.SaveChanges();
            return userAccount.Id;
        }


        public int CreerUtilisateur(string prenom, string nom, string password, string email)
        {
            return CreerUserAccount(prenom, nom, password, email, TypeRole.Utilisateur);
        }

        public int CreerAdmin(string prenom, string nom, string password, string email)
        {
            return CreerUserAccount(prenom, nom, password, email, TypeRole.Admin);
        }

        public int CreerUserAccountAssociation(string prenom, string nom, string password, string email)
        {
            return CreerUserAccount(prenom, nom, password, email, TypeRole.Association);      
        }


        public void ModifierUserAccount(int id, string prenom, string nom, string email, string telephone, TypeRole role, int? AssociationId, int? donateurID, int? adresseID, string ImagePath)

        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(id);

            if (userAccount != null)
            {
                userAccount.Prenom = prenom;
                userAccount.Nom = nom;
                userAccount.Mail = email;
                userAccount.Telephone = telephone;
                userAccount.Role = role;
                userAccount.AssociationId = AssociationId;
                userAccount.DonateurId = donateurID;
                userAccount.AdresseId = adresseID;
                userAccount.ImagePath = ImagePath;

                _bddContext.SaveChanges();
            }
        }

        public void ModifierMotDePasse(int id, string password)
        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(id);

            if (userAccount != null)
            {

                string motDePasse = EncodeMD5(password);

                userAccount.Password = motDePasse;

                _bddContext.SaveChanges();
            }
        }

        /*
        // Fonction servant notamment à ajouter un donateur au UserAccount lors de la création du donateur au moment de la création du don
        public void AjouterDonateur(int idUserAccount, int idDonateur)
        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(idUserAccount);

            if (userAccount != null)
            {
                DonServices donServices = new DonServices();
                userAccount.Donateur = new Donateur();
                userAccount.Donateur = (donServices.ObtenirDonateur(idDonateur));
                _bddContext.SaveChanges();
            }
        }
        */
        public void SupprimerUserAccount(int id)
        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(id);

            if (userAccount != null)
            {
                _bddContext.UserAccounts.Remove(userAccount);
                _bddContext.SaveChanges();
            }
        }

        public void DésactiverUserAccount(int id)
        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(id);

            if (userAccount != null)
            {
                userAccount.Actif = false;
                _bddContext.SaveChanges();
            }
        }

        public UserAccount Authentifier(string email, string password)
        {
            string motDePasse = EncodeMD5(password);

            UserAccount user = this._bddContext.UserAccounts.FirstOrDefault(u => u.Mail == email && u.Password == motDePasse);

            return user;
        }

         public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
