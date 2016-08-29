using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
    public class BinBuilderUS : IBinBuilder
    {
        public void BuildBins(Wheel wheel)
        {
            GenerateStraightBets(wheel);
            GenerateSplitBets(wheel);
            GenerateStreetBets(wheel);
            GenerateCornerBets(wheel);
			GenerateFiveBet(wheel);
			GenerateLineBets(wheel);
            GenerateDozenBets(wheel);
            GenerateColumnBets(wheel);
            GenerateEvenMoneyBets(wheel);
        }

        /// <summary>
        /// The top line bet (0,00,1,2,3).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateFiveBet(Wheel wheel)
        {
            Outcome fiveBet = Outcome.GetOrCreate("Five Bet", 6);
            wheel.AddOutcomeToBin(37, fiveBet);
            for (int i = 0; i <= 3; i++)
            {
                wheel.AddOutcomeToBin(i, fiveBet);
            }
        }


        /// <summary>
        /// Generates all column bets.
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateColumnBets(Wheel wheel)
        {
            string name;
            for (int column = 0; column < 3; column++)
            {
                name = "Column " + (column + 1).ToString();
                Outcome outcome = Outcome.GetOrCreate(name, 2);

                for (int row = 0; row < 12; row++)
                {
                    int binNumber = 3 * row + column + 1;
                    wheel.AddOutcomeToBin(binNumber, outcome);
                }
            }
        }


        /// <summary>
        /// Generate all corner bets (i.e. a bet on the corner of a number).
        /// </summary>
        /// <param name="wheel"></param>
        public void GenerateCornerBets(Wheel wheel)
        {
            string name;

            for (int row = 0; row < 11; row++)
            {
                int firstColumnNumber = 3 * row + 1;
				name = string.Format(
					"Corner {0},{1},{2},{3}",
					firstColumnNumber,
					firstColumnNumber + 1,
					firstColumnNumber + 3,
					firstColumnNumber + 4);

				wheel.AddOutcomeToBin(firstColumnNumber, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(firstColumnNumber + 1, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(firstColumnNumber + 3, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(firstColumnNumber + 4, Outcome.GetOrCreate(name, 8));

				int secondColumnNumber = 3 * row + 2;
				name = string.Format(
					"Corner {0},{1},{2},{3}",
					secondColumnNumber,
					secondColumnNumber + 1,
					secondColumnNumber + 3,
					secondColumnNumber + 4);

				wheel.AddOutcomeToBin(secondColumnNumber, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(secondColumnNumber + 1, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(secondColumnNumber + 3, Outcome.GetOrCreate(name, 8));
				wheel.AddOutcomeToBin(secondColumnNumber + 4, Outcome.GetOrCreate(name, 8));
			}
		}


        /// <summary>
        /// Generates all 'dozen' bets (i.e. 1-12, 13-24, 25-36).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateDozenBets(Wheel wheel)
        {
            string name;

            for (int d = 0; d < 3; d++)
            {
                name = string.Format(
                    "Dozen {0}-{1}",
                    (12 * d + 1),
                    (12 * d + 11 + 1)
                    );

                Outcome outcome = Outcome.GetOrCreate(name, 2);

                for (int m = 0; m < 12; m++)
                {
                    int binNumber = 12 * d + m + 1;
                    wheel.AddOutcomeToBin(binNumber, outcome);
                }
            }
        }

        /// <summary>
        /// Generates all even money bets (i.e. Red, Black, Even, Odd, High (19-36), and Low (1-18)).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateEvenMoneyBets(Wheel wheel)
        {
            Outcome red = Outcome.GetOrCreate("Red", 1);
            Outcome black = Outcome.GetOrCreate("Black", 1);
            Outcome even = Outcome.GetOrCreate("Even", 1);
            Outcome odd = Outcome.GetOrCreate("Odd", 1);
            Outcome high = Outcome.GetOrCreate("High", 1);
            Outcome low = Outcome.GetOrCreate("Low", 1);

            int[] redsArray = new int[18] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

            for (int i = 1; i < 37; i++)
            {
                // High and Low
                if (1 <= i && i < 19)
                    wheel.AddOutcomeToBin(i, low);
                else
                    wheel.AddOutcomeToBin(i, high);

                // Even and odd
                if (i % 2 == 0)
                    wheel.AddOutcomeToBin(i, even);
                else
                    wheel.AddOutcomeToBin(i, odd);

                // Red and black
                //if (redsList.Contains(i))
                if (Array.IndexOf(redsArray, i) >= 0)
                    wheel.AddOutcomeToBin(i, red);
                else
                    wheel.AddOutcomeToBin(i, black);
            }
        }


        /// <summary>
        /// Generates all line bets (i.e. a bet between 2 rows such as 1-3 + 4-6).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateLineBets(Wheel wheel)
        {
            string name;
            for (int row = 0; row < 11; row++)
            {
                int firstColumnNumber = 3 * row + 1;

                name = string.Format(
                    "Line {0}-{1}",
                    firstColumnNumber,
                    firstColumnNumber + 5
                    );

                Outcome outcome = Outcome.GetOrCreate(name, 5);

                for (int i = 0; i <= 5; i++)
                {
                    int binNumber = firstColumnNumber + i;
                    wheel.AddOutcomeToBin(binNumber, outcome);
                }
            }
        }


        /// <summary>
        /// Generates all of the split bets (i.e. bets on 2 numbers).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateSplitBets(Wheel wheel)
        {
            string name;

            // Generate left-right split bets
            for (int row = 0; row < 12; row++)
            {
                int firstColumnNuber = 3 * row + 1; //1, 4, 7, ..., 34
                name = string.Format(
                    "Split {0}+{1}",
                    firstColumnNuber,
                    firstColumnNuber + 1);

                wheel.AddOutcomeToBin(firstColumnNuber, Outcome.GetOrCreate(name, 17));
                wheel.AddOutcomeToBin(firstColumnNuber + 1, Outcome.GetOrCreate(name, 17));

                int secondColumnNumber = 3 * row + 2; //2, 5, 8, ..., 35
                name = string.Format(
                    "Split {0}+{1}",
                    secondColumnNumber,
                    secondColumnNumber + 1);

                wheel.AddOutcomeToBin(secondColumnNumber, Outcome.GetOrCreate(name, 17));
                wheel.AddOutcomeToBin(secondColumnNumber + 1, Outcome.GetOrCreate(name, 17));
            }

            // Generate up-down split bets
            for (int i = 1; i <= 33; i++)
            {
                name = string.Format(
                    "Split: {0}+{1}",
                    i, 
                    i + 3
                    );

                wheel.AddOutcomeToBin(i, Outcome.GetOrCreate(name, 17));
                wheel.AddOutcomeToBin(i + 3, Outcome.GetOrCreate(name, 17));
            }
        }


        /// <summary>
        /// Generates all straight bets for the wheel (i.e. a bet on a single number).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateStraightBets(Wheel wheel)
        {
            // Zero
            wheel.AddOutcomeToBin(0, Outcome.GetOrCreate("0", 35));
            
            // Double Zero
            wheel.AddOutcomeToBin(37, Outcome.GetOrCreate("00", 35));

            for (int i = 1; i < 37; i++)
            {
                wheel.AddOutcomeToBin(i, Outcome.GetOrCreate(i.ToString(), 35));
            }
        }


        /// <summary>
        /// Generates all stree bets for the wheel (i.e. bet on all 3 numbers in a column).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateStreetBets(Wheel wheel)
        {
            string name;
            for (int row = 0; row < 12; row++)
            {
                int firstColumnNumber = 3 * row + 1;

                name = string.Format(
                    "Street {0}+{1}+{2}", 
                    firstColumnNumber, 
                    firstColumnNumber + 1, 
                    firstColumnNumber + 2);

                Outcome outcome = Outcome.GetOrCreate(name, 11);

                wheel.AddOutcomeToBin(firstColumnNumber, outcome);
                wheel.AddOutcomeToBin(firstColumnNumber + 1, outcome);
                wheel.AddOutcomeToBin(firstColumnNumber + 2, outcome);
            }
        }
    }
}
