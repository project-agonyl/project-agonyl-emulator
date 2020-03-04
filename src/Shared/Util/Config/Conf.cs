#region copyright

// Copyright (c) 2018 Project Agonyl

#endregion copyright

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Agonyl.Shared.Util.Config
{
    public class Conf
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Read the key value in the config file
        /// </summary>
        /// <param name="Section">Section String</param>
        /// <param name="Key">Key String</param>
        /// <param name="iniPath">ini File Path String</param>
        /// <param name="defaultValue">default Return Value String</param>
        protected string GetIniValue(string Section, string Key, string defaultValue = "")
        {
            var temp = new StringBuilder(255);
            var i = GetPrivateProfileString(Section, Key, "", temp, 255, this.ConfFile);
            return (temp.ToString().Trim() != "") ? temp.ToString().Trim() : defaultValue;
        }

        /// <summary>
        /// IP of the server
        /// </summary>
        public string Host { get; protected set; }

        /// <summary>
        /// Port of the server
        /// </summary>
        public int Port { get; protected set; }

        /// <summary>
        /// ID of the server
        /// </summary>
        public int ServerId { get; protected set; }

        /// <summary>
        /// Name of the server
        /// </summary>
        public string ServerName { get; protected set; }

        /// <summary>
        /// ASD Database host
        /// </summary>
        public string ASDDbHost { get; protected set; }

        /// <summary>
        /// ASD Database port
        /// </summary>
        public int ASDDbPort { get; protected set; }

        /// <summary>
        /// Redis host
        /// </summary>
        public string RedisHost { get; protected set; }

        /// <summary>
        /// Redis port
        /// </summary>
        public int RedisPort { get; protected set; }

        /// <summary>
        /// Redis password
        /// </summary>
        public string RedisPassword { get; protected set; }

        /// <summary>
        /// ASD Database name
        /// </summary>
        public string ASDDbName { get; protected set; }

        /// <summary>
        /// ASD Database username
        /// </summary>
        public string ASDDbUserName { get; protected set; }

        /// <summary>
        /// ASD Database password
        /// </summary>
        public string ASDDbPassword { get; protected set; }

        /// <summary>
        /// Config file path
        /// </summary>
        public string ConfFile { get; protected set; }

        /// <summary>
        /// Initializes default config.
        /// </summary>
        public Conf()
        {
            this.ConfFile = Directory.GetCurrentDirectory() + "\\SvrInfo.ini";
        }

        /// <summary>
        /// Loads all required configuration
        /// </summary>
        public virtual void LoadAll()
        {
            this.Host = GetIniValue("STARTUP", "HOST", "0.0.0.0");
            this.Port = Convert.ToInt32(GetIniValue("STARTUP", "PORT", "3550"));
            this.ServerId = Convert.ToInt32(GetIniValue("STARTUP", "SERVERID", "0"));
            this.ServerName = GetIniValue("STARTUP", "SERVERNAME", "Agonyl");
            this.ASDDbHost = GetIniValue("ASD_DATABASE", "HOST", "127.0.0.1");
            this.ASDDbPort = Convert.ToInt32(GetIniValue("ASD_DATABASE", "PORT", "3306"));
            this.ASDDbName = GetIniValue("ASD_DATABASE", "NAME", "agonyl");
            this.ASDDbUserName = GetIniValue("ASD_DATABASE", "USERNAME", "agonyl");
            this.ASDDbPassword = GetIniValue("ASD_DATABASE", "PASSWORD", "agonyl");
            this.RedisHost = GetIniValue("REDIS", "HOST", "127.0.0.1");
            this.RedisPort = Convert.ToInt32(GetIniValue("REDIS", "PORT", "6379"));
            this.RedisPassword = GetIniValue("REDIS", "PASSWORD", "");
        }
    }
}
