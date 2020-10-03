using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;

namespace GameModeCreator.API.Command
{
    /// <summary>
    /// Original MetaCommand limiting its current Permission.
    /// </summary>
    public abstract class MetaCommand : ICommand
    {
        private bool IsAllowed(CommandSender sender, string permission)
        {
            return sender.FullPermissions || Exiled.Permissions.Extensions.Permissions.CheckPermission(sender, permission);
        }

        /// <inheritdoc/>
        public abstract string Command { get; }
        /// <inheritdoc/>
        public abstract string[] Aliases { get; }
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <summary>
        /// Permission necessary to execute the current command.
        /// </summary>
        public virtual string Permission { get { return string.Empty; } }

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // Verify if permission allows it
            if (!Permission.IsEmpty())
            {
                try
                {
                    if (!IsAllowed((CommandSender)sender, Permission))
                    {
                        response = "Command failed, you do not have permission to run this command !";
                        return false;
                    }
                }
                catch (Exception)
                {
                    response = "Unexpected error, sender should not be allowed to run this command...";
                    return false;
                }
            }
            return Compute(arguments, sender, out response);
        }
        /// <summary>
        /// Equivalent of Execute method, compute if permission allows it.
        /// </summary>
        /// <param name="arguments">Array of string arguments passed to its execution</param>
        /// <param name="sender">Sender's data</param>
        /// <param name="response">Outputed response message from the command</param>
        /// <returns>Verification of command validity</returns>
        public abstract bool Compute(ArraySegment<string> arguments, ICommandSender sender, out string response);
    }

    /// <summary>
    /// Gamemode Command abstract class, basic Command called only when his Gamemode type is same as current.
    /// </summary>
    /// <typeparam name="GM">Gamemode allowing its execution</typeparam>
    public abstract class GMCommand<GM> : MetaCommand where GM : Gamemode
    {
        /// <summary>
        /// Gamemode reference the Command is linked to.
        /// </summary>
        protected Gamemode gamemode;
        /// <summary>
        /// Default constructor, instantiate typeparam as linked Gamemode type.
        /// </summary>
        public GMCommand() => gamemode = Gamemode.current.GetType() == typeof(GM) ? Gamemode.current : null;

        /// <summary>
        /// Name of the command to call.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Other names the command can be called by.
        /// </summary>
        public abstract string[] Surnames { get; }

        /// <inheritdoc/>
        public override string Command { get { return $"gm.{ gamemode.Prefix.ToLower() }.{ Name.ToLower() }"; } }
        /// <inheritdoc/>
        public override string[] Aliases { get { return Surnames.Select(e => $"gm.{ gamemode.Prefix.ToLower() }.{ e.ToLower() }").ToArray(); } }
        /// <inheritdoc/>
        public override abstract string Description { get; }

        /// <inheritdoc/>
        public override bool Compute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            // Verify the type of the currently loaded Gamemode
            if (gamemode.GetType() != Gamemode.current?.GetType())
            {
                response = $"Command unknown, { gamemode.Name } is not currently running...";
                return false;
            }
            return Run(arguments, sender, out response);
        }
        /// <summary>
        /// Equivalent of Execute method, run if execution is allowed.
        /// </summary>
        /// <param name="arguments">Array of string arguments passed to its execution</param>
        /// <param name="sender">Sender's data</param>
        /// <param name="response">Outputed response message from the command</param>
        /// <returns>Verification of command validity</returns>
        public abstract bool Run(ArraySegment<string> arguments, ICommandSender sender, out string response);
    }
}
