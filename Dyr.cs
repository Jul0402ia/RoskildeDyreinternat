using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    abstract class Dyr
    {
    // Private instance fields
    private string navn;
        private int alder;
        private string race;
        private double vægt;
        private string farve;
        private bool erVaccineret;

        // Constructor
        public Dyr(string navn, int alder, string race, double vægt, string farve, bool erVaccineret)
        {
            this.navn = navn;
            this.alder = alder;
            this.race = race;
            this.vægt = vægt;
            this.farve = farve;
            this.erVaccineret = erVaccineret;
        }

        // Properties (offentlig adgang)
        public string Navn
        {
            get { return navn; }
            set { navn = value; }
        }

        public int Alder
        {
            get { return alder; }
            set { alder = value; }
        }

        public string Race
        {
            get { return race; }
            set { race = value; }
        }

        public double Vægt
        {
            get { return vægt; }
            set { vægt = value; }
        }

        public string Farve
        {
            get { return farve; }
            set { farve = value; }
        }

        public bool ErVaccineret
        {
            get { return erVaccineret; }
            set { erVaccineret = value; }
        }

        // Abstrakt metode, som underklasser skal implementere
        public abstract void LavLyde();

        // En almindelig metode
        public void VisInfo()
        {
            Console.WriteLine($"Navn: {navn}, Alder: {alder} år, Race: {race}, Vægt: {vægt} kg, Farve: {farve}, Vaccineret: {erVaccineret}");
        }
    }
}

