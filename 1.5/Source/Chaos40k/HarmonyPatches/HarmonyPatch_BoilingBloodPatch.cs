using HarmonyLib;
using RimWorld;
using Verse;

namespace Chaos40k
{
    [HarmonyPatch(typeof(Verb_MeleeAttack), "TryCastShot")]
    public class BoilingBloodPatch
    {
        public static void Postfix(bool __result, Verb_MeleeAttack __instance)
        {
            if (!__result)
            {
                return;
            }
            if (__instance.CurrentTarget.Pawn != null && __instance.CasterIsPawn)
            {
                Pawn targetPawn = __instance.CurrentTarget.Pawn;
                if (targetPawn.genes == null)
                {
                    return;
                }
                if (targetPawn.genes.HasActiveGene(Chaos40kDefOf.BEWH_KhorneBoilingBlood))
                {
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.Vaporize, 3, 0.65f, -1f, targetPawn);
                    __instance.CasterPawn.TakeDamage(dinfo);
                }
            }
        }

    }
}