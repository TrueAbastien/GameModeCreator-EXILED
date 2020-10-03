using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.API.Patch.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PatchOf : Attribute
    {
        public PatchOf(Type gm)
        {
            if (gm.BaseType == typeof(Gamemode))
                gamemode = gm;
        }
        public Type gamemode;
    }
}
