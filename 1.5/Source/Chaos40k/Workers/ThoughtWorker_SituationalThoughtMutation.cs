using RimWorld;
using System.Linq;
using System.Security.Cryptography;
using Verse;


namespace Chaos40k
{
    public class ThoughtWorker_SituationalThoughtMutation : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
            {
                return false;
            }
            if (!def.HasModExtension<DefModExtension_SituationalThought>())
            {
                return false;
            }
            else
            {
                DefModExtension_SituationalThought defMod = def.GetModExtension<DefModExtension_SituationalThought>();
                if (other.genes != null)
                {
                    return other.genes.GenesListForReading.Where(x => x.def == defMod.geneActivator).Count() > 0;
                }
            }
            return false;
        }
    }
}