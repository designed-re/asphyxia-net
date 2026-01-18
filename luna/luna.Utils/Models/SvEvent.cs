using System;

namespace luna.Models;

/// <summary>
/// Data store(Events) for Sound Voltex
/// </summary>
public partial class SvEvent
{
    public int Id { get; set; }

    public string Event { get; set; } = null!;

    public bool Enabled { get; set; }
}
