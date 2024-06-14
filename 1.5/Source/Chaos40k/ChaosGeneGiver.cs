using Core40k;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Chaos40k
{
    public class ChaosGeneGiver : ThingWithComps
    {
        private Dictionary<GeneDef, float> possibleGenesToGive;

        public GeneDef giftToGive = null;

        public GeneDef colourGene = null;

        public override void PostMake()
        {
            base.PostMake();
            possibleGenesToGive = def.GetModExtension<DefModExtension_ChaosItemGeneGiver>().possibleGenesToGive;
            colourGene = def.GetModExtension<DefModExtension_ChaosItemGeneGiver>().colourGene;
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
        {
            foreach (FloatMenuOption floatMenuOption in base.GetFloatMenuOptions(selPawn))
            {
                yield return floatMenuOption;   
            }
            if (selPawn.genes == null)
            {
                yield break;
            }
            GeneDef giftToGive = GetPossibleGift(selPawn);
            if (giftToGive == null)
            {
                yield return new FloatMenuOption("NoMorePossibleGifts".Translate(selPawn.Named("PAWN")), null);
                yield break;
            }
            yield return new FloatMenuOption("UseChaosItem".Translate(selPawn.Named("PAWN"), Label), delegate
            {
                if (!selPawn.IsPrisonerOfColony && !selPawn.Downed)
                {
                    selPawn.jobs.TryTakeOrderedJob(JobMaker.MakeJob(Chaos40kDefOf.BEWH_UseChaosItem, this), JobTag.Misc);
                }
            });
        }

        public GeneDef GetPossibleGift(Pawn pawn)
        {
            WeightedSelection<GeneDef> weightedSelection = new WeightedSelection<GeneDef>();

            foreach (KeyValuePair<GeneDef, float> item in possibleGenesToGive)
            {
                if (pawn.genes.HasActiveGene(item.Key))
                {
                    continue;
                }
                weightedSelection.AddEntry(item.Key, item.Value);
            }

            if (weightedSelection.NoEntriesOrNull())
            {
                return null;
            }
            GeneDef chosenGene = weightedSelection.GetRandom();
            return chosenGene;
        }
    
        public void GiveSelectedGene(Pawn pawn, GeneDef gene)
        {
            pawn.genes.AddGene(gene, true);
            string genesGotten = gene.labelShortAdj;

            if (gene == Chaos40kDefOf.BEWH_DaemonMutation && !pawn.genes.HasActiveGene(Chaos40kDefOf.BEWH_DaemonMutation))
            {
                pawn.genes.AddGene(Chaos40kDefOf.BEWH_DaemonHide, true);
                pawn.genes.AddGene(Chaos40kDefOf.BEWH_DaemonHorns, true);
                pawn.genes.AddGene(Chaos40kDefOf.BEWH_DaemonTail, true);
                pawn.genes.AddGene(Chaos40kDefOf.BEWH_DaemonWings, true);
                pawn.genes.AddGene(colourGene, true);

                genesGotten += ", " + Chaos40kDefOf.BEWH_DaemonHide.labelShortAdj + ", " + Chaos40kDefOf.BEWH_DaemonHorns.labelShortAdj + ", " + Chaos40kDefOf.BEWH_DaemonTail.labelShortAdj + ", " + Chaos40kDefOf.BEWH_DaemonWings.labelShortAdj + " and " + colourGene.labelShortAdj;
            }

            string letterText = "ChaosItemLetterTitle".Translate();
            string messageText = "ChaosItemLetterMessage".Translate(pawn.Named("PAWN"), genesGotten);
            Find.LetterStack.ReceiveLetter(letterText, messageText, LetterDefOf.NeutralEvent);

            if (stackCount > 1)
            {
                stackCount--;
            }
            else
            {
                Destroy();
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Defs.Look(ref giftToGive, "giftToGive");
            Scribe_Defs.Look(ref colourGene, "colourGene");
            Scribe_Collections.Look(ref possibleGenesToGive, "possibleGenesToGive");
        }

    }
}