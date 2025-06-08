using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeDyreinternat
{
    public interface IAdgangsKontrol
    {
        bool HarAdgang(int medarbejderId);
    }
}
