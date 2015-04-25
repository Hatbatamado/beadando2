using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class LancoltLista<T> : IEnumerable<T> where T : IFeladat
    {
        class ListaElem
        {
            public T tartalom;
            public ListaElem kovetkezo;
        }

        ListaElem fej = null;

        /// <summary>
        /// Új elem beszúrása prioritás szerint megfelelő helyre
        /// </summary>
        /// <param name="elem"></param>
        public void BeszurasPrioritasSzerint(T elem)
        {
            ListaElem aktualis = fej;
            ListaElem elozo = null;
            ListaElem kov = null;
            while (aktualis != null && aktualis.tartalom.Prioritas < elem.Prioritas)
            {
                elozo = aktualis;
                aktualis = aktualis.kovetkezo;
            }
            if (aktualis != null)
                kov = aktualis;

            ListaElem uj = new ListaElem();
            uj.tartalom = elem;
            uj.kovetkezo = kov;

            if (elozo != null)
                elozo.kovetkezo = uj;
            else
                fej = uj;

            if (fej == null)
                fej = uj;
        }

        /// <summary>
        /// Elem törlése a listából
        /// </summary>
        /// <param name="elem"></param>
        public void ElemTorles(IFeladat elem)
        {
            ListaElem aktualis = fej;
            ListaElem elozo = null;

            if (elem != null && aktualis != null)
            {
                while (((IFeladat)aktualis.tartalom) != elem && aktualis != null)
                {
                    elozo = aktualis;
                    aktualis = aktualis.kovetkezo;
                }
                if (elozo != null)
                    elozo.kovetkezo = aktualis.kovetkezo;
                else if (aktualis != null)
                    fej = aktualis.kovetkezo;
            }
        }

        /// <summary>
        /// Lista bejárást segítő osztály
        /// </summary>
        class ListaBejaro : IEnumerator<T>
        {
            ListaElem elso, jelenlegi;

            public ListaBejaro(ListaElem elso)
            {
                this.elso = elso;
            }

            public T Current
            {
                get { return jelenlegi.tartalom; }
            }

            public void Dispose()
            {
                elso = null;
                jelenlegi = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (jelenlegi == null)
                    jelenlegi = elso;
                else
                    jelenlegi = jelenlegi.kovetkezo;

                return jelenlegi != null;
            }

            public void Reset()
            {
                jelenlegi = null;
            }
        }

        /// <summary>
        /// Lista bejáráshoz szükséges enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListaBejaro(fej);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
