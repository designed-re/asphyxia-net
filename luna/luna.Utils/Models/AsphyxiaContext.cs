using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace luna.Models;

public partial class AsphyxiaContext : DbContext
{
    public AsphyxiaContext()
    {
    }

    private readonly IConfigurationRoot _config;
    // public AsphyxiaContext(IConfigurationRoot config)
    // {
    //     _config = config;
    // }

    public AsphyxiaContext(DbContextOptions<AsphyxiaContext> options)
        : base(options)
    {   
    }

    public virtual DbSet<Card> Cards { get; set; }
    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<SvItem> SvItems { get; set; }

    public virtual DbSet<SvMusic> SvMusics { get; set; }

    public virtual DbSet<SvParam> SvParams { get; set; }

    public virtual DbSet<SvProfile> SvProfiles { get; set; }

    public virtual DbSet<SvScore> SvScores { get; set; }

    public virtual DbSet<SvCourseRecord> SvCourseRecords { get; set; }

    public virtual DbSet<ValgeneTicket> ValgeneTickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("card", tb => tb.HasComment("Stores e-amusement cards"));

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CardId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("card_id");
            entity.Property(e => e.CardNo)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("card_no");
            entity.Property(e => e.Paseli)
                .HasColumnType("int(11)")
                .HasColumnName("paseli");
            entity.Property(e => e.PaseliSession)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("paseli_session");
            entity.Property(e => e.Pass)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("pass");
            entity.Property(e => e.RefId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasComment("same with dataid")
                .HasColumnName("ref_id");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("facility", tb => tb.HasComment("Stores facility"));

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.PCBId)
                .HasColumnName("pcb_id");
            entity.Property(e => e.Country)
                .HasColumnName("country");
            entity.Property(e => e.Region)
                .HasColumnName("region");
            entity.Property(e => e.Name)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasColumnType("int(11)")
                .HasColumnName("type")
                .HasDefaultValue(0);
            entity.Property(e => e.CountryName)
                .HasColumnName("country_name");
            entity.Property(e => e.CountryJName)
                .HasColumnName("country_jname");
            entity.Property(e => e.RegionName)
                .HasColumnName("region_name");
            entity.Property(e => e.RegionJName)
                .HasColumnName("region_jname");
            entity.Property(e => e.CustomerCode)
                .HasColumnName("customer_code");
            entity.Property(e => e.CompanyCode)
                .HasColumnName("company_code");
            entity.Property(e => e.FacilityId)
                .HasColumnName("facility_id");

        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<SvItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_items", tb => tb.HasComment("Data store(Items) for Sound Voltex"));

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

            entity.HasOne(d => d.ProfileNavigation).WithMany(p => p.SvItems)
                .HasForeignKey(d => d.Profile)
                .HasConstraintName("FK_profile_to_card(id)");
        });

        modelBuilder.Entity<SvMusic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_music", tb => tb.HasComment("Data store(Music) for Sound Voltex"));

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
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_params", tb => tb.HasComment("Data store(Params) for Sound Voltex"));

            entity.HasIndex(e => e.Profile, "FK_param_profile_to_profile(id)");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Param)
                .HasMaxLength(-1)
                .HasColumnName("param");
            entity.Property(e => e.ParamCount)
                .HasColumnType("int(11) unsigned")
                .HasColumnName("param_count");
            entity.Property(e => e.ParamId)
                .HasColumnType("int(11)")
                .HasColumnName("param_id");
            entity.Property(e => e.Profile)
                .HasColumnType("int(11)")
                .HasColumnName("profile");
            entity.Property(e => e.Type)
                .HasColumnType("int(11)")
                .HasColumnName("type");

            entity.HasOne(d => d.ProfileNavigation).WithMany(p => p.SvParams)
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_param_profile_to_profile(id)");
        });

        modelBuilder.Entity<SvProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_profile", tb => tb.HasComment("Data store(Profile) for Sound Voltex"));

            entity.HasIndex(e => e.Card, "card").IsUnique();

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
                .IsFixedLength()
                .HasColumnName("code");
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
                .HasDefaultValueSql("'1'")
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
                .HasDefaultValueSql("'VOLTEX'")
                .HasColumnName("kac_id");
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
                .HasDefaultValueSql("'VOLTEX'")
                .HasColumnName("name");
            entity.Property(e => e.Nemsys)
                .HasColumnType("int(11)")
                .HasColumnName("nemsys");
            entity.Property(e => e.NotesOption)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("notes_option");
            entity.Property(e => e.Pcb)
                .HasComment("equals with block_no")
                .HasColumnType("int(11)")
                .HasColumnName("pcb");
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

            entity.HasOne(d => d.CardNavigation).WithOne(p => p.SvProfile)
                .HasForeignKey<SvProfile>(d => d.Card)
                .HasConstraintName("FK_card_to_card(id)");
        });

        modelBuilder.Entity<SvScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_scores", tb => tb.HasComment("Data store(Scores) for Sound Voltex"));

            entity.HasIndex(e => e.MusicId, "FK_musicid_to_music(id)");

            entity.HasIndex(e => e.Profile, "FK_profile_to_profile(id)");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ButtonRate)
                .HasColumnType("int(11)")
                .HasColumnName("buttonRate");
            entity.Property(e => e.Clear)
                .HasColumnType("int(11)")
                .HasColumnName("clear");
            entity.Property(e => e.Exscore)
                .HasColumnType("int(11)")
                .HasColumnName("exscore");
            entity.Property(e => e.Grade)
                .HasColumnType("int(11)")
                .HasColumnName("grade");
            entity.Property(e => e.LongRate)
                .HasColumnType("int(11)")
                .HasColumnName("longRate");
            entity.Property(e => e.MusicId)
                .HasColumnType("int(11)")
                .HasColumnName("music_id");
            entity.Property(e => e.Profile)
                .HasColumnType("int(11)")
                .HasColumnName("profile");
            entity.Property(e => e.Score)
                .HasColumnType("int(11)")
                .HasColumnName("score");
            entity.Property(e => e.Type)
                .HasColumnType("int(11)")
                .HasColumnName("type");
            entity.Property(e => e.VolRate)
                .HasColumnType("int(11)")
                .HasColumnName("volRate");

            entity.HasOne(d => d.Music).WithMany(p => p.SvScores)
                .HasForeignKey(d => d.MusicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_musicid_to_music(id)");

            entity.HasOne(d => d.ProfileNavigation).WithMany(p => p.SvScores)
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_profile_to_profile(id)");
        });

        modelBuilder.Entity<SvCourseRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_course_records", tb => tb.HasComment("Data store(Course Records) for Sound Voltex"));

            entity.HasIndex(e => e.Profile, "FK_course_profile_to_profile(id)");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Profile)
                .HasColumnType("int(11)")
                .HasColumnName("profile");
            entity.Property(e => e.SeriesId)
                .HasColumnType("int(11)")
                .HasColumnName("series_id");
            entity.Property(e => e.CourseId)
                .HasColumnType("int(11)")
                .HasColumnName("course_id");
            entity.Property(e => e.Version)
                .HasColumnType("int(11)")
                .HasColumnName("version");
            entity.Property(e => e.Score)
                .HasColumnType("int(11)")
                .HasColumnName("score");
            entity.Property(e => e.Clear)
                .HasColumnType("int(11)")
                .HasColumnName("clear");
            entity.Property(e => e.Grade)
                .HasColumnType("int(11)")
                .HasColumnName("grade");
            entity.Property(e => e.Rate)
                .HasColumnType("int(11)")
                .HasColumnName("rate");
            entity.Property(e => e.Count)
                .HasColumnType("int(11)")
                .HasColumnName("count");

            entity.HasOne(d => d.ProfileNavigation).WithMany(p => p.SvCourseRecords)
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_profile_to_profile(id)");
        });

        modelBuilder.Entity<ValgeneTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sv_valgene_tickets", tb => tb.HasComment("Data store(Valgene Tickets) for Sound Voltex"));

            entity.HasIndex(e => e.Profile, "FK_valgene_profile_to_profile(id)");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Profile)
                .HasColumnType("int(11)")
                .HasColumnName("profile");
            entity.Property(e => e.TicketNum)
                .HasColumnType("int(11)")
                .HasColumnName("ticket_num");
            entity.Property(e => e.LimitDate)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("limit_date");

            entity.HasOne(d => d.ProfileNavigation).WithMany(p => p.ValgeneTickets)
                .HasForeignKey(d => d.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_valgene_profile_to_profile(id)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
