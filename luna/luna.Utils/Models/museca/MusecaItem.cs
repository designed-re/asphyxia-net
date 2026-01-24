using System;
using System.Collections.Generic;

namespace luna.Utils.Models.museca;

/// <summary>
/// Data store(Items) for Museca
/// </summary>
public partial class MusecaItem
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int ItemId { get; set; }

    public int Type { get; set; }

    public int Param { get; set; }

    public virtual MusecaProfile ProfileNavigation { get; set; } = null!;
}
