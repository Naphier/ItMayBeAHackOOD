

namespace Roulette.Tests
{
	/// <summary>
	/// Demonstration of a Game with the 'Passenger57' player implementation.
	/// </summary>
	public class GameDemoPassenger57
	{

		/// <summary>
		/// Runs the game simulation with minor output of the win/loss count to console.
		/// </summary>
		/// <param name="console"></param>
		public void Main(IOutputService console)
		{
			BinBuilderUS bbus = new BinBuilderUS();
			Wheel wheel = new Wheel(bbus);
			Table table = new Table(1000, 1);
			Game game = new Game(wheel, table);
			Passenger57 player = new Passenger57(table, wheel);
			int wins = 0;
			int losses = 0;
			Bet winBet = null;
			Bet loseBet = null;

			player.OnLose = (Bet b) => 
			{
				if (loseBet == null)
					loseBet = b;
				losses++;
			};

			player.OnWin = (Bet b) =>
			{
				if (winBet == null)
					winBet = b;
				wins++;
			};

			for (int i = 0; i < 200; i++)
			{
				game.Cycle(player);
			}

			console.WriteFormat(
				"Wins: {0}\t Winning bet: {1}\nLosses: {2}\t Losing bet: {3}", 
				wins, winBet, losses, loseBet);
		}

	}
}
