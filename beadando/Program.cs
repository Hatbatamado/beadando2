using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class Program
    {
        static void Main(string[] args)
        {
            const int CPUVegesIdoKapacitas = 1000;

            SzamitasiFeladat szf = new SzamitasiFeladat();
            szf.Prioritas = 5;
            SzamitasiFeladat szf2 = new SzamitasiFeladat();
            szf2.Prioritas = 0;
            SzamitasiFeladat szf3 = new SzamitasiFeladat();
            szf3.Prioritas = 7;
            SzamitasiFeladat szf4 = new SzamitasiFeladat();
            szf4.Prioritas = 3;

            LancoltLista<IFeladat> lista = new LancoltLista<IFeladat>();
            lista.BeszurasPrioritasSzerint(szf);
            lista.BeszurasPrioritasSzerint(szf2);
            lista.BeszurasPrioritasSzerint(szf3);
            lista.BeszurasPrioritasSzerint(szf4);

            Console.ReadKey();
        }
    }
}
