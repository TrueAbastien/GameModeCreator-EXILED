using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GameModeCreator.API;
using HarmonyLib;
using Exiled.Events.EventArgs;

namespace GameModeCreator.Loader
{
    public static class Loader
    {
        public static void Load(Assembly assembly, out Harmony harmony)
        {
            AssemblyRegister(assembly);
            Patcher.OnLoad(out harmony);

            Gamemode.current.RegisterEvents();
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnd;
        }

        public static void Unload(Assembly assembly, Harmony harmony)
        {
            Gamemode.current.UnregisterEvents();
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnd;

            Patcher.UnPatch(harmony);
            Gamemode.Unregister();
            Config.configs.Clear();
        }

        private static void OnRoundStart() => Gamemode.current?.OnRoundStart();
        private static void OnRoundEnd(RoundEndedEventArgs ev) => Gamemode.current?.OnRoundEnd();

        private static void AssemblyRegister(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.BaseType == typeof(Gamemode))
                {
                    var gm = (Gamemode)Activator.CreateInstance(type);
                    gm.Register();
                }

                else if (type.BaseType.IsGenericType)
                    if (type.BaseType.GetGenericTypeDefinition() == typeof(API.Config.GMConfig<>))
                        Config.RegisterProperties(type);
            }
        }
    }
}
