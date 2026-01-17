using System;
using System.Collections.Generic;

namespace luna.Models;

/// <summary>
/// Data store(Valgene Ticket) for Sound Voltex
/// </summary>
public partial class ValgeneTicket
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int TicketNum { get; set; }

    public ulong LimitDate { get; set; }

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
