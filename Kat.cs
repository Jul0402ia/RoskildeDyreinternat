using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class Kat : Dyr
    {
        // Private fields med lille begyndelsesbogstav
        private bool kanMedAndreKatte;
        private bool skalVæreIndekat;

        // Konstruktør med parametre 
        public Kat(string navn, string race,int alder, string køn, int chipnummer, string helbredstilstand, bool skalVæreIndekat, bool kanMedAndreKatte, bool erAdopteret)

            : base(navn, race, chipnummer, køn, alder, helbredstilstand, erAdopteret)
        {
            // Brug af 'this.' for at skelne mellem felt og parameter
            this.kanMedAndreKatte = kanMedAndreKatte;
            this.skalVæreIndekat = skalVæreIndekat;

        }
        //Properties
        public bool KanMedAndreKatte
        {
            get { return this.kanMedAndreKatte; }
            set { this.kanMedAndreKatte = value; }
        }
        public bool SkalVæreIndekat
        {
            get { return this.skalVæreIndekat; }
            set { this.skalVæreIndekat = value; }
        }
        //den printer alt info om kat
        public override string ToString()
        {
            return $"Navn: {Navn}\nRace: {Race}\nChipnummer: {Chipnummer}\nKøn: {Køn}\nAlder{Alder}\nHelbredstilstand {Helbredstilstand}\nErAdopteret {ErAdopteret}\nKanMedAndreHunde{KanMedAndreKatte}\nSkalVæreIndekat{SkalVæreIndekat}";
        }

        //den laver lyd 
        public override string LavLyd()
        {
            return "Katten siger: Miau!";
        }
        //skal bruges til AddKat (sikkerhed for at den samme kat ikke bliver tilføjet flere gange) 
        public override int GetChipnummer()
        {
            return Chipnummer;
        }

    }
}
