﻿using System;
using System.Collections.Generic;

namespace asphyxia.Models;

/// <summary>
/// Data store(Music) for Sound Voltex
/// </summary>
public partial class SvMusic
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string TitleYomigana { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public string ArtistYomigana { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual ICollection<SvScore> SvScores { get; } = new List<SvScore>();
}
