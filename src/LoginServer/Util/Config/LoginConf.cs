#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.IO;
using Agonyl.Shared.Util.Config;

namespace Agonyl.Login.Util.Config
{
    public class LoginConf : Conf
    {
        /// <summary>
        /// GameServer host
        /// </summary>
        public string GameServerHost { get; protected set; }

        /// <summary>
        /// GameServer port
        /// </summary>
        public int GameServerPort { get; protected set; }

        /// <summary>
        /// Initializes default config.
        /// </summary>
        public LoginConf()
            : base()
        {
            this.ConfFile = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "LoginServer.xml";
        }

        public override void LoadAll()
        {
            base.LoadAll();
            this.GameServerHost = "127.0.0.1";
            this.GameServerPort = 9867;
            try
            {
                var gameServerNode = this.XmlDocument.GetElementsByTagName("GameServer")[0];
                if (gameServerNode != null)
                {
                    foreach (System.Xml.XmlNode child in gameServerNode.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Host":
                                if (child.InnerText != null)
                                {
                                    this.GameServerHost = child.InnerText;
                                }

                                break;

                            case "Port":
                                if (child.InnerText != null)
                                {
                                    this.GameServerPort = Convert.ToInt32(child.InnerText);
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
