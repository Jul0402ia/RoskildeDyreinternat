using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiKunde
    {
        private DyrRepo dyrRepo;
        private BrugerRepo brugerRepo;
        private BesøgRepo besøgRepo;
        private Kunde aktivKunde;

        public UiKunde(DyrRepo dyrrepo, BrugerRepo brugerrepo, BesøgRepo besøgrepo)
        {
            this.dyrRepo = dyrrepo;
            this.brugerRepo = brugerrepo;
            this.besøgRepo = besøgrepo;
        }

        #region login med kunde-id 
        public void Start()
        {
            Console.WriteLine("Indtast kunde ID for login:");
            if (int.TryParse(Console.ReadLine(), out int kundeId))
            {
                if (brugerRepo.kundeListe.TryGetValue(kundeId, out Kunde kunde))
                {
                    aktivKunde = kunde;
                    Console.WriteLine($"Velkommen, {aktivKunde.Navn}!");
                    VisMenu();
                }
                else
                {
                    Console.WriteLine("Kunde ikke fundet.");
                }
            }
            else
            {
                Console.WriteLine("Ugyldigt ID.");
            }
        }
        #endregion

        #region Kundemenu
        private void VisMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== KUNDE MENU ===");
                Console.WriteLine("1. Vis mine oplysninger");
                Console.WriteLine("2. Vis mine bookinger");
                Console.WriteLine("3. Opdater mine oplysninger");
                Console.WriteLine("4. Slet min kundeprofil");
                Console.WriteLine("5. Hent mine besøg");
                Console.WriteLine("6. Kom ind på dyr menu");
                Console.WriteLine("0. Log ud");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        VisMineOplysninger();
                        break;
                    case "2":
                        VisMineBookinger();
                        break;
                    case "3":
                        OpdaterKundeInfo();
                        break;
                    case "4":
                        SletMinProfil();
                        break;
                    case "5":
                        VisMenu();
                        break;
                    case "0":
                        Console.WriteLine("Logger ud...");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg");
                        break;
                }
            } while (valg != "0");
        }

        #region 1.Vis Mine Oplysninger
        private void VisMineOplysninger()
        {
            Console.WriteLine($"ID: {aktivKunde.Id}");
            Console.WriteLine($"Navn: {aktivKunde.Navn}");
            Console.WriteLine($"Email: {aktivKunde.Email}");
            Console.WriteLine($"Telefon: {aktivKunde.Telefon}");
            Console.WriteLine($"Adresse: {aktivKunde.Adresse}");
            Console.WriteLine($"Alder: {aktivKunde.Alder}");
            Console.WriteLine($"Køn: {aktivKunde.Køn}");
            Console.WriteLine($"Rolle: {aktivKunde.Rolle}");
        }
        //public void VisLoggetBrugerInfo(Bruger loggetBruger)
        //{
        //    if (loggetBruger == null)
        //    {
        //        Console.WriteLine("Ingen bruger er logget ind.");
        //        return;
        //    }

        //    Console.WriteLine("=== Bruger Information ===");
        //    // Kalder PrintAltInfo() indirekte
        //    Console.WriteLine(loggetBruger.ToString());
        //    Console.WriteLine("==========================");
        //}
        #endregion 

        #region 2.Vis Mine Bookinger
        private void VisMineBookinger()
        {
            var bookinger = besøgRepo.HentBesøgForKunde(aktivKunde);
            if (bookinger.Count == 0)
            {
                Console.WriteLine("Du har ingen bookinger.");
            }
            else
            {
                Console.WriteLine("Dine bookinger:");
                foreach (var besog in bookinger)
                {
                    Console.WriteLine($"- Dato: {besog.Dato}, Dyr: {besog.PrintBesogsInfo()}");
                }
            }
        }
        #endregion

        #region 3.OpdaterKundeInfo
        private void OpdaterKundeInfo()
        {
            Console.WriteLine("Indtast nyt navn (enter for at beholde):");
            string navn = Console.ReadLine();
            Console.WriteLine("Indtast ny email (enter for at beholde):");
            string email = Console.ReadLine();
            Console.WriteLine("Indtast nyt telefonnummer (enter for at beholde):");
            string telefon = Console.ReadLine();
            Console.WriteLine("Indtast ny adresse (enter for at beholde):");
            string adresse = Console.ReadLine();

            // Hvis felt er tomt, behold nuværende værdi
            if (string.IsNullOrWhiteSpace(navn)) navn = aktivKunde.Navn;
            if (string.IsNullOrWhiteSpace(email)) email = aktivKunde.Email;
            if (string.IsNullOrWhiteSpace(telefon)) telefon = aktivKunde.Telefon;
            if (string.IsNullOrWhiteSpace(adresse)) adresse = aktivKunde.Adresse;

            brugerRepo.OpdaterKundeInfo(aktivKunde.Id, navn, email, telefon, adresse);
            Console.WriteLine("Kontaktoplysninger opdateret.");
        }
        #endregion

        #region 4. Slet min profil
        private void SletMinProfil()
        {
            Console.WriteLine("Vil du slette alle dine bookinger sammen med din profil? (ja/nej)");
            string svar = Console.ReadLine().ToLower();

            bool sletBesog = svar == "ja";
            // Vi skal bruge en medarbejderId til at validere sletning i BrugerRepo.SletKunde - kunder kan ikke slette selv uden medarbejder, 
            // så i en rigtig app ville vi lave en anden metode, men her kan vi f.eks. bruge 0 (ingen adgang), og dermed nægte sletning.

            Console.WriteLine("Indtast medarbejder ID der godkender sletning (kun medarbejdere med timer kan slette):");
            if (int.TryParse(Console.ReadLine(), out int medarbejderId))
            {
                try
                {
                    brugerRepo.SletKunde(aktivKunde.Id, medarbejderId, sletBesog);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Ugyldigt medarbejder ID. Kan ikke slette.");
            }
        }
        #endregion

        #endregion 

        #region DyrHåndteringsMenu
        private void VisDyrMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== DYRMENU ===");
                Console.WriteLine("1. Book et besøg");
                Console.WriteLine("2. Vis alle hunde");
                Console.WriteLine("3. Vis alle katte");
                Console.WriteLine("4. Søg hunde efter race");
                Console.WriteLine("5. Søg katte efter race");
                Console.WriteLine("6. Filtrerer dyr efter alderen");
                Console.WriteLine("7. Vis hunde der kan, eller ikke kan med andre hunde");
                Console.WriteLine("8. Vis hunde der er, eller ikke er trænet");
                Console.WriteLine("9. Vis katte der kan, eller ikke kan med andre katte");
                Console.WriteLine("10. Vis katte der er, eller ikke er indekatte");
                Console.WriteLine("0. Log ud");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        BookBesøgUI();
                        break;
                    case "2":
                        dyrRepo.VisHunde();
                        break;
                    case "3":
                        dyrRepo.VisKatte();
                        break;
                    case "4":
                        SøgHundePåRace();
                        break;
                    case "5":
                        SøgKattePåRace();
                        break;
                    case "6":
                        FiltrererIMellemDyretsAlder();
                        break;
                    case "7":
                        VisHundeDerKanMedAndreHunde();
                        break;
                    case "8":
                        VisHundeEfterTræningstilstand();
                        break;
                    case "9":
                        VisKatteDerKanMedAndreKatte();
                        break;
                    case "10":
                        VisKatteDerErIndekatte();
                        break;
                }
            } while (valg != "0");
        }
        #endregion

        #region 1. Book Besøg
        private void BookBesøgUI()
        {
            Console.WriteLine("=== Book et besøg ===");

            Console.Write("Indtast dato (dd-mm-yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dato))
            {
                Console.WriteLine("Ugyldig dato.");
                return;
            }

            Console.Write("Indtast dyrets chipnummer: ");
            if (!int.TryParse(Console.ReadLine(), out int chipnummer))
            {
                Console.WriteLine("Ugyldigt chipnummer.");
                return;
            }

            Console.Write("Indtast kunde-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int kundeId))
            {
                Console.WriteLine("Ugyldigt kunde-ID.");
                return;
            }

            // Find dyr og kunde i deres respektive lister/repos
            Dyr dyr = dyrRepo.FindDyrByChipnummer(chipnummer); // du skal have denne metode i DyrRepo
            Kunde kunde = brugerRepo.FindKundeById(kundeId);   // du skal have denne metode i BrugerRepo

            if (besøgRepo.BookBesøg(dato, kunde, dyr))
            {
                Console.WriteLine("Besøg blev booket!");
            }
            else
            {
                Console.WriteLine("Booking mislykkedes.");
            }
            Pause();
        }
        #endregion

        #region 2. & 3. Vis hundeliste og katteliste
        #endregion

        #region 4. & 5.Søg hund eller kat efter race
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
        #endregion

        #region 6.Filtrering dyr alder
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

        #region 7. & 8. Filtrer på hunde (kan/ikke kan med andre hunde & om de er trænet eller ej)
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

        #region 9. & 10.Filtrer på katte (kan/ikke kan med andre katte & om de er indekatte)

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
    


