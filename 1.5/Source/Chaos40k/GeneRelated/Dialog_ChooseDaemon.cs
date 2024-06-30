using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Chaos40k
{
    public class Dialog_ChooseDaemon : Window
    {
        private readonly List<Pawn> choices;

        private readonly int choicesAmount;

        private readonly Map map;

        private readonly IntVec3 position;


        public Dialog_ChooseDaemon()
        { }

        public Dialog_ChooseDaemon(List<Pawn> choices, Map map, IntVec3 position)
        {
            this.choices = choices;
            this.map = map;
            this.position = position;
            choicesAmount = choices.Count;
        }

        public override Vector2 InitialSize => new Vector2(160f * choicesAmount, 300f);

        public override void DoWindowContents(Rect inRect)
        {
            inRect = inRect.ContractedBy(15f, 7f);
            Widgets.Label(inRect.TopPartPixels(60f), "BEWH.ChooseDaemon".Translate());
            inRect.y += 60f;
            List<Rect> rects = Split(inRect, choices.Count, new Vector2(80f, 200f));
            for (int i = 0; i < choices.Count; i++)
            {
                Pawn pawn = choices[i];
                if (pawn.Dead)
                {
                    ResurrectionUtility.TryResurrect(pawn);
                }
                Rect item3 = rects[i];
                Rect rect2 = new Rect(item3.x, item3.y, 80f, 80f);
                Rect rect3 = new Rect(item3.x - 10, item3.y + 150f, 100f, 30f);
                Widgets.ThingIcon(rect2, pawn);

                if (Widgets.ButtonInvisible(rect2))
                {
                    Find.WindowStack.Add(new Dialog_InfoCard(pawn));
                }
                if (Widgets.ButtonText(rect3, "BEWH.SelectDaemon".Translate()))
                {
                    Current.Game.GetComponent<GameComponent_DaemonPrince>().ResurrectDaemon(pawn, position, map);
                    Close();
                    break;
                }
            }
        }

        private static List<Rect> Split(Rect rect, int parts, Vector2 size, bool vertical = false)
        {
            List<Rect> result = new List<Rect>();
            float distance = (vertical ? rect.height : rect.width) / (float)parts;
            Vector2 curLoc = new Vector2(rect.x, rect.y);
            Vector2 offset = (vertical ? new Vector2(0f, distance / 2f - size.y / 2f) : new Vector2(distance / 2f - size.x / 2f, 0f));
            for (float i = 0f; i < (vertical ? rect.height : rect.width); i += distance)
            {
                result.Add(new Rect(curLoc + offset, size));
                if (vertical)
                {
                    curLoc.y += distance;
                }
                else
                {
                    curLoc.x += distance;
                }
            }
            return result;
        }
    }
}