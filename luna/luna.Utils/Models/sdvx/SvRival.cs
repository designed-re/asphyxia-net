using System;

namespace luna.Utils.Models.sdvx;

/// <summary>
/// Data store(Rival) for Sound Voltex
/// </summary>
public partial class SvRival
{
    public int Id { get; set; }

    public string RefId { get; set; } = null!;

    public string RivalRefId { get; set; } = null!;

    public int SdvxId { get; set; }

    public string Name { get; set; } = null!;

    public bool Mutual { get; set; }

    public int Version { get; set; }

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
