using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Chaos40k
{
    public class GameComponent_DaemonPrince : GameComponent
    {

        private List<Pawn> daemonPawns = new List<Pawn>();

        public List<Pawn> DaemonPawns => daemonPawns;

        public GameComponent_DaemonPrince(Game game)
        {
        }

        public void AddDaemonToPool(Pawn p)
        {
            daemonPawns.Add(p);
        }

        public void ResurrectDaemon(Pawn p, IntVec3 position, Map map)
        {
            Mesh boltMesh = LightningBoltMeshPool.RandomBoltMesh;
            Material LightningMat = MatLoader.LoadMat("Weather/LightningBolt");
            Graphics.DrawMesh(boltMesh, position.ToVector3ShiftedWithAltitude(AltitudeLayer.Weather), Quaternion.identity, FadedMaterialPool.FadedVersionOf(LightningMat, 1), 0);

            //ResurrectionUtility.TryResurrect(p);

            GenSpawn.Spawn(p, position, map);
            
            DaemonDoStrike(p.Position, map, ref boltMesh);
            daemonPawns.Remove(p);
        }

        private static void DaemonDoStrike(IntVec3 strikeLoc, Map map, ref Mesh boltMesh)
        {
            SoundDefOf.Thunder_OffMap.PlayOneShotOnCamera(map);
            if (!strikeLoc.IsValid)
            {
                strikeLoc = CellFinderLoose.RandomCellWith((IntVec3 sq) => sq.Standable(map) && !map.roofGrid.Roofed(sq), map);
            }
            boltMesh = LightningBoltMeshPool.RandomBoltMesh;
            if (!strikeLoc.Fogged(map))
            {
                Vector3 loc = strikeLoc.ToVector3Shifted();
                for (int i = 0; i < 4; i++)
                {
                    FleckMaker.ThrowSmoke(loc, map, 1.5f);
                    FleckMaker.ThrowMicroSparks(loc, map);
                    FleckMaker.ThrowLightningGlow(loc, map, 1.5f);
                }
            }
            SoundInfo info = SoundInfo.InMap(new TargetInfo(strikeLoc, map));
            SoundDefOf.Thunder_OnMap.PlayOneShot(info);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref daemonPawns, "daemonPawns", LookMode.Reference);
        }
    }
}