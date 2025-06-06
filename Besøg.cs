using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{

    public class Besøg
    {
        private static int næsteBesøgsnummer = 1;
        public int Besøgsnummer { get; private set; }
        public DateTime Dato { get; set; }
        public Kunde Kunde { get; set; }
        public Dyr Dyr { get; set; }

        public Besøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            Besøgsnummer = næsteBesøgsnummer++;
            Dato = dato;
            Kunde = kunde;
            Dyr = dyr;
        }

        public string PrintBesogsInfo()
        {
            return $"Besøg #{Besøgsnummer} - {Dato}\n" +
                   $"Kunde: {Kunde.Navn}\n" +
                   $"Dyr: {Dyr.Navn} (Chipnummer: {Dyr.Chipnummer})\n";
        }
    }
}