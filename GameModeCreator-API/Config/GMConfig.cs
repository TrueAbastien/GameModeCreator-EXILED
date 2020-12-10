using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GameModeCreator.API.Config
{
    public abstract class MetaConfig
    {
        public Type type { get; protected set; }
        public MetaConfig(Type gm) => type = gm;
        public Dictionary<string, PropertyInfo> properties { get; protected set; } = new Dictionary<string, PropertyInfo>();
    }
    public abstract class GMConfig<GM> : MetaConfig where GM : Gamemode
    {
        public GMConfig() : base(typeof(GM)) { }
    }
}
