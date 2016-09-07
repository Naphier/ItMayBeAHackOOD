using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
	/// <summary>
	/// Abstract contract for all Player subclasses
	/// </summary>
	public abstract class Player
	{
		/// <summary>
		/// The outcome the player will bet on based on the player's strategey
		/// </summary>
		public abstract Outcome focusedOutcome { get; }

		/// <summary>
		/// The table the bets will be placed on.
		/// </summary>
		protected Table table;

		/// <summary>
		/// The wheel that will provide the available outcomes for the player's strategy.
		/// </summary>
		protected Wheel wheel;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="table">Required</param>
		/// <param name="wheel">Required</param>
		public Player(Table table, Wheel wheel)
		{
			this.table = table;
			this.wheel = wheel;
		}

		/// <summary>
		/// Places the bets on the table.
		/// </summary>
		public abstract void PlaceBets();

		/// <summary>
		/// Called by Game when the player's bet wins.
		/// </summary>
		/// <param name="bet">The winning bet.</param>
		public abstract void Win(Bet bet);

		/// <summary>
		/// Called by Game when the player's bet loses.
		/// </summary>
		/// <param name="bet">The losing bet.</param>
		public abstract void Lose(Bet bet);
	}


}
