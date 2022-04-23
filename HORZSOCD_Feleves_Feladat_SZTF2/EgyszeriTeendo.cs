using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
    class EgyszeriTeendo : Teendo
    {
       

        public EgyszeriTeendo(string Teendo_megnevezes, TimeSpan idoTartam,DateTime hatarido) : base(Teendo_megnevezes, idoTartam)
        {
            Hatarido(hatarido);
        }
        public override void Hatarido(DateTime hatarido)
        {
            this.hatarido = hatarido;
        }
    }
}
