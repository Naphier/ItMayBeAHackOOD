
using System;

namespace Roulette
{
	/// <summary>
	/// An implementation of the Player class that always bets on black.
	/// </summary>
	public class Passenger57 : Player
	{
		public delegate void OnResult(Bet bet);
		public OnResult OnLose;
		public OnResult OnWin;

		public Passenger57(Table table, Wheel wheel) : base(table, wheel)
		{
		}


		public override Outcome focusedOutcome
		{
			get
			{
				if (wheel == null)
				{
					throw new FieldAccessException("Wheel is not set!");
				}

				return wheel.GetOutcome("Black");
			}
		}

		public override void Lose(Bet bet)
		{
			if (OnLose != null)
				OnLose(bet);
		}

		private Random rng = new Random();

		/// <summary>
		/// Places a bet on focusedOutcome with a random amount between the table minimum and limit.
		/// </summary>
		public override void PlaceBets()
		{
			ushort betAmount = (ushort)rng.Next(table.minimum, table.limit);

			table.PlaceBet(new Bet(betAmount, focusedOutcome));
		}

		public override void Win(Bet bet)
		{
			if (OnWin != null)
				OnWin(bet);
		}
	}
}
