using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class Program
    {
        private static int CPUVegesIdoKapacitas = 5;
        private static int HanySzimulaciosKorMaximum = 2;
        static LancoltLista<IFeladat> lista = new LancoltLista<IFeladat>();

        enum FeladatokEnum { SzamitasiFeladat, MerevlemezIO, SorosPortIO };

        static void Main(string[] args)
        {
            ConsoleKeyInfo key; 
            do
            {
                Console.Clear();
                Console.WriteLine("*****MENU*****");
                Console.WriteLine("  1. Feladat felvétel");
                Console.WriteLine("  2. Feladatok ütemezése");
                Console.WriteLine("  3. Feladatok kilistázása");
                Console.WriteLine("  4. CPU csere");
                Console.WriteLine("  5. Szimulációs kör maximum beállítása");
                Console.WriteLine("  Esc - Kilépés");
                Console.WriteLine();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    FeladatFelvetel();
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    FeladatokUtemezese();
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    FeladatokKilistazasa();
                if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                    CPUCsere();
                if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                    SzimulaciosKorMax();
            }
            while (key.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Szimulációs kör maximum életciklus számát állítja be
        /// </summary>
        static void SzimulaciosKorMax()
        {
            ErtekBeallitas(false);
        }

        /// <summary>
        /// CPU véges időkapacitását állítja be
        /// </summary>
        static void CPUCsere()
        {
            ErtekBeallitas(true);
        }

        /// <summary>
        /// Paraméter alapján a CPU véges időkapacitását vagy a szimulációs kör max. életciklus számát állítja be
        /// </summary>
        /// <param name="CPU"></param>
        private static void ErtekBeallitas(bool CPU)
        {
            bool ujra = false;
            do
            {
                Console.Clear();
                if (CPU)
                    Console.WriteLine("A jelenlegi CPU véges időkapacításának értéke: " + CPUVegesIdoKapacitas);
                else
                    Console.WriteLine("A jelenlegi szimulációs kör maximum életciklus száma: " + HanySzimulaciosKorMaximum);
                Console.Write("Az új érték: ");
                string BeolvasottErtek = Console.ReadLine();
                int ertek;
                if (Int32.TryParse(BeolvasottErtek, out ertek))
                {
                    if (CPU)
                        CPUVegesIdoKapacitas = ertek;
                    else
                        HanySzimulaciosKorMaximum = ertek;
                    ujra = false;
                }
                else
                    ujra = true;
            } while (ujra);
        }

        /// <summary>
        /// 3 fajta feladat lehet felvenni, enum szerint meghívja felvételt intéző metódust
        /// </summary>
        static void FeladatFelvetel()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine("*****Feladat felvétel*****");
                Console.WriteLine("  1. Szamitasi feladat");
                Console.WriteLine("  2. MerevlemezIO feladat");
                Console.WriteLine("  3. SorosPortIO feladat");
                Console.WriteLine("  Esc - Vissza");
                Console.WriteLine();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    Feladatok(FeladatokEnum.SzamitasiFeladat);
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    Feladatok(FeladatokEnum.MerevlemezIO);
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    Feladatok(FeladatokEnum.SorosPortIO);
            }
            while (key.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// enum alapján kiírja a megfelelő szöveget a felhasználónak,  majd a bekért adatok alapján
        /// ha azok helyesek, akkor létrehoz belőlük egy példányt és hozzáadja a listához
        /// </summary>
        /// <param name="feladat"></param>
        static void Feladatok(FeladatokEnum feladat)
        {
            bool ujra = false;
            do
            {
                Console.Clear();
                switch (feladat)
                {
                    case FeladatokEnum.SzamitasiFeladat:
                        Console.WriteLine("Számitási feladat");
                        break;
                    case FeladatokEnum.MerevlemezIO:
                        Console.WriteLine("MerevlemezIO feladat");
                        break;
                    case FeladatokEnum.SorosPortIO:
                        Console.WriteLine("SorosPortIO feladat");
                        break;
                }
                
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
                        case FeladatokEnum.MerevlemezIO:
                            MerevlemezIO mIO = new MerevlemezIO();
                            mIO.Idoigeny = ido;
                            mIO.Prioritas = prio;
                            lista.BeszurasPrioritasSzerint(mIO);
                            break;
                        case FeladatokEnum.SorosPortIO:
                            SorosPortIO spIO = new SorosPortIO();
                            spIO.Idoigeny = ido;
                            spIO.Prioritas = prio;
                            lista.BeszurasPrioritasSzerint(spIO);
                            break;
                    }
                }
                else
                    ujra = true;
            }
            while (ujra);
        }

        /// <summary>
        /// Feladatok ütemezése 0/1 hátizsák probléma alapján
        /// </summary>
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
                //hiba üzenet, hogy a feladat átlépte a maximum időtartamú életciklust, majd a feladat törlése a listából
                Console.WriteLine(e.Message);
                lista.ElemTorles(e.Elem);
            }
            Console.WriteLine();
            Console.Write("A folytatáshoz nyomjon meg egy gombot");
            Console.ReadKey();
        }

        /// <summary>
        /// Felvett feladatok kilistázása
        /// </summary>
        static void FeladatokKilistazasa()
        {
            Console.WriteLine();
            CPUMuveletek cpuMuv = new CPUMuveletek();
            cpuMuv.ListaKiir(lista);
            Console.WriteLine();
            Console.Write("A folytatáshoz nyomjon meg egy gombot");
            Console.ReadKey();
        }
    }
}
