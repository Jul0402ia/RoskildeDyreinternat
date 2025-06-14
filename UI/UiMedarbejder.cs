using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiMedarbejder
    {
        private BrugerRepo brugerRepo;
        private Medarbejder aktivMedarbejder;
        private BesøgRepo besøgRepo;
        private DyrRepo dyrRepo;

        public UiMedarbejder(BrugerRepo brugerrepo, BesøgRepo besøgrepo, DyrRepo dyrrepo)
        {
            brugerRepo = brugerrepo;
            besøgRepo = besøgrepo;
            dyrRepo = dyrrepo;


        }

        public void Start()

        {
            Console.WriteLine("Indtast medarbejder ID for login:");
            int id = int.Parse(Console.ReadLine());

            if (brugerRepo.HarAdgang(id))
            {
                aktivMedarbejder = brugerRepo.medarbejderListe[id];
                Console.WriteLine($"Velkommen, {aktivMedarbejder.Navn}!");
                VisMedarbejderMenu();
            }
            else
            {
                Console.WriteLine("Adgang nægtet. Medarbejder skal have mere end 0 arbejdstimer.");
            }
        }
        #region MedarbejderHovedMenu
        private void VisMedarbejderMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== MEDARBEJDER MENU ===");
                Console.WriteLine("1. Medarbejder håndtering");
                Console.WriteLine("2. Kunde håndtering");
                Console.WriteLine("3. Dyr håndtering");
                Console.WriteLine("0. Tilbage til hovedmenu");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        VisMedarbejderHåndteringsMenu();
                        break;
                    case "2":
                        VisKundeHåndteringsMenu();
                        break;
                    case "3":
                        VisDyrHåndteringsMenu();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg");
                        break;
                }
            } while (valg != "0");
        }
        #endregion

        #region MedarbejderHåndteringsmenu
        private void VisMedarbejderHåndteringsMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== MEDARBEJDER MENU ===");
                Console.WriteLine("1. Vis mine oplysninger");
                Console.WriteLine("2. Opdaterer mine info");
                Console.WriteLine("3. Opret en ny medarbejder");
                Console.WriteLine("4. Vis bruger info ud fra ID (Søgning)");
                Console.WriteLine("5. Vis alle medarbejder (Filtrering)");
                Console.WriteLine("6. Vis frivillige (Filtrering)");
                Console.WriteLine("0. Log ud");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        VisMineOplysninger();
                        break;
                    case "2":
                        VisMedarbejdereMedAdgang();
                        break;
                    case "3":
                        VisFrivillige();
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

        #region 1. Vis Bruger info på den person der er logget på (bruger IKKE PrintAltInfo -> FIX) 
        private void VisMineOplysninger()
        {
            Console.WriteLine($"ID: {aktivMedarbejder.Id}");
            Console.WriteLine($"Navn: {aktivMedarbejder.Navn}");
            Console.WriteLine($"Email: {aktivMedarbejder.Email}");
            Console.WriteLine($"Telefon: {aktivMedarbejder.Telefon}");
            Console.WriteLine($"Adresse: {aktivMedarbejder.Adresse}");
            Console.WriteLine($"Stilling: {aktivMedarbejder.Stilling}");
            Console.WriteLine($"Antal Arbejdstimer: {aktivMedarbejder.Antalarbejdstimer}");
            Console.WriteLine($"Rolle: {aktivMedarbejder.Rolle}");
        }
        #endregion

        #region 2.Opdater oplysninger    
        public void OpdaterOgVisMedarbejderUI(int id, string navn, string email, string telefon, string adresse)
        {
            try
            {
                // Opdater medarbejder info
                brugerRepo.OpdaterMedarbejderInfo(id, navn, email, telefon, adresse);

                // Hent medarbejder info som tekst
                string info = brugerRepo.HentMedarbejderInfoSomTekst(id);

                // Vis info i UI (Console, GUI eller andet)
                Console.WriteLine("Opdateret medarbejder info:");
                Console.WriteLine(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
        }
        #endregion

        #region 3.Opret ny medarbejder - TOM   
        public void OpretMedarbejder()
        {
            Console.WriteLine("=== Opret ny medarbejder ===");

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Navn: ");
            string navn = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            Console.Write("Adresse: ");
            string adresse = Console.ReadLine();

            Console.Write("Stilling: ");
            string stilling = Console.ReadLine();

            Console.Write("Rolle: ");
            string rolle = Console.ReadLine();

            Console.Write("Antal arbejdstimer: ");
            int timer = int.Parse(Console.ReadLine());

            // Opret objekt
            Medarbejder ny = new Medarbejder(id, navn, email, telefon, adresse, rolle, stilling, timer);

            // Tilføj til repo
            brugerRepo.OpretMedarbejder(ny);

            Console.WriteLine($"Medarbejder '{ny.Navn}' er oprettet.");
        }

        #endregion

        #region 4.Vis bruger info (søgning ud fra id)   
        private void SøgBrugerInfo()
        {
            Console.Write("Indtast bruger ID at søge på: ");
            int brugerId = int.Parse(Console.ReadLine());
            try
            {
                brugerRepo.SøgningVisBrugerInfo(brugerId);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 5.Vis alle medarbejder  
        private void VisMedarbejdereMedAdgang()
        {
            // Her skal du selv vælge tallet – f.eks. 0 eller 5, afhængigt af hvad du mener med adgang
            int antalArbejdstimer = 0;
            var ansatte = brugerRepo.FiltreringMedarbejdereMedAdgang(0);

            Console.WriteLine("Medarbejdere med adgang (arbejdstimer > 0):");
            foreach (var m in ansatte)
            {
                Console.WriteLine($"ID: {m.Id}, Navn: {m.Navn}, Timer: {m.Antalarbejdstimer}");
            }
        }
        #endregion

        #region 6.Vis alle frivillige
        private void VisFrivillige()
        {
            var frivillige = brugerRepo.FiltreringFrivillige(0);  

            Console.WriteLine("Frivillige (arbejdstimer = 0):");
            foreach (var f in frivillige)
            {
                Console.WriteLine($"ID: {f.Id}, Navn: {f.Navn}");
            }
        }
        #endregion
        #endregion

        #region KundeHåndteringsmenu
        private void VisKundeHåndteringsMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== KUNDEHÅNDTERINGSMENU ===");
                Console.WriteLine("1. Vis kundeinfo ud fra id (søgning)");
                Console.WriteLine("2. Slet besøg for kunden (bruges ved dyrets sygdom)");
                Console.WriteLine("3. Slet kundens konto (samt alle kundens besøg)");
                Console.WriteLine("0. Log ud");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        SøgKundeInfo();
                        break;
                    case "2":
                        VisMedarbejdereMedAdgang();
                        break;
                    case "3":
                        SletBesøgForKunden();
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


        #region 1.Vis bruger info (søgning ud fra id)   
        private void SøgKundeInfo()
        {
            Console.Write("Indtast bruger ID at søge på: ");
            int brugerId = int.Parse(Console.ReadLine());
            try
            {
                brugerRepo.SøgningVisBrugerRolle(brugerId);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region 2. Slet besøg for kunde
        public void SletBesøgForKunden()
        {
            Console.Write("Indtast kunde ID til sletning af besøg: ");
            bool kundeIdOk = int.TryParse(Console.ReadLine(), out int kundeId);
            if (!kundeIdOk)
            {
                Console.WriteLine("Ugyldigt kunde ID.");
                return;
            }

            Console.Write("Indtast medarbejder ID: ");
            bool medarbejderIdOk = int.TryParse(Console.ReadLine(), out int medarbejderId);
            if (!medarbejderIdOk)
            {
                Console.WriteLine("Ugyldigt medarbejder ID.");
                return;
            }

            // Antag _repo er instansen, som indeholder metoden SletBesøgForKunde
            besøgRepo.SletBesøgForKunde(kundeId, medarbejderId);
        }
        #endregion
        #region 3. Slet Kunde Konto (+besøg kunden har lavet)
        public void StartSletKundeUI()
        {
            Console.Write("Indtast kunde ID der skal slettes: ");
            bool kundeIdOk = int.TryParse(Console.ReadLine(), out int kundeId);
            if (!kundeIdOk)
            {
                Console.WriteLine("Ugyldigt kunde ID.");
                return;
            }

            Console.Write("Indtast medarbejder ID: ");
            bool medarbejderIdOk = int.TryParse(Console.ReadLine(), out int medarbejderId);
            if (!medarbejderIdOk)
            {
                Console.WriteLine("Ugyldigt medarbejder ID.");
                return;
            }

            Console.Write("Skal kundens besøg slettes samtidig? (ja/nej): ");
            string svar = Console.ReadLine()?.Trim().ToLower();
            bool sletBesøg = svar == "ja" || svar == "j";

            brugerRepo.SletKunde(kundeId, medarbejderId, sletBesøg);
        }
        #endregion
        #endregion

        #region DyrHåndteringsMenu
        private void VisDyrHåndteringsMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== DYRHÅNDTERINGSMENU ===");
                Console.WriteLine("1. Tilføj ny hund");
                Console.WriteLine("2. Tilføj ny kat");
                Console.WriteLine("3. Slet dyret ud fra id");
                Console.WriteLine("4. Rediger info om dyret");
                Console.WriteLine("5. Vis liste af besøg på dyret ud fra id");
                Console.WriteLine("6. Filtrer besøg for et dyr ud fra chipnummer og selvvælgte datoer");
                Console.WriteLine("0. Log ud");
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
                        SletDyr();
                        break;
                    case "4":
                        RedigerDyr();
                        break;
                    case "5":
                        VisBesøgForDyr();
                        break;
                    case "6":
                        StartFiltrérBesøgUI();
                        break;
                }
            } while (valg != "0");
        }
        #endregion

        #region 1.Tilføj hund (exeption)
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
                dyrRepo.AddHund(nyHund, 6);

                Console.WriteLine("Hund tilføjet.");
            }
            catch (Exception)
            {
                Console.WriteLine("Der var en fejl i dit input. Prøv igen med de rigtige værdier.");
            }  
        }

        #endregion

        #region 2.Tilføj kat (exeption)
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
        }

        #endregion

        #region 3.Slet dyr (exeption)
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

        }
        #endregion

        #region 4.RedigerDyrInfo
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
        }
        #endregion

        #region 5.Vis dyr info om besøg ud fra chipnummer
        public void VisBesøgForDyr()
        {
            Console.Write("Indtast dyrets chipnummer: ");
            if (!int.TryParse(Console.ReadLine(), out int chipnummer))
            {
                Console.WriteLine("Ugyldigt chipnummer.");
                return;
            }

            // Kalder på repometoden
            List<Besøg> besøgForDyr = besøgRepo.HentBesøgForDyr(chipnummer);

            if (besøgForDyr.Count == 0)
            {
                Console.WriteLine("Ingen besøg fundet for dette chipnummer.");
                return;
            }

            Console.WriteLine($"\nBesøg for dyr med chipnummer {chipnummer}:");

            foreach (Besøg besøg in besøgForDyr)
            {
                Console.WriteLine($"Dato: {besøg.Dato:dd-MM-yyyy}, Kunde: {besøg.Kunde.Navn}, Dyr: {besøg.Dyr.Navn}");
            }
        }
        #endregion

        #region 6. Filtrering hvor man kan se alle besøg ud fra bestemte datoer (fra - til) ud fra dyrets chipnummer
        public void StartFiltrérBesøgUI()
        {
            Console.Write("Indtast dyrets chipnummer: ");
            if (!int.TryParse(Console.ReadLine(), out int chipnummer))
            {
                Console.WriteLine("Ugyldigt chipnummer.");
                return;
            }

            Console.Write("Indtast startdato (dd-mm-yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fraDato))
            {
                Console.WriteLine("Ugyldig startdato.");
                return;
            }

            Console.Write("Indtast slutdato (dd-mm-yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tilDato))
            {
                Console.WriteLine("Ugyldig slutdato.");
                return;
            }

            // Kalder repo-metoden
            List<Besøg> filtreredeBesøg = besøgRepo.FiltrerBesøgPåChipnummerOgDato(chipnummer, fraDato, tilDato);

            if (filtreredeBesøg.Count == 0)
            {
                Console.WriteLine("Ingen besøg fundet i det angivne interval.");
                return;
            }

            Console.WriteLine($"\nBesøg for dyr med chipnummer {chipnummer} fra {fraDato:dd-MM-yyyy} til {tilDato:dd-MM-yyyy}:");
            foreach (var besøg in filtreredeBesøg)
            {
                Console.WriteLine($"Dato: {besøg.Dato:dd-MM-yyyy}, Kunde: {besøg.Kunde.Navn}, Dyr: {besøg.Dyr.Navn}");
            }
        }

        #endregion
    }
}





        