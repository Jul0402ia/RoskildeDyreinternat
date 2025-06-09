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
      public Hund(string navn, string race,int alder, string køn, int chipnummer, string helbredstilstand, bool erTrænet, bool kanMedAndreHunde, bool erAdopteret)
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
        public override string ToString() 
        {
            return $"Navn: {Navn}\nRace: {Race}\nChipnummer: {Chipnummer}\nKøn: {Køn}\nAlder{Alder}\nHelbredstilstand {Helbredstilstand}\nErAdopteret {ErAdopteret}\nkanMedAndreHunde{KanMedAndreHunde}\nErTrænet{ErTrænet}";
        }

        //den laver lyd
        public override string LavLyd()
        {
            return "Hunden siger: Vuf!";
        }
        //skal bruges til AddHund (sikkerhed for at den samme hund ikke bliver tilføjet flere gange) 
        public override int GetChipnummer()
        {
            return Chipnummer;
        }
    }
}

    

