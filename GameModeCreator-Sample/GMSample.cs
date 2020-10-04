using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAPI = Exiled.API.Features;
using EHandlers = Exiled.Events.Handlers;

namespace GameModeCreator.Sample
{
    public class GMSample : API.Gamemode
    {
        public override string Name { get; } = "Sample";
        private Handlers.Player player;
        private Handlers.Server server;

        public override void RegisterEvents()
        {
            player = new Handlers.Player();
            server = new Handlers.Server();

            EHandlers.Player.Hurting += player.OnHurting;
            EHandlers.Player.Dying += player.OnDying;
            EHandlers.Player.ChangingItem += player.OnItemChanged;
        }
        public override void UnregisterEvents()
        {
            EHandlers.Player.Hurting -= player.OnHurting;
            EHandlers.Player.Dying -= player.OnDying;
            EHandlers.Player.ChangingItem -= player.OnItemChanged;

            player = null;
            server = null;
        }

        public override void OnRoundStart() => server?.OnRoundStart();
    }
}
