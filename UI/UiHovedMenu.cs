using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat.UI
{
    public class UiHovedMenu
    {
        public void VisMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HOVEDMENU ===");
                Console.WriteLine("1. Håndter dyr");
                Console.WriteLine("2. Håndter kunder");
                Console.WriteLine("3. Håndter medarbejdere");
                Console.WriteLine("0. Afslut");

                string valg = Console.ReadLine();
                switch (valg)
                {
                    case "1":
                        uiDyr.VisMenu();
                        break;
                    case "2":
                        uiKunde.VisMenu();
                        break;
                    case "3":
                        uiMedarbejder.VisMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }
    }
}
}
