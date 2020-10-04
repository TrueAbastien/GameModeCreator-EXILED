using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
using HarmonyLib;
using System.Reflection;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace GameModeCreator.Core
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;

        public override string Name => "Game Mode Creator";
        public override string Prefix => "GMC";
        public override string Author => "<pseudo>";

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;

            base.OnEnabled();
            Loader.Loader.Load(Assembly, out harmony);
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
            Loader.Loader.Unload(Assembly, harmony);
        }
    }
}
