using System;
using System.Collections.Generic;

namespace luna.Utils.Models;

/// <summary>
/// Data store(Params) for Sound Voltex
/// </summary>
public partial class SvParam
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int Type { get; set; }

    public int ParamId { get; set; }

    public string Param { get; set; }

    public uint ParamCount { get; set; }

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
