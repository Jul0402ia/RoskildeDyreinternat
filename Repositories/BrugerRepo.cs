using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            //Values betyder alle medarbejderne (uden ID’er)
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == medarbejderId)
                {
                    if (medarbejder.Antalarbejdstimer > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            // Hvis vi ikke fandt medarbejderen
            return false;
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
                //throw new ArgumentException($"Bruger med ID {kunde.Id} eksisterer allerede.");
                Console.WriteLine($"Bruger med ID {kunde.Id} eksisterer allerede.");
                return;
                }
            }

        // Opretter en medarbejder 
        public void OpretMedarbejder(Medarbejder nyMedarbejder)
        {
            // Tjek manuelt om ID allerede findes
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == nyMedarbejder.Id)
                {
                    Console.WriteLine("Der findes allerede en medarbejder med det ID.");
                    return;
                }
            }

            // Tilføj ny medarbejder
            medarbejderListe.Add(nyMedarbejder.Id, nyMedarbejder);
            Console.WriteLine($"Medarbejder '{nyMedarbejder.Navn}' er oprettet.");
        }

        // Opdaterer en medarbejders info
        public void OpdaterMedarbejderInfo(int id, string navn, string email, string telefon, string adresse)
        {
            bool fundet = false;

            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == id)
                {
                    medarbejder.Navn = navn;
                    medarbejder.Email = email;
                    medarbejder.Telefon = telefon;
                    medarbejder.Adresse = adresse;

                    Console.WriteLine($"Medarbejder med ID {id} er opdateret.");
                    fundet = true;
                    // Stop loopet når vi har fundet og opdateret
                    break; 
                }
                else if (medarbejder.Id != id)
                {
                    // Her gør vi ikke noget – vi leder bare videre
                }
            }

            if (!fundet)
            {
                Console.WriteLine($"Medarbejder med ID {id} blev ikke fundet.");
            }
        }

        // Opdaterer en kundes info
        public void OpdaterKundeInfo(int id, string navn, string email, string telefon, string adresse)
        {
            foreach (Kunde kundeliste in kundeListe.Values)
            {
                if (kundeliste.Id == id)
                {
                    // Opdater kundens info
                    kundeliste.Navn = navn;
                    kundeliste.Email = email;
                    kundeliste.Telefon = telefon;
                    kundeliste.Adresse = adresse;
                    return; // Afslut metoden når kunden er fundet og opdateret
                }
                else
                {
                    //throw new KeyNotFoundException($"Kunde med ID {id} blev ikke fundet.");
                    Console.WriteLine(($"Kunde med ID {id} blev ikke fundet."));
                    return;
                }
            }
        }
                
            

        // Returnerer medarbejder-info som tekst (bruges fx i GUI)
        public string HentMedarbejderInfoSomTekst(int id)
        {
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == id)
                {
                    // kan også bruge medarbejder.PrintAltInfo()
                    return medarbejder.ToString();  
                }
            }

            //throw new KeyNotFoundException($"Medarbejder med ID {id} blev ikke fundet.");
            Console.WriteLine($"Medarbejder med ID {id} blev ikke fundet.");
            return null; // Returner null hvis medarbejderen ikke findes
        }

        // Sletter en kunde (valgfrit inkl. besøg), kun medarbejder med adgang
        public void SletKunde(int kundeId, int medarbejderId, bool sletBesøg)
        {
            if (!HarAdgang(medarbejderId))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette kunder.");
                return;
            }

            // Find kunden manuelt
            Kunde fundetKunde = null;

            foreach (Kunde kunde in kundeListe.Values)
            {
                if (kunde.Id == kundeId)
                {
                    fundetKunde = kunde;
                    break;
                }
            }

            if (fundetKunde == null)
            {
                Console.WriteLine($"Kunde med ID {kundeId} blev ikke fundet.");
                return;
            }

            // Håndter besøg
            if (sletBesøg)
            {
                besøgRepo.SletBesøg(fundetKunde.Id);
                Console.WriteLine($"Alle besøg for kunden {fundetKunde.Navn} er slettet.");
            }
            else
            {
                var kundeBesøg = besøgRepo.HentBesøgForKunde(fundetKunde);
                if (kundeBesøg.Count > 0)
                {
                    Console.WriteLine("Kunden har stadig besøg registreret. Slet besøg først eller vælg at slette dem samtidig.");
                    return;
                }
            }

            // Fjern kunden
            bool blevSlettet = kundeListe.Remove(kundeId);
            if (blevSlettet)
            {
                Console.WriteLine($"Kunden {fundetKunde.Navn} (ID: {fundetKunde.Id}) er slettet.");
            }
            else
            {
                Console.WriteLine("Kunne ikke slette kunden.");
            }
        }

        // Finder en kunde baseret på ID
        public Kunde FindKundeById(int kundeId)
        {
                foreach (Kunde kunde in kundeListe.Values)
                {
                    if (kunde.Id == kundeId)
                    {
                        return kunde;
                    }
                }
                Console.WriteLine($"Kunde med ID {kundeId} blev ikke fundet.");
                return null;
            
        }

        #region Søgning: Vis brugerens rolle
        public void SøgningVisBrugerRolle(int id)
        {
            foreach
              (Kunde kunde in kundeListe.Values)
            {
                if (kunde.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {kunde.Id}, Rolle: {kunde.Rolle}");
                    return;
                }
            }
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {medarbejder.Id}, Rolle: {medarbejder.Rolle}");
                    return;
                }
            }
            Console.WriteLine($"Bruger med ID {id} blev ikke fundet.");
            return;
        }
        #endregion

        #region Søgning: Vis info om bruger (kun for medarbejder med adgang)
        public void SøgningVisBrugerInfo(int id)
        {
            if (!HarAdgang(id))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 timer må se brugerinfo.");
                return;
            }

            foreach (Kunde kunde in kundeListe.Values)
            {
                if (kunde.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {kunde.Id}\nNavn: {kunde.Navn}\nEmail: {kunde.Email}\nTlf: {kunde.Telefon}\nAdresse: {kunde.Adresse}\nAlder: {kunde.Alder}\nKøn: {kunde.Køn}");

                    // Vis bookinger
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

                    return;
                }
            }

            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {medarbejder.Id}\nNavn: {medarbejder.Navn}\nEmail: {medarbejder.Email}\nTlf: {medarbejder.Telefon}\nAdresse: {medarbejder.Adresse}\nRolle: {medarbejder.Rolle}\nStilling: {medarbejder.Stilling}\nArbejdstimer: {medarbejder.Antalarbejdstimer}");
                    return;
                }
            }

            Console.WriteLine($"Bruger med ID {id} blev ikke fundet.");
            return;
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