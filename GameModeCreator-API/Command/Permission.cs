using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.API.Command
{
    public static class Permission
    {
        public static string GET { get; } = "gm.get";
        public static string SET { get; } = "gm.set";
        public static string PUT { get; } = "gm.put";
        public static string DEL { get; } = "gm.del";

        public static string Manager { get; } = "gm.manager";
        public static string Admin { get; } = "gm.admin";
        public static string Mod { get; } = "gm.mod";
        public static string User { get; } = "gm.user";
    }
}
