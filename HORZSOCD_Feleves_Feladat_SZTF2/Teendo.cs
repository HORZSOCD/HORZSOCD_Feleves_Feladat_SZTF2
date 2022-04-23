using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
   
    abstract class Teendo:ITeendo
    {
        
        public string Teendo_megnevezes;
        public TimeSpan idoTartam { get; set; }
        public int prioritas { get; set; }
        public DateTime befejezes { get; set; }
        public DateTime kezdes { get; set; }
        public DateTime aznapiHatarido= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16,0, 0);
        public DateTime hatarido { get; set; }  
        public int renszeresseg { get; set; }
        public bool elnapolva { get; set; }
        public bool torolte { get; set; } 




        public Teendo(string Teendo_megnevezes, TimeSpan idoTartam , int rendszeresseg= 1)
        {
            this.Teendo_megnevezes = Teendo_megnevezes;
            this.idoTartam = idoTartam;
            this.befejezes = this.kezdes+idoTartam;
            Hatarido(aznapiHatarido);
        }

        public virtual void Hatarido(DateTime hatarido)
        {
            this.hatarido = hatarido;
        }
    }

}
