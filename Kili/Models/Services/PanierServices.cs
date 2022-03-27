using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;
using Microsoft.EntityFrameworkCore;

namespace Kili.Models.Services
{
    public class PanierServices
    {
        private BddContext _bddContext;
        public PanierServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerPanier()
        {
            Panier panier = new Panier() { Articles = new List<Article>() };
            _bddContext.Paniers.Add(panier);
            _bddContext.SaveChanges();
            return panier.PanierID;
        }

        public Panier ObtenirPanier(int panierId)
        {
            return _bddContext.Paniers.Include(p=> p.Articles).ThenInclude(xx=> xx.Produit).Where(c=> c.PanierID == panierId).FirstOrDefault();
        }

        public void AjouterArticle(int PanierId, Article article)
        {
            Panier panier = _bddContext.Paniers.Find(PanierId);
            panier.Articles.Add(article);

            _bddContext.SaveChanges();
        }

        public void MettreAjourQuantite(int ArticleID)
        {
            var article = _bddContext.Articles.Find(ArticleID);
            if(article != null)
            {
                article.Quantite += 1;
                _bddContext.SaveChanges();
            }
        }

        public void SupprimerArticle(int PanierID, int ArticleID)
        {
            Panier panier = ObtenirPanier(PanierID);
            Article article = panier.Articles.Where(yy => yy.ArticleId == ArticleID).FirstOrDefault();
            panier.Articles.Remove(article);
            _bddContext.SaveChanges();
        }
    }
}
