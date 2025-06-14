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
            // Gennemgå alle eksisterende kunder for at tjekke, om ID allerede findes
            foreach (Kunde eksisterendeKunde in kundeListe.Values)
            {
                if (eksisterendeKunde != null && eksisterendeKunde.Id == kunde.Id)
                {
                    Console.WriteLine($"Bruger med ID {kunde.Id} eksisterer allerede.");
                    return;
                }
            }
            // Hvis ID'et ikke er fundet, tilføj kunden
            kundeListe[kunde.Id] = kunde;
            Console.WriteLine($"Kunde {kunde.Navn} med ID {kunde.Id} er oprettet.");
        }

        // Opretter en medarbejder 
        public void OpretMedarbejder(Medarbejder medarbejder)
        {
            // Tjek manuelt om ID allerede findes
            foreach (Medarbejder eksisterendeMedarbejder in medarbejderListe.Values)
            {
                if (eksisterendeMedarbejder != null && eksisterendeMedarbejder.Id == medarbejder.Id)
                {
                    Console.WriteLine("Der findes allerede en medarbejder med det ID.");
                    return;
                }
            }

            // Tilføj ny medarbejder
            medarbejderListe[medarbejder.Id] = medarbejder;
            Console.WriteLine($"Medarbejder {medarbejder.Navn} med ID {medarbejder.Id} er oprettet.");
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
            // for avanceret at bruge throw new KeyNotFoundException - kender ikke til dette
            //throw new KeyNotFoundException($"Medarbejder med ID {id} blev ikke fundet.");
            Console.WriteLine($"Medarbejder med ID {id} blev ikke fundet.");
            return null;
        }

        // Sletter en kunde (valgfrit inkl. besøg), kun medarbejder med adgang
        public void SletKunde(int kundeId, int medarbejderId, bool sletBesøg)
        {
            // Tjek om medarbejderen har adgang
            if (!HarAdgang(medarbejderId))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette kunder.");
                return;
            }

            // Find kunden manuelt
            Kunde fundetKunde = null;
            foreach (Kunde kunde in kundeListe.Values)
            {
                if (kunde != null && kunde.Id == kundeId)
                {
                    fundetKunde = kunde;
                    break;
                }
            }

            // Hvis ikke fundet
            if (fundetKunde == null)
            {
                Console.WriteLine($"Kunde med ID {kundeId} blev ikke fundet.");
                return;
            }

            // Hvis besøg også skal slettes
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

            // "Slet" kunden ved at sætte værdien til null
            foreach (int id in kundeListe.Keys)
            {
                if (id == kundeId)
                {
                    kundeListe[id] = null;
                    Console.WriteLine($"Kunden {fundetKunde.Navn} (ID: {fundetKunde.Id}) er slettet (markeret som tom).");
                    return;
                }
            }
            Console.WriteLine("Noget gik galt – kunden kunne ikke slettes.");
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
            // Adgangskontrol: kun medarbejdere med arbejdstimer over 0 må søge
            if (!HarAdgang(id))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 timer må se brugerinfo.");
                return;
            }

            // Tjek om det er en kunde
            foreach (Kunde kunde in kundeListe.Values)
            {
                if (kunde != null && kunde.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {kunde.Id}");
                    Console.WriteLine($"Navn: {kunde.Navn}");
                    Console.WriteLine($"Email: {kunde.Email}");
                    Console.WriteLine($"Tlf: {kunde.Telefon}");
                    Console.WriteLine($"Adresse: {kunde.Adresse}");
                    Console.WriteLine($"Alder: {kunde.Alder}");
                    Console.WriteLine($"Køn: {kunde.Køn}");

                    // Vis kundens bookinger
                    Console.WriteLine("\nBookinger:");
                    var kundeBesøg = besøgRepo.HentBesøgForKunde(kunde);

                    if (kundeBesøg.Count == 0)
                    {
                        Console.WriteLine("Ingen bookinger fundet.");
                    }
                    else
                    {
                        foreach (var besøg in kundeBesøg)
                        {
                            Console.WriteLine($"- Dato: {besøg.Dato}, Dyr: {besøg.PrintBesogsInfo()}");
                        }
                    }

                    return;
                }
            }

            // Tjek om det er en medarbejder
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder != null && medarbejder.Id == id)
                {
                    Console.WriteLine($"Bruger ID: {medarbejder.Id}");
                    Console.WriteLine($"Navn: {medarbejder.Navn}");
                    Console.WriteLine($"Email: {medarbejder.Email}");
                    Console.WriteLine($"Tlf: {medarbejder.Telefon}");
                    Console.WriteLine($"Adresse: {medarbejder.Adresse}");
                    Console.WriteLine($"Rolle: {medarbejder.Rolle}");
                    Console.WriteLine($"Stilling: {medarbejder.Stilling}");
                    Console.WriteLine($"Arbejdstimer: {medarbejder.Antalarbejdstimer}");
                    return;
                }
            }

            // Ingen bruger fundet
            Console.WriteLine($"Bruger med ID {id} blev ikke fundet.");
        }


        #endregion

        #region Filtrering: Medarbejdere & Frivillige
        public List<Medarbejder> FiltreringMedarbejdereMedAdgang(int antalArbejdstimer)
        {
            List<Medarbejder> resulterer = new List<Medarbejder>();

            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder != null && medarbejder.Antalarbejdstimer > 0)
                {
                    resulterer.Add(medarbejder);
                }
            }

            return resulterer;
        }

        public List<Medarbejder> FiltreringFrivillige(int antalArbejdstimer)
            {
            List<Medarbejder> frivillige = new List<Medarbejder>();
            foreach (Medarbejder medarbejder in medarbejderListe.Values)
            {
                if (medarbejder != null && medarbejder.Antalarbejdstimer == 0)
                {
                    frivillige.Add(medarbejder);
                }
            }
            return frivillige;
        }
            #endregion
        }
    }