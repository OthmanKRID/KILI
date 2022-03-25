
﻿using Kili.Models.General;
using Kili.Models.GestionAdhesion;
﻿using Kili.Models.Dons;
using Microsoft.EntityFrameworkCore;
using Kili.Models.Services;

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
        public DbSet<Collecte> Collectes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=P@ssw0rd5;database=Kili");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            UserAccount_Services userAccountServices = new UserAccount_Services();
            DonServices donServices = new DonServices();


            userAccountServices.CreerAdmin("M.","Admin", "Admin", "Kili@mail.com");
            userAccountServices.CreerUserAccount("Fara", "Raza", "P@ssFara1", "Fara@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Romy","Kombet", "P@ssRomy1", "Romy@gmail.com", TypeRole.Utilisateur);
            userAccountServices.CreerUserAccount("Othman","Krid", "P@ssOthman1", "Othman@gmail.com", TypeRole.Utilisateur);
            

            //donServices.CreerDon(1000, TypeRecurrence.Unique, 1);
            //donServices.CreerDon(2000, TypeRecurrence.Unique, 1);
            //donServices.CreerDon(200, TypeRecurrence.Mensuel, 2);
            donServices.CreerCollecte("Collecte pour moi", 300000, "Une collecte intéressée");
            donServices.CreerCollecte("Collecte pour les millionnaires en détresse", 6000, "Une collecte généreuse");

            userAccountServices.DésactiverUserAccount(1);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserAccount>()
                .HasIndex(p => p.AssociationId)
                .IsUnique();

            modelBuilder.Entity<UserAccount>()
                .HasIndex(p => p.DonateurId)
                .IsUnique();

            modelBuilder.Entity<Don>()
                .HasIndex(p => p.PaiementId)
                .IsUnique();
        }

        public void DeleteCreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }

}

