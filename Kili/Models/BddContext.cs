using Kili.Models.Dons;
using Kili.Models.General;
using Microsoft.EntityFrameworkCore;

namespace Kili.Models
{
    public class BddContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Don> Dons { get; set; }
        public DbSet<Donateur> Donateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=P@ssw0rd5;database=Kili");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            UserAccount_Services userAccountServices = new UserAccount_Services();
            Association_Services associationServices = new Association_Services();

            //associationServices.CreerAssociation("Première Asso", new Adresse() { Numero = 1, Voie="rue du sport", CodePostal=34000, Ville="Montpellier" }, "Sport", new UserAccount() {UserName="username première asso", Password="aaa",Mail="1ere@mail.com" }); 

            userAccountServices.CreerAdmin("M.","Admin", "Admin", "Kili@mail.com");
            userAccountServices.CreerUserAccount("Fara", "Raza", "P@ssFara1", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy","Kombet", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman","Krid", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);


            userAccountServices.DésactiverUserAccount(1);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonnement>()
                .HasIndex(p => p.AssociationId)
                .IsUnique();

            modelBuilder.Entity<Donateur>()
                .HasIndex(p => p.UserAccountId)
                .IsUnique();
        }

        public void DeleteCreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }

}

