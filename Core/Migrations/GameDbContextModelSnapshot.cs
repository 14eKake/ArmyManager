using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Core.Models;

#nullable disable

namespace Core.Migrations;

[DbContext(typeof(GameDbContext))]
partial class GameDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

        modelBuilder.Entity<Player>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<string>("Name")
                .HasColumnType("TEXT");

            b.HasKey("Id");
            b.ToTable("Players");
        });

        modelBuilder.Entity<Army>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<string>("Name")
                .HasColumnType("TEXT");

            b.Property<int>("PlayerId")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("PlayerId");

            b.ToTable("Armies");
        });

        modelBuilder.Entity<Building>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<int>("PlayerId")
                .HasColumnType("INTEGER");

            b.Property<int>("ProductionRate")
                .HasColumnType("INTEGER");

            b.Property<int>("Produces")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("PlayerId");

            b.ToTable("Buildings");
        });

        modelBuilder.Entity<ResourceStock>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<int>("Amount")
                .HasColumnType("INTEGER");

            b.Property<int>("PlayerId")
                .HasColumnType("INTEGER");

            b.Property<int>("Type")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("PlayerId");

            b.ToTable("Resources");
        });

        modelBuilder.Entity<UnitStack>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<int>("ArmyId")
                .HasColumnType("INTEGER");

            b.Property<int>("Quantity")
                .HasColumnType("INTEGER");

            b.Property<int>("TypeId")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("ArmyId");

            b.HasIndex("TypeId");

            b.ToTable("UnitStacks");
        });

        modelBuilder.Entity<UnitType>(b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<float>("Attack")
                .HasColumnType("REAL");

            b.Property<string>("Cost")
                .HasColumnType("TEXT");

            b.Property<float>("Defense")
                .HasColumnType("REAL");

            b.Property<int>("ManpowerCost")
                .HasColumnType("INTEGER");

            b.Property<string>("Name")
                .HasColumnType("TEXT");

            b.Property<int>("TrainingTimeSeconds")
                .HasColumnType("INTEGER");

            b.Property<float>("Speed")
                .HasColumnType("REAL");

            b.HasKey("Id");

            b.ToTable("UnitTypes");
        });
    }
}
