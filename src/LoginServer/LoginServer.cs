#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using Agonyl.Login.Network;
using Agonyl.Login.Util;
using Agonyl.Login.Util.Config;
using Agonyl.Shared;
using Agonyl.Shared.Database;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Login
{
    public class LoginServer : Server
    {
        public static readonly LoginServer Instance = new LoginServer();

        /// <summary>
        /// Configuration.
        /// </summary>
        public LoginConf Conf { get; private set; }

        /// <summary>
        /// LoginServer console commands.
        /// </summary>
        public LoginConsoleCommands ConsoleCommands { get; private set; }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public override void Run()
        {
            base.Run();

            CliUtil.WriteHeader("Login Server", ConsoleColor.Magenta);
            CliUtil.LoadingTitle();

            Log.Status("Starting Login Server...");

            // Conf
            this.LoadConf(this.Conf = new LoginConf());

            // ASD Database
            this.InitDatabase(this.ASDDatabase = new ASD(), this.Conf);

            // Redis server
            this.Redis = new Redis(this.Conf.RedisHost, this.Conf.RedisPort, this.Conf.RedisPassword);

            // Remove previously logged in accounts
            this.Redis.ResetLoggedInAccountList();

            // Packet handlers
            LoginPacketHandler.Instance.RegisterMethods();

            // Server
            var mgr = new ConnectionManager<LoginConnection>(this.Conf.Host, this.Conf.Port);
            mgr.Start();

            // Ready
            CliUtil.RunningTitle();
            Log.Status("Login Server is ready, listening on {0}.", mgr.Address);

            // Commands
            this.ConsoleCommands = new LoginConsoleCommands();
            this.ConsoleCommands.Wait();
        }
    }
}
