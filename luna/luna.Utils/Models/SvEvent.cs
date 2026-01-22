using System;

namespace luna.Utils.Models;

/// <summary>
/// Data store(Events) for Sound Voltex
/// </summary>
public partial class SvEvent
{
    public int Id { get; set; }

    public string Event { get; set; } = null!;

    public bool Enabled { get; set; }

    public int Version { get; set; }
}
