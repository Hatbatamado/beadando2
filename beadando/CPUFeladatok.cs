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

        public void FeladatokUtemezes(LancoltLista<IFeladat> lista, int CPUVegesIdoKapacitas, int HanySzimulaciosKorMaximum)
        {
            int db = 0;
            foreach (var item in lista)
                db++;

            int[] prioritasTomb = new int[db + 1];
            int[] idoIgenyTomb = new int[db + 1];
            int[,] Eredmeny = new int[db + 1, CPUVegesIdoKapacitas + 1];

            prioritasTomb[0] = 0;
            idoIgenyTomb[0] = 0;
            
            int i = 1;
            foreach (var item in lista)
            {
                prioritasTomb[i] = item.Prioritas;
                idoIgenyTomb[i] = item.Idoigeny;
                i++;
            }

            Knapsack(CPUVegesIdoKapacitas, Eredmeny, db, idoIgenyTomb, prioritasTomb);
            KnapsackKiolvasasa(db, CPUVegesIdoKapacitas, Eredmeny, idoIgenyTomb, prioritasTomb, lista, HanySzimulaciosKorMaximum);
           
        }

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

        private void KnapsackKiolvasasa(int db, int CPUVegesIdoKapacitas, int[,] Eredmeny, int[] idoIgenyTomb,
            int[] prioritasTomb, LancoltLista<IFeladat> lista, int HanySzimulaciosKorMaximum)
        {
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

            if (a == 0)
                Console.WriteLine("Minden feladat végrehajtva!");

            foreach (var item in lista)
            {
                item.HanySzimulaciosKorOtaEl++;
                if (item.HanySzimulaciosKorOtaEl == HanySzimulaciosKorMaximum)
                    throw new HanySzimulaciosKorMaximumException(item);
            }
        }
        
        void item_FeladatBeutemezve(object sender)
        {
            if (sender is IFeladat)
            {
                Console.WriteLine("Végrehajtandó feladat időgénye: " + ((IFeladat)sender).Idoigeny + " prioritása: " + ((IFeladat)sender).Prioritas);
            }
        }
    }
}
