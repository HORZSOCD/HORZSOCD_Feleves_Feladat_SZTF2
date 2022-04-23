using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{



    class Naptar
    {
        public delegate void EsemenyKezelo(Teendo teendo);
        public event EsemenyKezelo SikeresHozzaadas;
        public event EsemenyKezelo SikertelenHozzaadas;
       
        public event EsemenyKezelo SikertelenTeendo;
        public event EsemenyKezelo Kozelgoesemeny;

        public delegate void SikeresEsemenyKezelo(Teendo teendo);
        public event SikeresEsemenyKezelo SikeresTeendo;
        
        public DateTime kezdodatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
        private LancoltLista<Teendo> teendoLista = new LancoltLista<Teendo>();
        private int index = 0;
        private Teendo[] teendokTomb;
        private LancoltLista<Teendo> segedLancoltlista = new LancoltLista<Teendo>();

        int segedPrioritas = 1;
        private TimeSpan maxIdokeret = new TimeSpan(8, 0, 0);
        private TimeSpan idokeret = new TimeSpan(0, 0, 0);
        private TimeSpan idokeretBackup;
        private TimeSpan hatralevoIdo;
        bool vege = false;
        private LancoltLista<Teendo> ujraTeendo = new LancoltLista<Teendo>();
        private int sikeresTeendo = 0;

        public void TeendoHozzaAdas(Teendo teendo)
        {
            segedLancoltlista.Add(teendo);
            index = 0;
            teendoLista = new LancoltLista<Teendo>();

            teendokTomb = new Teendo[segedLancoltlista.Count()];
            DateTime aznap = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            foreach (var item in segedLancoltlista)
            {
                teendokTomb[index] = item;
                index++;
            }

            if (teendokTomb.Length > 1)
            {
                TeendoRendezés(ref teendokTomb);
            }
            for (int i = 0; i < teendokTomb.Length; i++)
            {

                if (teendokTomb[i].hatarido > DateTime.Today && teendokTomb[i].hatarido <= aznap)
                {
                    teendoLista.Add(teendokTomb[i]);

                }

            }
            segedPrioritas = 1;
            idokeret = new TimeSpan(0, 0, 0);
            hatralevoIdo = maxIdokeret - idokeret;
            foreach (var item in teendoLista)
            {
                idokeretBackup = idokeret;
                idokeret += item.idoTartam;
                if (idokeret > maxIdokeret)
                {
                    idokeret = idokeretBackup;
                    SikertelenHozzaadas?.Invoke(teendo);
                    TeendoTorles(teendo);
                    teendo.torolte = true;
                    Console.WriteLine($"Nem tudjuk felvenni a következő tennivalót mert túl menne az időkereten: '{teendo.Teendo_megnevezes}'");
                }
                item.prioritas = segedPrioritas;
                segedPrioritas++;
                hatralevoIdo = maxIdokeret - idokeret;
            }
            if (teendo.hatarido > DateTime.Today && teendo.hatarido <= aznap && teendo.torolte == false)
            {
                SikeresHozzaadas?.Invoke(teendo);

            }
            else
            {
                if (teendo.elnapolva == false && teendo.torolte == false)
                {
                    SikertelenHozzaadas?.Invoke(teendo);
                }


            }
        }

        public void TeendoTorles(Teendo teendo)
        {
            teendoLista.Remove(teendo);
            segedLancoltlista.Remove(teendo);

        }

        private void TeendoRendezés(ref Teendo[] teendokTomb)
        {
            for (int i = 0; i < teendokTomb.Length - 1; i++)
            {
                for (int j = i + 1; j < teendokTomb.Length; j++)
                {
                    if (teendokTomb[i].hatarido < teendokTomb[j].hatarido)
                    {
                        Teendo tmp = teendokTomb[i];
                        teendokTomb[i] = teendokTomb[j];
                        teendokTomb[j] = tmp;
                    }
                }

            }

        }
        public void Bejaras(Naptar naptar)
        {
            Sorrend(naptar, idokeret);
            IdoElosztas(naptar);
            TeendoElvegzes(naptar);
            Console.WriteLine($"A nap folyamán {sikeresTeendo} sikeres teendőt végzett el.");

        }

        private void TeendoElvegzes(Naptar naptar)
        {
            
            foreach (var tennivalo in naptar.teendoLista)
            {
                Kozelgoesemeny?.Invoke(tennivalo);
                Checking(naptar, tennivalo);
            }
            if (vege)
            {
                vege = false;
                foreach (var tennivalo in naptar.ujraTeendo)
                {
                    Kozelgoesemeny?.Invoke(tennivalo);
                    tennivalo.befejezes = tennivalo.kezdes + tennivalo.idoTartam;
                    Checking(naptar, tennivalo);
                }
                
            }
        }

        private void Checking(Naptar naptar,Teendo tennivalo)
        {
            
            string valasz = string.Empty;
            if (tennivalo.kezdes<tennivalo.hatarido && tennivalo.befejezes<= tennivalo.hatarido)
            {
                if (tennivalo is EgyszeriTeendo)
                {

                    Console.WriteLine($"Sikerült teljesíteni ezt: {tennivalo.Teendo_megnevezes}?");
                    do
                    {
                        Console.WriteLine("Kérem válaszoljon: 'igen', 'nem'!");
                        valasz = Console.ReadLine();

                    } while ((valasz != "igen") && (valasz != "nem"));

                    if (valasz == "nem")
                    {
                        SikertelenTeendo?.Invoke(tennivalo);
                        naptar.TeendoTorles(tennivalo);
                        if (hatralevoIdo>=tennivalo.idoTartam)
                        {
                            tennivalo.hatarido = tennivalo.aznapiHatarido;
                            tennivalo.kezdes = kezdodatum;
                            naptar.TeendoHozzaAdas(tennivalo);
                            ujraTeendo.Add(tennivalo);
                            vege = true; 
                            
                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Sajnos el kell napolni a teendőt, mivel ma már nem fér bele\n\n");
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        
                        
                    }
                    else
                    {
                        SikeresTeendo?.Invoke(tennivalo);
                        SikeresteendoElvegezve();
                        Console.WriteLine();
                    }
                }
                else
                {
                    SikeresTeendo?.Invoke(tennivalo);
                    SikeresteendoElvegezve();
                    Console.WriteLine();
                }
                
            }
            else
            {
                SikertelenTeendo?.Invoke(tennivalo);
                
            }
           
        }

        private void SikeresteendoElvegezve()
        {
            sikeresTeendo++;
        }
        private void IdoElosztas(Naptar naptar)
        {
            
            foreach (var tennivalo in naptar.teendoLista)
            {
                tennivalo.kezdes = kezdodatum;
                tennivalo.befejezes = kezdodatum + tennivalo.idoTartam;
                if (tennivalo.kezdes < tennivalo.hatarido && tennivalo.befejezes <= tennivalo.hatarido)
                {
                    
                    kezdodatum = tennivalo.befejezes;
                }
                else
                {
                    kezdodatum = tennivalo.kezdes;
                }
                    
            }
        }

        private void Sorrend(Naptar naptar,TimeSpan idokeret)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int i = 0;
            Console.WriteLine("\nA mai teendők sorrendje:\n");
            foreach (var item in naptar.teendoLista)
            {
                
                Console.WriteLine($"{i + 1} '{item.Teendo_megnevezes}', szükséges idő: {item.idoTartam}");
                i++;
                
                
            }
            Console.WriteLine($"\nA mai tevékenységekhez szükséges időtartam: {idokeret}");
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;


        }

        
        


    }
}
