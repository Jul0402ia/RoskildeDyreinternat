using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace RoskildeDyreinternat.Repositories
    {
        public class BrugerRepo : IAdgangsKontrol
        {
            // Opslagstabeller for hurtig adgang via ID
            public Dictionary<int, Kunde> kundeListe = new Dictionary<int, Kunde>();
            public Dictionary<int, Medarbejder> medarbejderListe = new Dictionary<int, Medarbejder>();

            // Reference til BesøgRepo
            public BesøgRepo besøgRepo;

            // Konstruktør
            public BrugerRepo(BesøgRepo besøgRepo)
            {
                this.besøgRepo = besøgRepo;
            }

            // Tjekker om en medarbejder har adgang (arbejdstimer > 0)
            public bool HarAdgang(int medarbejderId)
            {
                return medarbejderListe.TryGetValue(medarbejderId, out Medarbejder medarbejder)
                       && medarbejder.Antalarbejdstimer > 0;
            }

            // Opretter en kunde
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

        // Opretter en medarbejder 
        public void OpretMedarbejder(Medarbejder medarbejder)
        {
            if (medarbejderListe.ContainsKey(medarbejder.Id))
            {
                Console.WriteLine("Der findes allerede en medarbejder med det ID.");
                return;
            }

            medarbejderListe.Add(medarbejder.Id, medarbejder);
            Console.WriteLine($"Medarbejder '{medarbejder.Navn}' er oprettet.");
        }

        // Opdaterer en medarbejders info
        public void OpdaterMedarbejderInfo(int id, string navn, string email, string telefon, string adresse)
            {
                if (medarbejderListe.TryGetValue(id, out Medarbejder medarbejder))
                {
                    medarbejder.Navn = navn;
                    medarbejder.Email = email;
                    medarbejder.Telefon = telefon;
                    medarbejder.Adresse = adresse;
                }
                else
                {
                    throw new KeyNotFoundException($"Medarbejder med ID {id} blev ikke fundet.");
                }
            }

            // Opdaterer en kundes info
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

            // Returnerer medarbejder-info som tekst (bruges fx i GUI)
            public string HentMedarbejderInfoSomTekst(int id)
            {
                if (medarbejderListe.TryGetValue(id, out Medarbejder medarbejder))
                {
                    return medarbejder.ToString();  // PrintAltInfo er ofte implementeret som ToString()
                }
                else
                {
                    throw new KeyNotFoundException($"Medarbejder med ID {id} blev ikke fundet.");
                }
            }

            // Sletter en kunde (valgfrit inkl. besøg), kun medarbejder med adgang
            public void SletKunde(int kundeId, int medarbejderId, bool sletBesøg)
            {
                if (!HarAdgang(medarbejderId))
                {
                    Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette kunder.");
                    return;
                }

                if (!kundeListe.TryGetValue(kundeId, out Kunde kunde))
                {
                    Console.WriteLine($"Kunde med ID {kundeId} blev ikke fundet.");
                    return;
                }

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

                if (kundeListe.Remove(kundeId))
                {
                    Console.WriteLine($"Kunden {kunde.Navn} (ID: {kunde.Id}) er slettet.");
                }
                else
                {
                    Console.WriteLine("Kunne ikke slette kunden.");
                }
            }

        // Finder en kunde baseret på ID
        public Kunde FindKundeById(int id)
        {
            if (kundeListe.TryGetValue(id, out Kunde kunde))
            {
                return kunde;
            }
            else
            {
                return null;
            }
        }

        #region Søgning: Vis brugerens rolle
        public void SøgningVisBrugerRolle(int id)
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
            #endregion

            #region Søgning: Vis info om bruger (kun for medarbejder med adgang)
            public void SøgningVisBrugerInfo(int brugerId, int medarbejderId)
            {
                if (!HarAdgang(medarbejderId))
                {
                    Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 timer må se brugerinfo.");
                    return;
                }

                if (kundeListe.TryGetValue(brugerId, out Kunde kunde))
                {
                    Console.WriteLine($"Bruger ID: {kunde.Id}\nNavn: {kunde.Navn}\nEmail: {kunde.Email}\nTlf: {kunde.Telefon}\nAdresse: {kunde.Adresse}\nAlder: {kunde.Alder}\nKøn: {kunde.Køn}");

                    var kundeBesøg = besøgRepo.HentBesøgForKunde(kunde);
                    Console.WriteLine("\nBookinger:");
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
            #endregion

            #region Filtrering: Medarbejdere & Frivillige
            public List<Medarbejder> FiltreringMedarbejdereMedAdgang()
            {
                return medarbejderListe.Values.Where(m => HarAdgang(m.Id)).ToList();
            }

            public List<Medarbejder> FiltreringFrivillige()
            {
                return medarbejderListe.Values.Where(m => !HarAdgang(m.Id)).ToList();
            }
            #endregion
        }
    }