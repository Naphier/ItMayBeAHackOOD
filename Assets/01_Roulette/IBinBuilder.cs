using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
	/// <summary>
	/// Contract for constructing all of the bins for a wheel.
	/// </summary>
    public interface IBinBuilder
    {
		/// <summary>
		/// Creates all bins for a wheel.
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void BuildBins(Wheel wheel);


		/// <summary>
		/// Generates all column bets.
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateColumnBets(Wheel wheel);


		/// <summary>
		/// Generate all corner bets (i.e. a bet on the corner of a number).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateCornerBets(Wheel wheel);


		/// <summary>
		/// Generates all 'dozen' bets (i.e. 1-12, 13-24, 25-36).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateDozenBets(Wheel wheel);


		/// <summary>
		/// Generates all even money bets (i.e. Red, Black, Even, Odd, High (19-36), and Low (1-18)).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateEvenMoneyBets(Wheel wheel);


		/// <summary>
		/// The top line bet (0,00,1,2,3).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateFiveBet(Wheel wheel);


		/// <summary>
		/// Generates all line bets (i.e. a bet between 2 rows such as 1-3 + 4-6).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateLineBets(Wheel wheel);


		/// <summary>
		/// Generates all of the split bets (i.e. bets on 2 numbers).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateSplitBets(Wheel wheel);


		/// <summary>
		/// Generates all straight bets for the wheel (i.e. a bet on a single number).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateStraightBets(Wheel wheel);


		/// <summary>
		/// Generates all stree bets for the wheel (i.e. bet on all 3 numbers in a column).
		/// </summary>
		/// <param name="wheel">The wheel to add the bin-outcomes to.</param>
		void GenerateStreetBets(Wheel wheel);
    }
}
