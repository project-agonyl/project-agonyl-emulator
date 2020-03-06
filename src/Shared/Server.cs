#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.IO;
using Agonyl.Shared.Data;
using Agonyl.Shared.Database;
using Agonyl.Shared.Util;
using Agonyl.Shared.Util.Config;

namespace Agonyl.Shared
{
    /// <summary>
    /// Base class for server applications.
    /// </summary>
    public abstract class Server
    {
        private bool _running;

        /// <summary>
        /// Game server's database.
        /// </summary>
        public ASD ASDDatabase { get; protected set; }

        /// <summary>
        /// Redis connection.
        /// </summary>
        public Redis Redis { get; protected set; }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public virtual void Run()
        {
            if (_running)
                throw new Exception("Server is already running.");
            _running = true;
        }

        /// <summary>
        /// Initializes ASD database connection with data from Conf.
        /// </summary>
        protected void InitDatabase(Base db, Conf conf)
        {
            try
            {
                db.Init(conf.ASDDbHost, conf.ASDDbUserName, conf.ASDDbPassword, conf.ASDDbName);
                Log.Info("Initialized " + db.GetDbName() + " database connection");
            }
            catch (Exception ex)
            {
                Log.Error("Failed to initialize " + db.GetDbName() + " database: {0}", ex.Message);
                CliUtil.Exit(1, true);
            }
        }

        /// <summary>
        /// Loads given conf class and stops start up when an error
        /// occurs.
        /// </summary>
        /// <param name="conf"></param>
        protected void LoadConf(Conf conf)
        {
            try
            {
                Log.Info("Loading configuration...");
                conf.LoadAll();
            }
            catch (Exception ex)
            {
                Log.Error("Failed to load configuration: {0}", ex.Message);
                CliUtil.Exit(1, true);
            }
        }
    }

    [Flags]
    public enum DataToLoad
    {
        Items = 0x01,
        Maps = 0x02,
        Monsters = 0x03,
        Skills = 0x04,
        CustomCommands = 0x800,

        All = 0x7FFFFFFF,
    }
}
