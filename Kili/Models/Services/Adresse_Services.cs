﻿using Kili.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services
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

        //Fonction permettant de modifier une Adresse
        public int CreerAdresse(uint numero, string voie, string complement, uint cp, string ville)
        {
                Adresse adresse = new Adresse() { Numero = numero, Voie = voie, Complement = complement, CodePostal = cp, Ville = ville };
                _bddContext.Adresses.Add(adresse);         
                _bddContext.SaveChanges();
            
            return adresse.Id;
        }

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

        public void SupprimerAdresse(int id)
        {
            Adresse adresse = _bddContext.Adresses.Find(id);

            if (adresse != null)
            {
                _bddContext.Adresses.Remove(adresse);
                _bddContext.SaveChanges();
            }
        }


        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
