using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
 
    // Public bagging fields med lille begyndelsesbogstav
    public class Besøg
    {
        private static int næsteBesøgsnummer = 1;
        public int Besøgsnummer { get; private set; }
        public DateTime Dato { get; set; }
        public Kunde Kunde { get; set; }
        public Dyr Dyr { get; set; }

        public Besøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            this.Besøgsnummer = næsteBesøgsnummer;
            næsteBesøgsnummer++;

            Dato = dato;
            Kunde = kunde;
            Dyr = dyr;
        }

        public string Book()
        {

            {
                string besked = $"Besøg #{Besøgsnummer} booket til {Dato}";
                Console.WriteLine(besked);
                return besked;
            }
        }

        public string PrintBesogsInfo()
        {
            return $"Besøg #{Besøgsnummer} - {Dato}\n" +
                   $"Kunde: {Kunde.Navn}\n" +
                   $"Dyr: {Dyr.Navn}\n";
        }
    }
}