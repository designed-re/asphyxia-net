using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asphyxia.Models
{
    public class Player
    {
        public int ID { get; set; }

        public string Passwd { get; set; }

        public string? Name { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
