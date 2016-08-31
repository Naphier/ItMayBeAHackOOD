using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Roulette
{
	/// <summary>
	/// Responsible for: 
	/// - Allowing bets to be added and removed.
	/// - Maintaining a list of bets placed.
	/// </summary>
	public class Table
	{
		public ushort limit { get; private set; }
		public ushort minimum { get; private set; }
		private List<Bet> bets = new List<Bet>();

		public Table(ushort limit, ushort minimum)
		{
			this.limit = limit;
			this.minimum = minimum;
		}


		/// <summary>
		/// Adds a bet to the table's bet list.
		/// </summary>
		/// <param name="bet">The bet to be placed.</param>
		public void PlaceBet(Bet bet)
		{
			bets.Add(bet);
		}


		/// <summary>
		/// The list of bets placed on the table.
		/// </summary>
		public ReadOnlyCollection<Bet> Bets
		{
			get
			{
				return bets.AsReadOnly();
			}
		}
		

		/// <summary>
		/// Throws an exception if any bet is invalid.
		/// </summary>
		public void ValidateBets()
		{
			uint sum = 0;
			foreach (var bet in bets)
			{
				if (bet.amount < minimum)
				{
					throw new ArgumentOutOfRangeException(
						string.Format("Invalid Bet '{0}' on Table: {1}",
						bet, this
						));
				}
				sum += bet.amount;
			}

			if (sum > limit)
				throw new ArgumentOutOfRangeException(
						string.Format("Table limit breached! Total Bets '{0}' on Table: {1}",
						sum, this
						));
		}


		/// <summary>
		/// Informational string representation of this object.
		/// </summary>
		/// <returns>Table's information.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Table(");

			int i = 0;
			foreach (var item in bets)
			{
				sb.Append(item);

				if (i < bets.Count - 1)
					sb.Append(", ");
				i++;
			}

			sb.Append(")");
			return sb.ToString();
		}


		/// <summary>
		/// A user-facing representation of this object.
		/// </summary>
		/// <returns>A user-friendly string.</returns>
		public string ToStringFriendly()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("Table Bets:");

			foreach (var item in bets)
			{
				sb.AppendLine("\t" + item.ToStringFriendly());
			}

			return sb.ToString();
		}

	}
}
