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
        private BrugerRepo brugerRepo;

        public UiGaest(BrugerRepo repo)
        {
            brugerRepo = repo;
        }

        public void Start()
        {
            Console.WriteLine("Velkommen til Dyreinternatet!");
            VisMenu();
        }

        private void VisMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== GÆST MENU ===");
                Console.WriteLine("1. Om om");
                Console.WriteLine("2. Kontaktinformation");
                Console.WriteLine("3. Åbningstider");
                Console.WriteLine("4. Opret konto");
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
                    case "0":
                        Console.WriteLine("Tak for besøget. Farvel!");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        break;
                }
            } while (valg != "0");
        }

        #region Om Os
        private void VisOmOs()
        {
            Console.WriteLine("\nDyreinternatet er en organisation, der hjælper hjemløse dyr.");
            Console.WriteLine("Vi tilbyder pleje, omsorg og nye hjem til hunde og katte.");
            Console.WriteLine("Vi tager gerne imod hjælp fra de frivillige.");
        }
        #endregion

        #region Kontakt info
        private void VisKontaktInfo()
        {
            Console.WriteLine("\nKontakt os på:");
            Console.WriteLine("Telefon: 12 34 56 78");
            Console.WriteLine("Email: kontakt@dyreinternatet.dk");
            Console.WriteLine("Adresse: Dyrevej 1, 4000 Roskilde");
        }
        #endregion

        #region Åbningstider
        private void VisAabningstider()
        {
            Console.WriteLine("\nÅbningstider:");
            Console.WriteLine("Mandag - Fredag: 12:00 - 17:00");
            Console.WriteLine("Lørdag: 10:00 - 14:00");
            Console.WriteLine("Søndag: Lukket");
        }
        #endregion

        #region OpretKundeKonto (try/catch)
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
    }
}

