using System;
using System.Collections.Generic;

namespace asphyxia.Models;

/// <summary>
/// Stores e-amusement cards
/// </summary>
public partial class Card
{
    public int Id { get; set; }

    public string CardId { get; set; } = null!;

    public string CardNo { get; set; } = null!;

    /// <summary>
    /// same with dataid
    /// </summary>
    public string RefId { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public int Paseli { get; set; }

    public string? PaseliSession { get; set; }

    public virtual SvProfile? SvProfile { get; set; }
}
