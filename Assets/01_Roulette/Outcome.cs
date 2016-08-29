using System.Collections.Generic;

namespace Roulette
{
    /// <summary>
    /// Responsible for representing the various Outcomes for the Roulette game.
    /// Each Outcome is unique and comparable by its name. 
    /// There cannot be mutiple odds for the same Outcome.
    /// Constructed through GetOrCreate() which maintains a list of available Outcomes.
    /// Likewise, existing Outcomes are accessed by GetOrCreate() to prevent duplication
    /// of an Outcome name with different odds.
    /// </summary>
    public class Outcome
    {
        private string name;
        private ushort odds;

        /// <summary>
        /// Construction should only happen through GetOrCreate().
        /// </summary>
        /// <param name="name">Unique name.</param>
        /// <param name="odds">The payout odds.</param>
        private Outcome(string name, ushort odds)
        {
            this.name = name;
            this.odds = odds;
        }

        private static List<Outcome> _outcomeList = new List<Outcome>();

        /// <summary>
        /// Get already existing Outcomes by name or create a new one by supplying the odds parameter.
        /// </summary>
        /// <param name="name">Use only this if looking up the Outcome.</param>
        /// <param name="odds">Required for creating a new Outcome. Optional for lookup.</param>
        /// <returns></returns>
        public static Outcome GetOrCreate(string name, ushort? odds = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentNullException("name");
            }

            foreach (var item in _outcomeList)
            {
                if (item.name == name)
                    return item;
            }

            if (odds == null)
            {
                throw new System.ArgumentException("Outcome Factory could not find an Outcome with name: '" + name +
                    "'. A value for odds must be supplied to create a new Outcome.");
            }

            Outcome outcome = new Outcome(name, (ushort)odds);

            if (!_outcomeList.Contains(outcome))
                _outcomeList.Add(outcome);

            return outcome;
        }


        /// <summary>
        /// Returns the payout amount based on the odds.
        /// </summary>
        /// <param name="amount">The amount of the bet.</param>
        /// <returns>The payout amount.</returns>
        public float GetWinAmount(float amount)
        {
            return odds * amount;
        }


        #region Overrides for Eqaulity comparisons
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;


            Outcome other = obj as Outcome;
            if ((System.Object)other == null)
            {
                return false;
            }

            return ((Outcome)obj).GetHashCode() == GetHashCode();
        }

        public static bool operator ==(Outcome a, Outcome b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return (a.name == b.name) && (a.odds == b.odds);
        }

        public static bool operator !=(Outcome a, Outcome b)
        {
            return !(a == b);
        }
        #endregion


        /// <returns>User friendly string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format("{0} | {1}:1", name, odds);
        }
    }
}
