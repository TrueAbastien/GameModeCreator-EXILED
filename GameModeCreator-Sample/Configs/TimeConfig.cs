using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Sample.Configs
{
    public class TimeConfig : API.Config.GMConfig<GMSample>
    {
        [API.Config.Attributes.Description("Time it takes for a Zombie to respawn")]
        public static float RespawnTime { get; set; } = 5f;

        [API.Config.Attributes.Description("Time the survivors need to last")]
        public static float GameTime { get; set; } = 300f;
    }
}
