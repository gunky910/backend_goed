using angularAPI.Models;
using System;
using System.Linq;

public class DBInitializer
{public static void Initialize(angularAPI.Models.Context context)
    {
        context.Database.EnsureCreated();
 
        if(context.Gebruikers.Any()){
            return;   // DB has been seeded
        }
        for (int i = 1; i <= 5; i++)
        {
            context.Gebruikers.Add(new Gebruiker { email = "gebruiker" + i + "@hotmail.com", gebruikersnaam = "gebruiker" + i, wachtwoord = "gebruiker" + i });
            context.SaveChanges();
            context.Lijsten.Add(new Lijst { naam = "lijst" + i, beschrijving = "voorbeeld van een beschrijving " + i, startDatum = new DateTime(), eindDatum = new DateTime(), gebruikerID = i });
            context.SaveChanges();
            context.Items.Add(new Item { naam = "item" + i, beschrijving = "voorbeeld van een item " + i, foto = "item" + i, lijstID = i });
            context.SaveChanges();
            context.Stemmen.Add(new Stem { gebruikerID = i, itemID = i });
            context.SaveChanges();
        }
        context.SaveChanges();
    }

}