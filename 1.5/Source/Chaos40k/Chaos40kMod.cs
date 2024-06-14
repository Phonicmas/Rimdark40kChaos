using HarmonyLib;
using UnityEngine;
using Verse;


namespace Chaos40k
{
    public class Chaos40kMod : Mod
    {
        public static Harmony harmony;

        //readonly Chaos40kModSettings settings;

        public Chaos40kMod(ModContentPack content) : base(content)
        {
            //settings = GetSettings<Chaos40kModSettings>();
            harmony = new Harmony("Chaos40k.Mod");
            harmony.PatchAll();
        }
    }
}