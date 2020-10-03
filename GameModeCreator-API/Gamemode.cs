using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.API
{
    public abstract class Gamemode
    {
        public static Gamemode current { get; protected set; }
        public void Register() => current = this;
        public static void Unregister() => current = null;

        public abstract string Name { get; }
        public virtual string Prefix { get { return Name; } }

        /// <summary>
        /// Register event handlers on load.
        /// </summary>
        public abstract void RegisterEvents();
        /// <summary>
        /// Unregister event handlers on unload.
        /// </summary>
        public abstract void UnregisterEvents();

        /// <summary>
        /// Called on each Round Start.
        /// </summary>
        public virtual void OnRoundStart() { }
        /// <summary>
        /// Called on each Round End.
        /// </summary>
        public virtual void OnRoundEnd() { }
    }
}
