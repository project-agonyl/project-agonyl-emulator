﻿#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.IO;
using Agonyl.Game.Network;
using Agonyl.Game.Util;
using Agonyl.Game.Util.Config;
using Agonyl.Shared;
using Agonyl.Shared.Data.Game;
using Agonyl.Shared.Database;
using Agonyl.Shared.Network;
using Agonyl.Shared.Util;

namespace Agonyl.Game
{
    internal class GameServer : Server
    {
        public static readonly GameServer Instance = new GameServer();

        public Database.ASD ASDDatabase;

        /// <summary>
        /// Configuration.
        /// </summary>
        public GameConf Conf { get; private set; }

        /// <summary>
        /// Game server's data loaded from files.
        /// </summary>
        public GameData GameData { get; protected set; }

        /// <summary>
        /// GameServer console commands.
        /// </summary>
        public GameConsoleCommands ConsoleCommands { get; private set; }

        /// <summary>
        /// LoginServer IPC handler.
        /// </summary>
        public GameConnection LoginServerConnection { get; private set; }

        public WorldManager World { get; private set; }

        public GameServer()
        {
            this.GameData = new GameData();
            this.World = new WorldManager();
        }

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
            this.InitDatabase(this.ASDDatabase = new Database.ASD(), this.Conf);

            // Redis server
            this.Redis = new Redis(this.Conf.RedisHost, this.Conf.RedisPort, this.Conf.RedisPassword);

            // Load game data files
            this.LoadData();

            // Packet handlers
            GamePacketHandler.Instance.RegisterMethods();

            Log.Info("Initializing game world...");
            this.World.Initialize();
            Log.Info("Game world initialized with {0} maps.", this.World.Count);

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

        /// <summary>
        /// Loads data from files.
        /// </summary>
        protected void LoadData()
        {
            try
            {
                this.LoadItemFiles();
                this.LoadMapFiles();
                this.LoadQuestFiles();
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

        private void LoadItemFiles()
        {
            Log.Info("Loading item data please wait...");
            this.LoadIT0();
            this.LoadIT1();
            this.LoadIT2();
            this.LoadIT3();
            Log.Info("Loaded " + this.GameData.Items.Count + " items");
        }

        private void LoadIT0()
        {
            Log.Info("Loading IT0 data...");
            var parser = new IT0Parser(this.Conf.GetIT0Path());
            var iT0exParser = new IT0exParser(this.Conf.GetIT0exPath());
            parser.ParseFile(ref this.GameData.Items, ref iT0exParser);
        }

        private void LoadIT1()
        {
            Log.Info("Loading IT1 data...");
            var parser = new IT1Parser(this.Conf.GetIT1Path());
            parser.ParseFile(ref this.GameData.Items);
        }

        private void LoadIT2()
        {
            Log.Info("Loading IT2 data...");
            var parser = new IT2Parser(this.Conf.GetIT2Path());
            parser.ParseFile(ref this.GameData.Items);
        }

        private void LoadIT3()
        {
            Log.Info("Loading IT3 data...");
            var parser = new IT3Parser(this.Conf.GetIT3Path());
            parser.ParseFile(ref this.GameData.Items);
        }

        private void LoadMapFiles()
        {
            Log.Info("Loading map data please wait...");
            foreach (var mapId in this.Conf.Maps)
            {
                var map = new Map(mapId);
                var mapParser = new MapParser(this.Conf.GetMapFilePath(mapId));
                mapParser.ParseFile(ref map);
                var ndtParser = new NdtParser(this.Conf.GetNdtFilePath(mapId));
                ndtParser.ParseFile(ref map);
                foreach (var shop in map.Shops)
                {
                    if (this.GameData.NPCData.ContainsKey(shop.Id))
                    {
                        continue;
                    }

                    var npcData = new NPCData();
                    var npcDataParser = new NPCDataParser(this.Conf.GetNpcFilePath(shop.Id));
                    npcDataParser.ParseData(ref npcData);
                    this.GameData.NPCData.Add(shop.Id, npcData);
                }

                foreach (var monster in map.Monsters)
                {
                    if (this.GameData.NPCData.ContainsKey(monster.Id))
                    {
                        continue;
                    }

                    var npcData = new NPCData();
                    var npcDataParser = new NPCDataParser(this.Conf.GetNpcFilePath(monster.Id));
                    npcDataParser.ParseData(ref npcData);
                    this.GameData.NPCData.Add(monster.Id, npcData);
                }

                this.GameData.Maps.Add(mapId, map);
            }

            Log.Info("Loaded " + this.GameData.Maps.Count + " maps");
        }

        private void LoadQuestFiles()
        {
            Log.Info("Loading quest data please wait...");
            if (Directory.Exists(this.Conf.GetQuestFolder()))
            {
                foreach (var file in Directory.GetFiles(this.Conf.GetQuestFolder(), "*.dat"))
                {
                    var questParser = new QuestParser(file);
                    var quest = questParser.ParseFile();
                    if (!this.GameData.Quests.ContainsKey(quest.Id))
                    {
                        this.GameData.Quests.Add(quest.Id, quest);
                    }
                }
            }

            Log.Info("Loaded " + this.GameData.Quests.Count + " quests");
        }
    }
}
