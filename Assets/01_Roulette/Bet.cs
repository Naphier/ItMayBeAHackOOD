
namespace Roulette
{
	/// <summary>
	/// Contains the information for a bet that a player will place on the table.
	/// </summary>
	public class Bet
	{
		/// <summary>
		/// The amount of the bet.
		/// </summary>
		public ushort amount { get; private set; }

		/// <summary>
		/// The ooutcome that has been bet upon.
		/// </summary>
		public Outcome outcome { get; private set; }


		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="amount">Amount of the bet.</param>
		/// <param name="outcome">The outcome that is being bet upon.</param>
		public Bet(ushort amount, Outcome outcome)
		{
			this.amount = amount;
			this.outcome = outcome;
		}

		/// <summary>
		/// The amount if the bet is won.
		/// </summary>
		/// <returns>Winning amount.</returns>
		public int GetWinAmount()
		{
			return outcome.GetWinAmount(amount);
		}


		/// <summary>
		/// Informational string representation of this object.
		/// </summary>
		/// <returns>bet's information.</returns>
		public override string ToString()
		{
			return string.Format("Bet({0}, {1})", amount, outcome);
		}

		/// <summary>
		/// A user-facing representation of this object.
		/// </summary>
		/// <returns>Nicely formatted string, i.e. "$100 on 0 @ 10:1"</returns>
		public string ToStringFriendly()
		{
			return string.Format("{0} on {1}", amount, outcome);
		}
	}
}
