using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoskildeDyreinternat.Repositories
{
    public class DyrRepo : IAdgangsKontrol
    {
        private BrugerRepo _brugerRepo;

        public Dictionary<int, Dyr> dyrDictionary = new Dictionary<int, Dyr>();
        List<Hund> HundeListe = new List<Hund>();
        List<Kat> KatteListe = new List<Kat>();

        public DyrRepo(BrugerRepo brugerRepo)
        {
            _brugerRepo = brugerRepo;
        }

        public bool HarAdgang(int medarbejderId)
        {
            foreach (Medarbejder medarbejder in _brugerRepo.medarbejderListe.Values)
            {
                if (medarbejder.Id == medarbejderId && medarbejder.Antalarbejdstimer > 0)
                {
                    return true; 
                }
            }
            Console.WriteLine("Nægtet adgang. Kun medarbejeder, som er ansat på +0h har adgang til denne funktion");
            return false;
        }

        // Tilføj hund(if), hvis medarbejder har adgang og chipnummer ikke findes i forvejen
        public bool AddHund(Hund hund, int medarbejderId)
        {
            if (!HarAdgang(medarbejderId))
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
            if (!HarAdgang(medarbejder.Id))
            {
                Console.WriteLine("Adgang nægtet. Medarbejderen har ingen arbejdstimer.");
                return false;
            }
            //Starter en tæller "i", som bruges til at gå gennem KatteListe.
            int i = 0;
            //kører så længe "i" er mindre end antallet af katte i listen (Count).
            while (i < KatteListe.Count)
            {
                //Tjekker, om den eksisterende kats chip er det samme som den nye kats chip.
                if (KatteListe[i].Chipnummer == kat.Chipnummer)
                {
                    Console.WriteLine("Katten findes allerede.");
                    return false;
                }
                //Øger "i" med 1, så næste kat i listen bliver tjekket i næste runde.
                i++;
            }
            KatteListe.Add(kat);
            dyrDictionary.Add(kat.Chipnummer, kat);
            return true;
        }
        #region SLET HVIS ALT VIRKER
        //SLET HVIS ALT VIRKER
        //public bool SletDyr(int chipnummer)
        //{
        //    // Gennemgå alle nøgler (chipnumre) i dictionary'en
        //    foreach (int nøgle in dyrDictionary.Keys)
        //    {
        //        // Tjek om nøglen matcher det chipnummer, vi vil slette
        //        if (nøgle == chipnummer)
        //        {
        //            // I stedet for Remove, sæt værdien til null
        //            dyrDictionary[nøgle] = null;
        //            Console.WriteLine($"Dyret med chipnummer {chipnummer} er slettet (markeret som tomt).");
        //            return true;
        //        }
        //    }

        //    // Hvis chipnummeret ikke blev fundet
        //    Console.WriteLine($"Intet dyr fundet med chipnummer {chipnummer}.");
        //    return false;
        //}
        #endregion

        // (while) Slet et dyr, hvis medarbejder har adgang
        public bool SletDyr(int chipnummer, Medarbejder medarbejder)
        {
            // Tjek adgang
            if (!HarAdgang(medarbejder.Id))
            {
                Console.WriteLine("Adgang nægtet.");
                return false;
            }
            // Tjek om chipnummer findes
            if (!dyrDictionary.ContainsKey(chipnummer))
            {
                Console.WriteLine($"Dyr med chipnummer {chipnummer} findes ikke.");
                return false;
            }
            // Hent dyret
            Dyr dyr = dyrDictionary[chipnummer];

            // Brug while-loop til at gennemgå HundeListe
            int i = 0;
            while (i < HundeListe.Count)
            {
                if (HundeListe[i] == dyr)
                {
                    HundeListe[i] = null;
                    break;
                }
                i++;
            }
            // Brug while-loop til at gennemgå KatteListe
            int j = 0;
            while (j < KatteListe.Count)
            {
                if (KatteListe[j] == dyr)
                {
                    KatteListe[j] = null;
                    break;
                }
                j++;
            }
            // Marker dyret som slettet i dictionary
            dyrDictionary[chipnummer] = null;

            Console.WriteLine($"Dyr med chipnummer {chipnummer} er slettet.");
            return true;
        }


        // Rediger info om et eksisterende dyr
        public bool RedigerDyr(int chipnummer, Medarbejder medarbejder, string nytNavn, string nyRace, int nyAlder, string nytHelbred)
        {
            // Tjek om medarbejderen har adgang
            if (!HarAdgang(medarbejder.Id))
            {
                Console.WriteLine("Adgang nægtet.");
                return false;
            }
            // Gå igennem hele dyrDictionary med foreach
            foreach (Dyr dyr in dyrDictionary.Values)
            {
                if (dyr != null && dyr.Chipnummer == chipnummer)
                {
                    dyr.Navn = nytNavn;
                    dyr.Race = nyRace;
                    dyr.Alder = nyAlder;
                    dyr.Helbredstilstand = nytHelbred;

                    Console.WriteLine("Dyrets oplysninger er opdateret.");
                    return true;
                }
            }
            Console.WriteLine("Dyr ikke fundet.");
            return false;
        }


        // Vis kun katte
        public void VisKatte()
        {
            for (int i = 0; i < KatteListe.Count; i++)
            {
                // Kun vis katte der ikke er slettet (null)
                if (KatteListe[i] != null) 
                {
                    Console.WriteLine(KatteListe[i].ToString());
                }
            }
        }

        #region Filtrering på katteliste og hunde liste efter race - skrevet søg, som er forkert
        public List<Kat> SøgKattePåRace(string race)
        {
            List<Kat> fundneKatte = new List<Kat>();

            foreach (var kat in KatteListe)
            {
                if (kat.Race.ToLower().Contains(race.ToLower()))
                {
                    fundneKatte.Add(kat);
                }
            }
            return fundneKatte;
        }

        // Vis kun hunde 
        public void VisHunde()
        {
           int i = 0; 
           while (i < HundeListe.Count)
           {
                if (HundeListe[i] != null)
                {
                    Console.WriteLine(HundeListe[i].ToString());
                }
                i++;
           }
        }

        //Filtrering eller søgning??
        public List<Hund> SøgHundePåRace(string race)
        {
            List<Hund> fundneHunde = new List<Hund>();

            for (int i = 0; i < HundeListe.Count; i++)
            {
                Hund hund = HundeListe[i];
                if (hund != null && hund.Race == race)
                {
                    fundneHunde.Add(hund);
                }
            }
            return fundneHunde;
        }
        #endregion

        public Dyr FindDyrByChipnummer(int chipnummer)
        {
            // Søg i både katte og hunde
            foreach (var kat in KatteListe)
            {
                if (kat.Chipnummer == chipnummer)
                {
                    return kat;
                }
            }
            foreach (var hund in HundeListe)
            {
                if (hund.Chipnummer == chipnummer)
                {
                    return hund;
                }
            }
            // Ikke fundet
            return null; 
        }

        #region Filtrering på dyrets alder

        public Dictionary<int, Dyr> FiltrererIMellemDyretsAlder(int minAlder, int maxAlder)
        {
            // En liste til at gemme dyr der passer inden for aldersgrænsen
            List<Dyr> resulteter = new List<Dyr>();

            // Gå gennem alle dyr i dictionary'en
            foreach (Dyr dyr in dyrDictionary.Values)
            {
                // Tjek at dyret ikke er null (altså ikke slettet), og at dyrets alder er mellem minimum og maksimum
                if (dyr != null && dyr.Alder >= minAlder && dyr.Alder <= maxAlder)
                {
                    resulteter.Add(dyr); // Tilføj til midlertidig resultat-liste
                }
            }
            // Opret en ny dictionary til at returnere resultaterne
            Dictionary<int, Dyr> resultatDictionary = new Dictionary<int, Dyr>();

            // Gå igennem resultat-listen og læg dyrene ind i dictionary'en igen
            foreach (Dyr dyr in resulteter)
            {
                resultatDictionary[dyr.Chipnummer] = dyr; // Chipnummer som nøgle
            }
            // Returnér den færdige dictionary med de filtrerede dyr
            return resultatDictionary;
        }

      
        #endregion

        #region Filtrering på hunde der er og ikke er trænet, og hunde der kan og ikke kan med andre hunde 
        public List<Hund> FiltreringPåHundeDerErTrænet(bool erTrænet)
        {
            List<Hund> filtreredeHunde = new List<Hund>();

            foreach (Hund hund in HundeListe)
            {
                if (hund.ErTrænet == erTrænet)
                {
                    filtreredeHunde.Add(hund);
                }
            }
            return filtreredeHunde;
        }
        

        public List<Hund> FiltreringPåHundeDerIkkeErTrænet(bool erTrænet)
        {
            List<Hund> filtreredeHunde = new List<Hund>();

            foreach (Hund hund in HundeListe)
            {
                if (hund.ErTrænet != erTrænet)
                {
                    filtreredeHunde.Add(hund);
                }
            }
            return filtreredeHunde;
        }

        public List<Hund> FiltreringPåHundeDerKanMedAndreHunde(bool kanMedAndreHunde)
        {
            List<Hund> filtreredeHunde = new List<Hund>();

            foreach (Hund hund in HundeListe)
            {
                if (hund.KanMedAndreHunde == kanMedAndreHunde)
                {
                    filtreredeHunde.Add(hund);
                }
            }
            return filtreredeHunde;
        }

        public List<Hund> FiltreringPåHundeDerIkkeKanMedAndreHunde(bool kanMedAndreHunde)
        {
            List<Hund> filtreredeHunde = new List<Hund>();

            foreach (Hund hund in HundeListe)
            {
                if (hund.KanMedAndreHunde != kanMedAndreHunde)
                {
                    filtreredeHunde.Add(hund);
                }
            }
            return filtreredeHunde;
        }
        #endregion

        #region Filtrering på katte der skal være indekatte, og katte der kan og ikke kan med andre katte 
        public List<Kat> FiltreringPåKatteDerKanMedAndreKatte(bool kanMedAndreKatte)
        {
            List<Kat> filtreredeKatte = new List<Kat>();

            foreach (Kat kat in KatteListe)
            {
                if (kat.KanMedAndreKatte == kanMedAndreKatte)
                {
                    filtreredeKatte.Add(kat);
                }
            }
            return filtreredeKatte;
        }
        public List<Kat> FiltreringPåKatteDerIkkeKanMedAndreKatte(bool kanMedAndreKatte)
        {
            List<Kat> filtreredeKatte = new List<Kat>();

            foreach (Kat kat in KatteListe)
            {
                if (kat.KanMedAndreKatte != kanMedAndreKatte)
                {
                    filtreredeKatte.Add(kat);
                }
            }
            return filtreredeKatte;
        }
        public List<Kat> FiltreringPåKatteDerSkalVæreIndekat(bool SkalVæreIndekat)
        {
            List<Kat> filtreredeKatte = new List<Kat>();

            foreach (Kat kat in KatteListe)
            {
                if (kat.SkalVæreIndekat == SkalVæreIndekat)
                {
                    filtreredeKatte.Add(kat);
                }
            }
            return filtreredeKatte;
        }
        public List<Kat> FiltreringPåKatteDerIkkeSkalVæreIndekat(bool SkalVæreIndekat)
        {
            List<Kat> filtreredeKatte = new List<Kat>();

            foreach (Kat kat in KatteListe)
            {
                if (kat.SkalVæreIndekat != SkalVæreIndekat)
                {
                    filtreredeKatte.Add(kat);
                }
            }
            return filtreredeKatte;
        }
        #endregion
    }
}
  
