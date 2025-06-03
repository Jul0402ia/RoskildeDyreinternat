using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public interface IBrugerRepo
    {
        void OpretKunde(Kunde kunde);
        void OpretMedarbejder(Medarbejder medarbejder);
        void OpdaterKundeInfo(int id, string navn, string email, string telefon, string adresse);
        void SletKunde(int id);
        void VisBrugerRolle(int id);
        void VisBrugerInfo(int id);
    }
}
