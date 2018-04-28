#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

using System;
using Agonyl.Game.Network;
using Agonyl.Game.Util;
using Agonyl.Game.Util.Config;
using Agonyl.Shared;
using Agonyl.Shared.Database;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Game
{
	class GameServer : Server
	{
		public static readonly GameServer Instance = new GameServer();

		/// <summary>
		/// Configuration.
		/// </summary>
		public GameConf Conf { get; private set; }

		/// <summary>
		/// Game server's database.
		/// </summary>
		public AgonylDb Database { get; private set; }

		/// <summary>
		/// LoginServer console commands.
		/// </summary>
		public GameConsoleCommands ConsoleCommands { get; private set; }

		/// <summary>
		/// Starts the server.
		/// </summary>
		public override void Run()
		{
			base.Run();

			CliUtil.WriteHeader("Game Server", ConsoleColor.Magenta);
			CliUtil.LoadingTitle();

			// Conf
			this.LoadConf(this.Conf = new GameConf());

			// Database
			this.InitDatabase(this.Database = new AgonylDb(), this.Conf);

			// Packet handlers
			GamePacketHandler.Instance.RegisterMethods();

			// Server
			var mgr = new ConnectionManager<GameConnection>(this.Conf.Host, this.Conf.Port);
			mgr.Start();

			// Ready
			CliUtil.RunningTitle();
			Log.Status("Server ready, listening on {0}.", mgr.Address);

			// Commands
			this.ConsoleCommands = new GameConsoleCommands();
			this.ConsoleCommands.Wait();
		}
	}
}
