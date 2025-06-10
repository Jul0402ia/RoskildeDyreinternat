using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoskildeDyreinternat.Repositories
{
    public class DyrRepo
    {
        public Dictionary<int, Dyr> dyrDictionary = new Dictionary<int, Dyr>();

        List<Hund> HundeListe = new List<Hund>();
        List<Kat> KatteListe = new List<Kat>();


    private readonly IAdgangsKontrol _adgangsKontrol;
        public DyrRepo(IAdgangsKontrol adgangsKontrol)
        {
            _adgangsKontrol = adgangsKontrol;
        }

        // Tilføj hund(if), hvis medarbejder har adgang og chipnummer ikke findes i forvejen
        public bool AddHund(Hund hund, int medarbejderId)
        {
            if (!_adgangsKontrol.HarAdgang(medarbejderId))
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
            if (!_adgangsKontrol.HarAdgang(medarbejder.Id))
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
            if (!_adgangsKontrol.HarAdgang(medarbejder.Id))
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
            if (!_adgangsKontrol.HarAdgang(medarbejder.Id))
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

        #region Søgning på katteliste og hunde liste efter race 
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
            foreach (var hund in HundeListe)
            {
                Console.WriteLine(hund.ToString());
            }
        }

        public List<Hund> SøgHundePåRace(string race)
        {
            List<Hund> fudneHund = new List<Hund>();
            foreach (var hund in HundeListe)
            {
                if (hund.Race.ToLower().Contains(race.ToLower()))
                {
                    fudneHund.Add(hund);
                }
            }
            return fudneHund;
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

            return null; // Ikke fundet
        }

        #region Filtrering på dyrets alder
        public List<Dyr> FiltrererIMellemDyretsAlder(int minAlder, int maxAlder)
        {
            List<Dyr> resultat = new List<Dyr>();

            foreach (Dyr dyr in dyrDictionary.Values)
            {
                // Tjek om det er Kat eller Hund, og om alder ligger mellem min og max
                if ((dyr is Kat && dyr.Alder >= minAlder && dyr.Alder <= maxAlder) ||
                    (dyr is Hund && dyr.Alder >= minAlder && dyr.Alder <= maxAlder))
                {
                    resultat.Add(dyr);
                }
            }

            return resultat;
        }
        #endregion

        #region Filtrering på hunde der er og ikke er trænet, og hunde der kan og ikke kan med andre hunde 
        public List<Hund> FiltreringPåHundeDerErTrænet(bool erTrænet)
        {
            List<Hund> filtreredeHunde = new List<Hund>();

            foreach (var hund in HundeListe)
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

            foreach (var hund in HundeListe)
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

            foreach (var hund in HundeListe)
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

            foreach (var hund in HundeListe)
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

            foreach (var kat in KatteListe)
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

            foreach (var kat in KatteListe)
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

            foreach (var kat in KatteListe)
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

            foreach (var kat in KatteListe)
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
  
