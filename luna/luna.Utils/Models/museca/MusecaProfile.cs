using System;
using System.Collections.Generic;

namespace luna.Utils.Models.museca;

/// <summary>
/// Data store(Profile) for Museca
/// </summary>
public partial class MusecaProfile
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string HiddenParam { get; set; } = null!;

    public int PlayCount { get; set; }

    public int DailyCount { get; set; }

    public int PlayChain { get; set; }

    public byte Headphone { get; set; }

    public int AppealId { get; set; }

    public int CommentId { get; set; }

    public int LastMusicId { get; set; }

    public byte LastMusicType { get; set; }

    public byte SortType { get; set; }

    public byte NarrowDown { get; set; }

    public byte GaugeOption { get; set; }

    public uint BlasterEnergy { get; set; }

    public uint BlasterCount { get; set; }

    public int CreatorId { get; set; }

    public short SkillLevel { get; set; }

    public short SkillNameId { get; set; }

    public int GamecoinPacket { get; set; }

    public int GamecoinBlock { get; set; }

    public int PacketBooster { get; set; }

    public int BlockBooster { get; set; }

    public virtual ICollection<MusecaItem> MusecaItems { get; } = new List<MusecaItem>();

    public virtual ICollection<MusecaScore> MusecaScores { get; } = new List<MusecaScore>();
}

