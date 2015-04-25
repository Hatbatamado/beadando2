using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class Program
    {
        public const int CPUVegesIdoKapacitas = 5;
        public const int HanySzimulaciosKorMaximum = 1;
        static LancoltLista<IFeladat> lista = new LancoltLista<IFeladat>();

        enum FeladatokEnum { SzamitasiFeladat };

        static void Main(string[] args)
        {
            ConsoleKeyInfo key; 
            do
            {
                Console.Clear();
                Console.WriteLine("*****MENU*****");
                Console.WriteLine("  1. Feladat felvétel");
                Console.WriteLine("  2. Feladatok ütemezése");
                Console.WriteLine("  Esc - Kilépés");
                Console.WriteLine();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    FeladatFelvetel();
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    FeladatokUtemezese();
            }
            while (key.Key != ConsoleKey.Escape);



            /*SzamitasiFeladat szf = new SzamitasiFeladat();
            szf.Idoigeny = 2;
            szf.Prioritas = 3;
            SzamitasiFeladat szf2 = new SzamitasiFeladat();
            szf2.Idoigeny = 3;
            szf2.Prioritas = 4;
            SzamitasiFeladat szf3 = new SzamitasiFeladat();
            szf3.Idoigeny = 4;
            szf3.Prioritas = 5;
            SzamitasiFeladat szf4 = new SzamitasiFeladat();
            szf4.Idoigeny = 5;
            szf4.Prioritas = 6;

            LancoltLista<IFeladat> lista = new LancoltLista<IFeladat>();
            lista.BeszurasPrioritasSzerint(szf);
            lista.BeszurasPrioritasSzerint(szf2);
            lista.BeszurasPrioritasSzerint(szf3);
            lista.BeszurasPrioritasSzerint(szf4);

            CPUMuveletek cpuMuv = new CPUMuveletek();
            cpuMuv.FeladatokUtemezes(lista, CPUVegesIdoKapacitas);

            Console.ReadKey();*/
        }

        static void FeladatFelvetel()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine("*****Feladat felvétel*****");
                Console.WriteLine("  1. Szamitasi feladat");
                Console.WriteLine("  Esc - Vissza");
                Console.WriteLine();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    Feladatok(FeladatokEnum.SzamitasiFeladat);
            }
            while (key.Key != ConsoleKey.Escape);
        }

        static void Feladatok(FeladatokEnum feladat)
        {
            bool ujra = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Számitási feladat");
                Console.Write("A feladat időigénye: ");
                string idoIgeny = Console.ReadLine();
                Console.Write("A feladat prioritása: ");
                string prioritas = Console.ReadLine();
                int ido = 0, prio = 0;
                if (Int32.TryParse(idoIgeny, out ido) && Int32.TryParse(prioritas, out prio))
                {
                    ujra = false;
                    switch (feladat)
                    {
                        case FeladatokEnum.SzamitasiFeladat:
                            SzamitasiFeladat szf = new SzamitasiFeladat();
                            szf.Idoigeny = ido;
                            szf.Prioritas = prio;
                            lista.BeszurasPrioritasSzerint(szf);
                            break;
                    }
                }
                else
                    ujra = true;
            }
            while (ujra);
        }

        static void FeladatokUtemezese()
        {
            Console.WriteLine();
            CPUMuveletek cpuMuv = new CPUMuveletek();
            try
            {
                cpuMuv.FeladatokUtemezes(lista, CPUVegesIdoKapacitas, HanySzimulaciosKorMaximum);
            }
            catch(HanySzimulaciosKorMaximumException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
            Console.Write("A folytatáshoz nyomjon meg egy gombot");
            Console.ReadKey();
        }
    }
}
