using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Fut();
        }

        
        
        
        private static void NaptarManagement(Naptar naptar)
        {


            naptar.SikeresHozzaadas += SikeresHozzaadas;
            naptar.SikertelenHozzaadas += SikertelenHozzaadas;
            naptar.SikeresTeendo += SikeresTeendo;
            naptar.SikertelenTeendo += SikertelenTeendo;
            naptar.Kozelgoesemeny += KozelgoTeendo;
            

            Teendo mindennapiTeendo1 = new MindennapiTeendo("Napi posta áttekintés", new TimeSpan(4, 30, 0));
            Teendo egyszeriTeendo1 = new EgyszeriTeendo("Törvényjavaslat előkészítés", new TimeSpan(0, 25, 0), new DateTime(2021, 12, 5,12,0,0));
            Teendo visszateroTeendo1 = new VisszateroTeendo("Média megjelenés", new TimeSpan(5, 40, 0), 2);
            Teendo visszateroTeendo2 = new VisszateroTeendo("Videó vágás", new TimeSpan(1, 10, 0), 2);
            Teendo egyszeriTeendo2 = new EgyszeriTeendo("Sajtó beszéd", new TimeSpan(0, 25, 0), new DateTime(2021, 12, 5, 12, 0, 0));
            Teendo visszateroTeendo3 = new VisszateroTeendo("Tárgyalás", new TimeSpan(1, 46, 0), 2);
            Teendo mindennapiTeendo2 = new MindennapiTeendo("Rendrakás", new TimeSpan(1, 30, 0));


            naptar.TeendoHozzaAdas(mindennapiTeendo1);
            naptar.TeendoHozzaAdas(egyszeriTeendo1);
            naptar.TeendoHozzaAdas(visszateroTeendo1);
            naptar.TeendoHozzaAdas(visszateroTeendo2);
            naptar.TeendoHozzaAdas(egyszeriTeendo2);
            naptar.TeendoHozzaAdas(visszateroTeendo3);
            naptar.TeendoHozzaAdas(mindennapiTeendo2);



        }

        public static void Fut()
        {
            
            Naptar naptar = new Naptar();
            try
            {
                NaptarManagement(naptar);
                

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            naptar.Bejaras(naptar);
            Console.ReadKey();
            
        }
        

        static void SikeresHozzaadas(Teendo teendo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sikeresen hozzáadva ezt a teendőt: '{teendo.Teendo_megnevezes}', határidő: {teendo.hatarido}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void SikertelenHozzaadas(Teendo teendo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Nem sikerült hozzáadni ezt a teendőt: '{teendo.Teendo_megnevezes}'. Kérjük adjon meg egy másik időpontot\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        

        static void SikeresTeendo(Teendo teendo)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sikeresen elvégezhető a következő teendő:'{teendo.Teendo_megnevezes}', eddig: {teendo.befejezes}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void SikertelenTeendo(Teendo teendo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Nem sikerült elévgezni a következő teendőt: {teendo.Teendo_megnevezes}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void KozelgoTeendo(Teendo teendo)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"A következo eseménye lesz: '{teendo.Teendo_megnevezes}', ekkor: {teendo.kezdes}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
