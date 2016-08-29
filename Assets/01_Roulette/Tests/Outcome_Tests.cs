using System;
using System.Collections.Generic;

namespace Roulette.Tests
{
    public class Outcome_Tests
    {
        private IOutputService _console;
        public Outcome_Tests(IOutputService console)
        {
            if (console == null)
                throw new ArgumentNullException("console");

            _console = console;
        }
        

        /// <summary>
        /// Tests for equality operations (== , !=, Equals(), List.Contains(), and Dictionary.ContainsKey()).
        /// </summary>
        /// <param name="testCount">Set the random number range and random string lengths.</param>
        /// <returns>True if all tests pass.</returns>
        public bool TestEqualityComparer(ushort testCount)
        {
            Random rng = new Random();
            int randomLength1 = rng.Next(1, testCount);
            int randomLength2 = rng.Next(1, testCount);
            string str1 = StringExtensions.RandomString(randomLength1);
            string str2 = StringExtensions.RandomString(randomLength2);

            ushort randomValue1 = (ushort)rng.Next(1, testCount);
            ushort randomValue2 = (ushort)rng.Next(1, testCount);

            Outcome a = Outcome.GetOrCreate(str1, randomValue1);
            Outcome b = Outcome.GetOrCreate(str1, randomValue1);
            Outcome c = Outcome.GetOrCreate(str2, randomValue2);

            bool pass = true;

            #region == and != operators
            if (a != b)
            {
                _console.WriteFormat("!= FAILURE!\nOutcome: {0}\ndoes not equal\nOutcome: {1}\n", a, b);
                pass = false;
            }

            if (a == c)
            {
                _console.WriteFormat("!= FAILURE!\nOutcome: {0}\nequals\nOutcome: {1}\n", a, c);
                pass = false;
            }
            #endregion

            #region Equal method
            if (!a.Equals(b))
            {
                _console.WriteFormat(
                    "!Equals() FAILURE!\nOutcome: {0} with hash: {1}\ndoes not equal\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), b, b.GetHashCode());
                pass = false;
            }

            if (a.Equals(c))
            {
                _console.WriteFormat(
                    "Equals() FAILURE!\nOutcome: {0} with hash: {1}\nequals\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), c, c.GetHashCode());
                pass = false;
            }
            #endregion

            #region Collections tests
            List<Outcome> outcomeList = new List<Outcome>();
            outcomeList.Add(a);

            if (!outcomeList.Contains(b))
            {
                _console.WriteFormat(
                    "!List.Contains() FAILURE!\nOutcome: {0} with hash: {1}\n" +
                    "does not equal\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), b, b.GetHashCode());
                pass = false;
            }

            if (outcomeList.Contains(c))
            {
                _console.WriteFormat(
                    "List.Contains() FAILURE!\nOutcome: {0} with hash: {1}\nequals\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), c, c.GetHashCode());
                pass = false;
            }

            Dictionary<Outcome, int> outcomeDict = new Dictionary<Outcome, int>();
            outcomeDict.Add(a, 1);
            if (!outcomeDict.ContainsKey(b))
            {
                _console.WriteFormat(
                    "!Dictionary.ContainsKey() FAILURE!\nOutcome: {0} with hash: {1}\n" + 
                    "does not equal\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), b, b.GetHashCode());
                pass = false;
            }

            if (outcomeDict.ContainsKey(c))
            {
                _console.WriteFormat(
                    "Dictionary.ContainsKey() FAILURE!\nOutcome: {0} with hash: {1}\n" + 
                    "equals\nOutcome: {2} with hash: {3}\n",
                    a, a.GetHashCode(), c, c.GetHashCode());
                pass = false;
            }
            #endregion

            return pass;
        }


        /// <summary>
        /// Tests that exceptions are set up correctly
        /// </summary>
        /// <param name="displayExceptions">Log the actual exception messages.</param>
        /// <returns>True if all tests pass.</returns>
        public bool TestExceptions(bool displayExceptions)
        {
            bool exc = false;
            bool pass = true;
            try
            {
                Outcome.GetOrCreate(null);
            }
            catch (Exception e)
            {
                if (displayExceptions)
                {
                    _console.Write(e);
                    _console.Write("\n\n");
                }
                exc = true;
            }

            if (!exc)
            {
                _console.WriteLine("GetOrCreate with null name did not throw an exception!");
                pass = false;
            }

            exc = false;
            try
            {
                Outcome.GetOrCreate("a");
            }
            catch (Exception e)
            {
                if (displayExceptions)
                {
                    _console.Write(e);
                    _console.Write("\n\n");
                }
                exc = true;
            }

            if (!exc)
            {
                _console.WriteLine(
                    "GetOrCreate for non-existant Outcome with no ODDS parameter did not throw an exception!");
                pass = false;
            }

            return pass;
        }


        /// <summary>
        /// Tests Outcome.GetWinAmount(float amount). 
        /// </summary>
        /// <param name="iterations">Number of iterations fo the test to perform.</param>
        /// <returns>True if all tests pass.</returns>
        public bool TestWinAmounts(ushort iterations)
        {
            bool pass = true;
            Random r = new Random();
            for (ushort i = 1; i <= iterations; i++)
            {
                Outcome a = Outcome.GetOrCreate(i.ToString(), i);
                float amount = (float)r.Next(1, iterations) + ((float)r.Next(1, iterations) / (float)iterations);
                float expected = i * amount;
                float actual = a.GetWinAmount(amount);
                if (actual != expected)
                {
                    pass = false;
                    _console.WriteFormat(
                        "TestWinAmounts FAILS -- Outcome: {0}  amount: {1}  expected: {2}  actual: {3}\n",
                        a, amount, expected, actual);
                    break;
                }
            }

            return pass;

        }
    }
}
