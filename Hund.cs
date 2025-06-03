using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
 
        public class Hund : Dyr
    {
        // Private fields med lille begyndelsesbogstav
        private bool kanMedAndreHunde;
        private bool erTrænet;

        // Konstruktør med parametre 
        public Hund(bool kanMedAndreHunde, bool erTrænet, string navn, string race, int chipnummer, string køn, int alder, string helbredstilstand, bool erAdopteret)

            : base(navn, race, chipnummer, køn, alder, helbredstilstand, erAdopteret)
        {
            // Brug af 'this.' for at skelne mellem felt og parameter
            this.kanMedAndreHunde = kanMedAndreHunde;
            this.erTrænet = erTrænet;
        }
        //Properties
        public bool KanMedAndreHunde
        {
            get { return kanMedAndreHunde; }
            set { kanMedAndreHunde = value; }
        }

        public bool ErTrænet
        {
            get { return erTrænet; }
            set { erTrænet = value; }
        }
        //den printer alt info om hund
        public override string PrintAltInfo() // <- krævet af abstract base class
        {
            return $"Navn: {Navn}\nRace: {Race}\nChipnummer: {Chipnummer}\nKøn: {Køn}\nAlder{Alder}\nHelbredstilstand {Helbredstilstand}\nErAdopteret {ErAdopteret}\nkanMedAndreHunde{KanMedAndreHunde}\nErTrænet{ErTrænet}";
        }

        //den laver lyd
        public override string Lavlyd()
        {
            return "Hunden siger: Vuf!";
        }

    }
}

    }
}
