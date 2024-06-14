using RimWorld;
using Verse;


namespace Chaos40k
{
    [DefOf]
    public static class Chaos40kDefOf
    {

        public static GeneDef BEWH_DaemonMutation;
        public static GeneDef BEWH_DaemonHide;
        public static GeneDef BEWH_DaemonWings;
        public static GeneDef BEWH_DaemonTail;
        public static GeneDef BEWH_DaemonHorns;

        public static GeneDef BEWH_UndividedMark;
        public static GeneDef BEWH_SlaaneshMark;
        public static GeneDef BEWH_TzeentchMark;
        public static GeneDef BEWH_NurgleMark;
        public static GeneDef BEWH_KhorneMark;

        public static GeneDef BEWH_KhorneBoilingBlood;
        public static GeneDef BEWH_KhorneMonstrousHands;

        public static MentalStateDef BEWH_KhornateHungerBeserk;

        public static HediffDef BEWH_TzeentchEverchangingDeficiancies;

        public static XenotypeIconDef BEWH_DPUndividedIcon;
        public static XenotypeIconDef BEWH_DPSlaaneshIcon;
        public static XenotypeIconDef BEWH_DPTzeentchIcon;
        public static XenotypeIconDef BEWH_DPNurgleIcon;
        public static XenotypeIconDef BEWH_DPKhorneIcon;

        public static JobDef BEWH_UseChaosItem;

        static Chaos40kDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(Chaos40kDefOf));
        }
    }
}