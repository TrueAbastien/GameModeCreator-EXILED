using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.Core
{
    public class Config : IConfig
    {
        [Description("Verify whether the Gamemode should be loaded or not")]
        public bool IsEnabled { get; set; } = true;
    }
}
