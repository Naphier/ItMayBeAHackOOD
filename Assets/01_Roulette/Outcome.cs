/// <summary>
/// A simulation of a roulette game.
/// </summary>
namespace Roulette
{
    /// <summary>
    /// Responsible for representing the various Outcomes for the Roulette game.
    /// Each Outcome is comparable by its name. 
    /// </summary>
    public class Outcome
    {
        public string name { get; private set; }
        public ushort odds { get; private set; }

        /// <summary>
        /// Constructions a new outcome.
        /// </summary>
        /// <param name="name">Unique name.</param>
        /// <param name="odds">The payout odds.</param>
        public Outcome(string name, ushort odds)
        {
            this.name = name;
            this.odds = odds;
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
