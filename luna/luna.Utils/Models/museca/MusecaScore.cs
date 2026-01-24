using System;
using System.Collections.Generic;

namespace luna.Utils.Models.museca;

/// <summary>
/// Data store(Scores) for Museca
/// </summary>
public partial class MusecaScore
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int MusicId { get; set; }

    public int Type { get; set; }

    public int Score { get; set; }

    public int Count { get; set; }

    public int ClearType { get; set; }

    public int ScoreGrade { get; set; }

    public int ButtonRate { get; set; }

    public int LongRate { get; set; }

    public int VolRate { get; set; }

    public virtual MusecaProfile ProfileNavigation { get; set; } = null!;
}
