using HugsLib.Utils;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace AntiAirWeapon
{
    public class AllMapProjectileStorage : WorldComponent, IExposable
    {
        public List<Thing> mapAndThings = new List<Thing>();
        //public Dictionary<int, AirTargets> mapAndThings = new Dictionary<int, AirTargets>();
        //List<int> maps;
        //List<AirTargets> things;

        public AllMapProjectileStorage(World world) : base(world)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref this.mapAndThings, "mapAndThings",   LookMode.Reference);//,ref maps,ref things);
        }

        public bool invalidThing(Thing thing) {
            if (thing == null || thing.Map == null || !thing.Spawned||thing.Destroyed)
            {
                return true;
            }
            return false;
        }

 



        public Thing mapHasThing(Thing thing ) {
            //Log.Message("Gtep1");
            if (invalidThing(thing)) {
                //Log.Message("Gtep1-1");
                //Log.Message("----这玩意儿无了");
                return null;
            }
            //Log.Message("Gtep2");
            bool hasList = mapAndThings != null;//mapAndThings.TryGetValue(thing.Map.Index , out AirTargets thingList);
            //Log.Message("Gtep3");
            if (hasList&& mapAndThings != null&& mapAndThings.Count>0) {
                //Log.Message("Gtep3-1");
                //Log.Message("----有物品列表");
                mapAndThings.RemoveAll(x => invalidThing(x));
                
                //Log.Message("Gtep3-2");
                Thing result=
                 mapAndThings.Find(a => invalidThing(a) &&

                    a.thingIDNumber == thing.thingIDNumber



                ) ;
                //Log.Message("Gtep3-3");
                //Log.Message("----has结果:"+result);
                return result;
            }
            //Log.Message("Gtep4");

            return null;
        }

        public void addThing(Thing thing)
        {
            //Log.Message("===========Step1");
            if (invalidThing(thing))
            {
                //Log.Message("Step1-end");
                return  ;
            }
            //Log.Message("Step2");
            bool hasList = mapAndThings != null;//.TryGetValue(thing.Map.Index , out AirTargets thingList);
            //Log.Message("Step3");
            if (!hasList)
            {

                mapAndThings = new List<Thing>();
                 
            }
            //Log.Message("Step4");
            if (mapHasThing(thing) == null)
            {
                //Log.Message("Step4-1");
                mapAndThings.Add(thing);
                //Log.Message("Step4-2");
                //Log.Message("----添加进入全局map物品" + thing.def.defName + "!");
            }
            else {
                //Log.Message("----已经有该物品" + thing.def.defName + "!");
            }

            //Log.Message("==========StepEnd===================");

            return ;
        }
        //清除 
        public bool removeThingsFromGlobal(int mapIndex, Thing thing)
        {

            bool hasList = mapAndThings != null;//.TryGetValue(mapIndex, out AirTargets thingList);
            if (hasList)
            {
                //Log.Message("----移除物品" + thing.def.defName + "!移除前剩余物品:"+thingList.Count);
                                 
                return mapAndThings.Remove(thing);
            }
            return false;
             
        }
    }
}