#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

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
		/// File databases.
		/// </summary>
		public AgonylData Data { get; private set; }

		/// <summary>
		/// Redis connection.
		/// </summary>
		public Redis Redis { get; protected set; }

		/// <summary>
		/// Initializes class.
		/// </summary>
		public Server()
		{
			this.Data = new AgonylData();
		}

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
		/// Initializes database connection with data from Conf.
		/// </summary>
		protected void InitDatabase(AgonylDb db, Conf conf)
		{
			try
			{
				Log.Info("Initializing database...");
				db.Init(conf.DbHost, conf.DbUserName, conf.DbPassword, conf.DbName);
			}
			catch (Exception ex)
			{
				Log.Error("Failed to initialize database: {0}", ex.Message);
				CliUtil.Exit(1, true);
			}
		}

		/// <summary>
		/// Loads data from files.
		/// </summary>
		protected void LoadData(DataToLoad toLoad, bool reload)
		{
			Log.Info("Loading data...");

			try
			{
				// @TODO Load binary zone data files
			}
			catch (DatabaseErrorException ex)
			{
				Log.Error(ex.ToString());
				CliUtil.Exit(1);
			}
			catch (FileNotFoundException ex)
			{
				Log.Error(ex.Message);
				CliUtil.Exit(1);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Error while loading data.");
				CliUtil.Exit(1);
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
