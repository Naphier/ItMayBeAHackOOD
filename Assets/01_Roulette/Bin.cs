using System;
using System.Collections.Generic;

namespace Roulette
{
    /// <summary>
    /// A collection of Outcomes to be associated with a bin number in Wheel.
    /// </summary>
    public class Bin: HashSet<Outcome>
    {
        /// <summary>
        /// Creates a new bin. Requires at least one outcome.
        /// </summary>
        /// <param name="outcome">The first and required Outcome.</param>
        /// <param name="outcomes">Additioanl Outcomes.</param>
        public Bin(Outcome outcome, params Outcome[] outcomes)
        {
            if (outcome == null)
                throw new ArgumentNullException("outcome");

            Add(outcome);

            if (outcomes != null)
            {
                foreach (var item in outcomes)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Human-friendly ToString with optional delimiter.
        /// </summary>
        /// <param name="delim">Delimiter to separate elemtents of this collection. Default is new line.</param>
        /// <returns>Human-friendly string listing the Outcomes in this Bin.</returns>
        public string ToString(string delim = "\n")
        {
            string s = base.ToString();

            if (delim != null)
            {
                s = "";

                if (this == null)
                {
                    s += "NULL";
                }
                else if (Count <= 0)
                {
                    s += "EMPTY";
                }
                else
                {
                    int i = 0;
                    foreach (var item in this)
                    {
                        s += item.ToString();
                        if (i < Count - 1)
                            s += delim;
                        i++;
                    }
                }
            }

            return s;
        }

    }
}
