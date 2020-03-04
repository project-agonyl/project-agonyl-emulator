#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

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
    internal class GameServer : Server
    {
        public static readonly GameServer Instance = new GameServer();

        /// <summary>
        /// Configuration.
        /// </summary>
        public GameConf Conf { get; private set; }

        /// <summary>
        /// Game server's database.
        /// </summary>
        public ASD ASDDatabase { get; private set; }

        /// <summary>
        /// GameServer console commands.
        /// </summary>
        public GameConsoleCommands ConsoleCommands { get; private set; }

        /// <summary>
        /// LoginServer IPC handler
        /// </summary>
        public GameConnection LoginServerConnection { get; private set; }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public override void Run()
        {
            base.Run();

            CliUtil.WriteHeader("Game Server", ConsoleColor.Magenta);
            CliUtil.LoadingTitle();

            Log.Status("Starting Game Server...");

            // Conf
            this.LoadConf(this.Conf = new GameConf());

            // Database
            this.InitDatabase(this.ASDDatabase = new ASD(), this.Conf);

            // Redis server
            this.Redis = new Redis(this.Conf.RedisHost, this.Conf.RedisPort, this.Conf.RedisPassword);

            // Packet handlers
            GamePacketHandler.Instance.RegisterMethods();

            // Server
            var mgr = new ConnectionManager<GameConnection>(this.Conf.Host, this.Conf.Port);
            mgr.Start();

            // Ready
            CliUtil.RunningTitle();
            Log.Status("Game Server is ready, listening on {0}.", mgr.Address);

            // Commands
            this.ConsoleCommands = new GameConsoleCommands();
            this.ConsoleCommands.Wait();
        }
    }
}
