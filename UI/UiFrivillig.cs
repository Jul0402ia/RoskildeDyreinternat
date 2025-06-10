using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiFrivillig
    {
        private BrugerRepo brugerRepo;
        private BesøgRepo besøgRepo;
        private DyrRepo dyrRepo;
        private Medarbejder aktivMedarbejder;

        public UiFrivillig(BrugerRepo brugerRepo, BesøgRepo besøgRepo, DyrRepo dyrRepo)
        {
            this.brugerRepo = brugerRepo;
            this.besøgRepo = besøgRepo;
            this.dyrRepo = dyrRepo;
        }

        public void Start()
        {
            Console.WriteLine("Indtast frivillig ID for login:");

            if (int.TryParse(Console.ReadLine(), out int id) &&
                id >= 0 && id < brugerRepo.medarbejderListe.Count)
            {
                aktivMedarbejder = brugerRepo.medarbejderListe[id];
                Console.WriteLine($"Velkommen, {aktivMedarbejder.Navn}!");

                if (aktivMedarbejder.Antalarbejdstimer == 0)
                {
                    Console.WriteLine("Advarsel: Medarbejderen har 0 arbejdstimer.");
                }

                VisFrivilligMenu();
            }
            else
            {
                Console.WriteLine("Ugyldigt ID. Login mislykkedes.");
            }
        }

        private void VisFrivilligMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== FRIVILLIG MENU ===");
                Console.WriteLine("1. Vis mine oplysninger");
                Console.WriteLine("2. Opdater mine oplysninger");
                Console.WriteLine("3. Hent besøg for dyr ud fra chipnummer");
                Console.WriteLine("4. Filtrer besøg på dyr efter chip og selvvalgte datoer");
                Console.WriteLine("0. Log ud");
                Console.Write("Vælg: ");
                valg = Console.ReadLine();

                switch (valg)
                {
                    case "1":
                        VisMineOplysninger();
                        break;
                    case "2":
                        OpdaterOgVisMedarbejderUI(
                            aktivMedarbejder.Id,
                            Input("Nyt navn: "),
                            Input("Ny email: "),
                            Input("Nyt telefonnummer: "),
                            Input("Ny adresse: "));
                        break;
                    case "3":
                        VisBesøgForDyr();
                        break;
                    case "4":
                        StartFiltrérBesøgUI();
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

        public void VisLoggetBrugerInfo(Bruger loggetBruger)
        {
            if (loggetBruger == null)
            {
                Console.WriteLine("Ingen bruger er logget ind.");
                return;
            }

            Console.WriteLine("=== Bruger Information ===");
            Console.WriteLine(loggetBruger.ToString());
            Console.WriteLine("==========================");
        }

        #region 1. Vis mine oplysninger
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

        #region 2. Opdater oplysninger    
        private void OpdaterOgVisMedarbejderUI(int id, string navn, string email, string telefon, string adresse)
        {
            try
            {
                brugerRepo.OpdaterMedarbejderInfo(id, navn, email, telefon, adresse);
                string info = brugerRepo.HentMedarbejderInfoSomTekst(id);
                Console.WriteLine("Opdateret frivilligs info:");
                Console.WriteLine(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
        }
        #endregion

        #region 3. Vis besøg for dyr ud fra chipnummer
        private void VisBesøgForDyr()
        {
            Console.Write("Indtast dyrets chipnummer: ");
            if (!int.TryParse(Console.ReadLine(), out int chipnummer))
            {
                Console.WriteLine("Ugyldigt chipnummer.");
                return;
            }

            var besøgForDyr = besøgRepo.HentBesøgForDyr(chipnummer);

            if (besøgForDyr.Count == 0)
            {
                Console.WriteLine("Ingen besøg fundet for dette chipnummer.");
                return;
            }

            Console.WriteLine($"\nBesøg for dyr med chipnummer {chipnummer}:");
            foreach (var besøg in besøgForDyr)
            {
                Console.WriteLine($"Dato: {besøg.Dato:dd-MM-yyyy}, Kunde: {besøg.Kunde.Navn}, Dyr: {besøg.Dyr.Navn}");
            }
        }
        #endregion

        #region 4. Filtrering af besøg efter chip og datoer
        private void StartFiltrérBesøgUI()
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

            var filtreredeBesøg = besøgRepo.FiltrerBesøgPåChipnummerOgDato(chipnummer, fraDato, tilDato);

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

        #region Hjælpefunktion
        private string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        #endregion
    }
}

