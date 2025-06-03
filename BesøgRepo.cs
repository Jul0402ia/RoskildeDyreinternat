using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class BesøgRepo
    {
        List<Besøg> _besøgListe = new List<Besøg>();

        //tilføjer et besøg til besoglisten
        public bool AddBesøg(Besøg besøg)
        {
            if (besøg != null && !_besøgListe.Contains(besøg))
            {
                _besøgListe.Add(besøg);
                return true;
            }
            return false;
        }

        //den returnerer en liste af alle besøg tilknyttet den givne kunde.

        public List<Besøg> HentBesøgForKunde(Kunde kunde)
        {
            return _besøgListe.Where(b => b.Kunde == kunde).ToList();
        }

        //StringBuilder som viser info igennem foreach
        public string ToString()
        {
            string sb = "";
            foreach (var besøg in _besøgListe)
            {
                sb += ($"Besøg #{besøg.Besøgsnummer} - {besøg.Dato} ");
                sb += ($"Kunde: {besøg.Kunde.Navn} ");
                sb += ($"Dyr: {besøg.Dyr.Navn}");
                sb += ("\n");
            }
            return sb.ToString();
        }


    }
}
