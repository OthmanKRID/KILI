using System;
using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;
using static Kili.Models.General.UserAccount;

namespace Kili.Models.Services.Vente_Services
{
    public class Catalogue_Services 
    {
        private BddContext _bddContext;
        public Catalogue_Services()
        {
            _bddContext = new BddContext();
        }
        //Fonction permettant d'obtenir la liste de tous les catalogues
        public List<Catalogue> ObtenirAllCatalogues()
        {
            return _bddContext.Catalogues.ToList();
        }

        //Fonction permettant d'obtenir un catalogue à partir de son Id
        public Catalogue ObtenirCatalogue(int id)
        {
            return _bddContext.Catalogues.Find(id);
        }

        //Fonction permettant d'obtenir un catalogue à partir de son Id en format string
        public Catalogue ObtenirCatalogue(string idstr)
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
            _bddContext.Catalogues.Add(catalogue);
            _bddContext.SaveChanges();
            return catalogue.CatalogueID;
        }

        //Fonction permettant de modifier un catalogue
        public void ModifierCatalogue(int catalogueid, string cataloguename, string cataloguedescription)
        {
            Catalogue catalogue = _bddContext.Catalogues.Find(catalogueid);
            if (catalogue != null)
            {
                catalogue.CatalogueName = cataloguename;
                catalogue.Description = cataloguedescription;
                _bddContext.SaveChanges();
            }
        }

        //Fonction permettant d'activer un catalogue
       /* public void ActiverCatalogue(int catalogueid)
        {
            Catalogue catalogue = _bddContext.Catalogues.Find(catalogueid);
            if(catalogue != null)
            {
                catalogue.Actif = true;
                _bddContext.SaveChanges();
            }
        }
        //Fonction permettant de désactiver un catalogue
        public void DesactiverCatalogue(int catalogueid)
        {
            Catalogue catalogue = _bddContext.Catalogues.Find(catalogueid);
            if (catalogue != null)
            {
                catalogue.Actif = false;
                _bddContext.SaveChanges();
            }
        }*/

        public void SupprimerCatalogue(int catalogueid)
        {
            Catalogue catalogue = _bddContext.Catalogues.Find(catalogueid);
            if (catalogue != null)
            {
                _bddContext.Catalogues.Remove(catalogue);
                _bddContext.SaveChanges();

            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }


    }
}
