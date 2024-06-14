using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Chaos40k
{
    public class JobDriver_UseChaosItem : JobDriver
    {
        public const int EnterDelay = 300;

        private ChaosGeneGiver ChaosItem => (ChaosGeneGiver)job.targetA.Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            GeneDef geneToGive = ChaosItem.GetPossibleGift(pawn);
            this.FailOnDespawnedOrNull(TargetIndex.A);
            this.FailOn(() => geneToGive == null);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.WaitWith(TargetIndex.A, EnterDelay, useProgressBar: true);
            yield return Toils_General.Do(delegate
            {
                ChaosItem.GiveSelectedGene(pawn, geneToGive);
            });
        }
    }
}