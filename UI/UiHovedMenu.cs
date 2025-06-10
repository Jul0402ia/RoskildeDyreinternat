using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.Repositories;

namespace RoskildeDyreinternat.UI
{
    public class UiHovedMenu
    {
        private UiGaest uiGaest;
        private UiKunde uiKunde;
        private UiMedarbejder uiMedarbejder;
        private UiFrivillig uiFrivillig;

        public UiHovedMenu(DyrRepo dyrRepo, BrugerRepo brugerRepo, BesøgRepo besøgRepo)
        {
            uiGaest = new UiGaest(dyrRepo, brugerRepo);
            uiKunde = new UiKunde(dyrRepo, brugerRepo, besøgRepo); // hvis du har UiKunde – ellers fjern denne
            uiMedarbejder = new UiMedarbejder(brugerRepo, besøgRepo, dyrRepo);
            uiFrivillig = new UiFrivillig(brugerRepo, besøgRepo, dyrRepo);
        }


        public void Start()
        {
            Console.WriteLine("Velkommen til Dyreinternatet!");
            VisMenu();
        }

        public void VisMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HOVEDMENU ===");
                Console.WriteLine("1. Kom ind som gæst");
                Console.WriteLine("2. Login som kunde");
                Console.WriteLine("3. Login som medarbejder");
                Console.WriteLine("4. Login som frivillig");
                Console.WriteLine("0. Afslut");

                string valg = Console.ReadLine();
                switch (valg)
                {
                    case "1":
                        uiGaest.Start();
                        break;
                    case "2":
                        uiKunde?.Start(); // hvis du har UiKunde
                        break;
                    case "3":
                        uiMedarbejder.Start();
                        break;
                    case "4":
                        uiFrivillig.Start();
                        break;
                    case "0":
                        Console.WriteLine("Farvel og tak!");
                        return;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }
    }
}
