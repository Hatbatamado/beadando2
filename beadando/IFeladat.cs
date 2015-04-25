using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    delegate void FeladatBeutemezveEsemeny(object sender);
    interface IFeladat
    {
        int Prioritas { get; set; }
        int Idoigeny { get; set; }
        int HanySzimulaciosKorOtaEl { get; set; }
        event FeladatBeutemezveEsemeny FeladatBeutemezve;
    }
}
