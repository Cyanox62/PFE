using UnityEngine;
using EXILED;
using Grenades;
using Mirror;
using System.Collections.Generic;

namespace EFE
{
	public class EFE : EXILED.Plugin
	{
		private EventHandlers EventHandlers;

		public static int magnitude;

		public override void OnEnable() 
		{
			if (!Config.GetBool("efe_enable", true)) // Enable config
				return;

			// We make our own dictionary stuff because the .GetStringDictionary of 'config' me and joker don't know how it works lol.
			string[] drops = Config.GetString("efe_setup", "Scp173:1,Scp0492:1").Split(',');

			EFEs cDrops = new EFEs();

			foreach (string drop in drops)
			{
				string[] d = drop.Split(':'); // d[0] = item, d[1] = amount
				cDrops.AddToList(d[0], int.Parse(d[1]));
			}

			EventHandlers = new EventHandlers(cDrops);
			Events.PlayerDeathEvent += EventHandlers.OnPlayerDeath;
		}

		public override void OnDisable() 
		{
			Events.PlayerDeathEvent -= EventHandlers.OnPlayerDeath;
			EventHandlers = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "EFE";
	}

	class EventHandlers
	{
		public EFEs allowedItems;
		public EventHandlers(EFEs drops)
		{
			allowedItems = drops;
		}
		public void OnPlayerDeath(ref PlayerDeathEvent ev)
		{
			foreach (KeyValuePair<RoleType, int> drop in allowedItems.drops)
			{

				if (ev.Player.characterClassManager.CurClass == drop.Key)
				{
					for (int i = 0; i < drop.Value; i++)
					{
						Grenade grenade = GameObject.Instantiate(ev.Player.GetComponent<GrenadeManager>().availableGrenades[0].grenadeInstance).GetComponent<Grenade>();
						grenade.InitData(ev.Player.GetComponent<GrenadeManager>(), Vector3.zero, Vector3.zero);
						NetworkServer.Spawn(grenade.gameObject);
						grenade.NetworkfuseTime = 0f;
					}
				}
			}
		}
	}
}
