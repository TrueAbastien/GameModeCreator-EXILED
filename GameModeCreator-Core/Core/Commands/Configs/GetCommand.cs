using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using System.Reflection;

namespace GameModeCreator.Core.Commands.Configs
{
    /// <summary>
    /// Get the value of a specific Config property linked to the specified Gamemode.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GetCommand : API.Command.MetaCommand
    {
        /// <inheritdoc/>
        public override string Command { get; } = "gm.config.get";
        /// <inheritdoc/>
        public override string[] Aliases { get; } = { };
        /// <inheritdoc/>
        public override string Description { get; } = "Get a proprety value from dynamic config.";

        /// <inheritdoc/>
        public override string Permission { get; } = API.Command.Permission.GET;

        /// <inheritdoc/>
        public override bool Compute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // Verify the following arguments: Gamemode prefix, Config property name.
            if (arguments.Count >= 2)
            {
                // Search and verify send Gamemode type
                Type type = arguments.ElementAt(0).ToLower() == API.Gamemode.current.Prefix.ToLower() ? API.Gamemode.current.GetType() : null;
                if (type != null)
                {
                    // Try to get configs linked to the specified Gamemode
                    if (Loader.Config.TryGetConfig(type, out IEnumerable<API.Config.MetaConfig> value))
                    {
                        // Look for specified property through every Configs
                        foreach (API.Config.MetaConfig config in value)
                        {
                            // When found, print out its description and value in response
                            if (config.properties.TryGetValue(arguments.ElementAt(1).ToLower(), out PropertyInfo info))
                            {
                                response = $"{ arguments.ElementAt(1) }, { (info.GetCustomAttribute(typeof(API.Config.Attributes.Description)) as API.Config.Attributes.Description)?.Text ?? "x" }, has the value: { info.GetValue(null, null) }.";
                                return true;
                            }
                        }
                        response = $"{ arguments.ElementAt(1) } property couldn't be found, is this for the right Gamemode ?";
                    }
                    else response = "No dynamic config has been registered to the selected Gamemode...";
                }
                else response = "No Gamemode could be found, is this a correct prefix ?";
            }
            else response = "Too few arguments were given, expecting a Gamemode prefix & a proprety name.";

            return false;
        }
    }
}
