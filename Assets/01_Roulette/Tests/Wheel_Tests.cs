using System;

namespace Roulette.Tests
{
    public class Wheel_Tests
    {
        private IOutputService _console;

        private const int SEED = 1;
        private const int BIN_SIZE = 38;
        private int[] randoms;
        public Wheel_Tests(IOutputService console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            _console = console;   
        }

        /// <summary>
        /// Gets the random values based on the input seed.
        /// </summary>
        /// <param name="count">Number of randoms ints to get.</param>
        /// <param name="seed">Seed for the random number generator.</param>
        /// <param name="maxValue">The maximum random nuber.</param>
        /// <returns>Array of random integers.</returns>
        public int[] GetRngValues(int count, int seed, int maxValue)
        {
            Random rng = new Random(seed);

            int[] randoms = new int[count];

            for (int i = 0; i < count; i++)
            {
                randoms[i] = rng.Next(0, maxValue);
            }

            return randoms;
        }

        /// <summary>
        /// Tests Wheel.GetRandomBin() and GetBinAtIndex()
        /// </summary>
        /// <returns>false on failure, true on pass</returns>
        public bool TestGetRandomBin()
        {
            bool pass = true;
            Wheel wheel = new Wheel(null, BIN_SIZE, SEED);

            randoms = GetRngValues(BIN_SIZE, SEED, BIN_SIZE - 1);
            ConstuctBins(wheel);

            Bin unexpectedBin = new Bin(new Outcome("unexpectedBin", ushort.MaxValue));

            for (int i = 0; i < randoms.Length; i++)
            {
                Bin randomBin = wheel.GetRandomBin();
                Bin expectedBin = wheel.GetBinAtIndex(randoms[i]);
                
                if (randomBin != expectedBin)
                {
                    pass = false;
                    _console.WriteLine(
                        string.Format("TestRandoms() FAILURE!  randoms[{0}]: {1}  randomBin: {2}  expectedBin: {3}\n",
                        i, randoms[i], randomBin, expectedBin));
                    break;
                }

                if (randomBin == unexpectedBin)
                {
                    pass = false;
                    _console.WriteLine(
                        string.Format("TestGetRandomBin() FAILURE!  randoms[{0}]: {1}  randomBin: {2} EQUAL unexpectedBin: {3}\n",
                        i, randoms[i], randomBin, unexpectedBin));
                    break;
                }

                if (expectedBin == unexpectedBin)
                {
                    pass = false;
                    _console.WriteLine(
                        string.Format("TestGetRandomBin() FAILURE!  randoms[{0}]: {1}  expectedBin: {2} EQUAL unexpectedBin: {3}\n",
                        i, randoms[i], expectedBin, unexpectedBin));
                    break;
                }
            }

            return pass;
        }


        /// <summary>
        /// Creates BIN_SIZE number of Outcomes and adds them to the wheel's bins.
        /// </summary>
        /// <param name="wheel">The wheel to add the outcomes to.</param>
        private void ConstuctBins(Wheel wheel)
        {
            for (int i = 0; i < BIN_SIZE; i++)
            {
                string name = ((char)(i + 48)).ToString();
                Outcome outcome = new Outcome(name, (ushort)i);
                wheel.AddOutcomeToBin(i, outcome);
            }
        }

		public bool TestOutcomesDictionary()
		{
			BinBuilderUS bbus = new BinBuilderUS();
			Wheel wheel = new Wheel(bbus);


			if (wheel.GetOutcome("0") == null)
			{
				return false;
			}

			return true;
		}
    }
}
