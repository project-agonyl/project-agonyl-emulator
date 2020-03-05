#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.IO;
using Agonyl.Shared.Util.Config;

namespace Agonyl.Game.Util.Config
{
    public class GameConf : Conf
    {
        /// <summary>
        /// GameServer data path to read data files from
        /// </summary>
        public string BaseGameDataPath { get; protected set; }

        public string TeleportTextFileName { get; protected set; }

        /// <summary>
        /// Initializes default config.
        /// </summary>
        public GameConf()
            : base()
        {
            this.ConfFile = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "GameServer.xml";
        }

        public override void LoadAll()
        {
            base.LoadAll();
            this.BaseGameDataPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "GameData";
            try
            {
                var DataConfig = XmlDocument.GetElementsByTagName("DataConfig")[0];
                if (DataConfig != null)
                {
                    foreach (System.Xml.XmlNode child in DataConfig.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "BaseDirectory":
                                if (child.InnerText != null)
                                {
                                    this.BaseGameDataPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + child.InnerText;
                                }
                                break;

                            case "TeleportTextFileName":
                                if (child.InnerText != null)
                                {
                                    this.TeleportTextFileName = child.InnerText;
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.Util.Log.Error(ex.Message);
            }
        }
    }
}
