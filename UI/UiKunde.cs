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
        private BrugerRepo brugerRepo;
        private BesøgRepo besøgRepo;
        private Kunde aktivKunde;

        public UiKunde(BrugerRepo repo, BesøgRepo besøgRepo)
        {
            this.brugerRepo = repo;
            this.besøgRepo = besøgRepo;
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

        private void VisMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== KUNDE MENU ===");
                Console.WriteLine("1. Vis mine oplysninger");
                Console.WriteLine("2. Vis mine bookinger");
                Console.WriteLine("3. Opdater mine kontaktoplysninger");
                Console.WriteLine("4. Slet min kundeprofil");
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
                        OpdaterKontaktInfo();
                        break;
                    case "4":
                        SletMinProfil();
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

        private void OpdaterKontaktInfo()
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
    }
}

