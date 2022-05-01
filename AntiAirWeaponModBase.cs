using HugsLib;
using HugsLib.Utils;
using System;
using System.Collections.Generic;
using Verse;

namespace AntiAirWeapon
{
    public class AntiAirWeaponModBase : ModBase
    {

        private static List<Action> TickActions = new List<Action>();


        public static AntiAirWeaponModBase Instance { get; private set; }
        public AllMapProjectileStorage _AllMapProjectileStorage { get { return Find.World.GetComponent<AllMapProjectileStorage>(); }   }

        public AntiAirWeaponModBase()
        {
            Instance = this;
        }

        public static void RegisterTickAction(Action action)
        {
            TickActions.Add(action);
        }

        public override void Tick(int currentTick)
        {
            foreach (Action action in TickActions)
            {
                action();
            }
            TickActions.Clear();
        }

        public override void WorldLoaded()
        {

           
            base.WorldLoaded();


        }









        public override string ModIdentifier =>
                "AntiAirWeapon";
    }


}
