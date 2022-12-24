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
                optionsBuilder.UseNpgsql(SharedConfiguration.DbConnectionString ?? "Data Source=localhost;Initial Catalog=DNDHelper;Integrated security=True");
            }
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Awards_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Awards_id_seq\"'::regclass)");
            entity.Property(e => e.Description).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.User).WithMany(p => p.Awards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Awards_Users");
        });

        modelBuilder.Entity<AwardSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AwardSessions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"AwardSessions_id_seq\"'::regclass)");
            entity.Property(e => e.ConnectionCode)
                .IsRequired()
                .HasMaxLength(10000);

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
            entity.HasKey(e => e.Id).HasName("Creatures_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Creatures_id_seq\"'::regclass)");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Items_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Items_id_seq\"'::regclass)");
            entity.Property(e => e.CreaturesId).HasColumnName("CreaturesID");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.StructuresId).HasColumnName("StructuresID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.Creatures).WithMany(p => p.Items)
                .HasForeignKey(d => d.CreaturesId)
                .HasConstraintName("FK_Items_Creatures");

            entity.HasOne(d => d.Structures).WithMany(p => p.Items)
                .HasForeignKey(d => d.StructuresId)
                .HasConstraintName("FK_Items_Structures");
        });

        modelBuilder.Entity<Landscape>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Landscapes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Landscapes_id_seq\"'::regclass)");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Locations_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Locations_id_seq\"'::regclass)");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<LocationsToContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LocationsToContents_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"LocationsToContents_id_seq\"'::regclass)");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

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
            entity.HasKey(e => e.Id).HasName("Nominations_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Nominations_id_seq\"'::regclass)");
            entity.Property(e => e.Description).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.Awards).WithMany(p => p.Nominations)
                .HasForeignKey(d => d.AwardsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nominations_Awards");
        });

        modelBuilder.Entity<NominationsSelectionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NominationsSelectionOptions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"NominationsSelectionOptions_id_seq\"'::regclass)");
            entity.Property(e => e.Description).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.Nomination).WithMany(p => p.NominationsSelectionOptions)
                .HasForeignKey(d => d.NominationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NominationsSelectionOptions_Nominations");

            entity.HasOne(d => d.User).WithMany(p => p.NominationsSelectionOptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_NominationsSelectionOptions_Users");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pictures_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Pictures_id_seq\"'::regclass)");
            entity.Property(e => e.PicturePath)
                .IsRequired()
                .HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<PicturesToOther>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PicturesToOther_pkey");

            entity.ToTable("PicturesToOther");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"PicturesToOther_id_seq\"'::regclass)");

            entity.HasOne(d => d.Award).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.AwardId)
                .HasConstraintName("FK_PicturesToOther_Awards");

            entity.HasOne(d => d.Creature).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.CreatureId)
                .HasConstraintName("FK_PicturesToOther_Creatures");

            entity.HasOne(d => d.Item).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_PicturesToOther_Items");

            entity.HasOne(d => d.Nomination).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationId)
                .HasConstraintName("FK_PicturesToOther_Nominations");

            entity.HasOne(d => d.NominationsSelectionOption).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationsSelectionOptionId)
                .HasConstraintName("FK_PicturesToOther_NominationsSelectionOptions");

            entity.HasOne(d => d.Picture).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.PictureId)
                .HasConstraintName("fk_picturestoother_pictures");

            entity.HasOne(d => d.Structure).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.StructureId)
                .HasConstraintName("FK_PicturesToOther_Structures");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Quests_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Quests_id_seq\"'::regclass)");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.IsComplited)
                .IsRequired()
                .HasDefaultValueSql("true");
            entity.Property(e => e.NextQuestId).HasColumnName("NextQuestID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.NextQuest).WithMany(p => p.InverseNextQuest)
                .HasForeignKey(d => d.NextQuestId)
                .HasConstraintName("FK_Quests_Quests");
        });

        modelBuilder.Entity<QuestsToItemsOrCreature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("QuestsToItemsOrCreatures_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"QuestsToItemsOrCreatures_id_seq\"'::regclass)");

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
            entity.HasKey(e => e.Id).HasName("Structures_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Structures_id_seq\"'::regclass)");
            entity.Property(e => e.FlavorText).HasMaxLength(10000);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<StructuresToItemsOrCreature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StructuresToItemsOrCreatures_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"StructuresToItemsOrCreatures_id_seq\"'::regclass)");
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
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Users_id_seq\"'::regclass)");
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(10000);
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Votes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Votes_id_seq\"'::regclass)");
            entity.Property(e => e.TelegramUserName)
                .IsRequired()
                .HasMaxLength(10000);

            entity.HasOne(d => d.NominationsSelectionOptions).WithMany(p => p.Votes)
                .HasForeignKey(d => d.NominationsSelectionOptionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Votes_NominationsSelectionOptions");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Votes_Users");
        });
        modelBuilder.HasSequence<int>("Awards_id_seq");
        modelBuilder.HasSequence<int>("AwardSessions_id_seq");
        modelBuilder.HasSequence<int>("Creatures_id_seq");
        modelBuilder.HasSequence<int>("Items_id_seq");
        modelBuilder.HasSequence<int>("Landscapes_id_seq");
        modelBuilder.HasSequence<int>("Locations_id_seq");
        modelBuilder.HasSequence<int>("LocationsToContents_id_seq");
        modelBuilder.HasSequence<int>("Nominations_id_seq");
        modelBuilder.HasSequence<int>("NominationsSelectionOptions_id_seq");
        modelBuilder.HasSequence<int>("Pictures_id_seq");
        modelBuilder.HasSequence<int>("PicturesToOther_id_seq");
        modelBuilder.HasSequence<int>("Quests_id_seq");
        modelBuilder.HasSequence<int>("QuestsToItemsOrCreatures_id_seq");
        modelBuilder.HasSequence<int>("Structures_id_seq");
        modelBuilder.HasSequence<int>("StructuresToItemsOrCreatures_id_seq");
        modelBuilder.HasSequence<int>("Users_id_seq");
        modelBuilder.HasSequence<int>("Votes_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
