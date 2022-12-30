﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asphyxia;

#nullable disable

namespace asphyxia.Migrations
{
    [DbContext(typeof(AsphyxiaContext))]
    [Migration("20221230023636_update-player-card-name")]
    partial class updateplayercardname
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("asphyxia.Models.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardId")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<string>("DataId")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefId")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("DataId");

                    b.HasIndex("PlayerID");

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("asphyxia.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Passwd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("asphyxia.Models.Card", b =>
                {
                    b.HasOne("asphyxia.Models.Player", "Player")
                        .WithMany("Cards")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("asphyxia.Models.Player", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
