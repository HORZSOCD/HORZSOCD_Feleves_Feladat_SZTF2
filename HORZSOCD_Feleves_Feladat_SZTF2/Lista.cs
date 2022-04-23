using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{

    class Lista<T> : IEnumerable<T>
    {
        class ListaElem
        {
            public T tartalom;
            public ListaElem kovetkezo;
        }


        /* a "bejárást elvégző" objektum osztálya */
        class ListaBejaro : IEnumerator<T>
        {
            ListaElem elso;         // elmentünk egy hivatkozást a lista első elemére

            ListaElem aktualis;     // ezzel mutatunk mindig a lista aktuális elemére a bejárás során

            public ListaBejaro(ListaElem elso)
            {
                this.elso = elso;
                // az aktualis mutató a bejárás megkezdése előtt az első elem "elé" mutat
                aktualis = new ListaElem();
                aktualis.kovetkezo = elso;
            }

            // az aktuális elem tartalmi részét adja vissza (a foreach ciklusváltozója ennek az értékét veszi fel)
            // megjegyzés: foreach-en belül ezért nem módosíthatók egy gyűjtemény elemei, mert a tulajdonság csak olvasható
            public T Current { get { return aktualis.tartalom; } }

            object IEnumerator.Current { get { return this.Current; } }

            // erőforrások felszabadítására szolgál
            // a foreach a bejárás végeztével automatikusan meghívja
            // esetünkben hagyhatnánk üresen is (foreach-nél a bejárás végeztével úgyis megszűnik a ListaBejaro objektum)
            public void Dispose()
            {
                elso = null;
                aktualis = null;
            }

            // lépteti az "aktualis" hivatkozást a következő listaelemre, és igazat vagy hamisat ad vissza attól függően, hogy listaelemre lépett-e vagy nem (lista vége, vagy üres volt a lista)
            public bool MoveNext()
            {
                if (aktualis == null)
                {
                    return false;
                }

                aktualis = aktualis.kovetkezo;
                return aktualis != null;
            }

            // alaphelyzetbe állítja a bejárót
            public void Reset()
            {
                aktualis = new ListaElem();
                aktualis.kovetkezo = elso;
            }
        }


        private ListaElem fej;

        public Lista()
        {
            fej = null;
        }

        public void Add(T tartalom)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = tartalom;
            uj.kovetkezo = fej;
            fej = uj;
        }
        public void Clear(string torlendo)
        {
            ListaElem p = fej;
            ListaElem e = null;
            while (p != null && !p.tartalom.Equals(torlendo))
            {
                e = p;
                p = p.kovetkezo;
            }
            if (p != null)
            {
                if (e == null)                      // megvan (első elem)
                    fej = p.kovetkezo;
                else                                // megvan (többi elem között)
                    e.kovetkezo = p.kovetkezo;
            }
            else                                    // nem talált VAGY üres lista
            {
                throw new NincsIlyenTeendoException($"Nincs ilyen elem ({torlendo}) a listában.");
            }
        }

        public int Count()
        {
            int db = 0;

            ListaElem p = fej;
            while (p != null)
            {
                db++;
                p = p.kovetkezo;                   
            }
            return db;
        }


        // létrehoz és visszaad egy IEnumerator<T> interfészt megvalósító, bejáró objektumot
        public IEnumerator<T> GetEnumerator()
        {
            return new ListaBejaro(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
