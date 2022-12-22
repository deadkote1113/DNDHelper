using System;
using Common.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Dal.DbModels;

public partial class DefaultDbContext : DbContext
{
    public DefaultDbContext()
    {
    }

    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<AwardSession> AwardSessions { get; set; }

    public virtual DbSet<Creature> Creatures { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Landscape> Landscapes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationsToContent> LocationsToContents { get; set; }

    public virtual DbSet<Nomination> Nominations { get; set; }

    public virtual DbSet<NominationsSelectionOption> NominationsSelectionOptions { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PicturesToOther> PicturesToOthers { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<QuestsToItemsOrCreature> QuestsToItemsOrCreatures { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<StructuresToItemsOrCreature> StructuresToItemsOrCreatures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SharedConfiguration.DbConnectionString ?? "Data Source=localhost;Initial Catalog=DNDHelper;Integrated security=True");
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Award>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.User).WithMany(p => p.Awards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Awards_Users");
        });

        modelBuilder.Entity<AwardSession>(entity =>
        {
            entity.Property(e => e.ConnectionCode).IsRequired();

            entity.HasOne(d => d.Award).WithMany(p => p.AwardSessions)
                .HasForeignKey(d => d.AwardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AwardSessions_Awards");

            entity.HasOne(d => d.User).WithMany(p => p.AwardSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AwardSessions_Users");
        });

        modelBuilder.Entity<Creature>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.CreaturesId).HasColumnName("CreaturesID");
            entity.Property(e => e.StructuresId).HasColumnName("StructuresID");
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.Creatures).WithMany(p => p.Items)
                .HasForeignKey(d => d.CreaturesId)
                .HasConstraintName("FK_Items_Creatures");

            entity.HasOne(d => d.Structures).WithMany(p => p.Items)
                .HasForeignKey(d => d.StructuresId)
                .HasConstraintName("FK_Items_Structures");
        });

        modelBuilder.Entity<Landscape>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<LocationsToContent>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.Landscape).WithMany(p => p.LocationsToContents)
                .HasForeignKey(d => d.LandscapeId)
                .HasConstraintName("FK_LocationsToContents_Landscapes");

            entity.HasOne(d => d.Location).WithMany(p => p.LocationsToContents)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationsToContents_Locations");

            entity.HasOne(d => d.Structure).WithMany(p => p.LocationsToContents)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_LocationsToContents_Structures");
        });

        modelBuilder.Entity<Nomination>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.Awards).WithMany(p => p.Nominations)
                .HasForeignKey(d => d.AwardsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nominations_Awards");
        });

        modelBuilder.Entity<NominationsSelectionOption>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.Nomination).WithMany(p => p.NominationsSelectionOptions)
                .HasForeignKey(d => d.NominationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NominationsSelectionOptions_Nominations");

            entity.HasOne(d => d.User).WithMany(p => p.NominationsSelectionOptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Nominations_Users");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.Property(e => e.PicturePath).IsRequired();
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<PicturesToOther>(entity =>
        {
            entity.ToTable("PicturesToOther");

            entity.HasOne(d => d.Award).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.AwardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PicturesToOther_Awards");

            entity.HasOne(d => d.Creature).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.CreatureId)
                .HasConstraintName("FK_PicturesToOther_Creatures");

            entity.HasOne(d => d.Item).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_PicturesToOther_Items");

            entity.HasOne(d => d.Nomination).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PicturesToOther_Nominations");

            entity.HasOne(d => d.NominationsSelectionOption).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationsSelectionOptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PicturesToOther_NominationsSelectionOptions");

            entity.HasOne(d => d.Picture).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.PictureId)
                .HasConstraintName("FK_PicturesToOther_Pictures");

            entity.HasOne(d => d.Structure).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_PicturesToOther_Structures");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.Property(e => e.NextQuestId).HasColumnName("NextQuestID");
            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(d => d.NextQuest).WithMany(p => p.InverseNextQuest)
                .HasForeignKey(d => d.NextQuestId)
                .HasConstraintName("FK_Quests_Quests");
        });

        modelBuilder.Entity<QuestsToItemsOrCreature>(entity =>
        {
            entity.HasOne(d => d.Creature).WithMany(p => p.QuestsToItemsOrCreatures)
                .HasForeignKey(d => d.CreatureId)
                .HasConstraintName("FK_QuestsToItems_Creatures");

            entity.HasOne(d => d.Item).WithMany(p => p.QuestsToItemsOrCreatures)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_QuestsToItems_Items");

            entity.HasOne(d => d.Quest).WithMany(p => p.QuestsToItemsOrCreatures)
                .HasForeignKey(d => d.QuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestsToItems_Quests");
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.Property(e => e.Title).IsRequired();
        });

        modelBuilder.Entity<StructuresToItemsOrCreature>(entity =>
        {
            entity.Property(e => e.StructureId).HasColumnName("StructureID");

            entity.HasOne(d => d.Creature).WithMany(p => p.StructuresToItemsOrCreatures)
                .HasForeignKey(d => d.CreatureId)
                .HasConstraintName("FK_StructuresToItemsOrCreatures_Creatures");

            entity.HasOne(d => d.Item).WithMany(p => p.StructuresToItemsOrCreatures)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_StructuresToItemsOrCreatures_Items");

            entity.HasOne(d => d.Structure).WithMany(p => p.StructuresToItemsOrCreatures)
                .HasForeignKey(d => d.StructureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StructuresToItemsOrCreatures_Structure");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Login, "Unique_Users_Login").IsUnique();

            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.Property(e => e.TelegramUserName).IsRequired();

            entity.HasOne(d => d.NominationsSelectionOptions).WithMany(p => p.Votes)
                .HasForeignKey(d => d.NominationsSelectionOptionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Votes_NominationsSelectionOptions");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Votes_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
