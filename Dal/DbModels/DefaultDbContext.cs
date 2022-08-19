using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Common.Configuration;

#nullable disable

namespace Dal.DbModels
{
    public partial class DefaultDbContext : DbContext
    {
        public DefaultDbContext()
        {
        }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Creature> Creatures { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Landscape> Landscapes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationsToContent> LocationsToContents { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<PicturesToOther> PicturesToOthers { get; set; }
        public virtual DbSet<Quest> Quests { get; set; }
        public virtual DbSet<QuestsToCreature> QuestsToCreatures { get; set; }
        public virtual DbSet<QuestsToItem> QuestsToItems { get; set; }
        public virtual DbSet<Structure> Structures { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SharedConfiguration.DbConnectionString ?? "Data Source=localhost;Initial Catalog=DNDHelper;Integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Creature>(entity =>
            {
                entity.Property(e => e.StructuresId).HasColumnName("StructuresID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Structures)
                    .WithMany(p => p.Creatures)
                    .HasForeignKey(d => d.StructuresId)
                    .HasConstraintName("FK_Creatures_Structures");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.CreaturesId).HasColumnName("CreaturesID");

                entity.Property(e => e.StructuresId).HasColumnName("StructuresID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Creatures)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CreaturesId)
                    .HasConstraintName("FK_Items_Creatures");

                entity.HasOne(d => d.Structures)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.StructuresId)
                    .HasConstraintName("FK_Items_Structures");
            });

            modelBuilder.Entity<Landscape>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.LocationsToContents)
                    .WithMany(p => p.Landscapes)
                    .HasForeignKey(d => d.LocationsToContentsId)
                    .HasConstraintName("FK_Landscapes_LocationsToContents");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<LocationsToContent>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationsToContents)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationsToContents_Locations");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.PicturePath).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.PicturesToOther)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.PicturesToOtherId)
                    .HasConstraintName("FK_Pictures_PicturesToOther");
            });

            modelBuilder.Entity<PicturesToOther>(entity =>
            {
                entity.ToTable("PicturesToOther");

                entity.HasOne(d => d.Creature)
                    .WithMany(p => p.PicturesToOthers)
                    .HasForeignKey(d => d.CreatureId)
                    .HasConstraintName("FK_PicturesToOther_Creatures");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PicturesToOthers)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_PicturesToOther_Items");

                entity.HasOne(d => d.Structure)
                    .WithMany(p => p.PicturesToOthers)
                    .HasForeignKey(d => d.StructureId)
                    .HasConstraintName("FK_PicturesToOther_Structures");
            });

            modelBuilder.Entity<Quest>(entity =>
            {
                entity.Property(e => e.NextQuestId).HasColumnName("NextQuestID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.NextQuest)
                    .WithMany(p => p.InverseNextQuest)
                    .HasForeignKey(d => d.NextQuestId)
                    .HasConstraintName("FK_Quests_Quests");

                entity.HasOne(d => d.QuestsToCreature)
                    .WithMany(p => p.Quests)
                    .HasForeignKey(d => d.QuestsToCreatureId)
                    .HasConstraintName("FK_Quests_QuestsToCreatures");

                entity.HasOne(d => d.QuestsToItems)
                    .WithMany(p => p.Quests)
                    .HasForeignKey(d => d.QuestsToItemsId)
                    .HasConstraintName("FK_Quests_QuestsToItems");
            });

            modelBuilder.Entity<QuestsToCreature>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.QuestsToCreatures)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestsToCreatures_Quests");
            });

            modelBuilder.Entity<QuestsToItem>(entity =>
            {
                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.QuestsToItemsNavigation)
                    .HasForeignKey(d => d.QuestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestsToItems_Quests");
            });

            modelBuilder.Entity<Structure>(entity =>
            {
                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.LocationsToContents)
                    .WithMany(p => p.Structures)
                    .HasForeignKey(d => d.LocationsToContentsId)
                    .HasConstraintName("FK_Structures_LocationsToContents");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login, "Unique_Users_Login")
                    .IsUnique();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
