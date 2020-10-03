using GameModeCreator.API.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Loader
{
    public static class Config
    {
        public static Dictionary<Type, MetaConfig> configs { get; private set; } = new Dictionary<Type, MetaConfig>();

        public static void RegisterProperties(Type type)
        {
            // Add new Config
            configs.Add(type, (MetaConfig)Activator.CreateInstance(type));

            // Register properties
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.GetCustomAttribute(typeof(API.Config.Attributes.Description)) != null)
                {
                    configs[configs.Last().Key].properties.Add(property.Name.ToLower(), property);
                }
            }
        }

        public static bool TryGetConfig(Type gamemode, out IEnumerable<MetaConfig> res)
        {
            res = configs.Values.Where(e => e.type == gamemode);
            return res.Count() > 0;
        }
    }
}
