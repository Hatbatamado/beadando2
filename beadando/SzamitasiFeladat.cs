using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class SzamitasiFeladat : IFeladat  
    {
        private int hanySzimulaciosKorOtaEl;
        private int prioritas;
        private int idoigeny;

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

        public event FeladatBeutemezveEsemeny FeladatBeutemezve;
    }
}
