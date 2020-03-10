#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.Collections.Generic;
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
        public string ItemDirectory { get; protected set; }
        public List<string> StarterGearWarrior = new List<string>();
        public List<string> StarterGearHK = new List<string>();
        public List<string> StarterGearMage = new List<string>();
        public List<string> StarterGearArcher = new List<string>();
        public string StarterStatsWarrior { get; protected set; }
        public string StarterStatsHK { get; protected set; }
        public string StarterStatsMage { get; protected set; }
        public string StarterStatsArcher { get; protected set; }
        public string StarterBodyWarrior { get; protected set; }
        public string StarterBodyHK { get; protected set; }
        public string StarterBodyMage { get; protected set; }
        public string StarterBodyArcher { get; protected set; }
        public ushort StarterLevel { get; protected set; }
        public string StarterLocationTemoz { get; protected set; }
        public string StarterLocationQuanato { get; protected set; }

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
            this.ItemDirectory = "item";
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

                            case "ItemDirectory":
                                if (child.InnerText != null)
                                {
                                    this.ItemDirectory = child.InnerText;
                                }
                                break;
                        }
                    }
                }
                var StarterGear = XmlDocument.GetElementsByTagName("StarterGear")[0];
                if (StarterGear != null)
                {
                    foreach (System.Xml.XmlNode child in StarterGear.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Warrior":
                                foreach (System.Xml.XmlNode subChild in child.ChildNodes)
                                {
                                    this.StarterGearWarrior.Add(subChild.Attributes["id"].Value + ";" + subChild.Attributes["option"].Value);
                                }
                                break;

                            case "Holyknight":
                                foreach (System.Xml.XmlNode subChild in child)
                                {
                                    this.StarterGearHK.Add(subChild.Attributes["id"].Value + ";" + subChild.Attributes["option"].Value);
                                }
                                break;

                            case "Mage":
                                foreach (System.Xml.XmlNode subChild in child)
                                {
                                    this.StarterGearMage.Add(subChild.Attributes["id"].Value + ";" + subChild.Attributes["option"].Value);
                                }
                                break;

                            case "Archer":
                                foreach (System.Xml.XmlNode subChild in child)
                                {
                                    this.StarterGearArcher.Add(subChild.Attributes["id"].Value + ";" + subChild.Attributes["option"].Value);
                                }
                                break;
                        }
                    }
                }
                var StarterStats = XmlDocument.GetElementsByTagName("StarterStats")[0];
                if (StarterStats != null)
                {
                    foreach (System.Xml.XmlNode child in StarterStats.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Warrior":
                                this.StarterStatsWarrior = child.InnerText;
                                break;

                            case "Holyknight":
                                this.StarterStatsHK = child.InnerText;
                                break;

                            case "Mage":
                                this.StarterStatsMage = child.InnerText;
                                break;

                            case "Archer":
                                this.StarterStatsArcher = child.InnerText;
                                break;
                        }
                    }
                }
                var StarterBody = XmlDocument.GetElementsByTagName("StarterBody")[0];
                if (StarterBody != null)
                {
                    foreach (System.Xml.XmlNode child in StarterBody.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Warrior":
                                this.StarterBodyWarrior = child.InnerText;
                                break;

                            case "Holyknight":
                                this.StarterBodyHK = child.InnerText;
                                break;

                            case "Mage":
                                this.StarterBodyMage = child.InnerText;
                                break;

                            case "Archer":
                                this.StarterBodyArcher = child.InnerText;
                                break;
                        }
                    }
                }
                var StarterLevel = XmlDocument.GetElementsByTagName("StarterLevel")[0];
                if (StarterLevel != null)
                {
                    this.StarterLevel = Convert.ToUInt16(StarterLevel.InnerText);
                }
                else
                {
                    this.StarterLevel = 1;
                }
                var StarterLocation = XmlDocument.GetElementsByTagName("StarterLocation")[0];
                if (StarterLocation != null)
                {
                    foreach (System.Xml.XmlNode child in StarterLocation.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Temoz":
                                this.StarterLocationTemoz = child.InnerText;
                                break;

                            case "Quanato":
                                this.StarterLocationQuanato = child.InnerText;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.Util.Log.Exception(ex);
            }
        }

        public string GetIT0Path()
        {
            return this.BaseGameDataPath + Path.DirectorySeparatorChar + this.ItemDirectory + Path.DirectorySeparatorChar + "0";
        }

        public string GetIT1Path()
        {
            return this.BaseGameDataPath + Path.DirectorySeparatorChar + this.ItemDirectory + Path.DirectorySeparatorChar + "1";
        }

        public string GetIT2Path()
        {
            return this.BaseGameDataPath + Path.DirectorySeparatorChar + this.ItemDirectory + Path.DirectorySeparatorChar + "2";
        }

        public string GetIT3Path()
        {
            return this.BaseGameDataPath + Path.DirectorySeparatorChar + this.ItemDirectory + Path.DirectorySeparatorChar + "3";
        }

        public string GetIT0exPath()
        {
            return this.BaseGameDataPath + Path.DirectorySeparatorChar + this.ItemDirectory + Path.DirectorySeparatorChar + "0ex";
        }
    }
}
