using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    abstract class CPUFeladatok : IFeladat
    {
        private int prioritas;
        protected int idoigeny;
        protected int hanySzimulaciosKorOtaEl;
        public event FeladatBeutemezveEsemeny FeladatBeutemezve;

        public CPUFeladatok()
        {
            FeladatBeutemezve = new FeladatBeutemezveEsemeny(item_FeladatBeutemezve);
        }

        public int HanySzimulaciosKorOtaEl
        {
            get
            {
                return hanySzimulaciosKorOtaEl;
            }
            set
            {
                hanySzimulaciosKorOtaEl = value;
            }
        }

        public int Prioritas
        {
            get
            {
                return prioritas;
            }
            set
            {
                prioritas = value;
            }
        }

        public int Idoigeny
        {
            get
            {
                return idoigeny;
            }
            set
            {
                idoigeny = value;
            }
        }

        /// <summary>
        /// A listában megadott elemek ütemezése 0/1 hátizsák probléma alapján
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="CPUVegesIdoKapacitas"></param>
        /// <param name="HanySzimulaciosKorMaximum"></param>
        public void FeladatokUtemezes(LancoltLista<IFeladat> lista, int CPUVegesIdoKapacitas, int HanySzimulaciosKorMaximum)
        {
            //lista elemek megszámolása a segéd tömbök miatt
            int db = 0;
            foreach (var item in lista)
                db++;

            //ha a lista üres, akkor minden feladat végre van hajtva, így további lépéseket nem kell végrehajtani
            if (db == 0)
            {
                Console.WriteLine("Minden feladat végrehajtva!");
                return;
            }

            //segéd tömbök
            int[] prioritasTomb = new int[db + 1];
            int[] idoIgenyTomb = new int[db + 1];
            int[,] Eredmeny = new int[db + 1, CPUVegesIdoKapacitas + 1];

            prioritasTomb[0] = 0;
            idoIgenyTomb[0] = 0;
            
            //segéd tömbök feltöltése
            int i = 1;
            foreach (var item in lista)
            {
                prioritasTomb[i] = item.Prioritas;
                idoIgenyTomb[i] = item.Idoigeny;
                i++;
            }           

            //Feladatok kiválogatása 0/1 hátizsák probléma alapján
            Knapsack(CPUVegesIdoKapacitas, Eredmeny, db, idoIgenyTomb, prioritasTomb);
            //Kiválogatott feladatok megjelenítése a felhasználónak és azok törlése a tömbből
            KnapsackKiolvasasa(db, CPUVegesIdoKapacitas, Eredmeny, idoIgenyTomb, prioritasTomb, lista, HanySzimulaciosKorMaximum);
           
        }

        /// <summary>
        /// 0/1 hátizsák probléma alapján a feladatok kiválogatása CPU időigény és prioritás szerint
        /// </summary>
        /// <param name="CPUVegesIdoKapacitas"></param>
        /// <param name="Eredmeny"></param>
        /// <param name="db"></param>
        /// <param name="idoIgenyTomb"></param>
        /// <param name="prioritasTomb"></param>
        private void Knapsack(int CPUVegesIdoKapacitas, int[,] Eredmeny, int db, int[] idoIgenyTomb, int[] prioritasTomb)
        {
            for (int x = 0; x < CPUVegesIdoKapacitas; x++)
                Eredmeny[0, x] = 0;
            for (int j = 1; j < db; j++)
                Eredmeny[j, 0] = 0;

            for (int j = 1; j <= db; j++)
                for (int x = 1; x <= CPUVegesIdoKapacitas; x++)
                {
                    if (idoIgenyTomb[j] <= x)
                        Eredmeny[j, x] = Math.Max(Eredmeny[j - 1, x], Eredmeny[j - 1, x - idoIgenyTomb[j]] + prioritasTomb[j]);
                    else
                        Eredmeny[j, x] = Eredmeny[j - 1, x];
                }
        }

        /// <summary>
        /// Kiválogatott feladatok megjelenítése a felhasználónak és azok törlése a tömbből
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CPUVegesIdoKapacitas"></param>
        /// <param name="Eredmeny"></param>
        /// <param name="idoIgenyTomb"></param>
        /// <param name="prioritasTomb"></param>
        /// <param name="lista"></param>
        /// <param name="HanySzimulaciosKorMaximum"></param>
        private void KnapsackKiolvasasa(int db, int CPUVegesIdoKapacitas, int[,] Eredmeny, int[] idoIgenyTomb,
            int[] prioritasTomb, LancoltLista<IFeladat> lista, int HanySzimulaciosKorMaximum)
        {
            //kiválogatott elemek indexének kiolvasása
            int i = db;
            int x = CPUVegesIdoKapacitas;
            int a = 0;
            int[] S = new int[db];
            while (i > 0 && x>0)
            {
                if (Eredmeny[i,x] != Eredmeny[i-1, x])
                {
                    S[a++] = i;
                    x = x - idoIgenyTomb[i];
                }
                i--;
            }

            //index alapján a feladatok megjelenítése eseménnyel majd azok törlése a listából
            foreach (var Sitem in S)
            {
                if (Sitem != 0)
                {
                    foreach (var item in lista)
                    {
                        if (item.Idoigeny == idoIgenyTomb[Sitem] && item.Prioritas == prioritasTomb[Sitem])
                        {
                            item.FeladatBeutemezve += FeladatBeutemezve;
                            FeladatBeutemezve(item);
                            lista.ElemTorles(item);
                        }
                    }
                }
            }

            //végre nem hajtott elemek HanySzimulaciosKorOtaEl tulajdonságát 1-nel növeljük
            //ha a növelt érték elérte a maximumot akkor kivételt dobunk
            foreach (var item in lista)
            {
                item.HanySzimulaciosKorOtaEl++;
                if (item.HanySzimulaciosKorOtaEl == HanySzimulaciosKorMaximum)
                    throw new HanySzimulaciosKorMaximumException(item);
            }
        }
        
        /// <summary>
        /// Esemény által kiíratjuk a végrehajtandó elemek időigényét és prioritását
        /// </summary>
        /// <param name="sender"></param>
        void item_FeladatBeutemezve(object sender)
        {
            if (sender is IFeladat)
            {
                Console.WriteLine("Végrehajtandó feladat időgénye: " + ((IFeladat)sender).Idoigeny + ", prioritása: " + ((IFeladat)sender).Prioritas);
            }
        }

        /// <summary>
        /// Feladatok lista kiíratása
        /// </summary>
        /// <param name="lista"></param>
        public void ListaKiir(LancoltLista<IFeladat> lista)
        {
            int i = 0;
            foreach (var item in lista)
                Console.WriteLine(++i + ". feladat időigénye: " + item.Idoigeny + ", prioritása: " + item.Prioritas +
                    ", élet ciklusa felvétel óta: " + item.HanySzimulaciosKorOtaEl);
        }
    }
}
