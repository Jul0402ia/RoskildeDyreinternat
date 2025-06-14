using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class Kunde : Bruger
    {
        // Private fields med lille begyndelsesbogstav
        private int alder;
        private string køn;

        // Konstruktør med parametre 
        public Kunde(int id, string navn, string email, string telefon, string adresse, string rolle, int alder, string køn)
            : base(id, navn, email, telefon, adresse, rolle)
        {
            // Brug af 'this.' for at skelne mellem felt og parameter
            this.alder = alder;
            this.køn = køn;
        }

        //Properties 
        public int Alder
        {
            get { return this.alder; }
            set { this.alder = value; }
        }
        public string Køn
        {
            get { return this.køn; }
            set { this.køn = value; }
        }

        //den printer alt info om  kunde
        public override string PrintAltInfo()
        {
            return $" ID: {Id}\nNavn: {Navn}\nEmail: {Email}\nAdresse: {Adresse}\nTlf: {Telefon}\nRolle: {Rolle}\nAlder: {Alder}\nKøn: {Køn}";

        }
    }
}
