using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public interface IBrugerRepo
    {
        public void OpretKunde(Kunde kunde);
        public void OpretMedarbejder(Medarbejder medarbejder);
        public void OpdaterKundeInfo(int id, string navn, string email, string telefon, string adresse);
        public void SletKunde(int kundeId, int medarbejderId, bool sletBesøg);
        public void VisBrugerRolle(int id);
        public void VisBrugerInfo(int brugerId, int medarbejderId);


    }
}
