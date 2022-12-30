using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicDB.Models
{
    public partial class AsphyxiaContext : DbContext
    {
        public AsphyxiaContext()
        {
        }

        public AsphyxiaContext(DbContextOptions<AsphyxiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<SvItem> SvItems { get; set; } = null!;
        public virtual DbSet<SvMusic> SvMusics { get; set; } = null!;
        public virtual DbSet<SvParam> SvParams { get; set; } = null!;
        public virtual DbSet<SvProfile> SvProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=asphyxia;user id=root;password=hide", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.9.2-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("card");

                entity.HasComment("Stores e-amusement cards");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CardId)
                    .HasMaxLength(16)
                    .HasColumnName("card_id")
                    .IsFixedLength();

                entity.Property(e => e.CardNo)
                    .HasMaxLength(16)
                    .HasColumnName("card_no")
                    .IsFixedLength();

                entity.Property(e => e.RefId)
                    .HasMaxLength(16)
                    .HasColumnName("ref_id")
                    .IsFixedLength();
            });

            modelBuilder.Entity<SvItem>(entity =>
            {
                entity.ToTable("sv_items");

                entity.HasComment("Data store(Items) for Sound Voltex");

                entity.HasIndex(e => e.Profile, "FK_profile_to_card(id)");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("item_id");

                entity.Property(e => e.Param)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("param");

                entity.Property(e => e.Profile)
                    .HasColumnType("int(11)")
                    .HasColumnName("profile");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("type");

                entity.HasOne(d => d.ProfileNavigation)
                    .WithMany(p => p.SvItems)
                    .HasForeignKey(d => d.Profile)
                    .HasConstraintName("FK_profile_to_card(id)");
            });

            modelBuilder.Entity<SvMusic>(entity =>
            {
                entity.ToTable("sv_music");

                entity.HasComment("Data store(Music) for Sound Voltex");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Artist)
                    .HasMaxLength(200)
                    .HasColumnName("artist");

                entity.Property(e => e.ArtistYomigana)
                    .HasMaxLength(200)
                    .HasColumnName("artist_yomigana");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.Property(e => e.TitleYomigana)
                    .HasMaxLength(200)
                    .HasColumnName("title_yomigana");
            });

            modelBuilder.Entity<SvParam>(entity =>
            {
                entity.ToTable("sv_params");

                entity.HasComment("Data store(Params) for Sound Voltex");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Param)
                    .HasColumnType("int(11)")
                    .HasColumnName("param");

                entity.Property(e => e.ParamCount)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("param_count");

                entity.Property(e => e.ParamId)
                    .HasColumnType("int(11)")
                    .HasColumnName("param_id");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<SvProfile>(entity =>
            {
                entity.ToTable("sv_profile");

                entity.HasComment("Data store(Profile) for Sound Voltex");

                entity.HasIndex(e => e.Card, "FK_card_to_card(id)");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AppealId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("appeal_id");

                entity.Property(e => e.ArsOption)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("ars_option");

                entity.Property(e => e.Bgm)
                    .HasColumnType("int(11)")
                    .HasColumnName("bgm");

                entity.Property(e => e.BlasterCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("blaster_count");

                entity.Property(e => e.BlasterEnergy)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("blaster_energy");

                entity.Property(e => e.BlasterPassEnable)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("blaster_pass_enable");

                entity.Property(e => e.BlasterPassLimitDate)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("blaster_pass_limit_date");

                entity.Property(e => e.Card)
                    .HasColumnType("int(11)")
                    .HasColumnName("card");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code")
                    .IsFixedLength();

                entity.Property(e => e.DayCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("day_count");

                entity.Property(e => e.DrawAdjust)
                    .HasColumnType("int(11)")
                    .HasColumnName("draw_adjust");

                entity.Property(e => e.EarlyLateDisp)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("early_late_disp");

                entity.Property(e => e.EffCLeft)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("eff_c_left");

                entity.Property(e => e.EffCRight)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("eff_c_right");

                entity.Property(e => e.ExtrackEnergy)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("extrack_energy");

                entity.Property(e => e.GaugeOption)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("gauge_option");

                entity.Property(e => e.Headphone)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("headphone");

                entity.Property(e => e.Hispeed)
                    .HasColumnType("int(11)")
                    .HasColumnName("hispeed");

                entity.Property(e => e.KacId)
                    .HasMaxLength(8)
                    .HasColumnName("kac_id")
                    .HasDefaultValueSql("'VOLTEX'");

                entity.Property(e => e.Lanespeed)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("lanespeed");

                entity.Property(e => e.LastMusicId)
                    .HasColumnType("int(11)")
                    .HasColumnName("last_music_id");

                entity.Property(e => e.LastMusicType)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("last_music_type");

                entity.Property(e => e.MaxPlayChain)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("max_play_chain");

                entity.Property(e => e.MaxWeekChain)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("max_week_chain");

                entity.Property(e => e.Name)
                    .HasMaxLength(8)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'VOLTEX'");

                entity.Property(e => e.Nemsys)
                    .HasColumnType("int(11)")
                    .HasColumnName("nemsys");

                entity.Property(e => e.NotesOption)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("notes_option");

                entity.Property(e => e.Pcb)
                    .HasColumnType("int(11)")
                    .HasColumnName("pcb")
                    .HasComment("equals with block_no");

                entity.Property(e => e.PlayChain)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("play_chain");

                entity.Property(e => e.PlayCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("play_count");

                entity.Property(e => e.SkillBaseId)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("skill_base_id");

                entity.Property(e => e.SkillLevel)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("skill_level");

                entity.Property(e => e.SkillNameId)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("skill_name_id");

                entity.Property(e => e.SortType)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("sort_type");

                entity.Property(e => e.StampA)
                    .HasColumnType("int(11)")
                    .HasColumnName("stampA");

                entity.Property(e => e.StampB)
                    .HasColumnType("int(11)")
                    .HasColumnName("stampB");

                entity.Property(e => e.StampC)
                    .HasColumnType("int(11)")
                    .HasColumnName("stampC");

                entity.Property(e => e.StampD)
                    .HasColumnType("int(11)")
                    .HasColumnName("stampD");

                entity.Property(e => e.SubBg)
                    .HasColumnType("int(11)")
                    .HasColumnName("sub_bg");

                entity.Property(e => e.TodayCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("today_count");

                entity.Property(e => e.WeekChain)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("week_chain");

                entity.Property(e => e.WeekCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("week_count");

                entity.Property(e => e.WeekPlayCount)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("week_play_count");

                entity.HasOne(d => d.CardNavigation)
                    .WithMany(p => p.SvProfiles)
                    .HasForeignKey(d => d.Card)
                    .HasConstraintName("FK_card_to_card(id)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
