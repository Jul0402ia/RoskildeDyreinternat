using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat
{

    class Program
    {
        static void Main(string[] args)
        {
            #region Alle Objekter
            // Opretter en ny instans af KundeRepo
            BesøgRepo besøgRepo = new BesøgRepo();
            var brugerRepo = new BrugerRepo(besøgRepo);

            DyrRepo repo = new DyrRepo();

            //Hunde
            repo.dyrDictionary.Add(1, new Hund("Stella", "Race1", 3, "hun", 1, "sund", true, true, false));
            repo.dyrDictionary.Add(2, new Hund("Bob", "Race2", 2, "han", 2, "sund", true, false, false));
            repo.dyrDictionary.Add(3, new Hund("Dennis", "Race3", 6, "han", 3, "mangler et ben", false, false, false));
            repo.dyrDictionary.Add(4, new Hund("Bella", "Race4", 4, "hun", 4, "sund", true, true, false));
            repo.dyrDictionary.Add(5, new Hund("Max", "Race5", 1, "han", 5, "let syg", false, true, false));
            repo.dyrDictionary.Add(6, new Hund("Charlie", "Race6", 5, "han", 6, "sund", true, false, false));
            repo.dyrDictionary.Add(7, new Hund("Molly", "Race7", 7, "hun", 7, "sund", false, true, false));
            repo.dyrDictionary.Add(8, new Hund("Rocky", "Race8", 4, "han", 8, "sund", true, true, false));
            repo.dyrDictionary.Add(9, new Hund("Daisy", "Race9", 3, "hun", 9, "sund", false, false, false));
            repo.dyrDictionary.Add(10, new Hund("Toby", "Race10", 2, "han", 10, "sund", true, false, false));


            //Katte
            repo.dyrDictionary.Add(101, new Kat("Denas", "Siamese", 5, "han", 101, "Sund", true, true, false));
            repo.dyrDictionary.Add(102, new Kat("Hansen", "Norsk Skovkat", 2, "hun", 102, "Mangler et øje", true, false, false));
            repo.dyrDictionary.Add(103, new Kat("Emil", "Maine Coon", 12, "han", 103, "Har sukkersyge", false, true, false));
            repo.dyrDictionary.Add(104, new Kat("Luna", "Bengal", 3, "hun", 104, "Sund", true, true, false));
            repo.dyrDictionary.Add(105, new Kat("Oscar", "Ragdoll", 7, "han", 105, "Let forkølet", false, false, false));
            repo.dyrDictionary.Add(106, new Kat("Mia", "Persian", 4, "hun", 106, "Sund", true, false, false));
            repo.dyrDictionary.Add(107, new Kat("Simba", "Savannah", 6, "han", 107, "Sund", false, true, false));
            repo.dyrDictionary.Add(108, new Kat("Nala", "Burmese", 5, "hun", 108, "Sund", true, true, false));
            repo.dyrDictionary.Add(109, new Kat("Felix", "British Shorthair", 8, "han", 109, "Sund", false, false, false));
            repo.dyrDictionary.Add(110, new Kat("Zoe", "Ragdoll", 3, "hun", 110, "Sund", true, false, false));



            foreach (var entry in repo.dyrDictionary)
            {
                Console.WriteLine($"ID: {entry.Key}, Navn: {entry.Value.Navn}");
                Console.WriteLine(entry.Value.LavLyd());
            }


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




            // Eksempel på at booke besøg for kunde1 med forskellige dyr på forskellige datoer:
            besøgRepo.BookBesøg(DateTime.Now.AddDays(1).Date.AddHours(9), kunde1, repo.dyrDictionary[1]);  // Hund Stella
            besøgRepo.BookBesøg(DateTime.Now.AddDays(2).Date.AddHours(10), kunde1, repo.dyrDictionary[101]); // Kat Denas
            besøgRepo.BookBesøg(DateTime.Now.AddDays(3).Date.AddHours(11), kunde1, repo.dyrDictionary[2]);  // Hund Bob

            // Kunde2 har kun 1 besøg:
            besøgRepo.BookBesøg(DateTime.Now.AddDays(1).Date.AddHours(14), kunde2, repo.dyrDictionary[3]);

            // Kunde3 har ingen besøg (ingen kald til BookBesøg)

            // Udskriv alle bookede besøg:
            Console.WriteLine(besøgRepo.ToString());
            #endregion
        }
    }
}
