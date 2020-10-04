using HarmonyLib;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EAPI = Exiled.API.Features;

namespace GameModeCreator.Sample.Handlers
{
    public class Server
    {
        public static Vector3 ZSpawnPos { get; private set; }

        internal void OnRoundStart()
        {
            var players = EAPI.Player.List.ToList();
            players.ShuffleList();
            int size;

            if ((size = players.Count) < 2)
            {
                EAPI.Log.Error("Too few players are connected to start the event properly...");
            }
            else
            {
                ZSpawnPos = EAPI.Map.Rooms.Where(e => e.Name.StartsWith("LCZ_372")).First().Position + new Vector3(0, 2f);
                EAPI.Map.Rooms.Where(e => e.Name.StartsWith("LCZ")).SelectMany(e => e.Doors).Distinct().Do(e =>
                {
                    e.SetState(true);
                    e.SetLock(true);
                });
                EAPI.Map.Doors.Where(e => e.DoorName.StartsWith("CHECKPOINT_LCZ")).Do(e =>
                {
                    e.SetState(false);
                    e.SetLock(true);
                });

                Timing.CallDelayed(.5f, () =>
                {
                    players[0].SetRole(RoleType.Scp0492);
                    int ii = 0;
                    for (; ii < size && ii < Configs.AmountConfig.SurvivorAmount;)
                        Methods.SpawnAsSurvivor(players[++ii]);
                    for (; ii < size;)
                        Methods.SpawnAsZombie(players[++ii]);
                });
            }
        }
    }
}
