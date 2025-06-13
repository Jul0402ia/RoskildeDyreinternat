using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return brugerRepo.medarbejderListe.TryGetValue(brugerId, out Medarbejder medarbejder)
                   && medarbejder.Antalarbejdstimer > 0;
        }


        public bool BookBesøg(DateTime dato, Kunde kunde, Dyr dyr)
        {
            try
            {
                // Tjek om der mangler kunde eller dyr
                if (kunde == null)
                    throw new ArgumentNullException(nameof(kunde), "Der mangler en kunde.");

                if (dyr == null)
                    throw new ArgumentNullException(nameof(dyr), "Der mangler et dyr.");

                // Tjek om dyret allerede er booket samme dag
                foreach (Besøg besøg in _besøgListe)
                {
                    if (besøg.Dyr.Chipnummer == dyr.Chipnummer && besøg.Dato.Date == dato.Date)
                        throw new InvalidOperationException($"{dyr.Navn} er allerede booket den {dato.ToShortDateString()}.");
                }

                // Opret og gem besøget
                Besøg nytBesøg = new Besøg(dato, kunde, dyr);
                _besøgListe.Add(nytBesøg);

                Console.WriteLine($"Besøg #{nytBesøg.Besøgsnummer} er booket til {dato}.");
                return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"[Fejl] {ex.ParamName}: {ex.Message}");
                return false;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[Fejl] {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Uventet fejl] {ex.Message}");
                return false;
            }
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
            if (!HarAdgang(medarbejderId))
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
        public List<Besøg> SøgBesøgVedNummer(int søgeNummer)
        {
            List<Besøg> result = new List<Besøg>();

            foreach (Besøg besøg in _besøgListe)
            {
                if (besøg.Besøgsnummer == søgeNummer)
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
                // Tjek om chipnummer matcher OG datoen ligger inden for intervallet
                if (besøg.Dyr.Chipnummer == chipnummer &&
                    besøg.Dato.Date >= fraDato.Date &&
                    besøg.Dato.Date <= tilDato.Date)
                {
                    filtreredeBesøg.Add(besøg);
                }
            }

            // Sorter resultaterne efter dato (valgfrit, men ofte nyttigt)
            filtreredeBesøg.Sort((b1, b2) => b1.Dato.CompareTo(b2.Dato));

            return filtreredeBesøg;
        }
#endregion
    }
}