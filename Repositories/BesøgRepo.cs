using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoskildeDyreinternat.UI;

namespace RoskildeDyreinternat.Repositories
{
    public class BesøgRepo : IAdgangsKontrol
    {
        public List<Besøg> _besøgListe = new List<Besøg>();

        // Reference til BrugerRepo
        private BrugerRepo brugerRepo;

        public BesøgRepo(BrugerRepo brugerRepo)
        {
            this.brugerRepo = brugerRepo;
        }

        // Kun medarbejdere med arbejdstimer > 0 har adgang
        public bool HarAdgang(int brugerId)
        {
            foreach (Medarbejder medarbejder in brugerRepo.medarbejderListe.Values)
            {
                if (medarbejder.Id == brugerId && medarbejder.Antalarbejdstimer > 0)
                {
                    return true; // Medarbejderen har adgang
                }
            }
            Console.WriteLine($"Adgang nægtet for bruger med ID {brugerId}. Kun medarbejdere med arbejdstimer > 0 har adgang.");
            return false; // Ingen adgang
        }

        public bool BookBesøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            try
            {
                if (kunde == null)
                {
                    Console.WriteLine("Fejl: Der mangler en kunde.");
                    return false;
                }

                if (dyr == null)
                {
                    Console.WriteLine("Fejl: Der mangler et dyr.");
                    return false;
                }

                foreach (Besøg besøg in _besøgListe)
                {
                    if (besøg != null && besøg.Dyr.Chipnummer == dyr.Chipnummer && besøg.Dato.Date == dato.Date)
                    {
                        Console.WriteLine($"{dyr.Navn} er allerede booket den {dato.ToShortDateString()}.");
                        return false;
                    }
                }

                // Opret og tilføj besøg
                Besøg nytBesøg = new Besøg(dato, kunde, dyr);
                _besøgListe.Add(nytBesøg);

                Console.WriteLine($"Besøg #{nytBesøg.Besøgsnummer} er booket til {dato}.");

                // Dyret siger tak
                Console.WriteLine($"{dyr.Navn} siger: Tak! {dyr.LavLyd()}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Uventet fejl] {ex.Message}");
                return false;
            }
        }
        //BookBesøg metode, stadig med try catch men tilføj throw 
       

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
           
            return kundensBesøg;
        }

        public List<Besøg> HentBesøgForDyr(int chipnummer)
        {
            List<Besøg> dyretsBesøg = new List<Besøg>();

            foreach (Besøg besøg in _besøgListe)
            {
                if (besøg != null && besøg.Dyr.Chipnummer == chipnummer)
                {
                    dyretsBesøg.Add(besøg);
                }
            }
            return dyretsBesøg;
        }

        public bool SletBesøg(int besøgId)
        {
            for (int i = 0; i < _besøgListe.Count; i++)
            {
                if (_besøgListe[i] != null && _besøgListe[i].Besøgsnummer == besøgId)
                {
                    //"slet" ved at gøre pladsen tom
                    _besøgListe[i] = null; 
                    Console.WriteLine($"Besøg med ID {besøgId} er slettet.");
                    return true;
                }
            }

            Console.WriteLine($"Besøg med ID {besøgId} blev ikke fundet.");
            return false;
        }

        //Slet besøg for en kunde, kun som en medarbejder på + 0 timer
        //public void SletBesøgForKunde(int kundeId, int medarbejderId)
        //{
        //    // Tjekker om medarbejderen har adgang
        //    if (!HarAdgang(medarbejderId))
        //    {
        //        Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette besøg.");
        //        return;
        //    }

        //    // Gennemgår alle besøg og "sletter" dem, hvor kundens ID matcher
        //    for (int i = 0; i < _besøgListe.Count; i++)
        //    {
        //        // Tjekker at pladsen ikke allerede er tom
        //        if (_besøgListe[i] != null && _besøgListe[i].Kunde.Id == kundeId)
        //        {
        //            _besøgListe[i] = null; // marker som slettet ved at sætte til null
        //        }
        //    }

        //    // Informér brugeren
        //    Console.WriteLine($"Alle besøg for kunde med ID {kundeId} er slettet (markeret som tomme).");
        //}
        public void SletBesøgForKunde(int kundeId, int medarbejderId)
        {
            if (!HarAdgang(medarbejderId))
            {
                Console.WriteLine("Adgang nægtet. Kun medarbejdere med over 0 arbejdstimer kan slette besøg.");
                return;
            }

            int antalSlettet = 0;

            for (int i = 0; i < _besøgListe.Count; i++)
            {
                if (_besøgListe[i] != null && _besøgListe[i].Kunde.Id == kundeId)
                {
                    _besøgListe[i] = null;
                    antalSlettet++;
                }
            }

            Console.WriteLine($"{antalSlettet} besøg for kunde med ID {kundeId} er slettet.");
        }

        // Overskriver standard ToString(), så objektet vises som tekst på en bestemt måde
        public override string ToString()
        {
            string tekst = "";

            foreach (var besøg in _besøgListe)
            {
                //Spring over hvis besøg er null (slettet)
                if (besøg != null)
                {
                    tekst += $"Besøg #{besøg.Besøgsnummer} - {besøg.Dato}\n";
                    tekst += $"Kunde: {besøg.Kunde.Navn}\n";
                    tekst += $"Dyr: {besøg.Dyr.Navn} (Chipnummer: {besøg.Dyr.Chipnummer})\n";
                    tekst += "\n";
                }
            }
            if (tekst == "")
                return "Der er ingen besøg at vise.";

            return tekst;
        }

        public List<Besøg> SøgBesøgVedNummer(int søgeNummer)
        {
            List<Besøg> result = new List<Besøg>();

            foreach (Besøg besøg in _besøgListe)
            {
                // Tjek om besøg er slettet (null)
                if (besøg != null && besøg.Besøgsnummer == søgeNummer)
                {
                    result.Add(besøg);
                }
            }
            return result;
        }

        #region Filtrering på besøg ud fra dyrets chipnummer og selv vælgte datoer 
        public List<Besøg> FiltrerBesøgPåChipnummerOgDato(int chipnummer, DateTime fraDato, DateTime tilDato)
        {
            List<Besøg> filtreredeBesøg = new List<Besøg>();

            foreach (var besøg in _besøgListe)
            {
                if (besøg != null &&
                    besøg.Dyr != null &&
                    besøg.Dyr.Chipnummer == chipnummer &&
                    besøg.Dato.Date >= fraDato.Date &&
                    besøg.Dato.Date <= tilDato.Date)
                {
                    filtreredeBesøg.Add(besøg);
                }
            }

            return filtreredeBesøg;
        }

        #endregion
    }
}