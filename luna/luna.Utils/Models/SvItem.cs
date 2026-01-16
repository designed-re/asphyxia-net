using System;
using System.Collections.Generic;

namespace asphyxia.Models;

/// <summary>
/// Data store(Items) for Sound Voltex
/// </summary>
public partial class SvItem
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public byte Type { get; set; }

    public uint ItemId { get; set; }

    public uint Param { get; set; }

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
