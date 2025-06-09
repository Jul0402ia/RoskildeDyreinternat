using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiDyr
    {
        private DyrRepo dyrRepo;
        private Medarbejder aktivMedarbejder;

        public UiDyr(DyrRepo repo, Medarbejder medarbejder)
        {
            dyrRepo = repo;
            aktivMedarbejder = medarbejder;
        }

        public void VisMenu()
        {
            string valg;
            do
            {
                Console.Clear();
                Console.WriteLine("=== DYR MENU ===");
                Console.WriteLine("1. Tilføj hund");
                Console.WriteLine("2. Tilføj kat");
                Console.WriteLine("3. Vis hunde");
                Console.WriteLine("4. Vis katte");
                Console.WriteLine("5. Slet dyr");
                Console.WriteLine("6. Rediger dyr");
                Console.WriteLine("7. Filtrér dyr");
                Console.WriteLine("0. Tilbage til hovedmenu");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        TilføjHund();
                        break;
                    case "2":
                        TilføjKat();
                        break;
                    case "3":
                        dyrRepo.VisHunde();
                        Pause();
                        break;
                    case "4":
                        dyrRepo.VisKatte();
                        Pause();
                        break;
                    case "5":
                        SletDyr();
                        break;
                    case "6":
                        RedigerDyr();
                        break;
                    case "7":
                        VisFiltreringsMenu();
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
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg");
                        Pause();
                        break;
                }

            } while (valg != "0");
        }
        #region Tilføj hund (exeption)
        private void TilføjHund()
        {
            try
            {
                Console.Write("Navn: ");
                string navn = Console.ReadLine();

                Console.Write("Race: ");
                string race = Console.ReadLine();

                Console.Write("Alder: ");
                int alder = int.Parse(Console.ReadLine());

                Console.Write("Køn: ");
                string køn = Console.ReadLine();

                Console.Write("Chipnummer: ");
                int chip = int.Parse(Console.ReadLine());

                Console.Write("Helbredstilstand: ");
                string helbred = Console.ReadLine();

                Console.Write("Er trænet (true/false): ");
                bool erTrænet = bool.Parse(Console.ReadLine());

                Console.Write("Kan med andre hunde (true/false): ");
                bool kanMedAndreHunde = bool.Parse(Console.ReadLine());

                Console.Write("Er adopteret (true/false): ");
                bool erAdopteret = bool.Parse(Console.ReadLine());

                Hund nyHund = new Hund(navn, race, alder, køn, chip, helbred, erTrænet, kanMedAndreHunde, erAdopteret);
                dyrRepo.AddHund(nyHund, aktivMedarbejder);

                Console.WriteLine("Hund tilføjet.");
            }
            catch (Exception)
            {
                Console.WriteLine("Der var en fejl i dit input. Prøv igen med de rigtige værdier.");
            }

            Pause();
        }

        #endregion

        #region Tilføj kat (exeption)
        private void TilføjKat()
        {
            try
            {
                Console.Write("Navn: ");
                string navn = Console.ReadLine();

                Console.Write("Race: ");
                string race = Console.ReadLine();

                Console.Write("Alder: ");
                int alder = int.Parse(Console.ReadLine());

                Console.Write("Køn: ");
                string køn = Console.ReadLine();

                Console.Write("Chipnummer: ");
                int chip = int.Parse(Console.ReadLine());

                Console.Write("Helbredstilstand: ");
                string helbred = Console.ReadLine();

                Console.Write("Skal være indekat (true/false): ");
                bool indekat = bool.Parse(Console.ReadLine());

                Console.Write("Kan med andre katte (true/false): ");
                bool kanMed = bool.Parse(Console.ReadLine());

                Console.Write("Er adopteret (true/false): ");
                bool erAdopteret = bool.Parse(Console.ReadLine());

                Kat nyKat = new Kat(navn, race, alder, køn, chip, helbred, indekat, kanMed, erAdopteret);
                dyrRepo.AddKat(nyKat, aktivMedarbejder);

                Console.WriteLine("Kat tilføjet.");
            }
            catch (Exception)
            {
                Console.WriteLine("Der var en fejl i dit input. Prøv igen med de rigtige værdier.");
            }

            Pause();
        }

        #endregion

        #region Slet dyr (exeption)
        private void SletDyr()
        {
            try
            {
                Console.Write("Indtast chipnummer på dyret der skal slettes: ");
                // Exception hvis input ikke er tal
                int chipnummer = int.Parse(Console.ReadLine());  

                bool slettet = dyrRepo.SletDyr(chipnummer, aktivMedarbejder);

                if (slettet)
                {
                    Console.WriteLine("Dyret blev slettet.");
                }
                else
                {
                    Console.WriteLine("Kunne ikke slette dyret.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ugyldigt chipnummer. Du skal indtaste et tal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            }

            Pause();
        }
        #endregion

        #region Rediger Dyr (exeption)
        private void RedigerDyr()
        {
            try
            {
                Console.Write("Indtast chipnummer på dyret der skal redigeres: ");
                int chipnummer = int.Parse(Console.ReadLine());  // kan kaste FormatException

                Console.Write("Nyt navn: ");
                string nytNavn = Console.ReadLine();

                Console.Write("Ny race: ");
                string nyRace = Console.ReadLine();

                Console.Write("Ny alder: ");
                int nyAlder = int.Parse(Console.ReadLine());  // kan kaste FormatException

                Console.Write("Ny helbredstilstand: ");
                string nytHelbred = Console.ReadLine();

                bool opdateret = dyrRepo.RedigerDyr(chipnummer, aktivMedarbejder, nytNavn, nyRace, nyAlder, nytHelbred);
                if (opdateret)
                {
                    Console.WriteLine("Dyret blev opdateret.");
                }
                else
                {
                    Console.WriteLine("Kunne ikke opdatere dyret.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ugyldigt input! Chipnummer og alder skal være tal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            }

            Pause();
        }
        #endregion

        #region Filtrerring på dyr (alder) exeption
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

        #region Søg kat eller hund efter race
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

        #region Filtrer på hunde (kan/ikke kan med andre hunde & om de er trænet eller ej)
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

        #region Filtrer på katte (kan/ikke kan med andre katte & om de er indekatte)

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
                Console.WriteLine($"\nKatte der {(SkalVæreIndekat? "skal" : "ikke skal")} være indekatte?:");
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

