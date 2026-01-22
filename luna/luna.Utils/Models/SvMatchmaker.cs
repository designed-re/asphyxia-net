using System;

namespace luna.Utils.Models;

/// <summary>
/// Data store(Matchmaker) for Sound Voltex global matching
/// </summary>
public partial class SvMatchmaker
{
    public int Id { get; set; }

    public int Version { get; set; }

    public long Timestamp { get; set; }

    public int CVersion { get; set; }

    public int PlayerNum { get; set; }

    public int PlayerRemaining { get; set; }

    public int Filter { get; set; }

    public int MusicId { get; set; }

    public int Seconds { get; set; }

    public int Port { get; set; }

    public string GlobalIp { get; set; } = null!;

    public string LocalIp { get; set; } = null!;

    public int Claim { get; set; }

    public int EntryId { get; set; }
}
