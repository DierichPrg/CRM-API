﻿// <auto-generated />
using Data.ModelsCrm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM_API.Migrations
{
    [DbContext(typeof(CrmContext))]
    partial class CrmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Data.ModelsCrm.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("Data.ModelsCrm.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdCompany")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCompany");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Data.ModelsCrm.User", b =>
                {
                    b.HasOne("Data.ModelsCrm.Company", "IdCompanyNavigation")
                        .WithMany("Users")
                        .HasForeignKey("IdCompany")
                        .IsRequired()
                        .HasConstraintName("FK_User_IdCompany");

                    b.Navigation("IdCompanyNavigation");
                });

            modelBuilder.Entity("Data.ModelsCrm.Company", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}