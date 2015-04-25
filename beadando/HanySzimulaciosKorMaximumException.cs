using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando
{
    class HanySzimulaciosKorMaximumException : Exception
    {
        public HanySzimulaciosKorMaximumException(IFeladat elem)
            : base(elem.Idoigeny + " időigényű és " + elem.Prioritas +
            " prioritású elem túllépte a maximum szimulációs kör életciklust!")
        { }
    }
}
