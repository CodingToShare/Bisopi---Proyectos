﻿// <auto-generated />
using System;
using Bisopi___Proyectos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bisopi___Proyectos.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230816022206_AddCities")]
    partial class AddCities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bisopi___Proyectos.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Client", b =>
                {
                    b.Property<Guid>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Currency", b =>
                {
                    b.Property<Guid>("CurrencyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CurrencyID");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Deal", b =>
                {
                    b.Property<Guid>("DealID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DealName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LeadID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("LeadValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("ProposalStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponsibleClient")
                        .HasColumnType("varchar(200)");

                    b.HasKey("DealID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ProposalStatusID");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Lead", b =>
                {
                    b.Property<Guid>("LeadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LeadName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<double?>("LeadValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("QuoteStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ResponsibleClient")
                        .HasColumnType("varchar(200)");

                    b.HasKey("LeadID");

                    b.HasIndex("ClientID");

                    b.HasIndex("QuoteStatusID");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Milestone", b =>
                {
                    b.Property<Guid>("MilestoneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DealID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsItChangeControl")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LeadID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("MilestoneDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MilestoneNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Value")
                        .HasColumnType("float");

                    b.HasKey("MilestoneID");

                    b.ToTable("Milestones");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Project", b =>
                {
                    b.Property<Guid>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActualCompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Billable")
                        .HasColumnType("bit");

                    b.Property<string>("ClarityCode")
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CurrencyID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerManager")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<Guid?>("DealID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EstimateRequestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EstimatedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstimatedHours")
                        .HasColumnType("int");

                    b.Property<double>("GrossMargin")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Justification")
                        .HasColumnType("varchar(1000)");

                    b.Property<Guid>("LeaderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<double>("ProjectCost")
                        .HasColumnType("float");

                    b.Property<Guid>("ProjectManagerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("ProjectStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("ProjectValue")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("RequestPriority")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ScalerCode")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SupportStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("TRMProject")
                        .HasColumnType("float");

                    b.HasKey("ProjectID");

                    b.HasIndex("ClientID");

                    b.HasIndex("CountryID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("ProjectStatusID");

                    b.HasIndex("ProjectTypeID");

                    b.HasIndex("SupportStatusID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.ProjectStatus", b =>
                {
                    b.Property<Guid>("ProjectStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProjectStatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("ProjectStatusID");

                    b.ToTable("ProjectsStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.ProjectType", b =>
                {
                    b.Property<Guid>("ProjectTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProjectTypeName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("ProjectTypeID");

                    b.ToTable("ProjectsTypes");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.ProposalStatus", b =>
                {
                    b.Property<Guid>("ProposalStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ProposalStatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("ProposalStatusID");

                    b.ToTable("ProposalsStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.QuoteStatus", b =>
                {
                    b.Property<Guid>("QuoteStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("QuoteStatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("QuoteStatusID");

                    b.ToTable("QuotesStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.SupportStatus", b =>
                {
                    b.Property<Guid>("SupportStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SupportStatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("SupportStatusID");

                    b.ToTable("SupportsStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.ModelsTemps.MilestoneTemp", b =>
                {
                    b.Property<Guid>("MilestoneTempID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CurrencyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DealID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsItChangeControl")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LeadID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("MilestoneDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MilestoneNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Value")
                        .HasColumnType("float");

                    b.HasKey("MilestoneTempID");

                    b.ToTable("MilestonesTemps");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Deal", b =>
                {
                    b.HasOne("Bisopi___Proyectos.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.ProposalStatus", "ProposalStatus")
                        .WithMany()
                        .HasForeignKey("ProposalStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ProposalStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Lead", b =>
                {
                    b.HasOne("Bisopi___Proyectos.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.QuoteStatus", "QuoteStatus")
                        .WithMany()
                        .HasForeignKey("QuoteStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("QuoteStatus");
                });

            modelBuilder.Entity("Bisopi___Proyectos.Models.Project", b =>
                {
                    b.HasOne("Bisopi___Proyectos.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.ProjectStatus", "ProjectStatus")
                        .WithMany()
                        .HasForeignKey("ProjectStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.ProjectType", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bisopi___Proyectos.Models.SupportStatus", "SupportStatus")
                        .WithMany()
                        .HasForeignKey("SupportStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Country");

                    b.Navigation("Currency");

                    b.Navigation("ProjectStatus");

                    b.Navigation("ProjectType");

                    b.Navigation("SupportStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
