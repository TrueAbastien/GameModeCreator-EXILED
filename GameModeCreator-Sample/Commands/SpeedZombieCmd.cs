using CommandSystem;
using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Sample.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpeedZombieCmd : API.Command.GMCommand<GMSample>
    {
        public override string Name { get; } = "boost";

        public override string[] Surnames { get; } = { "speed", "anger" };

        public override string Description { get; } = "Boost a zombie speed, either by 1 or by a specified amount.";

        public override string Permission { get; } = API.Command.Permission.Admin;

        public override bool Run(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            int amount = 1;
            if (arguments.Count > 0)
            {
                if (!int.TryParse(arguments.ElementAt(0), out amount))
                {
                    response = "First argument is incorrect, expecting an integer...";
                    return false;
                }
            }

            var zombies = Player.List.Where(e => e.Role == RoleType.Scp0492);
            for (int ii = 0; ii < amount; ++ii)
            {
                zombies.Do(e => e.ReferenceHub.playerEffectsController.EnableEffect<CustomPlayerEffects.Scp207>());
            }

            response = $"Every zombies has got its speed boosted by {amount} !";
            return true;
        }
    }
}
