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
    public class FindSurvivorCmd : API.Command.GMCommand<GMSample>
    {
        public override string Name { get; } = "find";

        public override string[] Surnames { get; } = { "camp", "where" };

        public override string Description { get; } = "Broadcast a Survivor position to everyone.";

        public override string Permission { get; } = API.Command.Permission.Mod;

        public override bool Run(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player target;
            if ((target = Player.List.ElementAt(0)) != null)
            {
                string msg = $"The survivor {target.Nickname} is located in {target.CurrentRoom.Name} !";
                if (arguments.Count() > 0)
                {
                    if (arguments.ElementAt(0).ToLower().StartsWith("z"))
                    {
                        Player.List.Where(e => e.Role == RoleType.Scp0492).Do(e => e.Broadcast(5, msg));

                        response = $"The command has successfully broadcast {target.Nickname} position to every Zombies !";
                        return true;
                    }
                    else
                    {
                        response = "Command unknown, if you wish to broadcast a survivor position to Zombies: only give 'z' as a first argument...";
                        return false;
                    }
                }

                Map.Broadcast(5, msg);
                response = $"Everyone is now aware of {target.Nickname} position !";
                return true;
            }
            else response = "Couldn't find a target to broadcast...";

            return false;
        }
    }
}
