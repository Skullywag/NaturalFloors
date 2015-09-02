using Verse;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse.Sound;
using RimWorld;
namespace NaturalFloors
{
	public class Building_Composter : Building_Storage
	{
		public override void SpawnSetup()
		{
			base.SpawnSetup();
		}

        public override void TickRare()
        {
            //List all things in cell
            List<Thing> list = Find.ThingGrid.ThingsListAt(Position);
            if (list.Count > 0)
            {
                //Loop over things
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].def.category == ThingCategory.Item)
                    {
                        if (list[i].def.defName != "FreshCompost")
                        {
                            DamageInfo dinfo = new DamageInfo(DamageDefOf.Rotting, 1, null, null, null);
                            Thing compost = ThingMaker.MakeThing(ThingDef.Named("FreshCompost"));
                            Thing thing = list[i];
                            int stacksize = thing.stackCount;
                            if (thing.def.defName.Contains("Corpse"))
                            {
                                Corpse corpse = thing as Corpse;
                                if (corpse.innerPawn.def.race.Animal || corpse.innerPawn.def.race.Humanlike)
                                {
                                    thing.TakeDamage(dinfo);
                                    compost.stackCount = 25;
                                }      
                            }
                            else
                            {
                                thing.TakeDamage(dinfo);
                                compost.stackCount = stacksize;
                            }
                            if (thing.HitPoints < 10)
                            {
                                thing.Destroy();
                                GenSpawn.Spawn(compost, Position);
                            }
                        }
                    }
                }
            }
        }
	}
}
