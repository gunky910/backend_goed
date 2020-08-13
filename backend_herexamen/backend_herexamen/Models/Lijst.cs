using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularAPI.Models
{
    public class Lijst
    {
        [Key]
        public int lijstID { get; set; }
        public string naam { get; set; }
        public string beschrijving { get; set; }
        public DateTime startDatum { get; set; }
        public DateTime eindDatum { get; set; }
        public int gebruikerID { get; set; }
        public ICollection<Item> items { get; set; }

}
    }