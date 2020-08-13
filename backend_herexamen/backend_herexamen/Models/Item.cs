using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularAPI.Models
{
    public class Item
    {
        [Key]
        public int itemID { get; set; }
        public int lijstID { get; set; }
        public string naam { get; set; }
        public string beschrijving { get; set; }
        public string foto { get; set; }

        public ICollection<Stem> stemmen { get; set; }

    }
}