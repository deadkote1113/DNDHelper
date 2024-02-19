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

    public virtual DbSet<Nomination> Nominations { get; set; }

    public virtual DbSet<NominationsSelectionOption> NominationsSelectionOptions { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PicturesToOther> PicturesToOthers { get; set; }

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

            entity.HasOne(d => d.Nomination).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationId)
                .HasConstraintName("FK_PicturesToOther_Nominations");

            entity.HasOne(d => d.NominationsSelectionOption).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.NominationsSelectionOptionId)
                .HasConstraintName("FK_PicturesToOther_NominationsSelectionOptions");

            entity.HasOne(d => d.Picture).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.PictureId)
                .HasConstraintName("fk_picturestoother_pictures");
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
