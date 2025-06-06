using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class Medarbejder : Bruger
    {
        //private fields med små bogstav
        private string stilling;
        private int antalarbejdstimer;

        // Konstruktør med parametre 
        public Medarbejder(int id, string navn, string email, string telefon, string adresse, string rolle, string stilling, int antalarbejdstimer)
          : base(id, navn, email, telefon, adresse, rolle)
        {
            // Brug af 'this.' for at skelne mellem felt og parameter
            this.stilling = stilling;
            this.antalarbejdstimer = antalarbejdstimer;
        }

        // Properties
        public string Stilling
        {
            get { return stilling; }
            set { stilling = value; }
        }

        public int Antalarbejdstimer

        {
            get { return antalarbejdstimer; }
            set { antalarbejdstimer = value; }

        }


        //den printer alt info om medarbejder
        public override string PrintAltInfo()
        {
            return $" ID: {Id}\nNavn: {Navn}\nEmail: {Email}\nAdresse: {Adresse}\nTlf: {Telefon}\nRolle: {Rolle}\nStilling: {Stilling}\nAntalArbejdstimer: {Antalarbejdstimer}";

        }
    }
}

