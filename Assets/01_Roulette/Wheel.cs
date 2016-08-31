using System;
using System.Collections.Generic;

namespace Roulette
{
	/// <summary>
	/// Responsible for:
	/// - Constructing and maintaining a list of bins (numbers that the wheel can land on) and their outcomes.
	/// - Supplying a random bin to simulate a wheel spin.
	/// - Maintiains a unique list of outcomes available for this wheel.
	/// </summary>
    public class Wheel
    {
        private Bin[] bins;
        private Random rng;
		/// <summary>
		/// A unique list of outcomes. Should only be accessed through methods.
		/// </summary>
		private Dictionary<string, Outcome> outcomes = new Dictionary<string, Outcome>();
		
		/// <summary>
		/// Constructs a new wheel, sets its random number generator, and has bin builder fill this wheel's bins.
		/// </summary>
		/// <param name="binBuilder">Should only be null for testing. The BinBuilder that will fill the wheel's bins.</param>
		/// <param name="binSize">Optional, default is 38 bins.</param>
		/// <param name="rngSeed">Should only be set when testing.</param>
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

			AddOutcomeToDictionary(outcome);
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


		/// <summary>
		/// Returns a Bin at a specific index. For testing purposes.
		/// </summary>
		/// <param name="index">The index of the bin to retrieve.</param>
		/// <returns>A Bin from the 'bin' array.</returns>
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


		/// <summary>
		/// Adds an outcome to the outcome dictionary if it is not already there.
		/// </summary>
		/// <param name="outcome">Outcome to add to the dictionary.</param>
		private void AddOutcomeToDictionary(Outcome outcome)
		{
			if (!outcomes.ContainsKey(outcome.name))
				outcomes.Add(outcome.name, outcome);
		}


		/// <summary>
		/// Retrieves outcome from the outcome dictionary by name.
		/// </summary>
		/// <param name="name">Name of the outcome.</param>
		/// <returns>An outcome if found otherwise null.</returns>
		public Outcome GetOutcome(string name)
		{
			if (outcomes.ContainsKey(name))
				return outcomes[name];

			return null;
		}
    }
}
