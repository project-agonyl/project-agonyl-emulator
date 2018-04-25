#region copyright
// Copyright (c) 2018 Project Agonyl
#endregion

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
			this.ConfFile = Directory.GetCurrentDirectory() + "\\LoginServer.ini";
		}

		public override void LoadAll()
		{
			base.LoadAll();
			this.GameServerHost = GetIniValue("GAMESERVER", "HOST", "127.0.0.1");
			this.GameServerPort = Convert.ToInt32(GetIniValue("GAMESERVER", "PORT", "9867"));
		}
	}
}
