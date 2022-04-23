using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
    class VisszateroTeendo : Teendo
    {
        
        public int hetiRendszeresseg { get; set; }

        public VisszateroTeendo(string Teendo_megnevezes, TimeSpan idoTartam, int hetiRendszeresseg ) : base(Teendo_megnevezes, idoTartam)
        {
            this.hetiRendszeresseg = hetiRendszeresseg;
        }
       
    }
}
