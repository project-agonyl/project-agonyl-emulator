#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System.IO;
using Agonyl.Shared.Util.Config;

namespace Agonyl.Game.Util.Config
{
    public class GameConf : Conf
    {
        /// <summary>
        /// GameServer host
        /// </summary>
        public string GameDataPath { get; protected set; }

        /// <summary>
        /// Initializes default config.
        /// </summary>
        public GameConf()
            : base()
        {
            this.ConfFile = Directory.GetCurrentDirectory() + "\\GameServer.ini";
        }

        public override void LoadAll()
        {
            base.LoadAll();
            this.GameDataPath = Directory.GetCurrentDirectory() + "\\" + GetIniValue("GAMEDATA", "FOLDER", "GameData");
        }
    }
}
