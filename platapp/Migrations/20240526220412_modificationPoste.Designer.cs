﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using platapp;

#nullable disable

namespace platapp.Migrations
{
    [DbContext(typeof(PContext))]
    [Migration("20240526220412_modificationPoste")]
    partial class modificationPoste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ParcUtilisateur", b =>
                {
                    b.Property<int>("ParcsId")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateursId")
                        .HasColumnType("int");

                    b.HasKey("ParcsId", "UtilisateursId");

                    b.HasIndex("UtilisateursId");

                    b.ToTable("ParcUtilisateur");
                });

            modelBuilder.Entity("platapp.Domain.Etablissement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Etablissement");
                });

            modelBuilder.Entity("platapp.Domain.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastActivityDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PosteFk")
                        .HasColumnType("int");

                    b.Property<int?>("UtilisateurFk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PosteFk");

                    b.HasIndex("UtilisateurFk");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("platapp.Domain.Parc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("EtablissementFk")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementFk");

                    b.ToTable("Parc");
                });

            modelBuilder.Entity("platapp.Domain.Poste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("SalleFk")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SalleFk");

                    b.ToTable("Poste");
                });

            modelBuilder.Entity("platapp.Domain.Salle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ParcFk")
                        .HasColumnType("int");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ParcFk");

                    b.ToTable("Salle");
                });

            modelBuilder.Entity("platapp.Domain.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Utilisateur");
                });

            modelBuilder.Entity("ParcUtilisateur", b =>
                {
                    b.HasOne("platapp.Domain.Parc", null)
                        .WithMany()
                        .HasForeignKey("ParcsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("platapp.Domain.Utilisateur", null)
                        .WithMany()
                        .HasForeignKey("UtilisateursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("platapp.Domain.Log", b =>
                {
                    b.HasOne("platapp.Domain.Poste", "Poste")
                        .WithMany("Logs")
                        .HasForeignKey("PosteFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("platapp.Domain.Utilisateur", "Utilisateur")
                        .WithMany("Logs")
                        .HasForeignKey("UtilisateurFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Poste");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("platapp.Domain.Parc", b =>
                {
                    b.HasOne("platapp.Domain.Etablissement", "Etablissement")
                        .WithMany("Parcs")
                        .HasForeignKey("EtablissementFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Etablissement");
                });

            modelBuilder.Entity("platapp.Domain.Poste", b =>
                {
                    b.HasOne("platapp.Domain.Salle", "Salle")
                        .WithMany("Postes")
                        .HasForeignKey("SalleFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Salle");
                });

            modelBuilder.Entity("platapp.Domain.Salle", b =>
                {
                    b.HasOne("platapp.Domain.Parc", "Parc")
                        .WithMany("Salles")
                        .HasForeignKey("ParcFk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Parc");
                });

            modelBuilder.Entity("platapp.Domain.Etablissement", b =>
                {
                    b.Navigation("Parcs");
                });

            modelBuilder.Entity("platapp.Domain.Parc", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("platapp.Domain.Poste", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("platapp.Domain.Salle", b =>
                {
                    b.Navigation("Postes");
                });

            modelBuilder.Entity("platapp.Domain.Utilisateur", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
