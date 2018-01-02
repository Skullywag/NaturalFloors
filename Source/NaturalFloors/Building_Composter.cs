using System.Linq;
using Verse;
using RimWorld;
namespace NaturalFloors
{
    public class Building_Composter : Building_Storage
    {
        private readonly Thing compost = ThingMaker.MakeThing(ThingDef.Named("freshCompost"));
        private readonly DamageInfo damageInfo = new DamageInfo(DamageDefOf.Rotting, 1);

        public override void TickRare()
        {
            //List all things in cell
            var thingsInComposter = Map.thingGrid.ThingsListAt(Position);
            if (thingsInComposter.Count <= 0)
            {
                return;
            }

            //Loop over things
            foreach (var thing in thingsInComposter.ToList())
            {
                if (thing.def.category != ThingCategory.Item || thing.def.defName == "freshCompost")
                {
                    continue;
                }
                
                if (thing.def.defName.Contains("Corpse"))
                {
                    var corpse = thing as Corpse;
                    if (corpse.InnerPawn.def.race.Animal || corpse.InnerPawn.def.race.Humanlike)
                    {
                        thing.TakeDamage(damageInfo);
                        compost.stackCount = 25;
                    }
                }
                else
                {
                    thing.TakeDamage(damageInfo);
                    compost.stackCount = thing.stackCount;
                }

                if (thing.HitPoints < 10)
                {
                    thing.Destroy();
                    GenSpawn.Spawn(compost, Position, Map);
                }
            }
        }
    }
}
