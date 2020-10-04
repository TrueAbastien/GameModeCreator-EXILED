using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Sample
{
    public static class Methods
    {
        public static void GiveAmmo(uint amount, AmmoType ammo, params Player[] players)
        {
            players.Do(e => e.Ammo[(int)ammo] += amount);
        }

        public static void SpawnAsSurvivor(Player p)
        {
            p.ReferenceHub.characterClassManager.SetPlayersClass(RoleType.ClassD, p.ReferenceHub.gameObject);
            p.AddItem(ItemType.GunUSP);
        }

        public static void SpawnAsZombie(Player p)
        {
            p.SetRole(RoleType.Scp0492);
            p.Position = Handlers.Server.ZSpawnPos;
        }
    }
}
