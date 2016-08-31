
using System.Collections.Generic;

namespace Roulette.Tests
{
	public class Table_Tests
	{
		IOutputService console;

		public Table_Tests(IOutputService console)
		{
			this.console = console;
		}

		public bool Tests()
		{
			bool pass = true;

			Table table = new Table(1000, 2);
			Outcome outcome = new Outcome("a", 1);

			Bet tooLow = new Bet(1, outcome);
			table.PlaceBet(tooLow);
			table.PlaceBet(tooLow);

			// Test invalid minimums.
			bool exc = false;
			try
			{
				table.ValidateBets();
			}
			catch (System.Exception)
			{
				exc = true;
			}

			if (!exc)
			{
				pass = false;
				console.WriteLine("Failed to throw exception on bet below the minimum.");
			}

			// Test over the table limit.
			table = new Table(10, 1);
			Bet bet = new Bet(5, outcome);
			table.PlaceBet(bet);
			table.PlaceBet(bet);
			table.PlaceBet(bet);

			exc = false;
			try
			{
				table.ValidateBets();
			}
			catch (System.Exception)
			{
				exc = true;
			}

			if (!exc)
			{
				pass = false;
				console.WriteLine("Failed to throw exception on total bets above the table limit.");
			}

			// Test bets list.
			Bet unexpectedBet = new Bet(5000, outcome);

			foreach (var item in table.Bets)
			{
				if (item == unexpectedBet)
				{
					pass = false;
					console.WriteLine("Failure! Found unexpected bet in bet list.");
				}

				if (item != bet)
				{
					pass = false;
					console.WriteLine("Failure! Did not find bet: '" + bet.ToString() + "' in bet list.");
				}
			}

			// Test to string methods
			if (string.IsNullOrEmpty(table.ToString()))
			{
				pass = false;
				console.WriteLine("Failure! ToString is null or empty");
			}


			if (string.IsNullOrEmpty(table.ToStringFriendly()))
			{
				pass = false;
				console.WriteLine("Failure! ToStringFriendly is null or empty");
			}

			return pass;
		}

		public void DisplayToStringMethods()
		{
			Table table = new Table(1000, 2);
			Outcome outcome = new Outcome("a", 1);

			for (int i = 1; i <= 10; i++)
			{
				Bet bet = new Bet((ushort)(i * 10), outcome);
				table.PlaceBet(bet);
			}

			console.WriteFormat("Table.ToString: \n{0}\n", table.ToString());
			console.WriteFormat("Table.ToStringFriendly: \n{0}\n", table.ToStringFriendly());
		}
	}
}
