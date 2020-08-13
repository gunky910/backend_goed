using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace angularAPI.Models
{
    public class Gebruiker
    {
        [Key]
        public int gebruikerID { get; set; }
        public string email { get; set; }
        public string wachtwoord { get; set; }
        public string gebruikersnaam { get; set; }
        
        public ICollection<Lijst> lijsten { get; set; }
        [NotMapped]
        public string token { get; set; }

    }
}