using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public abstract class Bruger
    {
        //private fields med små bogstav
        private int id;
        private string navn;
        private string email;
        private string telefon;
        private string adresse;
        private string rolle;


        // Konstruktør med parametre 

        public Bruger(int id, string navn, string email, string telefon, string adresse, string rolle)
        {
            // Brug af 'this.' for at skelne mellem felt og parameter
            this.id = id;
            this.navn = navn;
            this.email = email;
            this.telefon = telefon;
            this.adresse = adresse;
            this.rolle = rolle;
        }

        // Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Navn
        {
            get { return navn; }
            set { navn = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }
        public string Rolle
        {
            get { return rolle; }
            set { rolle = value; }

        }
        //den printer alt info om  brugertyper i de subklasser(kunde og medarbejder) hvor den bliver overridet 
        public abstract string PrintAltInfo();


    }



}
