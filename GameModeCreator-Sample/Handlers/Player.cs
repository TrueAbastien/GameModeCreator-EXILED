using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.API.Extensions;

namespace GameModeCreator.Sample.Handlers
{
    public class Player
    {
        internal void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Target.Role == RoleType.Scp0492)
            {
                ev.Amount = ev.Attacker?.IsAlive ?? false ? ev.DamageType == DamageTypes.Usp ? 10000 : 0 : 0;
            }
        }

        internal void OnDying(DyingEventArgs ev)
        {
            if (ev.Target.Role == RoleType.Scp0492)
            {
                if (ev.Killer != null)
                {
                    Methods.GiveAmmo(Configs.AmountConfig.AmmoGainAmount, AmmoType.Nato9, ev.Killer);
                }

                Timing.CallDelayed(.5f, () =>
                {
                    if (Round.ElapsedTime.TotalSeconds < Configs.TimeConfig.GameTime)
                    {
                        ev.Target?.SetRole(RoleType.Scp0492);
                    }
                });
            }
        }

        internal void OnItemChanged(ChangingItemEventArgs ev)
        {
            if (ev.NewItem.id.IsWeapon())
            {
                ev.Player.ReferenceHub.playerEffectsController.EnableEffect<CustomPlayerEffects.Disabled>();
            }
            else ev.Player.ReferenceHub.playerEffectsController.DisableEffect<CustomPlayerEffects.Disabled>();
        }
    }
}
