using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class LancoltLista<T> where T : IFeladat
    {
        class ListaElem
        {
            public T tartalom;
            public ListaElem kovetkezo;
        }

        ListaElem fej = null;

        public void BeszurasPrioritasSzerint(T elem)
        {
            ListaElem aktualis = fej;
            ListaElem elozo = null;
            ListaElem kov = null;
            while (aktualis != null && aktualis.tartalom.Prioritas > elem.Prioritas)
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

        //public void Listaz()
        //{
        //    ListaElem aktualis = fej;
        //    while (aktualis != null)
        //    {
        //        Console.WriteLine(aktualis.tartalom);
        //        aktualis = aktualis.kovetkezo;
        //    }
        //}
    }
}
