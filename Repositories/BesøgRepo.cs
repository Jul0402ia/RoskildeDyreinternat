using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat.Repositories
{
    public class BesøgRepo
    {
        private List<Besøg> _besøgListe = new List<Besøg>();
        // har fået anb. at bruge private readonly, for at beskytte data, men har ikke lært om det endnu
        public IAdgangsKontrol _adgangsKontrol;

        // Constructor: modtager adgangskontrol (BrugerRepo)
        public BesøgRepo(IAdgangsKontrol adgangsKontrol)
        {
            _adgangsKontrol = adgangsKontrol;
        }


        public bool BookBesøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            // Tjek om der mangler kunde eller dyr
            if (kunde == null)
            {
                Console.WriteLine("Der mangler en kunde.");
                return false;
            }

            if (dyr == null)
            {
                Console.WriteLine("Der mangler et dyr.");
                return false;
            }

            // Tjek om dyret allerede er booket samme dag
            foreach (Besøg besøg in _besøgListe)
            {
                if (besøg.Dyr.Chipnummer == dyr.Chipnummer && besøg.Dato.Date == dato.Date)
                {
                    Console.WriteLine($"{dyr.Navn} er allerede booket den {dato.ToShortDateString()}.");
                    return false;
                }
            }

            // Opret og gem besøget
            Besøg nytBesøg = new Besøg(dato, kunde, dyr);
            _besøgListe.Add(nytBesøg);

            Console.WriteLine($"Besøg #{nytBesøg.Besøgsnummer} er booket til {dato}.");
            return true;
        }


        public List<Besøg> HentBesøgForKunde(Kunde kunde)
        {
            List<Besøg> kundensBesøg = new List<Besøg>();

            if (kunde == null)
            {
                return kundensBesøg;
            }

            foreach (Besøg besøg in _besøgListe)
            {
                if (besøg.Kunde == kunde)
                {
                    kundensBesøg.Add(besøg);
                }
            }

            // Sorter besøgene efter dato
            kundensBesøg.Sort((b1, b2) => b1.Dato.CompareTo(b2.Dato));

            return kundensBesøg;
        }


        public List<Besøg> HentBesøgForDyr(int chipnummer)
        {
            List<Besøg> dyretsBesøg = new List<Besøg>();

            foreach (Besøg besøg in _besøgListe)
            {
                if (besøg.Dyr.Chipnummer == chipnummer)
                {
                    dyretsBesøg.Add(besøg);
                }
            }

            // Sorter besøgene efter dato
            dyretsBesøg.Sort((b1, b2) => b1.Dato.CompareTo(b2.Dato));

            return dyretsBesøg;
        }


        public bool SletBesøg(int besøgId)
        {
            // Gå gennem alle besøg i listen
            foreach (Besøg besøg in _besøgListe)
            {
                // Tjek om besøgets ID matcher 
                if (besøg.Besøgsnummer == besøgId)
                {
                    // Fjern besøget fra listen
                    _besøgListe.Remove(besøg);
                    // Besøget fundet og slettet
                    return true; 
                }
            }

            // Besøget ikke fundet 
            Console.WriteLine($"Besøg med ID {besøgId} blev ikke fundet.");
            return false;
        }


        //Slet besøg for en kunde, kun som en medarbejder på + 0 timer
        public void SletBesøgForKunde(int kundeId, int medarbejderId)
        {
            if (!_adgangsKontrol.HarAdgang(medarbejderId))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette besøg.");
                return;
            }

            _besøgListe.RemoveAll(b => b.Kunde.Id == kundeId);
            Console.WriteLine($"Alle besøg for kunde med ID {kundeId} er slettet.");
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var besøg in _besøgListe.OrderBy(b => b.Dato))
            {
                sb.AppendLine($"Besøg #{besøg.Besøgsnummer} - {besøg.Dato}");
                sb.AppendLine($"Kunde: {besøg.Kunde.Navn}");
                sb.AppendLine($"Dyr: {besøg.Dyr.Navn} (Chipnummer: {besøg.Dyr.Chipnummer})");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}