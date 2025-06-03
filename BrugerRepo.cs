using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{

    public class BrugerRepo : IBrugerRepo
    {
        //Dictionary bruges til at slå noget op hurtigt ved hjælp af en key (Kunde ID) (Medarbejder ID)
        Dictionary<int, Kunde> _kundeListe = new Dictionary<int, Kunde>();
        Dictionary<int, Medarbejder> _medarbejderListe = new Dictionary<int, Medarbejder>();

        // Private fields med lille begyndelsesbogstav
        private BesøgRepo _besøgRepo;

        // Konstruktør med parametre 
        public BrugerRepo(BesøgRepo besøgRepo)
        {
            _besøgRepo = besøgRepo;
        }

        // Opretter en ny kunde og tilføjer den til listen, hvis ID'et ikke allerede findes
        public void OpretKunde(Kunde kunde)
        {
            if (!_kundeListe.ContainsKey(kunde.Id))
            {
                _kundeListe.Add(kunde.Id, kunde);
            }
            else
            {
                throw new ArgumentException($"Bruger med ID {kunde.Id} eksisterer allerede.");
            }
        }
        // Opretter en ny medarbejder og tilføjer den til listen, hvis ID'et ikke allerede findes
        public void OpretMedarbejder(Medarbejder medarbejder)
        {
            if (!_medarbejderListe.ContainsKey(medarbejder.Id))
            {
                _medarbejderListe.Add(medarbejder.Id, medarbejder);
            }
            else
            {
                throw new ArgumentException($"Medarbejder med ID {medarbejder.Id} eksisterer allerede.");
            }
        }
        // Opdaterer info om kunden i en kundeliste (dictonary) baseret på kundens ID 
        public void OpdaterKundeInfo(int id, string navn, string email, string telefon, string adresse)
        {
            if (_kundeListe.TryGetValue(id, out Kunde kunde))
            {
                kunde.Navn = navn;
                kunde.Email = email;
                kunde.Telefon = telefon;
                kunde.Adresse = adresse;
            }
            else
            {
                throw new KeyNotFoundException($"Bruger med ID {id} blev ikke fundet.");
            }
        }
        //Sletter en kunde fra en kundeliste (diconary) baseret på kundens ID
        public void SletKunde(int id)
        {
            if (_kundeListe.TryGetValue(id, out Kunde kunde))
            {
                _kundeListe.Remove(id);
            }
            else
            {
                throw new KeyNotFoundException($"Bruger med ID {id} blev ikke fundet.");
            }
        }
        //den viser hvilken rolle en bruger har (om det er en kunde eller en medarbejder) ud fra brugerens ID
        public void VisBrugerRolle(int id)
        {
            if (_kundeListe.TryGetValue(id, out Kunde kunde))
            {
                Console.WriteLine($"Du har søgt efter ID: {id}:");
                Console.WriteLine($"Bruger ID: {kunde.Id}, Rolle: {kunde.Rolle}");
            }
            else
            {
                if (_medarbejderListe.TryGetValue(id, out Medarbejder medarbejder))
                {
                    Console.WriteLine($"Du har søgt efter ID: {id}:");
                    Console.WriteLine($"Bruger ID: {medarbejder.Id}, Rolle: {medarbejder.Rolle}");
                }
                else
                {
                    throw new KeyNotFoundException($"Bruger med ID {id} blev ikke fundet.");
                }
            }
        }
        //den tjekker om ID findes i kundeliste (dictonary), hvis den ikke findes der tjekker den i medarbejderlisten (dictonary), hvis ingen passer til ID'et informerer den om ingen bruger er blevet fundet 
        public void VisBrugerInfo(int id)
        {
            if (_kundeListe.TryGetValue(id, out Kunde kunde))
            {
                Console.WriteLine($"Du har søgt efter ID: {id}:");
                Console.WriteLine();
                Console.WriteLine($"Bruger ID: {kunde.Id}\n Navn: {kunde.Navn}\n Email: {kunde.Email}\n Tlf: {kunde.Telefon}\n Adresse: {kunde.Adresse}\n Alder: {kunde.Alder}\n Køn: {kunde.Køn}");
                Console.WriteLine();
                var kundeBesøg = _besøgRepo.HentBesøgForKunde(kunde);
                Console.WriteLine("Bookinger:");
                if (kundeBesøg.Count == 0)
                {
                    Console.WriteLine("Ingen bookinger fundet.");
                }
                else
                {
                    foreach (var besog in kundeBesøg)
                    {
                        Console.WriteLine($"- Dato: {besog.Dato}, Dyr: {besog.PrintBesogsInfo()}");
                    }
                }

            }
            else if (_medarbejderListe.TryGetValue(id, out Medarbejder medarbejder))
            {
                Console.WriteLine($"Du har søgt efter ID: {id}:");
                Console.WriteLine();
                Console.WriteLine($"Bruger ID: {medarbejder.Id}\n Navn: {medarbejder.Navn}\n Email: {medarbejder.Email}\n Tlf: {medarbejder.Telefon}\n"
                    + $" Adresse: {medarbejder.Adresse}\n Rolle: {medarbejder.Rolle}\n Stilling: {medarbejder.Stilling}\n Arbejdstimer: {medarbejder.Antalarbejdstimer}");
                Console.WriteLine();
            }
            else
            {
                throw new KeyNotFoundException($"Bruger med ID {id} blev ikke fundet.");
            }
        }
    }
}


