using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;
namespace Kili.Models.Services
{
    public class CatalogueServices
    {
        private BddContext _bddContext;
        public CatalogueServices()
        {
            this._bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //Fonction permettant d'obtenir la liste de tous les catalogues

        public List<Catalogue> ObtenirAllCatalogues()
        {
            List<Catalogue> catalogues = this._bddContext.Catalogues.ToList();
            return catalogues;
        }

        //Fonction permettant d'obtenir un catalogue à partir de son Id
        public Catalogue ObtenirCatalogue(int id)
        {
            return this._bddContext.Catalogues.Find(id);
        }

        //Fonction permettant d'obtenir un catalogue à partir de son Id en format string
        public Catalogue ObtenirCatalogue (string idstr)
        {
            int id;
            if (int.TryParse(idstr, out id))
            {
                return this.ObtenirCatalogue(id);
            }
            return null;
        }

        //Fonction permettant de créer un Catalogue
        public int CreerCatalogue(string cataloguename, string cataloguedescription)  
        {
            Catalogue catalogue = new Catalogue()
            {
                CatalogueName = cataloguename,
                Description = cataloguedescription
            };
            this._bddContext.Catalogues.Add(catalogue);
            this._bddContext.SaveChanges();
            return catalogue.CatalogueID;

        }

        //Fonction permettant de modifier un catalogue
        public void ModifierCatalogue (int catalogueid, string cataloguename, string cataloguedescription)
        {
            Catalogue catalogue = this._bddContext.Catalogues.Find(catalogueid);
            if (catalogue != null)
            {
                catalogue.CatalogueName = cataloguename;
                catalogue.Description = cataloguedescription;
                this._bddContext.SaveChanges();

            }
            
        }

        //Fonction permettant de supprimer un catalogue

        public void SupprimerCatalogue(int id)
        {
            Catalogue catalogue = this._bddContext.Catalogues.Find (id);
            this._bddContext.Catalogues.Remove(catalogue);
            this._bddContext.SaveChanges();
        }

        public void SupprimerCatalogue(string cataloguename, string cataloguedescription)
        {
            Catalogue catalogue = this._bddContext.Catalogues.Where( c => c.CatalogueName == cataloguename && c.Description == cataloguedescription ).FirstOrDefault();
            if(catalogue != null)
            {
                this._bddContext.Catalogues.Remove (catalogue);
                this._bddContext.SaveChanges() ;
            }
        }

        
    }
}
