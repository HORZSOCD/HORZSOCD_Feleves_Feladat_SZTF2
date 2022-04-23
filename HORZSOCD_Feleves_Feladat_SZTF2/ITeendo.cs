using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
    interface ITeendo
    {
        TimeSpan idoTartam { get; set; }
        int prioritas { get; set; }
    }
}
