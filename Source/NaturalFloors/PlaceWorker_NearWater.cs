using System;
using Verse;
using RimWorld;
namespace NaturalFloors
{
    public class PlaceWorker_NearWater : PlaceWorker
    {
        public bool water = false;
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot)
        {
            foreach (IntVec3 current in GenRadial.RadialCellsAround(loc, 5f, true))
            {
                if(current.GetTerrain() == TerrainDef.Named("WaterDeep") || current.GetTerrain() == TerrainDef.Named("WaterShallow") || current.GetTerrain() == TerrainDef.Named("Marsh"))
                {
                    water = true;
                }         
            }
            if(water == false)
            {
                return new AcceptanceReport("Must be near water source.");
            }
            else
            {
                return true;
            }
        }
    }
}
