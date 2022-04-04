using System.Collections.Generic;
using System.Linq;
using Kili.Models.Vente;
using Kili.Models.General;
using System;
using Microsoft.EntityFrameworkCore;

namespace Kili.Models.Services
{
    public class CommandeServices
    {
        private BddContext _bddContext;

        public CommandeServices()
        {
            _bddContext = new BddContext();
        }

        //Fonction permettant d'obtenir la liste de toutes les commandes
        public List<Commande> ObtenirAllCommandes()
        {
            return _bddContext.Commandes.ToList();
        }

        public int CreerCommande(Panier panier)
        {
            Commande commande = new Commande() { PanierID = panier.PanierID };
            _bddContext.Commandes.Add(commande);
            _bddContext.SaveChanges();
            return commande.CommandeID;

        }

        public Commande ObtenirCommande(int commandeID)
        {
            return _bddContext.Commandes.Include(c => c.Panier).ThenInclude(pp => pp.Articles).ThenInclude(xx => xx.Produit).Where(c => c.CommandeID == commandeID).FirstOrDefault();
        }

        public void AjouterPanierCommander(int commandeID, Panier panier)
        {
            Commande commande = _bddContext.Commandes.Find(commandeID);
            commande.Panier = panier;

            _bddContext.SaveChanges();
        }

        //Fonction permettant de créer les coordonnées d'un acheteur

        public int CreerCoordonnees(UserAccount useraccount, Adresse adresselivraison, Adresse adressefacturation)
        {

            CoordonneesAcheteur coordonneesAcheteur = new CoordonneesAcheteur() { Useraccount = useraccount, AdresseLivraison = adresselivraison, AdresseFacturation = adressefacturation };
            _bddContext.CoordonneesAcheteurs.Add(coordonneesAcheteur);
            _bddContext.SaveChanges();
            return coordonneesAcheteur.CoordonneesAcheteurID;
        }

        /*public int CreerCoordonnées(int? adresseID)
        {

            CoordonneesAcheteur coordonneesAcheteur = new CoordonneesAcheteur() { AdresseID = adresseID };
            _bddContext.CoordonneesAcheteurs.Add(coordonneesAcheteur);
            _bddContext.SaveChanges();
            return coordonneesAcheteur.CoordonneesAcheteurID;
        }*/

        public CoordonneesAcheteur ObtenirCoorCoordonneesAcheteur(int Id)
        {
            return _bddContext.CoordonneesAcheteurs.Find(Id);
        }
    }
}
