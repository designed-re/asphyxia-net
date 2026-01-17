using System;
using System.Collections.Generic;

namespace luna.Models;

/// <summary>
/// Data store(Scores) for Sound Voltex
/// </summary>
public partial class SvScore
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int MusicId { get; set; }

    public int Type { get; set; }

    public int Score { get; set; }

    public int Exscore { get; set; }

    public int Clear { get; set; }

    public int Grade { get; set; }

    public int ButtonRate { get; set; }

    public int LongRate { get; set; }

    public int VolRate { get; set; }

    public virtual SvMusic Music { get; set; } = null!;

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
