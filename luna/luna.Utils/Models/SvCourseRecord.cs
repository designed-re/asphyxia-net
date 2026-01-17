using System;
using System.Collections.Generic;

namespace luna.Models;

/// <summary>
/// Data store(Course Records) for Sound Voltex
/// </summary>
public partial class SvCourseRecord
{
    public int Id { get; set; }

    public int Profile { get; set; }

    public int SId { get; set; }

    public int CourseId { get; set; }

    public int Version { get; set; }

    public int Score { get; set; }

    public int Clear { get; set; }

    public int Grade { get; set; }

    public int Rate { get; set; }

    public int Count { get; set; }

    public virtual SvProfile ProfileNavigation { get; set; } = null!;
}
