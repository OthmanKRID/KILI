
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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            

            if (System.Diagnostics.Debugger.IsAttached)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;password=P@ssw0rd5;database=Kili");
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
            userAccountServices.CreerUserAccount("Fara", "Raza", "P@ssFara1", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy","Kombet", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman","Krid", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("De Maillard", "François", "P@ssw0rd5", "François@gmail.fr", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Frelon", "Eric", "P@ssw0rd5", "Rico@hotmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Neveu", "Anne-Lucie", "P@ssw0rd5", "Luciole@gmail.fr", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Costes", "Celine", "P@ssw0rd5", "crapounette@gmail.fr", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Planchais", "Pascal", "P@ssw0rd5", "pascal@gmail.fr", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Arinloye", "Amour", "P@ssw0rd5", "amour@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Ovigneur", "Thomas", "P@ssw0rd5", "thomas@gmail.fr", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Huyn", "Emmanuel", "P@ssw0rd5", "emmanuel@gmail.fr", TypeRole.Utilisateur);

            adresseService.CreerAdresse(15, "rue", "Gabriel", 93000, "Pantin");
            adresseService.CreerAdresse(25, "rue", "Peri", 93100, "Romainville");


            associationServices.CreerAssociation("Première Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Sport, userAccountServices.ObtenirUserAccount(1) );
            associationServices.CreerAssociation("Hello cartel", new Adresse() { Numero = 20, Voie = "rue de la mer", CodePostal = 13000, Ville = "Marseille" }, ThemeAssociation.Environnement, userAccountServices.ObtenirUserAccount(2));
            associationServices.CreerAssociation("Paris Motorcycle Club", new Adresse() { Numero = 30, Voie = "rue Jean Jaurès ", CodePostal = 75010, Ville = "Paris" }, ThemeAssociation.club_de_loisirs, userAccountServices.ObtenirUserAccount(3));
            associationServices.CreerAssociation("4eme Asso", new Adresse() { Numero = 1, Voie = "rue du sport", CodePostal = 34000, Ville = "Montpellier" }, ThemeAssociation.Environnement, userAccountServices.ObtenirUserAccount(4));


            associationServices.CreerAssociation("Les amateurs de Bordeaux", new Adresse() { Numero = 5, Voie = "rue du Laos", CodePostal = 75019, Ville = "Bordeaux" }, ThemeAssociation.Arts_et_culture, userAccountServices.ObtenirUserAccount(5));
            associationServices.CreerAssociation("Basket & Backgammon", new Adresse() { Numero = 20, Voie = "rue de la mer", CodePostal = 13000, Ville = "Marseille" }, ThemeAssociation.Sport, userAccountServices.ObtenirUserAccount(6));
            associationServices.CreerAssociation("Les Amis des Peoples", new Adresse() { Numero = 30, Voie = "rue Gabriel", CodePostal = 75017, Ville = "Paris" }, ThemeAssociation.Arts_et_culture, userAccountServices.ObtenirUserAccount(7));
            associationServices.CreerAssociation("Réunion Randonnées", new Adresse() { Numero = 35, Voie = "rue du soleil", CodePostal = 97490, Ville = "Saint-Denis" }, ThemeAssociation.Sport, userAccountServices.ObtenirUserAccount(8));
            associationServices.CreerAssociation("Paris 20e Solidaire", new Adresse() { Numero = 85, Voie = "rue Jean Lolive", CodePostal = 75020, Ville = "Paris" }, ThemeAssociation.Humanitaire_caritative, userAccountServices.ObtenirUserAccount(9));
            associationServices.CreerAssociation("Club des petits loups", new Adresse() { Numero = 3, Voie = "rue du Manin", CodePostal = 75019, Ville = "Paris" }, ThemeAssociation.club_de_loisirs, userAccountServices.ObtenirUserAccount(10));
            associationServices.CreerAssociation("La course du coeur", new Adresse() { Numero = 85, Voie = "rue Jean Lolive", CodePostal = 75020, Ville = "Paris" }, ThemeAssociation.Humanitaire_caritative, userAccountServices.ObtenirUserAccount(11));
            associationServices.ModifierAssociationDescriptifImage(1, "Association visant le bien commun et la production d'herbes arromatiques aux bienfaits reconnus. Nous sommes un acteur international mobilisé pour le droit et la justice", "/images/Hello.png");
            associationServices.ModifierAssociationDescriptifImage(2, "Association visant le bien commun et la production d'herbes arromatiques aux bienfaits reconnus. Nous sommes un acteur international mobilisé pour le droit et la justice", "/images/Hello.png");
            associationServices.ModifierAssociationDescriptifImage(3, "Paris Motorcycle Club est The club qui regroupe les amateurs de moto américaine, de tatouages et de bottes en cuir.", "/images/Harley.png");
            associationServices.ModifierAssociationDescriptifImage(4, "Association visant le bien commun et la production d'herbes arromatiques aux bienfaits reconnus. Nous sommes un acteur international mobilisé pour le droit et la justice", "/images/Hello.png");
            associationServices.ModifierAssociationDescriptifImage(5, "Les amateurs de Bordeaux est une association qui fédère les acteurs de la vigne de la région Bordelaise. Sa mission est de promouvoir une consommation alliant plaisir et responsabilité dans la dégustation, grâce à des actions ciblées.","/images/degustationvin.jpg");
            associationServices.ModifierAssociationDescriptifImage(6, "Le basket-ball et le backgammon proposés par notre association se veulent en priorité des moments de convivialité et d’échanges. L’objectif principal est centré sur le plaisir. Dès 18 ans, débutant comme confirmé, vous êtes les bienvenus.", "/images/basket.jpg");
            associationServices.ModifierAssociationDescriptifImage(7, "L’Association Littéraire et Artistique Les Amis des Peoples est une société savante et indépendante qui se destine à l’étude et à la discussion des problèmes existentiels des Peoples et de leur impact sur notre société. La vie des célébrités participe aujourd’hui à la protection des droits fondamentaux de l’homme.", "/images/livre.png");
            associationServices.ModifierAssociationDescriptifImage(8, "Réunion Randonnée est une association proposant principalement des sorties de randonnées pédestres sur notre belle ile de La Réunion. Elle peut toutefois proposer des sorties pour d’autres activités de plein air ou culturelles. Réunion Randonnée est affiliée à la Fédération Française de la Randonnée Pédestre(FFRandonnée).", " /images/randonnee.jpg");
            associationServices.ModifierAssociationDescriptifImage(9, "Paris 20e Solidaire a été créée en 1935 pour réaliser des actions de solidarités avec les personnes démunies et isolées. L'activité de domiciliation est est au coeur de notre association.Elle est ouverte à toute personne et à toute famille sans domicile ou en difficulté.", "/images/solidaire.jpg");
            associationServices.ModifierAssociationDescriptifImage(10, "Le club des petits loups offre aux enfants de 4 ans à 12 ans habitant Paris 19e un lieu foisonnant d'activités ludiques : aire de jeux, découverte des arts, atelier de pyrogravure... et tant d'autres choses!", " /images/loups.png");
            associationServices.ModifierAssociationDescriptifImage(11, "La course du coeur est une association qui sensibilise à l'importance du sport pour prévenir les maladies cardio-vasculaires. Il s'agit d'une course de relais débutée en 2000 et qui traverse les 5 continents grâce à la participation de bénévoles. Cette idée originale nous est venue en regardant Forest Gump.", "/images/Forrest.jpg");

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

            abonnement_Services.AjouterService(5, abonnement_Services.ObtenirServiceDansOffre(1));
            abonnement_Services.AjouterService(5, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(8, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(8, abonnement_Services.ObtenirServiceDansOffre(3));
            abonnement_Services.AjouterService(9, abonnement_Services.ObtenirServiceDansOffre(1));
            abonnement_Services.AjouterService(9, abonnement_Services.ObtenirServiceDansOffre(2));
            abonnement_Services.AjouterService(9, abonnement_Services.ObtenirServiceDansOffre(3));
            abonnement_Services.AjouterService(10, abonnement_Services.ObtenirServiceDansOffre(1));
            abonnement_Services.AjouterService(11, abonnement_Services.ObtenirServiceDansOffre(2));


            donServices.CreerCollecte("Collecte de lutte contre la pauvreté à Paris 20e", 0, "Lutter contre la pauvreté, c'est possible ! Vous pouvez participer au financement de denrées alimentaires et de vêtements à destination d'une épicerie solidaire pour les personnes en situation de précarité.", 9, null);
            donServices.CreerCollecte("Collecte : sorties pour les personnes âgées précaires", 0, "Lutter contre l'isolement des personnes âgées, c'est possible ! Vous pouvez participer au financement de sortie pour les personnes au minimum vieillesse .", 9, null);
            donServices.CreerCollecte("Collecte pour financer des équipements de randonnée pour les personnes handicapées ",0, "La nature doit est accessible à tous, y compris aux personnes en situation de handicap. Cette collecte permettra de financer des équipements aidant les personnes à mobilité réduite", 8, "/images/handicap.jpg");
            donServices.CreerCollecte("Collecte pour notre ami Francis, vigneron en difficulté", 0, "Collecte pour notre ami Francis, vigneron en difficulté à Pessac suite à l'incendie qui a ravagé une partie de ses vignes", 5, null);
            donServices.CreerCollecte("Collecte pour l'organisation d'un relais à travers la Sibérie Orientale", 0, "Collecte pour l'organisation d'un relais à travers la sibérie orientale, à destination des populations que nous n'avons pas encore rejoint", 11, "/images/siberie.jpg");


            donServices.CreerDonateur(5);
            donServices.CreerDonateur(6);
            donServices.CreerDon(100, TypeRecurrence.Unique, 1, 1);
            donServices.CreerDon(20, TypeRecurrence.Unique, 1, 2);
            donServices.CreerDon(200, TypeRecurrence.Mensuel, 2, 2);


            

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

