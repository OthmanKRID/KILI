using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;

namespace Kili.Models.Services
{
    public class LivraisonServices
    {
        private BddContext _bddContext;

        public LivraisonServices()
        {
            _bddContext = new BddContext();
        }

        //Fonction permettant d'obtenir la liste des Livraisons possibles
        public List<Livraison>ObtenirAllLivraisons()
        {
            return _bddContext.Livraisons.ToList();
        }

        //Fonction permettant d'obtenir une Livraison à partir de son Id
        public Livraison ObtenirLivraison(int id)
        {
            return _bddContext.Livraisons.Find(id);
        }
    }



}
