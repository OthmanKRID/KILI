using System;
using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;

namespace Kili.Models.Services.Vente_Services
{
    public class Panier_Services
    {
        private BddContext _bddContext;
        public Panier_Services()
        {
            _bddContext = new BddContext();
        }

        //Fonction permettant d'obtenir la liste de tous les paniers
        public List<Panier> ObtenirAllPaniers()
        {
            return _bddContext.Paniers.ToList();
        }

        //Fonction permettant d'obtenir un panier à partir de son Id
        public Panier ObtenirPanier(int id)
        {
            return _bddContext.Paniers.Find(id);
        }

        //Fonction permettant d'obtenir un panier à partir de son Id en format string
        public Panier ObtenirPanier(string idstr)
        {
            int id;
            if (int.TryParse(idstr, out id))
            {
                return this.ObtenirPanier(id);
            }
            return null;
        }

        //Fonction permettant de créer un panier
        public string CreerPanier(int quantitePanier, DateTime dateCreationPanier, int produitIDpanier)
        {
            Panier panier = new Panier()
            {
                Quantite = quantitePanier,
                DateCreation = dateCreationPanier,
                ProduitID = produitIDpanier
            };
            _bddContext.Paniers.Add(panier);
            _bddContext.SaveChanges();
            return panier.ArticleID;
            
        }
    }
}

