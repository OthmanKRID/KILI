
﻿using Kili.Models.General;
using Kili.Models.GestionAdhesion;
﻿using Kili.Models.Dons;
using Kili.Models.Services;
using Microsoft.EntityFrameworkCore;
using Kili.Models.Vente;
using System;
using Microsoft.Extensions.Configuration;
using System.Configuration;



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
        public DbSet<ServiceDon> ServicesDon { get; set; }
        public DbSet<ServiceBoutique> ServicesBoutique { get; set; }
        public DbSet<Don> Dons { get; set; }
        public DbSet<Donateur> Donateurs { get; set; }
        public DbSet<Collecte> Collectes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PanierService> PaniersServices { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<MoyenPaiement> MoyenPaiements { get; set; }
    


        //Vente 
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CoordonneesAcheteur> CoordonneesAcheteurs { get; set; }

        public DbSet<ServiceBoutique> ServiceBoutiques { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            

            if (System.Diagnostics.Debugger.IsAttached)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;password=RRRRR;database=Kili");
            }
            else
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }

        }

        public void InitializeDb()
        {
       

            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            UserAccount_Services userAccountServices = new UserAccount_Services();
            Association_Services associationServices = new Association_Services();
            Abonnement_Services abonnement_Services = new Abonnement_Services();
            DonServices donServices = new DonServices();
            Adresse_Services adresseService = new Adresse_Services();


            userAccountServices.CreerAdmin("M.","Admin", "P@ssw0rd5", "Kili@mail.com");
            userAccountServices.CreerUserAccount("Fara", "Raza", "123456", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy","Kombet", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman","Krid", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);


            adresseService.CreerAdresse(15, "rue", "Gabriel", 93000, "Pantin");
            adresseService.CreerAdresse(25, "rue", "Peri", 93100, "Romainville");

            donServices.CreerDonateur(1);
            donServices.CreerDonateur(2);

            

            userAccountServices.DésactiverUserAccount(1);


            associationServices.CreerAssociation("Première Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Sport, userAccountServices.ObtenirUserAccount(1) );
            associationServices.CreerAssociation("Deuxième Asso", new Adresse() { Numero = 20, Voie = "rue de la mer", CodePostal = 13000, Ville = "Marseille" }, ThemeAssociation.Arts_et_culture, userAccountServices.ObtenirUserAccount(2));
            associationServices.CreerAssociation("Troisième Asso", new Adresse() { Numero = 30, Voie = "champs elysés", CodePostal = 75000, Ville = "Paris" }, ThemeAssociation.Environnement, userAccountServices.ObtenirUserAccount(3));
            associationServices.CreerAssociation("4eme Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Environnement, userAccountServices.ObtenirUserAccount(4));

            //abonnement_Services.AjouterServiceDansOffre(19.99, 1, TypeService.Adhesion);
            abonnement_Services.AjouterServiceDansOffre(149.99, 12, TypeService.Adhesion);
            //abonnement_Services.AjouterServiceDansOffre(19.99, 1, TypeService.Don);
            abonnement_Services.AjouterServiceDansOffre(149.99, 12, TypeService.Don);
            //abonnement_Services.AjouterServiceDansOffre(19.99, 1, TypeService.Boutique);
            abonnement_Services.AjouterServiceDansOffre(149.99, 12, TypeService.Boutique);


            abonnement_Services.AjouterService(1, abonnement_Services.ObtenirServiceDansOffre(1));
            abonnement_Services.AjouterService(1, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(1, abonnement_Services.ObtenirServiceDansOffre(3));
            abonnement_Services.AjouterService(2, abonnement_Services.ObtenirServiceDansOffre(1));
            abonnement_Services.AjouterService(2, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(3, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(3, abonnement_Services.ObtenirServiceDansOffre(3));

            donServices.CreerCollecte("Collecte pour moi", 0, "Une collecte intéressée", 1);
            donServices.CreerCollecte("Collecte pour les millionnaires en détresse",0, "Une collecte généreuse", 3);
            donServices.CreerDon(1000, TypeRecurrence.Unique, 1, 1);
            donServices.CreerDon(2000, TypeRecurrence.Unique, 1, 2);
            donServices.CreerDon(200, TypeRecurrence.Mensuel, 2, 2);


            

            //Vente en ligne
            this.Catalogues.AddRange(
                new Catalogue
                {
                    CatalogueID = 01,
                    CatalogueName = "Montres et bracelets",
                    Description = "Bracelet made in france",

                },
               new Catalogue

               {
                   CatalogueID = 02,
                   CatalogueName = "Chaussures et ceintures",
                   Description = "Chaussures made in france",

               },
               new Catalogue

               {
                   CatalogueID = 03,
                   CatalogueName = "Sacs et tote bags",
                   Description = "Sacs made in france"

               }
               ,
               new Catalogue

               {
                   CatalogueID = 04,
                   CatalogueName = "Echarpes et foulards",
                   Description = "Echarpes made in france"

               }
                 );

            this.Produits.AddRange(
           new Produit
           {
               ProduitID = 001,
               DateCreation = "01/04/2022",
               Designation = "Bracelet pour homme",
               Format = "22 à 25 cm",
               Description = "Bracelet Sequoia - beige.Fabriqué à la main en France dans Aveyron.",
               PrixUnitaire = 39,
               Devise = "EUR",
               ImagePath = "/images-boutique/bracelet.jpg",
               CatalogueID = 01,
           },

           new Produit
           {
               ProduitID = 002,
               DateCreation = "01/04/2022",
               Designation = "Espadrilles pour femme",
               Format = "Pointure 36",
               Description = "Espadrilles marinières fabriquées de manière artisanale en France.",
               PrixUnitaire = 28,
               Devise = "EUR",
               ImagePath = "/images-boutique/espadrille.jpg",
               CatalogueID = 02,
           }
            
            ,
            new Produit
            {
                ProduitID = 004,
                DateCreation = "01/02/2022",
                Designation = "Écharpe pour hommes",
                Format = "100cm * 65cm",
                Description = "Écharpe unique fabriquée à la main",
                PrixUnitaire = 44,
                Devise = "EUR",
                ImagePath = "/images-boutique/echarpe-homme.jpg",
                CatalogueID = 04,
            },

            new Produit
            {
                ProduitID = 005,
                DateCreation = "01/02/2022",
                Designation = "Montre skateboard",
                Format = "Poids inférieur à 25 Grammes",
                Description = "Bracelet cuir : brun ou noir",
                PrixUnitaire = 280,
                Devise = "EUR",
                ImagePath = "/images-boutique/montre.jpg",
                CatalogueID = 01,
            },

            new Produit
            {
                ProduitID = 006,
                DateCreation = "01/02/2022",
                Designation = "Sac Messenger pour homme",
                Format = "41x33x13cm",
                Description = "Sac d'épaule en toie, grand compartiment pour les livres et ordinateur portable",
                PrixUnitaire = 65,
                Devise = "EUR",
                ImagePath = "/images-boutique/messenger.jpg",
                CatalogueID = 02,
            },

            new Produit
            {
                ProduitID = 007,
                Designation = "Papillon pour femme",
                DateCreation = "01/02/2022",
                Format = "Pointure 39",
                Description = "Fait main. 100% française",
                PrixUnitaire = 135,
                Devise = "EUR",
                ImagePath = "/images-boutique/papillon.jpg",
                CatalogueID = 02,
            },

            new Produit
            {
                ProduitID = 008,
                Designation = "Ceinture en Cuir pour homme",
                DateCreation = "01/02/2022",
                Format = "Taille 56-64",
                Description = "Ceinture Classique, Must de Cartier, Métal Doré, Made in France",
                PrixUnitaire = 195,
                Devise = "EUR",
                ImagePath = "/images-boutique/ceinture.jpg",
                CatalogueID = 02,
            }
            ,
            new Produit
            {
                ProduitID = 003,
                DateCreation = "01/03/2022",
                Designation = "Tote bag Mojito",
                Format = "37cm x 40cm. Anses de 64cm",
                Description = "Tissé et imprimé en France. Sac résistant en coton avec impression",
                PrixUnitaire = 15,
                Devise = "EUR",
                ImagePath = "/images-boutique/mojito.jpg",
                CatalogueID = 03,
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
                     LivraisonPrice = 0,
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

            /*modelBuilder.Entity<Association>()
                .HasIndex(p => p.UserAccountId)
                .IsUnique();*/

            /*modelBuilder.Entity<Abonnement>()
                            .HasIndex(p => p.AssociationId)
                            .IsUnique();*/

            modelBuilder.Entity<Abonnement>()
                .HasIndex(p => p.ServiceDonId)
                .IsUnique();

            modelBuilder.Entity<UserAccount>()
                .HasIndex(p => p.DonateurId)
                .IsUnique();

            modelBuilder.Entity<Don>()
                .HasIndex(p => p.PaiementId)
                .IsUnique();

            modelBuilder.Entity<Paiement>()
                .HasIndex(p => p.MoyenPaiementId)
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

