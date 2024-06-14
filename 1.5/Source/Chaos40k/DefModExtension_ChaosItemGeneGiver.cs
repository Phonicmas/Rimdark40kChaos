using System.Collections.Generic;
using Verse;


namespace Chaos40k
{
    public class DefModExtension_ChaosItemGeneGiver : DefModExtension
    {
        public Dictionary<GeneDef, float> possibleGenesToGive;

        public GeneDef colourGene;
    }
}