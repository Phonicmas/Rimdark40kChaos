using Verse;


namespace Chaos40k
{
    public class Chaos40kUtils
    {
        public static bool IsDaemonPrince(Pawn pawn)
        {
            if (pawn.genes.HasActiveGene(Chaos40kDefOf.BEWH_DaemonMutation))
            {
                return true;
            }
            return false;
        }
    }
}