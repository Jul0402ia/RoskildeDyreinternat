using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiGaest
    {
        private DyrRepo dyrRepo;
        private BrugerRepo brugerRepo;

        public UiGaest(DyrRepo dyrrepo, BrugerRepo brugerrepo)
        {
            dyrRepo = dyrrepo;
            brugerRepo = brugerrepo;
        }
        public void Start()
        {
            Console.WriteLine("Velkommen til Dyreinternatet!");
            VisGæstMenu();
        }

        private void VisGæstMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== GÆST MENU ===");
                Console.WriteLine("1. Om om");
                Console.WriteLine("2. Kontaktinformation");
                Console.WriteLine("3. Åbningstider");
                Console.WriteLine("4. Opret konto");
                Console.WriteLine("5. Vis alle hunde");
                Console.WriteLine("6. Vis alle katte");
                Console.WriteLine("7. Vis filtrerings/søgningsmenu");
                Console.WriteLine("0. Afslut");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        VisOmOs();
                        break;
                    case "2":
                        VisKontaktInfo();
                        break;
                    case "3":
                        VisAabningstider();
                        break;
                    case "4":
                        OpretKundeKonto();
                        break;
                    case "5":
                        dyrRepo.VisHunde();
                        break;
                    case "6":
                        dyrRepo.VisKatte();
                        break;
                    case "7":
                        VisFiltreringsMenu();
                        break;
                    case "0":
                        Console.WriteLine("Tak for besøget. Farvel!");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        break;
                }
            } while (valg != "0");
        }
        private void VisFiltreringsMenu()
        {
            string valg;
            do
            {
                Console.Clear();
                Console.WriteLine("=== FILTRER DYRE MENU ===");
                Console.WriteLine("1. Filtrér på alder");
                Console.WriteLine("2. Søg katte på race");
                Console.WriteLine("3. Søg hunde på race");
                Console.WriteLine("4. Vis hunde efter træningstilstand");
                Console.WriteLine("5. Vis hunde der kan/ikke kan med andre hunde");
                Console.WriteLine("6. Vis katte der kan/ikke kan med andre katte");
                Console.WriteLine("7. Vis katte der er/ikke er indekatte");
                Console.WriteLine("8. Vis alle hunde");
                Console.WriteLine("9. Vis alle katte");
                Console.WriteLine("0. Tilbage til dyr menu");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        FiltrererIMellemDyretsAlder();
                        break;
                    case "2":
                        SøgKattePåRace();
                        break;
                    case "3":
                        SøgHundePåRace();
                        break;
                    case "4":
                        VisHundeEfterTræningstilstand();
                        break;
                    case "5":
                        VisHundeDerKanMedAndreHunde();
                        break;
                    case "6":
                        VisKatteDerKanMedAndreKatte();
                        break;
                    case "7":
                        VisKatteDerErIndekatte();
                        break;
                    case "8":
                        dyrRepo.VisHunde();
                        break;
                    case "9":
                        dyrRepo.VisKatte();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg");
                        Pause();
                        break;
                }

            } while (valg != "0");
        }

        #region Om Os #1
        private void VisOmOs()
        {
            Console.WriteLine("\nDyreinternatet er en organisation, der hjælper hjemløse dyr.");
            Console.WriteLine("Vi tilbyder pleje, omsorg og nye hjem til hunde og katte.");
            Console.WriteLine("Vi tager gerne imod hjælp fra de frivillige.");
        }
        #endregion

        #region Kontakt info #2

        private void VisKontaktInfo()
        {
            Console.WriteLine("\nKontakt os på:");
            Console.WriteLine("Telefon: 12 34 56 78");
            Console.WriteLine("Email: kontakt@dyreinternatet.dk");
            Console.WriteLine("Adresse: Dyrevej 1, 4000 Roskilde");
        }
        #endregion

        #region Åbningstider #3
        private void VisAabningstider()
        {
            Console.WriteLine("\nÅbningstider:");
            Console.WriteLine("Mandag - Fredag: 12:00 - 17:00");
            Console.WriteLine("Lørdag: 10:00 - 14:00");
            Console.WriteLine("Søndag: Lukket");
        }
        #endregion

        #region OpretKundeKonto #4 (try/catch)
        private void OpretKundeKonto()
        {
            try
            {
                Console.WriteLine("\n--- Opret konto ---");

                Console.Write("ID (nummer): ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Navn: ");
                string navn = Console.ReadLine();

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Telefon: ");
                string telefon = Console.ReadLine();

                Console.Write("Adresse: ");
                string adresse = Console.ReadLine();

                Console.Write("Alder: ");
                int alder = int.Parse(Console.ReadLine());

                Console.Write("Køn: ");
                string køn = Console.ReadLine();

                Console.Write("Vælg rolle (f.eks. Kunde): ");
                string rolle = Console.ReadLine();

                // Opret ny kunde
                Kunde nyKunde = new Kunde(id, navn, email, telefon, adresse, rolle, alder, køn);
                
              
                brugerRepo.OpretKunde(nyKunde);

                Console.WriteLine($"\nKonto for {navn} er oprettet!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ugyldigt input. Husk at ID og Alder skal være tal.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            }
        }
        #endregion

        #region Vis alle hunde #5 - tom hentes fra repo

        #endregion

        #region Vis alle katte #6 - tom hentes fra repo

        #endregion

        #region Filtrering og søgningsmenu #7 

        #endregion
        
        #region 1.Filtrerring på dyr (alder) exeption
        private void FiltrererIMellemDyretsAlder()
        {
            try
            {
                Console.Write("Min alder: ");
                int min = int.Parse(Console.ReadLine());

                Console.Write("Max alder: ");
                int max = int.Parse(Console.ReadLine());

                var dyrIFilter = dyrRepo.FiltrererIMellemDyretsAlder(min, max);
                foreach (var dyr in dyrIFilter)
                {
                    Console.WriteLine(dyr);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ugyldigt input! Indtast venligst et tal.");
            }

            Pause();
        }
        #endregion

        #region 2. & 3.Søg kat eller hund efter race
        private void SøgKattePåRace()
        {
            Console.Write("Indtast katterace du vil søge efter: ");
            string race = Console.ReadLine();

            List<Kat> fundneKatte = dyrRepo.SøgKattePåRace(race);

            if (fundneKatte.Count > 0)
            {
                Console.WriteLine("\nFundne katte:");
                foreach (var kat in fundneKatte)
                {
                    Console.WriteLine($"Navn: {kat.Navn}, Race: {kat.Race}, Alder: {kat.Alder}, Køn: {kat.Køn}, Chipnummer: {kat.Chipnummer}, Helbred: {kat.Helbredstilstand}, Indekat: {kat.SkalVæreIndekat}, Kan med andre katte: {kat.KanMedAndreKatte}, Adopteret: {kat.ErAdopteret}");
                }
            }
            else
            {
                Console.WriteLine("Ingen katte fundet med den angivne race.");
            }

            Pause();
        }
        private void SøgHundePåRace()
        {
            Console.Write("Indtast hunderace du vil søge efter: ");
            string race = Console.ReadLine();

            List<Hund> fundneHunde = dyrRepo.SøgHundePåRace(race);

            if (fundneHunde.Count > 0)
            {
                Console.WriteLine("\nFundne hunde:");
                foreach (var hund in fundneHunde)
                {
                    Console.WriteLine($"Navn: {hund.Navn}, Race: {hund.Race}, Alder: {hund.Alder}, Køn: {hund.Køn}, Chipnummer: {hund.Chipnummer}, Helbred: {hund.Helbredstilstand}, Trænet: {hund.ErTrænet}, Kan med andre hunde: {hund.KanMedAndreHunde}, Adopteret: {hund.ErAdopteret}");
                }
            }
            else
            {
                Console.WriteLine("Ingen hunde fundet med den angivne race.");
            }

            Pause();
        }
        #endregion

        #region 4. & 5.Filtrer på hunde (kan/ikke kan med andre hunde & om de er trænet eller ej)
        private void VisHundeDerKanMedAndreHunde()
        {
            Console.Write("Skal hundene kunne med andre hunde? (true/false): ");
            bool kanMedAndreHunde;
            while (!bool.TryParse(Console.ReadLine(), out kanMedAndreHunde))
            {
                Console.Write("Ugyldigt input. Skriv 'true' eller 'false': ");
            }

            List<Hund> hunde = dyrRepo.FiltreringPåHundeDerKanMedAndreHunde(kanMedAndreHunde);

            if (hunde.Count == 0)
            {
                Console.WriteLine("Ingen hunde fundet med det kriterie.");
            }
            else
            {
                Console.WriteLine($"\nHunde der {(kanMedAndreHunde ? "kan" : "ikke kan")} med andre hunde:");
                foreach (var hund in hunde)
                {
                    Console.WriteLine($"Navn: {hund.Navn}, Race: {hund.Race}, Alder: {hund.Alder}, Chipnummer: {hund.Chipnummer}, Er trænet: {hund.ErTrænet}");
                }
            }

            Pause();
        }

        private void VisHundeEfterTræningstilstand()
        {
            Console.Write("Skal hundene være trænet? (true/false): ");
            bool erTrænet;
            while (!bool.TryParse(Console.ReadLine(), out erTrænet))
            {
                Console.Write("Ugyldigt input. Skriv 'true' eller 'false': ");
            }

            List<Hund> hunde = dyrRepo.FiltreringPåHundeDerErTrænet(erTrænet);

            if (hunde.Count == 0)
            {
                Console.WriteLine("Ingen hunde fundet med det kriterie.");
            }
            else
            {
                Console.WriteLine($"\nHunde der {(erTrænet ? "er trænet" : "ikke er trænet")}:");
                foreach (var hund in hunde)
                {
                    Console.WriteLine($"Navn: {hund.Navn}, Race: {hund.Race}, Alder: {hund.Alder}, Chipnummer: {hund.Chipnummer}, Kan med andre hunde: {hund.KanMedAndreHunde}");
                }
            }

            Pause();
        }
        #endregion

        #region 6. & 7.Filtrer på katte (kan/ikke kan med andre katte & om de er indekatte)

        private void VisKatteDerKanMedAndreKatte()
        {
            Console.Write("Skal katte kunne med andre katte? (true/false): ");
            bool kanMedAndreKatte;
            while (!bool.TryParse(Console.ReadLine(), out kanMedAndreKatte))
            {
                Console.Write("Ugyldigt input. Skriv 'true' eller 'false': ");
            }

            List<Kat> kate = dyrRepo.FiltreringPåKatteDerKanMedAndreKatte(kanMedAndreKatte);

            if (kate.Count == 0)
            {
                Console.WriteLine("Ingen katte fundet med det kriterie.");
            }
            else
            {
                Console.WriteLine($"\nKatte der {(kanMedAndreKatte ? "kan" : "ikke kan")} med andre katte:");
                foreach (var kat in kate)
                {
                    Console.WriteLine($"Navn: {kat.Navn}, Race: {kat.Race}, Alder: {kat.Alder}, Chipnummer: {kat.Chipnummer}, Kan med andre katte?: {kat.KanMedAndreKatte}, Er det en indekat: {kat.SkalVæreIndekat}");
                }
            }

            Pause();
        }

        private void VisKatteDerErIndekatte()
        {
            Console.Write("Skal katte være indekatte? (true/false): ");
            bool SkalVæreIndekat;
            while (!bool.TryParse(Console.ReadLine(), out SkalVæreIndekat))
            {
                Console.Write("Ugyldigt input. Skriv 'true' eller 'false': ");
            }

            List<Kat> kate = dyrRepo.FiltreringPåKatteDerSkalVæreIndekat(SkalVæreIndekat);

            if (kate.Count == 0)
            {
                Console.WriteLine("Ingen katte fundet med det kriterie.");
            }
            else
            {
                Console.WriteLine($"\nKatte der {(SkalVæreIndekat ? "skal" : "ikke skal")} være indekatte?:");
                foreach (var kat in kate)
                {
                    Console.WriteLine($"Navn: {kat.Navn}, Race: {kat.Race}, Alder: {kat.Alder}, Chipnummer: {kat.Chipnummer},Kan med andre katte?: {kat.KanMedAndreKatte}, Er det en indekat: {kat.SkalVæreIndekat}");
                }
            }

            Pause();
        }
        #endregion

        private void Pause()
        {
            Console.WriteLine("Tryk på en tast for at fortsætte...");
            Console.ReadKey();
        }
    }
}

