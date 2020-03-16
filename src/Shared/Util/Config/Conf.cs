#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.IO;
using System.Xml;

namespace Agonyl.Shared.Util.Config
{
    public class Conf
    {
        /// <summary>
        /// IP of the server.
        /// </summary>
        public string Host { get; protected set; }

        /// <summary>
        /// Port of the server.
        /// </summary>
        public int Port { get; protected set; }

        /// <summary>
        /// ID of the server.
        /// </summary>
        public int ServerId { get; protected set; }

        /// <summary>
        /// Name of the server.
        /// </summary>
        public string ServerName { get; protected set; }

        /// <summary>
        /// ASD Database host.
        /// </summary>
        public string ASDDbHost { get; protected set; }

        /// <summary>
        /// ASD Database port.
        /// </summary>
        public int ASDDbPort { get; protected set; }

        /// <summary>
        /// Redis host.
        /// </summary>
        public string RedisHost { get; protected set; }

        /// <summary>
        /// Redis port.
        /// </summary>
        public int RedisPort { get; protected set; }

        /// <summary>
        /// Redis password.
        /// </summary>
        public string RedisPassword { get; protected set; }

        /// <summary>
        /// ASD Database name.
        /// </summary>
        public string ASDDbName { get; protected set; }

        /// <summary>
        /// ASD Database username.
        /// </summary>
        public string ASDDbUserName { get; protected set; }

        /// <summary>
        /// ASD Database password.
        /// </summary>
        public string ASDDbPassword { get; protected set; }

        /// <summary>
        /// Config file path.
        /// </summary>
        public string ConfFile { get; protected set; }

        /// <summary>
        /// Loaded config file data.
        /// </summary>
        public XmlDocument XmlDocument { get; protected set; }

        /// <summary>
        /// Initializes default config.
        /// </summary>
        public Conf()
        {
            this.ConfFile = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "Server.xml";
        }

        /// <summary>
        /// Loads all required configuration.
        /// </summary>
        public virtual void LoadAll()
        {
            // Set default values
            this.Host = "0.0.0.0";
            this.Port = 3550;
            this.ServerId = 0;
            this.ServerName = "Agonyl";
            this.ASDDbHost = "127.0.0.1";
            this.ASDDbPort = 3306;
            this.ASDDbName = "ASD";
            this.ASDDbUserName = "agonyl";
            this.ASDDbPassword = "agonyl";
            this.RedisHost = "127.0.0.1";
            this.RedisPort = 6379;
            this.RedisPassword = string.Empty;
            try
            {
                this.XmlDocument = new XmlDocument();
                this.XmlDocument.Load(this.ConfFile);
                var serverNode = this.XmlDocument.GetElementsByTagName("Server")[0];
                if (serverNode != null)
                {
                    this.ServerId = serverNode.Attributes["id"].Value != null ? Convert.ToInt32(serverNode.Attributes["id"].Value) : this.ServerId;
                    foreach (XmlNode child in serverNode.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Name":
                                if (child.InnerText != null)
                                {
                                    this.ServerName = child.InnerText;
                                }

                                break;

                            case "Host":
                                if (child.InnerText != null)
                                {
                                    this.Host = child.InnerText;
                                }

                                break;

                            case "Port":
                                if (child.InnerText != null)
                                {
                                    this.Port = Convert.ToInt32(child.InnerText);
                                }

                                break;
                        }
                    }
                }

                var redisNode = this.XmlDocument.GetElementsByTagName("Redis")[0];
                if (redisNode != null)
                {
                    foreach (XmlNode child in redisNode.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Host":
                                if (child.InnerText != null)
                                {
                                    this.RedisHost = child.InnerText;
                                }

                                break;

                            case "Port":
                                if (child.InnerText != null)
                                {
                                    this.RedisPort = Convert.ToInt32(child.InnerText);
                                }

                                break;

                            case "Password":
                                if (child.InnerText != null)
                                {
                                    this.RedisPassword = child.InnerText;
                                }

                                break;
                        }
                    }
                }

                var asdNode = this.XmlDocument.GetElementsByTagName("ASD")[0];
                if (asdNode != null)
                {
                    foreach (XmlNode child in asdNode.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "Name":
                                if (child.InnerText != null)
                                {
                                    this.ASDDbName = child.InnerText;
                                }

                                break;

                            case "Host":
                                if (child.InnerText != null)
                                {
                                    this.ASDDbHost = child.InnerText;
                                }

                                break;

                            case "Port":
                                if (child.InnerText != null)
                                {
                                    this.ASDDbPort = Convert.ToInt32(child.InnerText);
                                }

                                break;

                            case "Username":
                                if (child.InnerText != null)
                                {
                                    this.ASDDbUserName = child.InnerText;
                                }

                                break;

                            case "Password":
                                if (child.InnerText != null)
                                {
                                    this.ASDDbPassword = child.InnerText;
                                }

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
