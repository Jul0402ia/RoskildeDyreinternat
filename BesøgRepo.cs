using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public class BesøgRepo
    {
        public List<Besøg> _besøgListe = new List<Besøg>();
        BesøgRepo besøgRepo = new BesøgRepo();


        // Book et besøg og tjek om dyret allerede er booket samme dag
        public bool BookBesøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            if (kunde == null || dyr == null)
                return false;

            bool erBooket = _besøgListe.Any(b =>
                b.Dyr.Chipnummer == dyr.Chipnummer &&
                b.Dato.Date == dato.Date);

            if (erBooket)
            {
                Console.WriteLine($"Dyret {dyr.Navn} er allerede booket den {dato.Date.ToShortDateString()}.");
                return false;
            }

            Besøg nytBesøg = new Besøg(dato, kunde, dyr);
            _besøgListe.Add(nytBesøg);
            Console.WriteLine($"Besøg #{nytBesøg.Besøgsnummer} booket til {dato}.");
            return true;
        }

        // Returner alle besøg for en kunde
        public List<Besøg> HentBesøgForKunde(Kunde kunde)
        {
            if (kunde == null)
                return new List<Besøg>();

            return _besøgListe.Where(b => b.Kunde == kunde)
                              .OrderBy(b => b.Dato)
                              .ToList();
        }

        // Returner alle besøg for et dyr via chipnummer
        public List<Besøg> HentBesøgForDyr(int chipnummer)
        {
            return _besøgListe.Where(b => b.Dyr.Chipnummer == chipnummer)
                              .OrderBy(b => b.Dato)
                              .ToList();
        }

        //kunden kan selv slette et besøg, eller en medarbejder kan gøre det ved evt sygdom
        public bool SletBesøg(int besøgId)
        {
            var besøg = _besøgListe.FirstOrDefault(b => b.Besøgsnummer == besøgId);
            if (besøg != null)
            {
                _besøgListe.Remove(besøg);
                return true;
            }
            return false;
        }

        //Meotden bliver brugt af medarbejder til at slette alle besøg på en kunde hvis konto skal slettes
            public void SletBesøgForKunde(int kundeId)
        {
            _besøgListe.RemoveAll(b => b.Kunde.Id == kundeId);
        }
        
        // Udskriv alle besøg (fx til oversigt eller debugging)
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