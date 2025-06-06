using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class DyrRepo
    {
        public Dictionary<int, Dyr> dyrDictionary = new Dictionary<int, Dyr>();

        List<Hund> HundeListe = new List<Hund>();
        List<Kat> KatteListe = new List<Kat>();



        // Tjekker om en medarbejder har adgang (arbejdstimer > 0)
        private bool HarAdgang(Medarbejder medarbejder)
        {
            return medarbejder != null && medarbejder.Antalarbejdstimer > 0;
        }

        // Tilføj hund(if), hvis medarbejder har adgang og chipnummer ikke findes i forvejen
        public bool AddHund(Hund hund, Medarbejder medarbejder)
        {
            if (!HarAdgang(medarbejder))
            {
                Console.WriteLine("Adgang nægtet. Medarbejderen har ingen arbejdstimer.");
                return false;
            }

            if (hund != null && !dyrDictionary.ContainsKey(hund.Chipnummer))
            {
                HundeListe.Add(hund);
                dyrDictionary.Add(hund.Chipnummer, hund);
                return true;
            }

            Console.WriteLine("Hunden findes allerede eller er null.");
            return false;
        }

        // Tilføj kat (while-loop), hvis medarbejder har adgang og chipnummer ikke findes i forvejen

        public bool AddKat(Kat kat, Medarbejder medarbejder)
        {
            if (!HarAdgang(medarbejder))
            {
                Console.WriteLine("Adgang nægtet. Medarbejderen har ingen arbejdstimer.");
                return false;
            }

            int i = 0;
            while (i < KatteListe.Count)
            {
                if (KatteListe[i].Chipnummer == kat.Chipnummer)
                {
                    Console.WriteLine("Katten findes allerede.");
                    return false;
                }
                i++;
            }

            KatteListe.Add(kat);
            dyrDictionary.Add(kat.Chipnummer, kat);
            return true;
        }

        public bool SletDyr(int chipnummer)
        {
            return dyrDictionary.Remove(chipnummer);
        }
        // (if-else) Slet et dyr, hvis medarbejder har adgang
        public bool SletDyr(int chipnummer, Medarbejder medarbejder)
        {
            if (!HarAdgang(medarbejder))
            {
                Console.WriteLine("Adgang nægtet. Medarbejderen har ingen arbejdstimer.");
                return false;
            }

            if (dyrDictionary.TryGetValue(chipnummer, out Dyr dyr))
            {
                if (dyr is Hund hund)
                {
                    HundeListe.Remove(hund);
                }
                else if (dyr is Kat kat)
                {
                    KatteListe.Remove(kat);
                }

                dyrDictionary.Remove(chipnummer);
                Console.WriteLine("Dyr er slettet.");
                return true;
            }

            Console.WriteLine("Dyr ikke fundet.");
            return false;
        }

        // Rediger info om et eksisterende dyr
        public bool RedigerDyr(int chipnummer, Medarbejder medarbejder, string nytNavn, string nyRace, int nyAlder, string nytHelbred)
        {
            if (!HarAdgang(medarbejder))
            {
                Console.WriteLine("Adgang nægtet. Medarbejderen har ingen arbejdstimer.");
                return false;
            }

            if (dyrDictionary.TryGetValue(chipnummer, out Dyr dyr))
            {
                dyr.Navn = nytNavn;
                dyr.Race = nyRace;
                dyr.Alder = nyAlder;
                dyr.Helbredstilstand = nytHelbred;
                Console.WriteLine("Dyrets oplysninger er opdateret.");
                return true;
            }

            Console.WriteLine("Dyr ikke fundet.");
            return false;
        }

        // Vis kun katte
        public void VisKatte()
        {
            foreach (var kat in KatteListe)
            {
                Console.WriteLine(kat.ToString());
            }
        }

        // Vis kun hunde 
        public void VisHunde()
        {
            foreach (var hund in HundeListe)
            {
                Console.WriteLine(hund.ToString());
            }
        }

        #region Filtrering ud fra dictonary på hunde og katte
        public List<Hund> FindAlleHunde()
        {
            return dyrDictionary.Values.OfType<Hund>().ToList();
        }

        public List<Kat> FindAlleKatte()
        {
            return dyrDictionary.Values.OfType<Kat>().ToList();
        }
        #endregion
    }
}
  
