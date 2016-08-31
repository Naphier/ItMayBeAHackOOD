using System;

namespace Roulette.Tests
{
    public class BinBuilderUS_Tests
    {
        private IOutputService _console;

        public BinBuilderUS_Tests(IOutputService console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            _console = console;
        }


		public void DisplayAll()
		{
			Wheel wheel = new Wheel(new BinBuilderUS());
			for (int i = 0; i < wheel.GetBinSize(); i++)
			{
				_console.WriteLine(
					string.Format("{0}~{1}", i, wheel.GetBinAtIndex(i).ToString("~")));
			}
		}

        public void DisplayFiveBet()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateFiveBet(wheel);

            for (int i = 0; i < wheel.GetBinSize(); i++)
            {
                if (wheel.GetBinAtIndex(i) != null)
                {
                    _console.WriteLine(
                        string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
                }
            }
        }

        public void DisplayStraightBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateStraightBets(wheel);

            for (int i = 0; i < wheel.GetBinSize(); i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplayStreetBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateStreetBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() -1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplaySplitBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateSplitBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() - 1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplayColumnBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateColumnBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() - 1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplayDozenBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateDozenBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() - 1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplayLineBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateLineBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() - 1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

        public void DisplayEvenMoneyBets()
        {
            Wheel wheel = new Wheel();
            IBinBuilder binBuilder = new BinBuilderUS();
            binBuilder.GenerateEvenMoneyBets(wheel);

            // skip 0 and 00
            for (int i = 1; i < wheel.GetBinSize() - 1; i++)
            {
                _console.WriteLine(
                    string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
            }
        }

		public void DisplayCornerBets()
		{
			Wheel wheel = new Wheel();
			IBinBuilder binBuilder = new BinBuilderUS();
			binBuilder.GenerateCornerBets(wheel);

			// skip 0 and 00
			for (int i = 1; i < wheel.GetBinSize() - 1; i++)
			{
				if (wheel.GetBinAtIndex(i) != null)
				{
					_console.WriteLine(
						string.Format("i: {0}  bin: {1}", i, wheel.GetBinAtIndex(i).ToString(",")));
				}
			}
		}
    }
}
