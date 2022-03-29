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

        //Fonction permettant d'obtenir une Commande à partir de son Id
        /*public Commande ObtenirCommande(int id)
        {
            return _bddContext.Commandes.Find(id);
        }*/

        //Fonction permettant de créer une Commande
        /*public void CreerCommande()
        {
            Commande commande = new Commande() { DetailCommande = new List<DetailCommande>() };
            _bddContext.Commandes.Add(commande);
            _bddContext.SaveChanges();
            return commande.CommandeID;
        }*/
        /*public int CreerCommande(string lastname, string firstname, Adresse adresselivraison, Adresse adressefacturation, string telephone, DateTime date)
        {
            Commande commande = new Commande() { LastName = lastname,Firstname = firstname, AdresseLivraison = adresselivraison, AdresseFacturation = adressefacturation, Telephone = telephone, Date = date};
            _bddContext.Commandes.Add(commande);
            _bddContext.SaveChanges();
            return commande.CommandeID;
        }*/

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

        /*public Commande ObtenirCommande(int commandeID)
        {
            return _bddContext.Commandes.Include(c=> c.DetailCommandes).ThenInclude(pp=>pp.Produit).Where(x=> x.CommandeID == commandeID).FirstOrDefault();
        }

        public void AjouterDetailCommande(int commandeID, DetailCommande detailCommande)
        {
            Commande commande = _bddContext.Commandes.Find(commandeID);
            commande.DetailCommandes.Add(detailCommande);

            _bddContext.SaveChanges();
        }

        public void MettreAjourQuantiteDetailCommande(int DetailCommandeID)
        {
            var detailcommande = _bddContext.DetailsCommandes.Find(DetailCommandeID);
            if(detailcommande != null)
            {
                detailcommande.Quantite += 1;
                _bddContext.SaveChanges();
            }
        }

        public void SupprimerDetailCommande(int CommandeID, int DetailCommandeID)
        {
            Commande commande = ObtenirCommande(CommandeID);
            DetailCommande detailCommande = commande.DetailCommandes.Where(xx => xx.DetailCommandeID == DetailCommandeID).FirstOrDefault();
            commande.DetailCommandes.Remove(detailCommande);
            _bddContext.SaveChanges();
        }*/


        //Fonction permettant d'obtenir la liste de tous les détails commandes
        /*public List<DetailCommande> ObtenirAllDetailCommandes()
        {
            return _bddContext.DetailsCommandes.ToList();
        }*/

        //Fonction permettant d'obtenir un détail de Commande à partir de son Id
        /*public List<DetailCommande> ObtenirDetailCommande(int id)
        {
            return _bddContext.DetailsCommandes.Find(id);  
        }*/
    }
}
