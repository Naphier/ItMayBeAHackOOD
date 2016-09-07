using System;
using System.Collections.ObjectModel;

namespace Roulette
{
	/// <summary>
	/// Responsible for placing the player's bets, selecting a winning bin, and resolving the bets.
	/// </summary>
	public class Game
	{
		/// <summary>
		/// The game's wheel.
		/// </summary>
		public Wheel wheel { get; private set; }

		/// <summary>
		/// The game's table.
		/// </summary>
		public Table table { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="wheel">required</param>
		/// <param name="table">required</param>
		public Game(Wheel wheel, Table table)
		{
			this.wheel = wheel;
			this.table = table;
		}

		/// <summary>
		/// Runs the game simulation. Places player's bets, gets a random bin, validates the table's bets,
		/// resolves the bets, and clears the bets from the table.
		/// </summary>
		/// <param name="player">The player who will be placing bets.</param>
		public void Cycle(Player player)
		{
			player.PlaceBets();
			Bin winningBin = wheel.GetRandomBin();
			table.ValidateBets();

			foreach (Bet bet in table.Bets)
			{
				if (winningBin.Contains(bet.outcome))
				{
					player.Win(bet);
				}
				else
				{
					player.Lose(bet);
				}
			}

			table.ClearBets();
		}
	}
}
