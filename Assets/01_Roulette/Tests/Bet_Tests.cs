using System;

namespace Roulette.Tests
{
	public class Bet_Tests
	{
		IOutputService console;

		public Bet_Tests(IOutputService console)
		{
			if (console == null)
				throw new ArgumentNullException("console");

			this.console = console;
		}

		public bool TestWinAmount()
		{
			bool pass = true;
			ushort odds = 2;
			ushort amount = 100;
			Outcome outcome = new Outcome("a", odds);

			Bet bet = new Bet(amount, outcome);

			int expectedWinAmount = odds * amount;

			if (bet.GetWinAmount() != expectedWinAmount)
			{
				pass = false;
				console.WriteLine(
					string.Format(
						"FAILURE! GetWinAmount(): {0}  expected: {1}", 
						bet.GetWinAmount(), 
						expectedWinAmount));
			}

			return pass;
		}

		public bool TestToString()
		{
			Outcome outcome = new Outcome("a", 1);
			Bet bet = new Bet(1, outcome);

			if (string.IsNullOrEmpty(bet.ToString()))
				return false;

			return true;
		}

		public bool TestToStringFriendly()
		{
			Outcome outcome = new Outcome("a", 1);
			Bet bet = new Bet(1, outcome);

			if (string.IsNullOrEmpty(bet.ToStringFriendly()))
				return false;

			return true;
		}
	}
}
