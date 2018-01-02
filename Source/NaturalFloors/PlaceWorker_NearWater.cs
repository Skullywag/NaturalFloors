using System.Collections.Generic;
using Verse;
namespace NaturalFloors
{
    public class PlaceWorker_NearWater : PlaceWorker
    {
        private readonly List<string> waterSourceNames = new List<string>()
        {
            "WaterDeep",
            "WaterOceanDeep",
            "WaterMovingDeep",
            "WaterShallow",
            "WaterOceanShallow",
            "WaterMovingShallow",
            "Marsh",
            "Ice" //maybe remove?
        };

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 location, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            foreach (var current in GenRadial.RadialCellsAround(location, 5f, true))
            {
                foreach (var waterSourceName in waterSourceNames)
                {
                    if (current.GetTerrain(map) == TerrainDef.Named(waterSourceName))
                    {
                        return true;
                    }
                }
            }

            return "MustBeNearWaterSource".Translate();
        }
    }
}
