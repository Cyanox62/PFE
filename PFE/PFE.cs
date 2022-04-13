using System;
using Exiled.API.Features;
using Handlers = Exiled.Events.Handlers;

namespace PFE
{
    public class Plugin : Plugin<Config>
    {
		public override string Author { get; } = "Cyanox";
		public override string Name { get; } = "PFE";
		public override string Prefix { get; } = "PFE";
		public override Version Version { get; } = new Version(3, 1, 0);
		public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);


		public static Plugin Singleton;
		public PlayerEvents PlayerEvents;

		public override void OnEnabled() 
		{
			if (!Config.IsEnabled) return;

			base.OnEnabled();

			Singleton = this;
			PlayerEvents = new PlayerEvents(this);

            Handlers.Player.Dying += PlayerEvents.OnPlayerDeath;
		}

		public override void OnDisabled() 
		{
			base.OnDisabled();

			Handlers.Player.Dying -= PlayerEvents.OnPlayerDeath;

			PlayerEvents = null;

		}
	}
}
