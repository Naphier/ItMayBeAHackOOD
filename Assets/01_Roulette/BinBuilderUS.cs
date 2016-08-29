using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
	/// <summary>
	/// Builds the bins for a wheel based on US betting rules.
	/// </summary>
    public class BinBuilderUS : IBinBuilder
    {
		#region Constants
		private const int ODDS_FIVE_BET = 6;
		private const int ODDS_COLUMN_BET = 2;
		private const int ODDS_CORNER_BET = 8;
		private const int ODDS_DOZEN_BET = 2;
		private const int ODDS_EVEN_BET = 1;
		private const int ODDS_LINE_BET = 5;
		private const int ODDS_SPLIT_BET = 17;
		private const int ODDS_STRAIGHT_BET = 35;
		private const int ODDS_STREET_BET = 11;

		private const string NAME_FIVE_BET = "Five Bet";
		private const string NAME_COLUMN_BET = "Column";
		private const string NAME_CORNER_BET = "Corner";
		private const string NAME_DOZEN_BET = "Dozen";
		private const string NAME_RED_BET = "Red";
		private const string NAME_BLACK_BET = "Black";
		private const string NAME_EVEN_BET = "Even";
		private const string NAME_ODD_BET = "Odd";
		private const string NAME_HIGH_BET = "High";
		private const string NAME_LOW_BET = "Low";
		private const string NAME_LINE_BET = "Line";
		private const string NAME_SPLIT_BET = "Split";
		private const string NAME_STREET_BET = "Street";

		// Yeah, I know you can still modify elements, but hopefully the readonly modifier is a good enough indicator
		// that you shouldn't.
		private readonly int[] REDS_ARRAY = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
		#endregion

		/// <summary>
		/// Creates all bins for a wheel.
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
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
        /// Generates all column bets.
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateColumnBets(Wheel wheel)
        {
            string name;
            for (int column = 0; column < 3; column++)
            {
                name = NAME_COLUMN_BET + (column + 1).ToString();
                Outcome outcome = Outcome.GetOrCreate(name, ODDS_COLUMN_BET);

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
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		public void GenerateCornerBets(Wheel wheel)
        {
            string name;

            for (int row = 0; row < 11; row++)
            {
                int firstColumnNumber = 3 * row + 1;
				name = string.Format(
					NAME_CORNER_BET + " {0},{1},{2},{3}",
					firstColumnNumber,
					firstColumnNumber + 1,
					firstColumnNumber + 3,
					firstColumnNumber + 4);

				wheel.AddOutcomeToBin(firstColumnNumber, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(firstColumnNumber + 1, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(firstColumnNumber + 3, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(firstColumnNumber + 4, Outcome.GetOrCreate(name, ODDS_CORNER_BET));

				int secondColumnNumber = 3 * row + 2;
				name = string.Format(
					NAME_CORNER_BET + " {0},{1},{2},{3}",
					secondColumnNumber,
					secondColumnNumber + 1,
					secondColumnNumber + 3,
					secondColumnNumber + 4);

				wheel.AddOutcomeToBin(secondColumnNumber, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(secondColumnNumber + 1, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(secondColumnNumber + 3, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
				wheel.AddOutcomeToBin(secondColumnNumber + 4, Outcome.GetOrCreate(name, ODDS_CORNER_BET));
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
                    NAME_DOZEN_BET + " {0}-{1}",
                    (12 * d + 1),
                    (12 * d + 11 + 1)
                    );

                Outcome outcome = Outcome.GetOrCreate(name, ODDS_DOZEN_BET);

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
            Outcome red = Outcome.GetOrCreate(NAME_RED_BET, ODDS_EVEN_BET);
            Outcome black = Outcome.GetOrCreate(NAME_BLACK_BET, ODDS_EVEN_BET);
            Outcome even = Outcome.GetOrCreate(NAME_EVEN_BET, ODDS_EVEN_BET);
            Outcome odd = Outcome.GetOrCreate(NAME_ODD_BET, ODDS_EVEN_BET);
            Outcome high = Outcome.GetOrCreate(NAME_HIGH_BET, ODDS_EVEN_BET);
            Outcome low = Outcome.GetOrCreate(NAME_LOW_BET, ODDS_EVEN_BET);

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
                if (Array.IndexOf(REDS_ARRAY, i) >= 0)
                    wheel.AddOutcomeToBin(i, red);
                else
                    wheel.AddOutcomeToBin(i, black);
            }
        }


		/// <summary>
		/// The top line bet (0,00,1,2,3).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		public void GenerateFiveBet(Wheel wheel)
		{
			Outcome fiveBet = Outcome.GetOrCreate(NAME_FIVE_BET, ODDS_FIVE_BET);
			wheel.AddOutcomeToBin(37, fiveBet);
			for (int i = 0; i <= 3; i++)
			{
				wheel.AddOutcomeToBin(i, fiveBet);
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
                    NAME_LINE_BET + " {0}-{1}",
                    firstColumnNumber,
                    firstColumnNumber + 5
                    );

                Outcome outcome = Outcome.GetOrCreate(name, ODDS_LINE_BET);

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
			string format = NAME_SPLIT_BET + " {0}+{1}";

            // Generate left-right split bets
            for (int row = 0; row < 12; row++)
            {
                int firstColumnNuber = 3 * row + 1; //1, 4, 7, ..., 34
                name = string.Format(
                    format,
                    firstColumnNuber,
                    firstColumnNuber + 1);

                wheel.AddOutcomeToBin(firstColumnNuber, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));
                wheel.AddOutcomeToBin(firstColumnNuber + 1, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));

                int secondColumnNumber = 3 * row + 2; //2, 5, 8, ..., 35
                name = string.Format(
                    format,
                    secondColumnNumber,
                    secondColumnNumber + 1);

                wheel.AddOutcomeToBin(secondColumnNumber, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));
                wheel.AddOutcomeToBin(secondColumnNumber + 1, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));
            }

            // Generate up-down split bets
            for (int i = 1; i <= 33; i++)
            {
                name = string.Format(
                    format,
                    i, 
                    i + 3
                    );

                wheel.AddOutcomeToBin(i, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));
                wheel.AddOutcomeToBin(i + 3, Outcome.GetOrCreate(name, ODDS_SPLIT_BET));
            }
        }


        /// <summary>
        /// Generates all straight bets for the wheel (i.e. a bet on a single number).
        /// </summary>
        /// <param name="wheel">The wheel to add the bin-outcomes to.</param>
        public void GenerateStraightBets(Wheel wheel)
        {
            // Zero
            wheel.AddOutcomeToBin(0, Outcome.GetOrCreate("0", ODDS_STRAIGHT_BET));
            
            // Double Zero
            wheel.AddOutcomeToBin(37, Outcome.GetOrCreate("00", ODDS_STRAIGHT_BET));

            for (int i = 1; i < 37; i++)
            {
                wheel.AddOutcomeToBin(i, Outcome.GetOrCreate(i.ToString(), ODDS_STRAIGHT_BET));
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
                    NAME_STREET_BET + " {0}+{1}+{2}", 
                    firstColumnNumber, 
                    firstColumnNumber + 1, 
                    firstColumnNumber + 2);

                Outcome outcome = Outcome.GetOrCreate(name, ODDS_STREET_BET);

                wheel.AddOutcomeToBin(firstColumnNumber, outcome);
                wheel.AddOutcomeToBin(firstColumnNumber + 1, outcome);
                wheel.AddOutcomeToBin(firstColumnNumber + 2, outcome);
            }
        }
    }
}
