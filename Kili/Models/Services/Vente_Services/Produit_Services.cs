using System;
using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;
using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services.Vente_Services
{
    public class Produit_Services
    {
            private BddContext _bddContext;
            public Produit_Services()
            {
                _bddContext = new BddContext();
            }
            //Fonction permettant d'obtenir la liste de tous les produits
            public List<Produit> ObtenirAllProduits()
            {
                return _bddContext.Produits.ToList();
            }

            //Fonction permettant de créer un Produit
            public int CreerProduit(string designationproduit, string formatproduit, string descriptionproduit, double prixunitaireproduit, string deviseproduit, int catalogueidproduit)
            {
                Produit produit = new Produit()
                {
                    Designation = designationproduit,
                    Format = formatproduit,
                    Description = descriptionproduit,
                    PrixUnitaire = prixunitaireproduit,
                    Devise = deviseproduit,
                    CatalogueID = catalogueidproduit
                };
                _bddContext.Produits.Add(produit);
                _bddContext.SaveChanges();
                return produit.ProduitID;
            }

            //Fonction permettant d'obtenir un catalogue à partir de son Id
            public Produit ObtenirProduit(int id)
            {
                return _bddContext.Produits.Find(id);
            }

            //Fonction permettant d'obtenir un Produit à partir de son Id en format string
            public Produit ObtenirProduit(string idStr)
            {
                int id;
                if (int.TryParse(idStr, out id))
                {
                    return this.ObtenirProduit(id);
                }
                return null;
            }
            public void Dispose()
            {
                _bddContext.Dispose();
            }
        }
    }

