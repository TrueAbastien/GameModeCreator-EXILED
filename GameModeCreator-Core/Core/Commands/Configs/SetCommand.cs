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
    /// Set the value of a specific Config property linked to the specified Gamemode.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetCommand : API.Command.MetaCommand
    {
        /// <inheritdoc/>
        public override string Command { get; } = "gm.config.set";
        /// <inheritdoc/>
        public override string[] Aliases { get; } = { };
        /// <inheritdoc/>
        public override string Description { get; } = "Set a proprety value from dynamic config.";

        /// <inheritdoc/>
        public override string Permission { get; } = API.Command.Permission.SET;

        /// <inheritdoc/>
        public override bool Compute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // Verify the following arguments: Gamemode prefix, Config property name, new value.
            if (arguments.Count >= 3)
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
                            // When found, try to change its value
                            if (config.properties.TryGetValue(arguments.ElementAt(1).ToLower(), out PropertyInfo info))
                            {
                                try
                                {
                                    info.SetValue(null, Convert.ChangeType(arguments.ElementAt(2), info.PropertyType), null);
                                }
                                catch (Exception)
                                {
                                    response = $"An exception has occured, its type and/or the string format might be incorrect...";
                                    return false;
                                }

                                response = $"{ arguments.ElementAt(1) }, { (info.GetCustomAttribute(typeof(API.Config.Attributes.Description)) as API.Config.Attributes.Description)?.Text ?? "x" }, was set to: { arguments.ElementAt(2) }.";
                                return true;
                            }
                        }
                        response = $"{ arguments.ElementAt(1) } property couldn't be found, is this for the right Gamemode ?";
                    }
                    else response = "No dynamic config has been registered to the selected Gamemode...";
                }
                else response = "No Gamemode could be found, is this a correct prefix ?";
            }
            else response = "Too few arguments were given, expecting a Gamemode prefix, a proprety name and a value.";

            return false;
        }
    }
}
