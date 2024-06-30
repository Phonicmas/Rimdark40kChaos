using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Chaos40k
{
    public class Recipe_SummonDeadDemon : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            Find.WindowStack.Add(new Dialog_ChooseDaemon(Current.Game.GetComponent<GameComponent_DaemonPrince>().DaemonPawns, billDoer.Map, billDoer.Position));
        }
    }
}