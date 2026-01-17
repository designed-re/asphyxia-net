using System;
using System.Collections.Generic;

namespace luna.Models;

/// <summary>
/// Data store(Profile) for Sound Voltex
/// </summary>
public partial class SvProfile
{
    public int Id { get; set; }

    public int Card { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public ushort AppealId { get; set; }

    public int LastMusicId { get; set; }

    public byte LastMusicType { get; set; }

    public byte SortType { get; set; }

    public byte Headphone { get; set; }

    public uint BlasterEnergy { get; set; }

    public uint BlasterCount { get; set; }

    public ushort ExtrackEnergy { get; set; }

    public int Hispeed { get; set; }

    public uint Lanespeed { get; set; }

    public byte GaugeOption { get; set; }

    public byte ArsOption { get; set; }

    public byte NotesOption { get; set; }

    public byte EarlyLateDisp { get; set; }

    public int DrawAdjust { get; set; }

    public byte EffCLeft { get; set; }

    public byte EffCRight { get; set; }

    public string KacId { get; set; } = null!;

    public short SkillLevel { get; set; }

    public short SkillBaseId { get; set; }

    public short SkillNameId { get; set; }

    public sbyte BlasterPassEnable { get; set; }

    public ulong BlasterPassLimitDate { get; set; }

    /// <summary>
    /// equals with block_no
    /// </summary>
    public int Pcb { get; set; }

    public uint PlayCount { get; set; }

    public uint DayCount { get; set; }

    public uint TodayCount { get; set; }

    public uint PlayChain { get; set; }

    public uint MaxPlayChain { get; set; }

    public uint WeekCount { get; set; }

    public uint WeekPlayCount { get; set; }

    public uint WeekChain { get; set; }

    public uint MaxWeekChain { get; set; }

    public int Bgm { get; set; }

    public int SubBg { get; set; }

    public int Nemsys { get; set; }

    public int StampA { get; set; }

    public int StampB { get; set; }

    public int StampC { get; set; }

    public int StampD { get; set; }

    public virtual Card CardNavigation { get; set; } = null!;

    public virtual ICollection<SvItem> SvItems { get; } = new List<SvItem>();

    public virtual ICollection<SvParam> SvParams { get; } = new List<SvParam>();

    public virtual ICollection<SvScore> SvScores { get; } = new List<SvScore>();
}
