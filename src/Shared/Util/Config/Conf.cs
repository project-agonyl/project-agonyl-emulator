#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

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
		/// Database host
		/// </summary>
		public string DbHost { get; protected set; }

		/// <summary>
		/// Database port
		/// </summary>
		public int DbPort { get; protected set; }

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
		/// Database name
		/// </summary>
		public string DbName { get; protected set; }

		/// <summary>
		/// Database username
		/// </summary>
		public string DbUserName { get; protected set; }

		/// <summary>
		/// Database password
		/// </summary>
		public string DbPassword { get; protected set; }

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
			this.DbHost = GetIniValue("DATABASE", "HOST", "127.0.0.1");
			this.DbPort = Convert.ToInt32(GetIniValue("DATABASE", "PORT", "3306"));
			this.DbName = GetIniValue("DATABASE", "NAME", "agonyl");
			this.DbUserName = GetIniValue("DATABASE", "USERNAME", "agonyl");
			this.DbPassword = GetIniValue("DATABASE", "PASSWORD", "agonyl");
			this.RedisHost = GetIniValue("REDIS", "HOST", "127.0.0.1");
			this.RedisPort = Convert.ToInt32(GetIniValue("REDIS", "PORT", "6379"));
			this.RedisPassword = GetIniValue("REDIS", "PASSWORD", "");
		}
	}
}
