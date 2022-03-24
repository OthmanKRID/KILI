using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kili.Models.Vente;
using Microsoft.AspNetCore.Mvc;
using Kili.Models;
using Kili.Models.General;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Features;
using Owin;
using Microsoft.AspNetCore.Identity;
using System.Web.Http;
using System.Threading;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Kili.Models.Services.Vente_Services
{
    public class PanierServices : IDisposable
    {
        public string ShoppingID { get; set; }
        private BddContext _bddContext = new BddContext();

        public const string CleSessionPanier = "PanierID";

        public void AjouterDansPanier(int id)
        {
            //Récupérer le produit de la base de données
            ShoppingID = ObtenirArticleID();

            var articlePanier = _bddContext.Paniers.SingleOrDefault(
                a => a.PanierID == ShoppingID
                && a.ProduitID == id);
            if (articlePanier == null)
            {
                // Créer un nouvel article de panier si aucun article de panier n'existe.
                articlePanier = new Panier
                {
                    ArticleID = Guid.NewGuid().ToString(),
                    ProduitID = id,
                    PanierID = ShoppingID,
                    Produit = _bddContext.Produits.SingleOrDefault(
                        p => p.ProduitID == id),
                    Quantite = 1,
                    DateCreation = DateTime.Now
                };
                _bddContext.Paniers.Add(articlePanier);
            }
            else
            {
                // Si l'article existe dans le panier, en rajouter un à la quantité.
                articlePanier.Quantite++;
            }
            _bddContext.SaveChanges();
        }
        public void Dispose()
        {
            if (_bddContext != null)
            {
                _bddContext.Dispose();
                _bddContext = null;
            }
        }
        public string ObtenirArticleID()
        {
            if(HttpContext.Session
        }
    }
}
