using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat.Repositories
{
    public class BrugerRepo : IBrugerRepo
    {
        // Dictionary gør det hurtigt at slå brugere op via deres ID
        public Dictionary<int, Kunde> kundeListe = new Dictionary<int, Kunde>();
        public Dictionary<int, Medarbejder> medarbejderListe = new Dictionary<int, Medarbejder>();

        // Felt til at holde referencen til BesøgRepo (for at kunne hente bookinger til kunder)
        private BesøgRepo besøgRepo;

        // Konstruktør: (modtagelse af et BesøgRepo-objekt og gemmer det i feltet ovenfor)
        public BrugerRepo(BesøgRepo besøgRepo)
        {
            this.besøgRepo = besøgRepo; // uden "this." gemmes det ikke korrekt
        }

        // Oprettelse af en ny kunde + tilføjelse til kunde-listen, hvis ID'et ikke allerede findes
        public void OpretKunde(Kunde kunde)
        {
            if (!kundeListe.ContainsKey(kunde.Id))
            {
                kundeListe.Add(kunde.Id, kunde);
            }
            else
            {
                throw new ArgumentException($"Bruger med ID {kunde.Id} eksisterer allerede.");
            }
        }

        // Opretteelse af en ny medarbejder + tilføjelse af den til medarbejder-listen, hvis ID'et ikke allerede findes
        public void OpretMedarbejder(Medarbejder medarbejder)
        {
            if (!medarbejderListe.ContainsKey(medarbejder.Id))
            {
                medarbejderListe.Add(medarbejder.Id, medarbejder);
            }
            else
            {
                throw new ArgumentException($"Medarbejder med ID {medarbejder.Id} eksisterer allerede.");
            }
        }

        // Opdaterer info om en kunde hvis ID'et findes
        public void OpdaterKundeInfo(int id, string navn, string email, string telefon, string adresse)
        {
            if (kundeListe.TryGetValue(id, out Kunde kunde))
            {
                kunde.Navn = navn;
                kunde.Email = email;
                kunde.Telefon = telefon;
                kunde.Adresse = adresse;
            }
            else
            {
                throw new KeyNotFoundException($"Kunde med ID {id} blev ikke fundet.");
            }
        }
        // Slet en kunde hvis ID'et findes (kun medarbejder (frivillige passer til medarbejder klassen, dog er de sat til at arbejde 0 timer))
        public void SletKunde(int kundeId, int medarbejderId, bool sletBesøg)
        {
            if (medarbejderListe.TryGetValue(medarbejderId, out Medarbejder medarbejder) && medarbejder.Antalarbejdstimer > 0)
            {
                if (kundeListe.TryGetValue(kundeId, out Kunde kunde))
                {
                    if (sletBesøg)
                    {
                        besøgRepo.SletBesøg(kunde.Id);
                        Console.WriteLine($"Alle besøg for kunden {kunde.Navn} er slettet.");
                    }
                    else
                    {
                        var kundeBesøg = besøgRepo.HentBesøgForKunde(kunde);
                        if (kundeBesøg.Count > 0)
                        {
                            Console.WriteLine("Kunden har stadig besøg registreret. Slet besøg først eller vælg at slette dem samtidig.");
                            return;
                        }
                    }

                    bool succes = kundeListe.Remove(kundeId);
                    if (succes)
                    {
                        Console.WriteLine($"Kunden {kunde.Navn} (ID: {kunde.Id}) er slettet.");
                    }
                    else
                    {
                        Console.WriteLine("Kunne ikke slette kunden.");
                    }
                }
                else
                {
                    Console.WriteLine($"Kunde med ID {kundeId} blev ikke fundet.");
                }
            }
            else
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette kunder.");
            }
        }

        // Viser hvilken rolle en bruger har ud fra deres ID (godt hvis man skal tjekke om personen er ansat eller frivillig)
        public void VisBrugerRolle(int id)
        {
            if (kundeListe.TryGetValue(id, out Kunde kunde))
            {
                Console.WriteLine($"Bruger ID: {kunde.Id}, Rolle: {kunde.Rolle}");
            }
            else if (medarbejderListe.TryGetValue(id, out Medarbejder medarbejder))
            {
                Console.WriteLine($"Bruger ID: {medarbejder.Id}, Rolle: {medarbejder.Rolle}");
            }
            else
            {
                throw new KeyNotFoundException($"Bruger med ID {id} blev ikke fundet.");
            }
        }

        // Viser detaljer om både kunder og medarbejdere ud fra ID (funktionen kun tilgængelig for de ansætte på over 0 timer (ikke frivillige))
        public void VisBrugerInfo(int brugerId, int medarbejderId)
        {
            if (medarbejderListe.TryGetValue(medarbejderId, out Medarbejder medarbejder) && medarbejder.Antalarbejdstimer > 0)
            {
                if (kundeListe.TryGetValue(brugerId, out Kunde kunde))
                {
                    Console.WriteLine($"Bruger ID: {kunde.Id}\nNavn: {kunde.Navn}\nEmail: {kunde.Email}\nTlf: {kunde.Telefon}\nAdresse: {kunde.Adresse}\nAlder: {kunde.Alder}\nKøn: {kunde.Køn}");
                    Console.WriteLine();

                    var kundeBesøg = besøgRepo.HentBesøgForKunde(kunde);
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
                else if (medarbejderListe.TryGetValue(brugerId, out Medarbejder med))
                {
                    Console.WriteLine($"Bruger ID: {med.Id}\nNavn: {med.Navn}\nEmail: {med.Email}\nTlf: {med.Telefon}\nAdresse: {med.Adresse}\nRolle: {med.Rolle}\nStilling: {med.Stilling}\nArbejdstimer: {med.Antalarbejdstimer}");
                }
                else
                {
                    throw new KeyNotFoundException($"Bruger med ID {brugerId} blev ikke fundet.");
                }
            }
            else
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 timer må se brugerinfo.");
            }
        }
    }
}

