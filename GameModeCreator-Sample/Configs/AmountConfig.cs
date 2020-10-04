using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Sample.Configs
{
    public class AmountConfig : API.Config.GMConfig<GMSample>
    {
        [API.Config.Attributes.Description("Amount of ammo gained on Zombie killed")]
        public static uint AmmoGainAmount { get; set; } = 5;

        [API.Config.Attributes.Description("Amount of people spawning as Class D")]
        public static uint SurvivorAmount { get; set; } = 3;
    }
}
