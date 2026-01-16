using System;
using System.Collections.Generic;

namespace MusicDB.Models
{
    /// <summary>
    /// Stores e-amusement cards
    /// </summary>
    public partial class Card
    {
        public Card()
        {
            SvProfiles = new HashSet<SvProfile>();
        }

        public int Id { get; set; }
        public string CardId { get; set; } = null!;
        public string CardNo { get; set; } = null!;
        public string RefId { get; set; } = null!;

        public virtual ICollection<SvProfile> SvProfiles { get; set; }
    }
}
