using UnityEngine;

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

        void Start()
        {
            //RunOutcomeTests();
            //RunBinTests();
            //RunWheelTests();
            RunBinBuilderUSTests();
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
        }


        void RunBinTests()
        {
            Bin_Tests binTests = new Bin_Tests(console);

            if (binTests.Test())
                console.WriteLine("Bin_Tests.Test() - all tests pased");
            else
                console.WriteLine("Bin_Tests.Test() - tests failed");
        }

        void RunWheelTests()
        {
            Wheel_Tests wheelTests = new Wheel_Tests(console);
            if (wheelTests.TestGetRandomBin())
                console.WriteLine("Wheel_Tests.TestGetRandomBin() - all tests pased");
            else
                console.WriteLine("Wheel_Tests.TestGetRandomBin() - tests failed");
        }

        // See RouletteTableLayout.png to compare these results with.
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
    }
}