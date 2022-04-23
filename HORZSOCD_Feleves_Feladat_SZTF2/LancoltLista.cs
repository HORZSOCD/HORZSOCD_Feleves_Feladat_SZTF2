using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HORZSOCD_Feleves_Feladat_SZTF2
{

    class LancoltLista<T> : IEnumerable<T> where T:ITeendo
    {
        class ListaElem
        {
            public T Tartalom;
            public ListaElem Kovetkezo;
        }
        ListaElem fej;

        class Listabejaro : IEnumerator<T>
        {
            ListaElem elso;
            ListaElem aktualis;

            public Listabejaro(ListaElem elso)
            {
                this.elso = elso;
                aktualis = new ListaElem();
                aktualis.Kovetkezo = elso;
            }

            public T Current
            {
                get { return aktualis.Tartalom; }
            }

            object IEnumerator.Current { get { return this.Current; } }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (aktualis == null)
                {
                    return false;
                }
                aktualis = aktualis.Kovetkezo;
                return aktualis != null;
            }

            public void Reset()
            {
                aktualis = new ListaElem();
                aktualis.Kovetkezo = elso;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Listabejaro(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Listabejaro(fej);
        }

        public void Add(T tartalom)
        {
            ListaElem uj = new ListaElem();
            uj.Tartalom = tartalom;
            uj.Kovetkezo = fej;
            fej = uj;
        }

        public void Remove(T tartalom)
        {
            ListaElem p = fej;
            ListaElem e = null;
            while (p != null && !p.Tartalom.Equals(tartalom))
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                if (e == null)                      
                    fej = p.Kovetkezo;
                else                                
                    e.Kovetkezo = p.Kovetkezo;
            }
            
        }
        public int Count()
        {
            int db = 0;

            ListaElem p = fej;
            while (p != null)
            {
                db++;
                p = p.Kovetkezo;
            }
            return db;
        }

    }
}
    




    
    

