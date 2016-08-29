using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roulette
{
    public class Wheel
    {
        private Bin[] bins;
        private Random rng;

        public Wheel(IBinBuilder binBuilder = null, int binSize = 38, int rngSeed = int.MinValue)
        {
            bins = new Bin[binSize];

            // Generate a random seed from the current UTC Unix timestamp.
            if (rngSeed == int.MinValue)
            {
                int seedFromTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                rng = new Random(seedFromTime);
            }
            else
                rng = new Random(rngSeed);

			if (binBuilder != null)
				binBuilder.BuildBins(this);
        }

        /// <summary>
        /// Gets the size of the bins array.
        /// </summary>
        /// <returns>Size of the bins array.</returns>
        public int GetBinSize()
        {
            return bins.Length;
        }

        /// <summary>
        /// Adds an outcome to the 'bins' array at the specified index.
        /// </summary>
        /// <param name="index">Index to set the outcome at.</param>
        /// <param name="outcome">The outcome to set.</param>
        public void AddOutcomeToBin(int index, Outcome outcome)
        {
            if (index < 0 || index >= bins.Length)
                throw new IndexOutOfRangeException(
                    string.Format(
                        "bin index range [0,{1}]. index attempted: {2}", 
                        (bins.Length - 1), 
                        index));

            if (bins[index] == null)
                bins[index] = new Bin(outcome);
            else
                bins[index].Add(outcome);
        }


        /// <summary>
        /// Gets a random bin from the bins array based on the random number generator.
        /// </summary>
        /// <returns>A random bin.</returns>
        public Bin GetRandomBin()
        {
            int next = rng.Next(0, bins.Length - 1);
            if (bins[next] == null)
                throw new Exception("bin at index: " + next + " is null. bins array must be filled first");

            return bins[next];
        }

        public Bin GetBinAtIndex(int index)
        {
            if (index < 0 || index >= bins.Length)
                throw new IndexOutOfRangeException(
                    string.Format(
                        "bin index range [0,{1}]. index attempted: {2}",
                        (bins.Length - 1),
                        index));

            return bins[index];
        }
    }
}
