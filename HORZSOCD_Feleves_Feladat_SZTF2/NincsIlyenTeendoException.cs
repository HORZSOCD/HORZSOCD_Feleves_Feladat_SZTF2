using System;
using System.Runtime.Serialization;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
    
    public class NincsIlyenTeendoException : Exception
    {
        
        public NincsIlyenTeendoException(string message) : base(message)
        {
        }

    }
}