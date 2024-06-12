
using platapp.Domain;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platapp
{
    public class PContext : DbContext
    {
        //DbSet
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<Etablissement> Etablissement { get; set; }
        public DbSet<Salle> Salle { get; set; }
        public DbSet<Poste> Poste { get; set; }
        public DbSet<Parc> Parc { get; set; }
        public DbSet<Log> Log { get; set; }

        //OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
              Initial Catalog=PlateformeApplicative;Integrated Security=true;
              MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
        //OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Activite>().OwnsOne(a => a.Zone);
           modelBuilder.Entity<Parc>().HasOne(p => p.Etablissement)
                .WithMany(etab => etab.Parcs)
                .HasForeignKey(p => p.EtablissementFk)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Salle>().HasOne(sal => sal.Parc)
                .WithMany(par => par.Salles)
                .HasForeignKey(sal => sal.ParcFk)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Poste>().HasOne(post => post.Salle)
                .WithMany(sal => sal.Postes)
                .HasForeignKey(post => post.SalleFk)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Log>().HasOne(l => l.Utilisateur)
                .WithMany(ut => ut.Logs)
                .HasForeignKey(l => l.UtilisateurFk)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Log>().HasOne(l => l.Poste)
               .WithMany(ut => ut.Logs)
               .HasForeignKey(l => l.PosteFk)
               .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

        }
        //ConfigureConventions
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(50);
            base.ConfigureConventions(configurationBuilder);
        }
    }
}

