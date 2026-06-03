using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace EncounterForgeSSR.Data.Migrations;

[DbContext(typeof(AppDbContext))]
public partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.0");

        modelBuilder.Entity("EncounterForgeSSR.Models.AppUser", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<DateTime>("CreatedAtUtc")
                .HasColumnType("TEXT");

            b.Property<string>("PasswordHash")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("TEXT");

            b.Property<string>("UserName")
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnType("TEXT");

            b.HasKey("Id");

            b.HasIndex("UserName")
                .IsUnique();

            b.ToTable("AppUsers");
        });

        modelBuilder.Entity("EncounterForgeSSR.Models.Encounter", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<DateTime>("CreatedAtUtc")
                .HasColumnType("TEXT");

            b.Property<string>("Difficulty")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("TEXT");

            b.Property<string>("Environment")
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnType("TEXT");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("TEXT");

            b.Property<string>("Notes")
                .HasMaxLength(4000)
                .HasColumnType("TEXT");

            b.Property<int>("PartyLevel")
                .HasColumnType("INTEGER");

            b.Property<int>("PartySize")
                .HasColumnType("INTEGER");

            b.Property<DateTime>("UpdatedAtUtc")
                .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("Encounters");
        });

        modelBuilder.Entity("EncounterForgeSSR.Models.MonsterCatalog", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<string>("ChallengeRating")
                .IsRequired()
                .HasColumnType("TEXT");

            b.Property<string>("MonsterType")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("TEXT");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("TEXT");

            b.Property<string>("SuggestedNotes")
                .HasMaxLength(500)
                .HasColumnType("TEXT");

            b.HasKey("Id");

            b.ToTable("MonsterCatalogs");
        });

        modelBuilder.Entity("EncounterForgeSSR.Models.MonsterEntry", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<string>("ChallengeRating")
                .IsRequired()
                .HasColumnType("TEXT");

            b.Property<int>("EncounterId")
                .HasColumnType("INTEGER");

            b.Property<int?>("MonsterCatalogId")
                .HasColumnType("INTEGER");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("TEXT");

            b.Property<string>("Notes")
                .HasMaxLength(2000)
                .HasColumnType("TEXT");

            b.Property<int>("Quantity")
                .HasColumnType("INTEGER");

            b.Property<int>("SortOrder")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("EncounterId");

            b.HasIndex("MonsterCatalogId");

            b.ToTable("MonsterEntries");
        });

        modelBuilder.Entity("EncounterForgeSSR.Models.MonsterEntry", b =>
        {
            b.HasOne("EncounterForgeSSR.Models.Encounter", "Encounter")
                .WithMany("MonsterEntries")
                .HasForeignKey("EncounterId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Encounter");
        });

        modelBuilder.Entity("EncounterForgeSSR.Models.Encounter", b =>
        {
            b.Navigation("MonsterEntries");
        });
    }
}
