
﻿using Kili.Models.General;
using Kili.Models.GestionAdhesion;
﻿using Kili.Models.Dons;
using Microsoft.EntityFrameworkCore;
using Kili.Models.Vente;

namespace Kili.Models
{
    public class BddContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<Adhesion> Adhesions { get; set; }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Cotisation> Cotisations { get; set; }
        public DbSet<ServiceAdhesion> ServicesAdhesion { get; set; }
        public DbSet<Don> Dons { get; set; }
        public DbSet<Donateur> Donateurs { get; set; }


        //Vente 
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CoordonneesAcheteur> CoordonneesAcheteurs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=RRRRR;database=NewBaseKILI");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            UserAccount_Services userAccountServices = new UserAccount_Services();
            Association_Services associationServices = new Association_Services();


            userAccountServices.CreerAdmin("M.","Admin", "Admin", "Kili@mail.com");
            userAccountServices.CreerUserAccount("Fara", "Raza", "P@ssFara1", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy","Kombet", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman","Krid", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);


            userAccountServices.DésactiverUserAccount(1);

            associationServices.CreerAssociation("Première Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Sport, 1 );
            associationServices.CreerAssociation("Deuxième Asso", new Adresse() { Numero = 20, Voie = "rue de la mer", CodePostal = 13000, Ville = "Marseille" }, ThemeAssociation.Arts_et_culture, 2 );
            associationServices.CreerAssociation("Troisièeme Asso", new Adresse() { Numero = 30, Voie = "champs elysés", CodePostal = 75000, Ville = "Paris" }, ThemeAssociation.Environnement, 3);
            associationServices.CreerAssociation("4eme Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Environnement, 4);

            //Vente en ligne
            this.Catalogues.AddRange(
                new Catalogue
                {
                    CatalogueID = 01,
                    CatalogueName = "Epices",
                    Description = "Epices et condiments du monde",

                },
               new Catalogue

               {
                   CatalogueID = 02,
                   CatalogueName = "Sacs et accessoires",
                   Description = "Sacs et accessoires du monde",

               }

                 );

            this.Produits.AddRange(
           new Produit
           {
               ProduitID = 001,
               Designation = "Pili Pili",
               Format = "100g",
               Description = "Piment rouge en provenance de Madagascar, pour donner goût à vos plats.",
               PrixUnitaire = 5,
               Devise = "EUR",
               ImagePath = "pilipili.jpg",
               CatalogueID = 01,
           },

           new Produit
           {
               ProduitID = 002,
               Designation = "Sac croco",
               Format = "25.5 cm * 31 cm * 15 cm",
               Description = "Sac fabriqué à partir de la peau de crocodile du Burkina Faso.",
               PrixUnitaire = 50,
               Devise = "EUR",
               ImagePath = "saccroco.jpg",
               CatalogueID = 02,
           }
            );
            this.SaveChanges();
             this.Livraisons.AddRange(
                 new Livraison
                 {
                     LivraisonID = 001,
                     LivraisonName = "Livraison à domicile - Colissimo",
                     LivraisonDescription = "Livré en 2 jours ouvrés si vous commandez avant 14h du lundi au vendredi",
                     LivraisonPrice = 0,
                     LivraisonDevise = "EUR",
                 },
                 new Livraison
                 {
                     LivraisonID = 002,
                     LivraisonName = "Livraison à domicile - Chronopost",
                     LivraisonDescription = "Livré avant 13h le jour suivant si vous commandez avant 11h30 du lundi au vendredi",
                     LivraisonPrice = 9.99,
                     LivraisonDevise = "EUR",
                 },
                 new Livraison
                 {
                     LivraisonID = 003,
                     LivraisonName = "En point de retrait",
                     LivraisonDescription = "Mondial Relay",
                     LivraisonPrice = 0,
                     LivraisonDevise = "EUR",
                 }
                 );
            this.SaveChanges();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<UserAccount>()
              //  .HasIndex(p => p.PersonneID)
                //.IsUnique();

            modelBuilder.Entity<Association>()
                .HasIndex(p => p.UserAccountId)
                .IsUnique();

/*modelBuilder.Entity<Abonnement>()
                .HasIndex(p => p.AssociationId)
                .IsUnique();*/

            modelBuilder.Entity<Donateur>()
                .HasIndex(p => p.UserAccountId)
                .IsUnique();

            //Vene en ligne
            modelBuilder.Entity<Catalogue>()
                .HasIndex(p => p.CatalogueID)
                .IsUnique();

            modelBuilder.Entity<Produit>()
                .HasIndex(p => p.ProduitID)
                .IsUnique();

            modelBuilder.Entity<Panier>()
                .HasIndex(p => p.PanierID)
                .IsUnique();

            modelBuilder.Entity<Livraison>()
                .HasIndex(p => p.LivraisonID)
                .IsUnique();
        }

        public void DeleteCreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }

}

