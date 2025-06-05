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

        public bool AddHund(Hund hund)
        {
            if (hund != null && !HundeListe.Any(h => h.GetChipnummer() == hund.GetChipnummer()))
            {
                HundeListe.Add(hund);
                return true;
            }

            Console.WriteLine("Hunden findes allerede.");
            return false;
        }

        public bool AddKat(Kat kat)
        {
            if (kat == null) return false;

            int i = 0;
            while (i < KatteListe.Count)
            {
                if (KatteListe[i].Chipnummer == kat.Chipnummer)
                {
                    return false;
                }
                i++;
            }

            KatteListe.Add(kat);
            return true;
        }

        public bool SletDyr(int chipnummer)
        {
            return dyrDictionary.Remove(chipnummer);
        }
    }
}
