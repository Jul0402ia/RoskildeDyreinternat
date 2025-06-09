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

        public UiMedarbejder(BrugerRepo repo)
        {
            brugerRepo = repo;
        }

        public void Start()
        {
            Console.WriteLine("Indtast medarbejder ID for login:");
            int id = int.Parse(Console.ReadLine());

            if (brugerRepo.HarAdgang(id))
            {
                aktivMedarbejder = brugerRepo.medarbejderListe[id];
                Console.WriteLine($"Velkommen, {aktivMedarbejder.Navn}!");
                VisMenu();
            }
            else
            {
                Console.WriteLine("Adgang nægtet. Medarbejder skal have mere end 0 arbejdstimer.");
            }
        }

        private void VisMenu()
        {
            string valg;
            do
            {
                Console.WriteLine("\n=== MEDARBEJDER MENU ===");
                Console.WriteLine("1. Vis mine oplysninger");
                Console.WriteLine("2. Vis medarbejdere med adgang");
                Console.WriteLine("3. Vis frivillige (uden adgang)");
                Console.WriteLine("4. Søg bruger info (kunde/medarbejder)");
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
                    case "4":
                        SøgBrugerInfo();
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
            Console.WriteLine($"ID: {aktivMedarbejder.Id}");
            Console.WriteLine($"Navn: {aktivMedarbejder.Navn}");
            Console.WriteLine($"Email: {aktivMedarbejder.Email}");
            Console.WriteLine($"Telefon: {aktivMedarbejder.Telefon}");
            Console.WriteLine($"Adresse: {aktivMedarbejder.Adresse}");
            Console.WriteLine($"Rolle: {aktivMedarbejder.Rolle}");
            Console.WriteLine($"Stilling: {aktivMedarbejder.Stilling}");
            Console.WriteLine($"Arbejdstimer: {aktivMedarbejder.Antalarbejdstimer}");
        }

        private void VisMedarbejdereMedAdgang()
        {
            var ansatte = brugerRepo.FiltreringMedarbejdereMedAdgang();
            Console.WriteLine("Medarbejdere med adgang (arbejdstimer > 0):");
            foreach (var m in ansatte)
            {
                Console.WriteLine($"ID: {m.Id}, Navn: {m.Navn}, Timer: {m.Antalarbejdstimer}");
            }
        }

        private void VisFrivillige()
        {
            var frivillige = brugerRepo.FiltreringFrivillige();
            Console.WriteLine("Frivillige (arbejdstimer = 0):");
            foreach (var f in frivillige)
            {
                Console.WriteLine($"ID: {f.Id}, Navn: {f.Navn}");
            }
        }

        private void SøgBrugerInfo()
        {
            Console.Write("Indtast bruger ID at søge på: ");
            int brugerId = int.Parse(Console.ReadLine());
            try
            {
                brugerRepo.SøgningVisBrugerInfo(brugerId, aktivMedarbejder.Id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
