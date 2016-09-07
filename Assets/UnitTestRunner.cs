using UnityEngine;

/// <summary>
/// Test for the roulette simulation.
/// </summary>
namespace Roulette.Tests
{
    /// <summary>
    /// Monobehaviour to run unit tests.
    /// </summary>
    public class UnitTestRunner : MonoBehaviour
    {
        IOutputService console
        {
            get
            {
                return uGUIConsole.Instance;
            }
        }

		#region Tests to run.
		public bool outcomeTests = true;
		public bool binTests = true;
		public bool wheelTests = true;
		public bool binBuilderUsTests = true;
		public bool betTests = true;
		public bool tableTests = true;
		public bool runP57Demo = true;
		#endregion


		/// <summary>
		/// Runs tests that have been selected in the inspector.
		/// </summary>
		void Start()
        {
			if (outcomeTests)
				RunOutcomeTests();
			if (binTests)
				RunBinTests();
			if (wheelTests)
				RunWheelTests();
			if (binBuilderUsTests)
				RunBinBuilderUSTests();
			if (betTests)
				RunBetTests();
			if (tableTests)
				RunTableTests();
			if (runP57Demo)
				RunGameDemoP57();

		}


        /// <summary>
        /// Runs tests for the Outcome class.
        /// </summary>
        void RunOutcomeTests()
        {
            
            Outcome_Tests outcome_tests = new Outcome_Tests(console);

            bool pass = true;
			ushort testCount = 100;
            for (int i = 0; i < testCount; i++)
            {
                if (!outcome_tests.TestEqualityComparer(testCount))
                {
                    pass = false;
                    break;
                }
            }

            if (pass)
                console.WriteLine("Outcome_Tests.TestEqualityComparer() - all tests passed");
            else
                console.WriteLine("Outcome_Tests.TestEqualityComparer() - tests failed");

            pass = outcome_tests.TestExceptions(false);

            if (pass)
                console.WriteLine("Outcome_Tests.TestExceptions() - all tests passed");
            else
                console.WriteLine("Outcome_Tests.TestExceptions() - tests failed");

            pass = outcome_tests.TestWinAmounts(500);

            if (pass)
                console.WriteLine("Outcome_Tests.TestWinAmounts() - all tests passed");
            else
                console.WriteLine("Outcome_Tests.TestWinAmounts() - tests failed");

			pass = outcome_tests.TestToString();
			if (pass)
				console.WriteLine("Outcome_Tests.TestToString() - all tests passed");
			else
				console.WriteLine("Outcome_Tests.TestToString() - tests failed");
		}

		
		/// <summary>
		/// Runs tests for the Bin class.
		/// </summary>
        void RunBinTests()
        {
            Bin_Tests binTests = new Bin_Tests(console);

            if (binTests.Test())
                console.WriteLine("Bin_Tests.Test() - all tests pased");
            else
                console.WriteLine("Bin_Tests.Test() - tests failed");

			if (binTests.TestToString())
				console.WriteLine("Bin_Tests.TestToString() - all tests pased");
			else
				console.WriteLine("Bin_Tests.TestToString() - tests failed");
		}


		/// <summary>
		/// Runs tests for the Wheel class.
		/// </summary>
        void RunWheelTests()
        {
            Wheel_Tests wheelTests = new Wheel_Tests(console);
            if (wheelTests.TestGetRandomBin())
                console.WriteLine("Wheel_Tests.TestGetRandomBin() - all tests pased");
            else
                console.WriteLine("Wheel_Tests.TestGetRandomBin() - tests failed");

			if (wheelTests.TestOutcomesDictionary())
				console.WriteLine("Wheel_Tests.TestOutcomesDictionary() - all tests pased");
			else
				console.WriteLine("Wheel_Tests.TestOutcomesDictionary() - tests failed");
		}

		
		/// <summary>
		/// Runs tests for the BinBuilderUS class.
		/// See RouletteTableLayout.png to compare these results with.
		/// </summary>
		void RunBinBuilderUSTests()
        {
            BinBuilderUS_Tests bbusTests = new BinBuilderUS_Tests(console);

			//bbusTests.DisplayFiveBet();
			//bbusTests.DisplayStraightBets();
			//bbusTests.DisplayStreetBets();
			//bbusTests.DisplaySplitBets();
			//bbusTests.DisplayColumnBets();
			//bbusTests.DisplayDozenBets();
			//bbusTests.DisplayLineBets();
			//bbusTests.DisplayEvenMoneyBets();
			//bbusTests.DisplayCornerBets();
			bbusTests.DisplayAll();
        }


		/// <summary>
		/// Runs tests for the Bets class.
		/// </summary>
		void RunBetTests()
		{
			Bet_Tests betTests = new Bet_Tests(console);
			if (betTests.TestWinAmount())
				console.WriteLine("Bet_Tests.TestWinAndLoseAmounts() - all tests pased");
			else
				console.WriteLine("Bet_Tests.TestWinAndLoseAmounts() - tests failed");

			if (betTests.TestToString())
				console.WriteLine("Bet_Tests.TestToString() - all tests pased");
			else
				console.WriteLine("Bet_Tests.TestToString() - tests failed");

			if (betTests.TestToString())
				console.WriteLine("Bet_Tests.TestToString() - all tests pased");
			else
				console.WriteLine("Bet_Tests.TestToString() - tests failed");
		}


		/// <summary>
		/// Runs tests on the Table class.
		/// </summary>
		void  RunTableTests()
		{
			Table_Tests tt = new Table_Tests(console);

			//tt.DisplayToStringMethods();

			if (tt.Tests())
				console.WriteLine("Table_Tests.Tests() - all tests pased");
			else
				console.WriteLine("Table_Tests.Tests() - tests failed");
		}


		void RunGameDemoP57()
		{
			GameDemoPassenger57 demo = new GameDemoPassenger57();
			demo.Main(console);
		}
    }
}