using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace angularAPI.Models
{
    public class Stem
    {
        [Key]
        public int stemID { get; set; }
        public int itemID { get; set; }
        public int gebruikerID { get; set; }
        
    }
}