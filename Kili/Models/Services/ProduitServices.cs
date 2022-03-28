using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;

namespace Kili.Models.Services
{
    public class ProduitServices
    {
        private BddContext _bddContext;
        public ProduitServices()
        {
            this._bddContext = new BddContext();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //Fonction permettant d'obtenir la liste de tous les produits
        public List<Produit> ObtenirAllProduits()
        {
            return _bddContext.Produits.ToList();
        }

        //Fonction permettant de créer un Produit
        public int CreerProduit(string designationproduit, string formatproduit, string descriptionproduit, string imagepathproduit, double prixunitaireproduit, string deviseproduit, int catalogueidproduit)
        {
            Produit produit = new Produit()
            {
                Designation = designationproduit,
                Format = formatproduit,
                Description = descriptionproduit,
                ImagePath = imagepathproduit,
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

        //Fonction permettant de modifier un produit
        public void ModifierProduit(int produitid, string designationproduit, string formatproduit, string descriptionproduit, string imagepathproduit, double prixunitaireproduit, string deviseproduit)
        {
            Produit produit = this._bddContext.Produits.Find(produitid);
            if (produit != null)
            {
                produit.Designation = designationproduit;
                produit.Format = formatproduit;
                produit.Description = descriptionproduit;
                produit.ImagePath = imagepathproduit;
                produit.PrixUnitaire = prixunitaireproduit;
                produit.Devise = deviseproduit;

                this._bddContext.SaveChanges();

            }

        }

        //Fonction permettant de supprimer un produit
        public void SupprimerProduit(int id)
        {
            Produit produit = this._bddContext.Produits.Find(id);
            this._bddContext.Produits.Remove(produit);
            this._bddContext.SaveChanges();
        }

        public void SupprimerProduit(string designationproduit, string formatproduit, string descriptionproduit, string imagepathproduit, double prixunitaireproduit, string deviseproduit, int catalogueidproduit)
        {
           Produit produit = this._bddContext.Produits.Where(produit => produit.Designation == designationproduit && produit.Format == formatproduit && produit.Description == descriptionproduit &&
           produit.ImagePath == imagepathproduit && produit.PrixUnitaire == prixunitaireproduit && produit.Devise == deviseproduit && produit.CatalogueID == catalogueidproduit).FirstOrDefault();
            if(produit != null)
            {
                this._bddContext.Produits.Remove(produit);
                this._bddContext.SaveChanges();
            }
        }
    }
}
        
            
    
 

        
  

