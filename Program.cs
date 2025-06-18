using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoskildeDyreinternat.Repositories;
using RoskildeDyreinternat.UI;

namespace RoskildeDyreinternat
{

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Midlertidig null
            BesøgRepo midlertidigBesøgRepo = null;

            // 2. Opret BrugerRepo først
            BrugerRepo brugerRepo = new BrugerRepo(midlertidigBesøgRepo);

            // 3. Opret DyrRepo
            DyrRepo dyrRepo = new DyrRepo(brugerRepo); // brugerRepo = IAdgangsKontrol

            // 4. Opret BesøgRepo
            BesøgRepo besøgRepo = new BesøgRepo(brugerRepo);

            // 5. Sæt BesøgRepo i BrugerRepo (færdiggør koblingen)
            brugerRepo.besøgRepo = besøgRepo;
          


            //Oprettet kunder
            Kunde kunde1 = new Kunde(99, "Ida", "IdaEmail", "1234567", "Højvej 1", "Kunde", 23, "Mand");
            Kunde kunde2 = new Kunde(98, "Lone", "LoneEmail", "7654321", "Blomstervej", "Kunde", 67, "Kvinde");
            Kunde kunde3 = new Kunde(97, "Læse", "Læse@mail.com", "12345678", "Durumvej 20", "Kunde", 34, "Mand");
            Kunde kunde4 = new Kunde(96, "Luke", "luke@gmsil.com", "2323232", "Sezzamvej35 2.tv", "Kunde", 36, "Mand");
            Kunde kunde5 = new Kunde(95, "Anna", "anna@mail.com", "99887766", "Roservej 12", "Kunde", 28, "Kvinde");
            Kunde kunde6 = new Kunde(94, "Mikkel", "mikkel@mail.com", "55443322", "Lærkevej 8", "Kunde", 41, "Mand");
            Kunde kunde7 = new Kunde(93, "Sofie", "sofie@mail.com", "11223344", "Bøgvej 5", "Kunde", 35, "Kvinde");
            Kunde kunde8 = new Kunde(92, "Jonas", "jonas@mail.com", "66778899", "Egevej 15", "Kunde", 22, "Mand");
            Kunde kunde9 = new Kunde(91, "Maria", "maria@mail.com", "22334455", "Pilvej 9", "Kunde", 30, "Kvinde");
            Kunde kunde10 = new Kunde(90, "Peter", "peter@mail.com", "77665544", "Fyrvej 3", "Kunde", 45, "Mand");

            brugerRepo.OpretKunde(kunde1);
            brugerRepo.OpretKunde(kunde2);
            brugerRepo.OpretKunde(kunde3);
            brugerRepo.OpretKunde(kunde4);
            brugerRepo.OpretKunde(kunde5);
            brugerRepo.OpretKunde(kunde6);
            brugerRepo.OpretKunde(kunde7);
            brugerRepo.OpretKunde(kunde8);
            brugerRepo.OpretKunde(kunde9);
            brugerRepo.OpretKunde(kunde10);

            //Oprettet medarbejder
            Medarbejder medarbejder1 = new Medarbejder(1, "Emil", "emil@hund.dk", "+45 232323", "Denvej 7", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder2 = new Medarbejder(2, "Emma", "emma@kat.dk", "+45 34 34 34 34", "Gladvej 2", "Medarbejder", "Dyrpasser", 37);
            Medarbejder medarbejder3 = new Medarbejder(3, "Erik", "erik@doctor.dk", "+45 11 11 22 22", "Flotvej 63", "Medarbejder", "Dyrlæge", 35);
            Medarbejder medarbejder4 = new Medarbejder(4, "Lise", "lise@hund.dk", "+45 44 55 66 77", "Bakkevej 10", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder5 = new Medarbejder(5, "Mads", "mads@kat.dk", "+45 88 77 66 55", "Søndergade 5", "Medarbejder", "Dyrpasser", 40);
            Medarbejder medarbejder6 = new Medarbejder(6, "Sara", "sash@dyrlæge.dk", "+45 22 33 44 55", "Nordvej 3", "Medarbejder", "Dyrlæge", 38);
            Medarbejder medarbejder7 = new Medarbejder(7, "Peter", "peter@hund.dk", "+45 99 88 77 66", "Østergade 12", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder8 = new Medarbejder(8, "Anna", "anna@kat.dk", "+45 55 66 77 88", "Vejgade 9", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder9 = new Medarbejder(9, "Martin", "martin@hund.dk", "+45 66 77 88 99", "Lundvej 4", "Medarbejder", "Dyrpasser", 42);
            Medarbejder medarbejder10 = new Medarbejder(10, "Camilla", "camilla@dyrlæge.dk", "+45 77 88 99 00", "Skovvej 11", "Medarbejder", "Dyrlæge", 39);
            Medarbejder medarbejder11 = new Medarbejder(11, "Jesper", "jesper@hund.dk", "+45 88 99 00 11", "Havevej 6", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder12 = new Medarbejder(12, "Sofie", "sofie@kat.dk", "+45 99 00 11 22", "Parkvej 7", "Medarbejder", "Dyrpasser", 35);
            Medarbejder medarbejder13 = new Medarbejder(13, "Jacob", "jacob@dyrlæge.dk", "+45 00 11 22 33", "Skovvej 12", "Medarbejder", "Dyrlæge", 41);
            Medarbejder medarbejder14 = new Medarbejder(14, "Ida", "ida@hund.dk", "+45 11 22 33 44", "Markvej 14", "Frivillig", "Frivillig", 0);
            Medarbejder medarbejder15 = new Medarbejder(15, "Thomas", "thomas@kat.dk", "+45 22 33 44 55", "Lindevej 15", "Medarbejder", "Dyrpasser", 38);


            brugerRepo.OpretMedarbejder(medarbejder1);
            brugerRepo.OpretMedarbejder(medarbejder2);
            brugerRepo.OpretMedarbejder(medarbejder3);
            brugerRepo.OpretMedarbejder(medarbejder4);
            brugerRepo.OpretMedarbejder(medarbejder5);
            brugerRepo.OpretMedarbejder(medarbejder6);
            brugerRepo.OpretMedarbejder(medarbejder7);
            brugerRepo.OpretMedarbejder(medarbejder8);
            brugerRepo.OpretMedarbejder(medarbejder9);
            brugerRepo.OpretMedarbejder(medarbejder10);
            brugerRepo.OpretMedarbejder(medarbejder11);
            brugerRepo.OpretMedarbejder(medarbejder12);
            brugerRepo.OpretMedarbejder(medarbejder13);
            brugerRepo.OpretMedarbejder(medarbejder14);
            brugerRepo.OpretMedarbejder(medarbejder15);

            //Hunde
            List<Hund> hunde = new List<Hund>
            {
            new Hund("Stella", "Bulldog", 3, "hun", 1, "sund", true, true, false),
            new Hund("Bob", "Ukendt", 2, "han", 2, "sund", true, false, false),
            new Hund("Dennis", "Ukendt", 6, "han", 3, "mangler et ben", false, false, false),
            new Hund("Bella", "Bulldog", 4, "hun", 4, "sund", true, true, false),
            new Hund("Max", "Puddel", 1, "han", 5, "let syg", false, true, false),
            new Hund("Charlie", "Bulldog", 5, "han", 6, "sund", true, false, false),
            new Hund("Molly", "Puddel", 7, "hun", 7, "sund", false, true, false),
            new Hund("Rocky", "Bulldog", 4, "han", 8, "sund", true, true, false),
            new Hund("Daisy", "Puddel", 3, "hun", 9, "sund", false, false, false),
            new Hund("Toby", "Bulldog", 2, "han", 10, "sund", true, false, false)
            };

            foreach (var hund in hunde)
            {
                dyrRepo.AddHund(hund, 6); 
            }

            //Katte
            List<Kat> katte = new List<Kat>
            {
            new Kat("Denas", "Siamese", 5, "han", 101, "Sund", true, true, false),
            new Kat("Hansen", "Norsk Skovkat", 2, "hun", 102, "Mangler et øje", true, false, false),
            new Kat("Emil", "Maine Coon", 12, "han", 103, "Har sukkersyge", false, true, false),
            new Kat("Luna", "Bengal", 3, "hun", 104, "Sund", true, true, false),
            new Kat("Oscar", "Ragdoll", 7, "han", 105, "Let forkølet", false, false, false),
            new Kat("Mia", "Persian", 4, "hun", 106, "Sund", true, false, false),
            new Kat("Simba", "Savannah", 6, "han", 107, "Sund", false, true, false),
            new Kat("Nala", "Burmese", 5, "hun", 108, "Sund", true, true, false),
            new Kat("Felix", "British Shorthair", 8, "han", 109, "Sund", false, false, false),
            new Kat("Zoe", "Ragdoll", 3, "hun", 110, "Sund", true, false, false)

            };

            foreach (var kat in katte)
            {
                dyrRepo.AddKat(kat, medarbejder6);
            }

            foreach (var dyr in dyrRepo.dyrDictionary.Values)
            {
                Console.WriteLine($"{dyr.Navn} siger: {dyr.LavLyd()}");
            }

          
            Console.WriteLine("=== TEST AF DYRMETODER ===");

            // Test af TælAktiveHunde
            Console.WriteLine($"Antal aktive hunde: {dyrRepo.TælAktiveHunde()}");

            // Test af SkrivAlleHundNavne
            Console.WriteLine("Navne på alle hunde:");
            dyrRepo.SkrivAlleHundNavne();

            // Test af SkrivSundeKatte
            Console.WriteLine("Sunde katte:");
            dyrRepo.SkrivSundeKatte();

            // Test af FindFørsteKatMedAlder
            int søgtAlder = 3;
            Kat fundetKat = dyrRepo.FindFørsteKatMedAlder(søgtAlder);
            if (fundetKat != null)
                Console.WriteLine($"Første kat med alder {søgtAlder} er: {fundetKat.Navn}");
            else
                Console.WriteLine($"Ingen kat med alder {søgtAlder} blev fundet.");

            // Test af VisDyrEfterType
            Console.WriteLine("Vis alle katte:");
            dyrRepo.VisDyrEfterType("kat");

            

            // Eksempel på at booke besøg for kunde1 med forskellige dyr på forskellige datoer:
            besøgRepo.BookBesøg(DateTime.Now.AddDays(1).Date.AddHours(9), kunde1, dyrRepo.dyrDictionary[1]);  // Hund Stella
            try
            {
                // Hent dyret sikkert fra dyrDictionary
                if (dyrRepo.dyrDictionary.TryGetValue(1, out Dyr dyr))
                {
                    // Forsøg at booke besøget
                    bool success = besøgRepo.BookBesøg(DateTime.Now.AddDays(1).Date.AddHours(9), kunde1, dyr);

                    if (!success)
                    {
                        Console.WriteLine("Booking mislykkedes.");
                    }
                }
                else
                {
                    Console.WriteLine("Fejl: Dyr med chipnummer 1 blev ikke fundet.");
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Fejl: {ex.ParamName} er null. Besked: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Uventet fejl: {ex.Message}");
            }

            besøgRepo.BookBesøg(DateTime.Now.AddDays(2).Date.AddHours(10), kunde1, dyrRepo.dyrDictionary[101]); // Kat Denas
            besøgRepo.BookBesøg(DateTime.Now.AddDays(3).Date.AddHours(11), kunde1, dyrRepo.dyrDictionary[2]);  // Hund Bob

            // Kunde2 har kun 1 besøg:
            besøgRepo.BookBesøg(DateTime.Now.AddDays(1).Date.AddHours(14), kunde2, dyrRepo.dyrDictionary[3]);

            // Kunde3 har ingen besøg (ingen kald til BookBesøg)

            // Udskriv alle bookede besøg:
            Console.WriteLine(besøgRepo.ToString());


;
            

            //// Kør UI
            //UiHovedMenu hovedMenu = new UiHovedMenu(dyrRepo, brugerRepo, besøgRepo);
            //hovedMenu.Start();
        }
    }
}
