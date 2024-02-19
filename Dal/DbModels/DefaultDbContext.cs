﻿using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<AwardEvent> AwardEvents { get; set; }

    public virtual DbSet<AwardSession> AwardSessions { get; set; }

    public virtual DbSet<Nomination> Nominations { get; set; }

    public virtual DbSet<NominationsSelectionOption> NominationsSelectionOptions { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PicturesToOther> PicturesToOthers { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NewDNDHelper;Username=postgres;Password=1113");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Awards_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Awards_id_seq\"'::regclass)");

            entity.HasOne(d => d.User).WithMany(p => p.Awards)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Awards_Users");
        });

        modelBuilder.Entity<AwardEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AwardEvents_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"AwardEvents_id_seq\"'::regclass)");

            entity.HasOne(d => d.Awards).WithMany(p => p.AwardEvents)
                .HasForeignKey(d => d.AwardsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AwardEvents_Awards");
        });

        modelBuilder.Entity<AwardSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AwardSessions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"AwardSessions_id_seq\"'::regclass)");

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

            entity.HasOne(d => d.Awards).WithMany(p => p.Nominations)
                .HasForeignKey(d => d.AwardsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nominations_Awards");

            entity.HasOne(d => d.Reader).WithMany(p => p.Nominations)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nominations_Readers");
        });

        modelBuilder.Entity<NominationsSelectionOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NominationsSelectionOptions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"NominationsSelectionOptions_id_seq\"'::regclass)");

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
        });

        modelBuilder.Entity<PicturesToOther>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PicturesToOther_pkey");

            entity.ToTable("PicturesToOther");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"PicturesToOther_id_seq\"'::regclass)");

            entity.HasOne(d => d.AwardEvent).WithMany(p => p.PicturesToOthers)
                .HasForeignKey(d => d.AwardEventId)
                .HasConstraintName("FK_PicturesToOther_AwardEvents");

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

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Readers_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Readers_id_seq\"'::regclass)");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Users_id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Votes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Votes_id_seq\"'::regclass)");

            entity.HasOne(d => d.NominationsSelectionOptions).WithMany(p => p.Votes)
                .HasForeignKey(d => d.NominationsSelectionOptionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Votes_NominationsSelectionOptions");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Votes_Users");
        });
        modelBuilder.HasSequence<int>("AwardEvents_id_seq");
        modelBuilder.HasSequence<int>("Awards_id_seq");
        modelBuilder.HasSequence<int>("AwardSessions_id_seq");
        modelBuilder.HasSequence<int>("Nominations_id_seq");
        modelBuilder.HasSequence<int>("NominationsSelectionOptions_id_seq");
        modelBuilder.HasSequence<int>("Pictures_id_seq");
        modelBuilder.HasSequence<int>("PicturesToOther_id_seq");
        modelBuilder.HasSequence<int>("Readers_id_seq");
        modelBuilder.HasSequence<int>("Users_id_seq");
        modelBuilder.HasSequence<int>("Votes_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
