using System;

namespace Roulette.Tests
{
    public class Bin_Tests
    {
        private IOutputService _console;

        public Bin_Tests(IOutputService console)
        {
            if(console == null)
                throw new ArgumentNullException("console");

            _console = console;
        }

        /// <summary>
        /// Creates several instances of Outcome and establishes that Bins can be constructed
        /// from the outcomes.
        /// </summary>
        /// <returns>false if tests fail, otherwise true.</returns>
        public bool Test()
        {
            bool pass = true;

            Outcome a = new Outcome("a", 1);
            Outcome b = new Outcome("b", 2);
            Outcome c = new Outcome("c", 3);
            Outcome d = new Outcome("d", 4);
            
            Bin bin = new Bin(a, b, c);

            if (!bin.Contains(a))
            {
                _console.WriteLine("FAILURE! bin doesn't contain Outcome: " + a.ToString());
                pass = false;
            }

            if (!bin.Contains(b))
            {
                _console.WriteLine("FAILURE! bin doesn't contain Outcome: " + b.ToString());
                pass = false;
            }

            if (!bin.Contains(c))
            {
                _console.WriteLine("FAILURE! bin doesn't contain Outcome: " + c.ToString());
                pass = false;
            }

            if (bin.Contains(d))
            {
                _console.WriteLine("FAILURE! bin should not contain Outcome: " + c.ToString());
                pass = false;
            }
            return pass;
        }


		public bool TestToString()
		{
			Outcome a = new Outcome("a", 1);
			Outcome b = new Outcome("b", 2);
			Outcome c = new Outcome("c", 3);
			Outcome d = new Outcome("d", 4);

			Bin bin = new Bin(a, b, c, d);

			if (string.IsNullOrEmpty(bin.ToString()))
				return false;

			if (string.IsNullOrEmpty(bin.ToString(",")))
				return false;

			return true;
		}
    }
}
