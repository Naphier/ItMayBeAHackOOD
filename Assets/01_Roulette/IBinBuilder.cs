using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
    public interface IBinBuilder
    {
        void BuildBins(Wheel wheel);

        void GenerateFiveBet(Wheel wheel);

        void GenerateStraightBets(Wheel wheel);

        void GenerateSplitBets(Wheel wheel);

        void GenerateStreetBets(Wheel wheel);

        void GenerateCornerBets(Wheel wheel);

        void GenerateLineBets(Wheel wheel);

        void GenerateDozenBets(Wheel wheel);

        void GenerateColumnBets(Wheel wheel);

        void GenerateEvenMoneyBets(Wheel wheel);
    }
}
