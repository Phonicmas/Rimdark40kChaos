using HarmonyLib;
using RimWorld;
using Verse;

namespace Chaos40k
{
    [HarmonyPatch(typeof(RecipeDef), "AvailableNow", MethodType.Getter)]
    public class RequiresDeadDaemonPatch
    {
        public static void Postfix(ref bool __result, RecipeDef __instance)
        {
            if (!__result || !__instance.HasModExtension<DefModExtension_ResummonDaemonPrince>())
            {
                return;
            }
            if (Current.Game.GetComponent<GameComponent_DaemonPrince>().DaemonPawns.NullOrEmpty())
            {
                __result = false;
            }
        }
    }
}