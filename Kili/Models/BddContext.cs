using Kili.Models.General;
using Microsoft.EntityFrameworkCore;
using Kili.Models.Vente;

namespace Kili.Models
{
    public class BddContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Adresse> Adresses { get; set; }

        //Vente 
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Panier> Paniers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=RRRRR;database=GOKILI");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            UserAccount_Services userAccountServices = new UserAccount_Services();
            Association_Services associationServices = new Association_Services();

            //associationServices.CreerAssociation("Première Asso", new Adresse() { Numero = 1, Voie="rue du sport", CodePostal=34000, Ville="Montpellier" }, "Sport", new UserAccount() {UserName="username première asso", Password="aaa",Mail="1ere@mail.com" }); 

            userAccountServices.CreerAdmin("Admin", "Admin", "Kili@mail.com");
            userAccountServices.CreerUserAccount("Fara", "P@ssFara1", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);


            userAccountServices.DésactiverUserAccount(1);

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

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>()
                .HasIndex(p => p.PersonneID)
                .IsUnique();

            modelBuilder.Entity<Abonnement>()
                .HasIndex(p => p.AssociationId)
                .IsUnique();

            modelBuilder.Entity<Catalogue>()
                .HasIndex(p => p.CatalogueID)
                .IsUnique();

            modelBuilder.Entity<Produit>()
                .HasIndex(p => p.ProduitID)
                .IsUnique();
        }

        public void DeleteCreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }

}

