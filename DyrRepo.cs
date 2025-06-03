using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
 
    public class DyrRepo

    {
        List<Hund> HundeListe = new List<Hund>();
        List<Kat> KatteListe = new List<Kat>();




        // Hundeliste hvor der kan tilføjes en hund
        public bool AddHund(Hund hund)
        {
            if (hund != null && !HundeListe.Any(h => h.GetChipnummer() == hund.GetChipnummer()))
            {
                this.HundeListe.Add(hund);
                return true;
            }
            else
            {
                //Her kan der tilføjes en besked her
                Console.WriteLine("Hunden findes allerede.");
                return false;

            }
        }

        // Katteliste hvor der kan tilføjes en kat
        public bool AddKat(Kat kat)
        {
            if (kat != null && !KatteListe.Any(k => k.GetChipnummer() == kat.GetChipnummer()))
            {
                this.KatteListe.Add(kat);
                return true;
            }
            else
            {
                //Her kan der tilføjes en besked her
                Console.WriteLine("Hunden findes allerede.");
                return false;

            }
        }
        // Returnerer en liste med alle hunde, der er gemt i systemet.
        public List<Hund> GetAllHunde()
        {
            return HundeListe;
        }
        // Returnerer en liste med alle katte, der er gemt i systemet.
        public List<Kat> GetAllKatte()
        {
            return KatteListe;
        }


        //Søgning ud for chipnummer

        public void VisDyrInfo(int chipnummer)
        {
            // Søg i hundelisten
            foreach (var hund in HundeListe)
            {
                if (hund.GetChipnummer() == chipnummer)
                {
                    Console.WriteLine($"Du har søgt efter Chipnummeret: {chipnummer}:");
                    Console.WriteLine("Hund fundet:");
                    Console.WriteLine();
                    Console.WriteLine(hund.PrintAltInfo()); // Bruger ToString()
                    Console.WriteLine();
                    return;
                }
            }

            // Søg i kattelisten
            foreach (var kat in KatteListe)
            {
                if (kat.GetChipnummer() == chipnummer)
                {
                    Console.WriteLine($"Du har søgt efter Chipnummeret: {chipnummer}:");
                    Console.WriteLine("Kat fundet:");
                    Console.WriteLine();
                    Console.WriteLine(kat.PrintAltInfo()); // Bruger ToString()
                    Console.WriteLine();
                    return;
                }
            }

            // Hvis ingen dyr blev fundet
            Console.WriteLine($"Intet dyr fundet med chipnummer {chipnummer}.");


            //While-loop: Tjekker om den nyoprettede kat, ikke har samme chipnummer som en kat på listen.
            //public bool AddKat(Kat _kat)
            //{
            //    if (_kat == null)
            //        return false;

            //    int i = 0;
            //    while (i < KatteListe.Count)
            //    {
            //        if (KatteListe[i].chipnummer == _kat.chipnummer)
            //        {
            //            return false; // Dublet fundet – tilføj ikke
            //        }
            //        i++;
            //    }

            //    KatteListe.Add(_kat); // Ingen dublet fundet – tilføj katten
            //    return true;
            //}
        }


        //fitrering ud fra køn på dyr
        public List<Dyr> GetDyrByKøn(string køn)
        {
            List<Dyr> dyrliste = new List<Dyr>();
            // Tilføjer og returner en liste med dyr af det ønskede køn
            foreach (var hund in HundeListe)
            {

                if (hund.GetKøn().ToLower().Contains(køn.ToLower()))
                {
                    dyrliste.Add(hund);



                }
            }

            // Søg i kattelisten
            foreach (var kat in KatteListe)
            {

                if (kat.GetKøn().ToLower().Contains(køn.ToLower()))
                {
                    dyrliste.Add(kat);

                }
            }
            return dyrliste;



        }

        //metoden til at tilføje en hund eller kat - er det noget endet retunerer den "ukendt dyrtype"
        public bool TilføjDyr(Dyr dyr)
        {
            if (dyr == null)
                return false;

            if (dyr is Hund hund)
            {
                return AddHund(hund);
            }
            else if (dyr is Kat kat)
            {
                return AddKat(kat);
            }
            else
            {
                Console.WriteLine("Ukendt dyretype.");
                return false;
            }
        }


        // der er lavet en liste med dyr der kan vælges (1 eller 2), og en filtreringsmulighed for at køn (3 og 4)
        public void ValgteDyr(int valgteDyr)
        {
            Console.WriteLine("----------------------------------");
            if (valgteDyr == 1)
            {

                foreach (var hund in HundeListe)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine(hund.PrintAltInfo());

                }

            }
            else if (valgteDyr == 2)
            {
                foreach (var kat in KatteListe)

                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine(kat.PrintAltInfo());

                }

            }

            else if (valgteDyr == 3)
            {

                GetDyrByKøn("han");


            }


            else if (valgteDyr == 4)
            {

                GetDyrByKøn("hun");

            }

            else
            {

                Console.WriteLine("Ukendt dyretype");

            }
        }

    }

}
